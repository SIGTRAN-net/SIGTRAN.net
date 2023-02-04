/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using System.Net;

namespace SigtranNet.Protocols.Network.IP.IPv4.Controllers;

/// <summary>
/// A factory that creates IPv4 Controllers.
/// </summary>
internal class IPv4ControllerFactory
{
    private readonly IPv4ControllerOptions options;
    private IPAddress? sourceIPAddress;

    /// <summary>
    /// Initializes a new instance of <see cref="IPv4ControllerFactory" />.
    /// </summary>
    /// <param name="options">The IPv4 Controller options.</param>
    internal IPv4ControllerFactory(IPv4ControllerOptions options)
    {
        this.options = options;
    }

    /// <summary>
    /// Creates an <see cref="IPv4Controller" />.
    /// </summary>
    /// <returns>
    /// An <see cref="IPv4Controller" />.
    /// </returns>
    internal IPv4Controller Create(IPAddress destinationAddress)
    {
        var sourceAddress = this.sourceIPAddress ??= ResolveSourceIPAddress();
        return new(options, sourceAddress, destinationAddress);
    }

    private static IPAddress ResolveSourceIPAddress()
    {
        var hostName = Dns.GetHostName();
        return Dns.GetHostEntry(hostName).AddressList[2]; // TODO use Network Interface option
    }
}
