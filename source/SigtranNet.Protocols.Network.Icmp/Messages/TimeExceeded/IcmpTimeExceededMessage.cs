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

namespace SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.TimeExceeded;

/// <summary>
/// A Time Exceeded Message in the Internet Control Message Protocol (ICMP).
/// </summary>
/// <remarks>
///     From <a href="https://datatracker.ietf.org/doc/rfc792/">RFC 792</a>:
///     <code>
///         If the gateway processing a datagram finds the time to live field
///         is zero it must discard the datagram.  The gateway may also notify
///         the source host via the time exceeded message.
///
///         If a host reassembling a fragmented datagram cannot complete the
///         reassembly due to missing fragments within its time limit it
///         discards the datagram, and it may send a time exceeded message.
///
///         If fragment zero is not available then no time exceeded need be
///         sent at all.
///
///         Code 0 may be received from a gateway.  Code 1 may be received
///         from a host.
///     </code>
/// </remarks>
internal readonly partial struct IcmpTimeExceededMessage : IIcmpMessage<IcmpTimeExceededMessage>
{
    /// <summary>
    /// The code that identifies the reason for an exceeded time.
    /// </summary>
    internal readonly IcmpTimeExceededCode code;

    /// <summary>
    /// The original Internet Protocol header from the datagram that caused the message.
    /// </summary>
    internal readonly IPv4Header ipHeaderOriginal;

    /// <summary>
    /// The first 64 bits of the original datagram that caused the message.
    /// </summary>
    internal readonly ReadOnlyMemory<byte> originalDataDatagramSample;

    /// <summary>
    /// Initializes a new instance of <see cref="IcmpTimeExceededMessage" />.
    /// </summary>
    /// <param name="code">The code that indicates the reason for the exceeded time.</param>
    /// <param name="ipHeaderOriginal">The Internet Header that was received in the original datagram that caused the message.</param>
    /// <param name="originalDataDatagramSample">The first 64 bits of the original datagram's payload.</param>
    internal IcmpTimeExceededMessage(
        IcmpTimeExceededCode code,
        IPv4Header ipHeaderOriginal,
        ReadOnlyMemory<byte> originalDataDatagramSample)
    {
        this.code = code;
        this.ipHeaderOriginal = ipHeaderOriginal;
        this.originalDataDatagramSample = originalDataDatagramSample;
    }
}
