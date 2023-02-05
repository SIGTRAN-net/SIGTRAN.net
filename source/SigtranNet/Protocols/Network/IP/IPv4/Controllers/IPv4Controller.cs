/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Network.IP.IPv4.Controllers.Exceptions;
using SigtranNet.Protocols.Network.IP.IPv4.Options;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace SigtranNet.Protocols.Network.IP.IPv4.Controllers;

/// <summary>
/// A controller for managing IP traffic between two endpoints.
/// </summary>
internal sealed class IPv4Controller
{
    private readonly NetworkInterface networkInterface;
    private readonly ushort maximumTransmissionUnit;
    private readonly byte timeToLive;
    private readonly IPAddress sourceIPAddress;
    private readonly IPAddress destinationIPAddress;
    private readonly Socket socket;
    private ushort identificationCurrent;

    /// <summary>
    /// Initializes a new instance of <see cref="IPv4Controller" />.
    /// </summary>
    /// <param name="controllerOptions">The configuration options for the IPv4 Controller.</param>
    /// <param name="sourceIPAddress">The source IP Address.</param>
    /// <param name="destinationIPAddress">The destination IP Address.</param>
    internal IPv4Controller(
        IPv4ControllerOptions controllerOptions,
        IPAddress sourceIPAddress,
        IPAddress destinationIPAddress)
    {
        this.networkInterface = ResolveNetworkInterface(controllerOptions);
        this.maximumTransmissionUnit = ResolveMaximumTransmissionUnitDefault(this.networkInterface);
        this.timeToLive = controllerOptions.TimeToLive;
        this.sourceIPAddress = sourceIPAddress;
        this.destinationIPAddress = destinationIPAddress;

        // Initialize socket.
        this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
        this.socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, true);
        this.socket.Bind(new IPEndPoint(sourceIPAddress, 0));

        // Initialize identification.
        this.identificationCurrent = 0;
    }

    /// <summary>
    /// Gets the current Maximum Transmission Unit (MTU) for the connection.
    /// </summary>
    /// <remarks>
    ///     This value defaults to the MTU indicated by the first network adapter that supports IPv4.
    /// </remarks>
    internal ushort MaximumTransmissionUnit => this.maximumTransmissionUnit;

    /// <summary>
    /// Gets the Time to Live (TTL).
    /// </summary>
    internal byte TimeToLive => this.timeToLive;

    /// <summary>
    /// Gets the Source IP Address.
    /// </summary>
    internal IPAddress SourceIPAddress => this.sourceIPAddress;

    /// <summary>
    /// Gets the Destination IP Address.
    /// </summary>
    internal IPAddress DestinationIPAddress => this.destinationIPAddress;

    private static NetworkInterface ResolveNetworkInterface(IPv4ControllerOptions controllerOptions)
    {
        var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

        /* Use preferred Network Interface */
        if (controllerOptions.NetworkInterfacePreferred is { } networkInterfacePreferred)
        {
            if (!networkInterfaces.Any(ni => ni.Id == networkInterfacePreferred.Id))
                throw new IPv4ControllerCannotResolveNetworkInterfaceException();
            return networkInterfacePreferred;
        }

        /* Use first available Network Interface that supports IPv4 */
        if (networkInterfaces.FirstOrDefault(ni => ni.Supports(NetworkInterfaceComponent.IPv4)) is not { } networkInterface)
            throw new IPv4ControllerCannotResolveNetworkInterfaceException();

        return networkInterface;
    }

    private static ushort ResolveMaximumTransmissionUnitDefault(NetworkInterface networkInterface)
        => (ushort)networkInterface.GetIPProperties().GetIPv4Properties().Mtu;

    internal async Task SendAsync(
        IPProtocol protocol,
        ReadOnlyMemory<byte> payload,
        CancellationToken cancellationToken = default)
    {
        var numberOfDatagrams = payload.Length / this.maximumTransmissionUnit;
        if (numberOfDatagrams <= 1)
        {
            // Send exactly one datagram, no fragmentation.
            var datagram =
                new IPv4Datagram(
                    IPv4TypeOfService.PrecedencePriority | IPv4TypeOfService.LowDelay | IPv4TypeOfService.ReliabilityHigh,
                    this.identificationCurrent,
                    IPv4Flags.DontFragment,
                    fragmentOffset: 0,
                    this.timeToLive,
                    protocol,
                    this.sourceIPAddress,
                    this.destinationIPAddress,
                    new ReadOnlyMemory<IIPv4Option>(), // TODO options
                    payload);
            var datagramMemory = new Memory<byte>(new byte[datagram.header.totalLength]);
            await datagram.WriteAsync(datagramMemory, cancellationToken);
            this.socket.Connect(this.destinationIPAddress, 0);
            await this.socket.SendAsync(datagramMemory, cancellationToken);
            return;
        }

        // TODO generate datagrams, fragmented if number > 1
        var memoryLengthTotal = 0; // TODO total memory length including overhead of headers
        var memory = new Memory<byte>(new byte[memoryLengthTotal]);
        // TODO foreach datagram, IBinarySerializable.Write(...) to range in memory.
        unchecked { identificationCurrent++; } // roundabout
    }
}
