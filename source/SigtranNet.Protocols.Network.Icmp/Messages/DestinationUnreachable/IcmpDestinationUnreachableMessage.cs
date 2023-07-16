/*
 * This file is part of SIGTRAN.net.
 * 
 * SIGTRAN.net is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, 
 * either version 3 of the License, or (at your option) any later version.
 * 
 * SIGTRAN.net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. 
 * See the GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License along with SIGTRAN.net. If not, see <https://www.gnu.org/licenses/>.
 */

using SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header;

namespace SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.DestinationUnreachable;

/// <summary>
/// A Destination Unreachable message in the Internet Control Message Protocol (ICMP).
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
    internal readonly IPv4Header ipHeaderOriginal;

    /// <summary>
    /// The first 64 bits of the original datagram that caused the message.
    /// </summary>
    /// <remarks>
    ///     From <a href="https://datatracker.ietf.org/doc/rfc792/">RFC 792</a>:
    ///     <code>
    ///         This data is used by the host to match the
    ///         message to the appropriate process. If a higher level protocol
    ///         uses port numbers, they are assumed to be in the first 64 data
    ///         bits of the original datagram's data.
    ///     </code>
    /// </remarks>
    internal readonly ReadOnlyMemory<byte> originalDataDatagramSample;

    /// <summary>
    /// Initializes a new instance of <see cref="IcmpDestinationUnreachableMessage" />.
    /// </summary>
    /// <param name="code">The code that indicates the reason for the unreachable destination.</param>
    /// <param name="ipHeaderOriginal">The Internet Header that was received in the original datagram that caused the message.</param>
    /// <param name="originalDataDatagramSample">The first 64 bits of the original datagram's payload.</param>
    internal IcmpDestinationUnreachableMessage(
        IcmpDestinationUnreachableCode code,
        IPv4Header ipHeaderOriginal,
        ReadOnlyMemory<byte> originalDataDatagramSample)
    {
        this.code = code;
        this.ipHeaderOriginal = ipHeaderOriginal;
        this.originalDataDatagramSample = originalDataDatagramSample;
    }
}
