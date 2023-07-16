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

using SigtranNet.Protocols.Transport.Packets;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks;
using SigtranNet.Protocols.Transport.Sctp.Packets.Header;

namespace SigtranNet.Protocols.Transport.Sctp.Packets;

/// <summary>
/// An SCTP packet.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         An SCTP packet is composed of a common header and chunks. A chunk contains either control information or user data.
///     </code>
///     <code>
///         INIT, INIT ACK, and SHUTDOWN COMPLETE chunks MUST NOT be bundled with any other chunk into an SCTP packet. All other chunks MAY be bundled to form an SCTP packet that does not exceed the PMTU.
///     </code>
///     <code>
///         If a user data message does not fit into one SCTP packet, it can be fragmented into multiple chunks.
///     </code>
///     <code>
///         All integer fields in an SCTP packet MUST be transmitted in network byte order, unless otherwise stated.
///     </code>
/// </remarks>
internal readonly partial struct SctpPacket : ITransportPacket<SctpPacket>
{
    /// <summary>
    /// The SCTP Common Header for this packet.
    /// </summary>
    internal readonly SctpCommonHeader commonHeader;

    /// <summary>
    /// The SCTP chunks in this packet.
    /// </summary>
    internal readonly ReadOnlyMemory<ISctpChunk> chunks;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpPacket" />.
    /// </summary>
    /// <param name="commonHeader">The Common Header of an SCTP Packet.</param>
    /// <param name="chunks">The Chunks in the SCTP Packet.</param>
    internal SctpPacket(
        SctpCommonHeader commonHeader,
        ReadOnlyMemory<ISctpChunk> chunks)
    {
        // Note: trust that the checksum in the commonHeader is valid; validation occurs during (de)serialization.
        this.commonHeader = commonHeader;
        this.chunks = chunks;
    }
}
