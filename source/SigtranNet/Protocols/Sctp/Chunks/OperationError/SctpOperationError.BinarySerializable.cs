/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Sctp.Chunks.Exceptions;
using SigtranNet.Protocols.Sctp.Chunks.Parameters.ErrorCause;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Sctp.Chunks.OperationError;

internal readonly partial struct SctpOperationError
{
    /// <inheritdoc />
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the specified Chunk Type is not equal to <see cref="SctpChunkType.OperationError" />.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if there is not at least one Error Cause in the ERROR chunk.
    /// </exception>
    public static SctpOperationError FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;

        /* Chunk header */
        var chunkType = (SctpChunkType)memorySpan[0];
        if (chunkType != ChunkTypeImplicit)
            throw new SctpChunkTypeInvalidException(chunkType);
        // Skip Flags; ERROR does not have Chunk Flags.
        var chunkLength = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[sizeof(ushort)..]);

        /* Error Causes */
        var errorCauses = new List<ISctpErrorCause>();
        var offset = sizeof(uint);
        while (offset < chunkLength)
        {
            var errorCause = ISctpErrorCause.FromReadOnlyMemory(memory[offset..]);
            offset += errorCause.ErrorCauseLength;
            errorCauses.Add(errorCause);
        }

        return new SctpOperationError(errorCauses.ToArray());
    }

    /// <inheritdoc />
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the specified Chunk Type is not equal to <see cref="SctpChunkType.OperationError" />.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if there is not at least one Error Cause in the ERROR chunk.
    /// </exception>
    public static SctpOperationError Read(BinaryReader binaryReader) =>
        ISctpChunk<SctpOperationError>.Read(binaryReader);

    /// <inheritdoc />
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the specified Chunk Type is not equal to <see cref="SctpChunkType.OperationError" />.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if there is not at least one Error Cause in the ERROR chunk.
    /// </exception>
    public static SctpOperationError Read(Stream stream) =>
        ISctpChunk<SctpOperationError>.Read(stream);

    /// <inheritdoc />
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the specified Chunk Type is not equal to <see cref="SctpChunkType.OperationError" />.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if there is not at least one Error Cause in the ERROR chunk.
    /// </exception>
    public static Task<SctpOperationError> ReadAsync(Stream stream, CancellationToken cancellationToken) =>
        ISctpChunk<SctpOperationError>.ReadAsync(stream, cancellationToken);

    /// <inheritdoc />
    public ReadOnlyMemory<byte> ToReadOnlyMemory()
    {
        var memory = new Memory<byte>(new byte[this.chunkLength]);
        this.Write(memory.Span);
        return memory;
    }

    /// <inheritdoc />
    public void Write(Span<byte> span)
    {
        /* Chunk header */
        span[0] = (byte)ChunkTypeImplicit;
        // Skip Flags; ERROR does not have Chunk Flags.
        BinaryPrimitives.WriteUInt16BigEndian(span[sizeof(ushort)..], this.chunkLength);

        /* Error Causes */
        var offset = sizeof(uint);
        var errorCausesSpan = this.errorCauses.Span;
        for (var i = 0; i < errorCauses.Length; i++)
        {
            errorCausesSpan[i].Write(span[offset..]);
            offset += errorCausesSpan[i].ErrorCauseLength;
        }
    }
}
