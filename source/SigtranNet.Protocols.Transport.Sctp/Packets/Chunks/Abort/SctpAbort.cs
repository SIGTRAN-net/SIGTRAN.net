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

using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Abort;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.ErrorCause;

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Abort;

/// <summary>
/// An Abort chunk in an SCTP Packet.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         The ABORT chunk is sent to the peer of an association to close the association. The ABORT chunk MAY contain error causes to inform the receiver about the reason of the abort. DATA chunks MUST NOT be bundled with ABORT chunks. Control chunks (except for INIT, INIT ACK, and SHUTDOWN COMPLETE) MAY be bundled with an ABORT chunk, but they MUST be placed before the ABORT chunk in the SCTP packet; otherwise, they will be ignored by the receiver.
/// 
///         If an endpoint receives an ABORT chunk with a format error or no TCB is found, it MUST silently discard it.Moreover, under any circumstances, an endpoint that receives an ABORT chunk MUST NOT respond to that ABORT chunk by sending an ABORT chunk of its own.
///     </code>
///     <code>
///         See <a href="https://datatracker.ietf.org/doc/html/rfc9260#sec_error_chunk">Section 3.3.10</a> for Error Cause definitions.
///     </code>
///     <code>
///         Note: Special rules apply to this chunk for verification; please see <a href="https://datatracker.ietf.org/doc/html/rfc9260#sec_exceptions_in_verification_tag_rules">Section 8.5.1</a> for details.
///     </code>
/// </remarks>
internal readonly partial struct SctpAbort : ISctpChunk<SctpAbort>
{
    private const SctpChunkType ChunkTypeImplicit = SctpChunkType.Abort;

    /// <summary>
    /// The Chunk Flags.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         Reserved: 7 bits
    ///
    ///         T bit: 1 bit
    ///         The T bit is set to 0 if the sender filled in the Verification Tag expected by the peer.If the Verification Tag is reflected, the T bit MUST be set to 1. Reflecting means that the sent Verification Tag is the same as the received one
    ///     </code>
    /// </remarks>
    internal readonly SctpAbortFlags chunkFlags;

    /// <summary>
    /// The Chunk Length.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         This value represents the size of the chunk in bytes, including the Chunk Type, Chunk Flags, Chunk Length, and Chunk Value fields. Therefore, if the Chunk Value field is zero-length, the Length field will be set to 4. The Chunk Length field does not count any chunk padding. However, it does include any padding of variable-length parameters other than the last parameter in the chunk.
    /// 
    ///             Note: A robust implementation is expected to accept the chunk whether or not the final padding has been included in the Chunk Length.
    ///     </code>
    /// </remarks>
    internal readonly ushort chunkLength;

    /// <summary>
    /// Zero or more Error Causes.
    /// </summary>
    internal readonly ReadOnlyMemory<ISctpErrorCause> errorCauses;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpAbort" />.
    /// </summary>
    /// <param name="chunkFlags">The Chunk Flags.</param>
    /// <param name="errorCauses">Zero or more Error Causes.</param>
    internal SctpAbort(
        SctpAbortFlags chunkFlags,
        ReadOnlyMemory<ISctpErrorCause> errorCauses)
    {
        this.chunkFlags = chunkFlags;

        this.chunkLength = sizeof(uint);
        var errorCausesSpan = errorCauses.Span;
        for (var i = 0; i < errorCauses.Length; i++)
        {
            this.chunkLength += errorCausesSpan[i].ErrorCauseLength;
        }

        this.errorCauses = errorCauses;
    }

    SctpChunkType ISctpChunk.ChunkType => ChunkTypeImplicit;
    byte ISctpChunk.ChunkFlags => (byte)this.chunkFlags;
    ushort ISctpChunk.ChunkLength => this.chunkLength;
    ReadOnlyMemory<ISctpChunkParameter> ISctpChunk.Parameters => new();
}
