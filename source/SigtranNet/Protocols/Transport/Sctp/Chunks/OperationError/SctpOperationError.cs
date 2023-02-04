/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Exceptions;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.OperationError;

/// <summary>
/// An Operation Error (ERROR) chunk in an SCTP Packet.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         An endpoint sends this chunk to its peer endpoint to notify it of certain error conditions. It contains one or more error causes. An Operation Error is not considered fatal in and of itself, but the corresponding error cause MAY be used with an ABORT chunk to report a fatal condition.
///     </code>
/// </remarks>
internal readonly partial struct SctpOperationError : ISctpChunk<SctpOperationError>
{
    private const SctpChunkType ChunkTypeImplicit = SctpChunkType.OperationError;

    /// <summary>
    /// The Chunk Length.
    /// </summary>
    internal readonly ushort chunkLength;

    /// <summary>
    /// One or more Error Causes.
    /// </summary>
    internal readonly ReadOnlyMemory<ISctpErrorCause> errorCauses;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpOperationError" />.
    /// </summary>
    /// <param name="errorCauses">One or more Error Causes.</param>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if there is not at least one Error Cause.
    /// </exception>
    internal SctpOperationError(ReadOnlyMemory<ISctpErrorCause> errorCauses)
    {
        this.chunkLength = sizeof(uint);
        var errorCausesSpan = errorCauses.Span;
        for (var i = 0; i < errorCausesSpan.Length; i++)
        {
            this.chunkLength += errorCausesSpan[i].ErrorCauseLength;
        }
        if (chunkLength < sizeof(uint) + 1)
            throw new SctpChunkLengthInvalidException(chunkLength);
        chunkLength += (ushort)(chunkLength % sizeof(uint)); // Chunk padding

        this.errorCauses = errorCauses;
    }

    /// <inheritdoc />
    SctpChunkType ISctpChunk.ChunkType => ChunkTypeImplicit;

    /// <inheritdoc />
    /// <remarks>
    ///     ERROR chunks do not have Chunk Flags.
    /// </remarks>
    byte ISctpChunk.ChunkFlags => 0;

    /// <inheritdoc />
    ushort ISctpChunk.ChunkLength => this.chunkLength;

    /// <inheritdoc />
    ReadOnlyMemory<ISctpChunkParameter> ISctpChunk.Parameters => new();
}
