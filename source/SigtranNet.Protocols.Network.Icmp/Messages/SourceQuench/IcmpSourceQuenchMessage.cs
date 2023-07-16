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

namespace SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.SourceQuench;

/// <summary>
/// A Source Quench Message in the Internet Control Message Protocol (ICMP).
/// </summary>
/// <remarks>
///     From <a href="https://datatracker.ietf.org/doc/rfc792/">RFC 792</a>:
///     <code>
///         A gateway may discard internet datagrams if it does not have the
///         buffer space needed to queue the datagrams for output to the next
///         network on the route to the destination network. If a gateway
///         discards a datagram, it may send a source quench message to the
///         internet source host of the datagram.A destination host may also
///         send a source quench message if datagrams arrive too fast to be
///         processed. The source quench message is a request to the host to
///         cut back the rate at which it is sending traffic to the internet
///         destination.  The gateway may send a source quench message for
///         every message that it discards.On receipt of a source quench
///         message, the source host should cut back the rate at which it is
///         sending traffic to the specified destination until it no longer
///         receives source quench messages from the gateway.The source host
///         can then gradually increase the rate at which it sends traffic to
///         the destination until it again receives source quench messages.
/// 
///         The gateway or host may send the source quench message when it
///         approaches its capacity limit rather than waiting until the
///         capacity is exceeded.This means that the data datagram which
///         triggered the source quench message may be delivered.
/// 
///         Code 0 may be received from a gateway or a host.
///     </code>
/// </remarks>
internal readonly partial struct IcmpSourceQuenchMessage : IIcmpMessage<IcmpSourceQuenchMessage>
{
    /// <summary>
    /// The Internet Header of the original datagram.
    /// </summary>
    internal readonly IPv4Header ipHeaderOriginal;

    /// <summary>
    /// The first 64 bits of the original datagram's payload.
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
    /// Initializes a new instance of <see cref="IcmpSourceQuenchMessage" />.
    /// </summary>
    /// <param name="ipHeaderOriginal">The Internet Header of the original datagram.</param>
    /// <param name="originalDataDatagramSample">The first 64 bits of the original datagram's payload.</param>
    internal IcmpSourceQuenchMessage(IPv4Header ipHeaderOriginal, ReadOnlyMemory<byte> originalDataDatagramSample)
    {
        this.ipHeaderOriginal = ipHeaderOriginal;
        this.originalDataDatagramSample = originalDataDatagramSample;
    }
}
