/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Network.Icmp.Messages.Echo;
using SigtranNet.Protocols.Network.IP;
using SigtranNet.Protocols.Network.IP.IPv4.Controllers;

namespace SigtranNet.Protocols.Network.Icmp.Controllers;

/// <summary>
/// A controller that handles Internet Control Message Protocol (ICMP) messages.
/// </summary>
internal sealed class IcmpController : IIPProtocolController
{
    private readonly IPv4Controller ipController;

    /// <summary>
    /// Initializes a new instance of <see cref="IcmpController" />.
    /// </summary>
    /// <param name="ipController">The controller for the Internet Protocol (IP) version 4.</param>
    internal IcmpController(IPv4Controller ipController)
    {
        this.ipController = ipController;
    }

    internal async Task PingAsync(CancellationToken cancellationToken = default)
    {
        var memory = new Memory<byte>(new byte[sizeof(ulong) + sizeof(uint)]);
        // TODO identifier, sequence number
        var echoMessage = new IcmpEchoMessage(false, 1, 1, new ReadOnlyMemory<byte>(new byte[] { 1, 2, 3, 4 }));
        echoMessage.Write(memory.Span);
        await this.ipController.SendAsync(IPProtocol.ICMP, memory, cancellationToken);
    }
}
