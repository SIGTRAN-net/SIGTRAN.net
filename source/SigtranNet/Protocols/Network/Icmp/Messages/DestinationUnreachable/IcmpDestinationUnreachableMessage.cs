/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Network.IP;

namespace SigtranNet.Protocols.Network.Icmp.Messages.DestinationUnreachable;

/// <summary>
/// Destination Unreachable.
/// </summary>
/// <remarks>
///     From <a href="https://datatracker.ietf.org/doc/rfc792/">RFC 792</a>:
///     <code>
///         If, according to the information in the gateway's routing tables,
///         the network specified in the internet destination field of a
///         datagram is unreachable, e.g., the distance to the network is
///         infinity, the gateway may send a destination unreachable message
///         to the internet source host of the datagram.In addition, in some
///         networks, the gateway may be able to determine if the internet
///         destination host is unreachable.Gateways in these networks may
///         send destination unreachable messages to the source host when the
///         destination host is unreachable.
///
///         If, in the destination host, the IP module cannot deliver the
///         datagram because the indicated protocol module or process port is
///         not active, the destination host may send a destination
///         unreachable message to the source host.
///
///         Another case is when a datagram must be fragmented to be forwarded
///         by a gateway yet the Don't Fragment flag is on.  In this case the
///         gateway must discard the datagram and may return a destination
///         unreachable message.
///
///         Codes 0, 1, 4, and 5 may be received from a gateway.  Codes 2 and
///         3 may be received from a host.
///     </code>
/// </remarks>
internal readonly partial struct IcmpDestinationUnreachableMessage : IIcmpMessage<IcmpDestinationUnreachableMessage>
{
    /// <summary>
    /// The code that identifies the reason for an unreachable destination.
    /// </summary>
    internal readonly IcmpDestinationUnreachableCode code;

    /// <summary>
    /// The original Internet Protocol header from the datagram that caused the message.
    /// </summary>
    internal readonly IIPHeader ipHeaderOriginal;

    /// <summary>
    /// The first 64 bits of the original datagram that caused the message.
    /// </summary>
    internal readonly ReadOnlyMemory<byte> originalDataDatagramSample;

    /// <summary>
    /// Initializes a new instance of <see cref="IcmpDestinationUnreachableMessage" />.
    /// </summary>
    /// <param name="code">The code that indicates the reason for the unreachable destination.</param>
    /// <param name="ipHeaderOriginal">The Internet Header that was received in the original datagram that caused the message.</param>
    /// <param name="originalDataDatagramSample">The first 64 bits of the original datagram's data.</param>
    internal IcmpDestinationUnreachableMessage(
        IcmpDestinationUnreachableCode code,
        IIPHeader ipHeaderOriginal,
        ReadOnlyMemory<byte> originalDataDatagramSample)
    {
        this.code = code;
        this.ipHeaderOriginal = ipHeaderOriginal;
        this.originalDataDatagramSample = originalDataDatagramSample;
    }
}
