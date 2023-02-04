/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks;

namespace SigtranNet.Protocols.Transport.Sctp;

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
internal readonly partial struct SctpPacket
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
