/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks.Exceptions;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause.Exceptions;
using SigtranNet.Protocols.Transport.Sctp.Chunks;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Abort;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Abort;

internal readonly partial struct SctpAbort
{
    /// <inheritdoc />
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the Chunk Type is not the ABORT chunk type.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if the specified Chunk Length is smaller than the length of the Chunk Header.
    /// </exception>
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if an invalid or unsupported Error Cause Code is provided.
    /// </exception>
    public static SctpAbort FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;

        /* Chunk Header */
        var chunkType = (SctpChunkType)memorySpan[0];
        if (chunkType != ChunkTypeImplicit)
            throw new SctpChunkTypeInvalidException(chunkType);
        var chunkFlags = (SctpAbortFlags)memorySpan[1];
        var chunkLength = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[2..]);
        if (chunkLength < sizeof(uint))
            throw new SctpChunkLengthInvalidException(chunkLength);

        /* Chunk Parameters */
        var errorCauses = new List<ISctpErrorCause>();
        var offset = sizeof(uint);
        while (offset < chunkLength)
        {
            var errorCause = ISctpErrorCause.FromReadOnlyMemory(memory[offset..]);
            errorCauses.Add(errorCause);
            offset += errorCause.ErrorCauseLength;
        }

        return new(
            chunkFlags,
            errorCauses.ToArray());
    }

    /// <inheritdoc />
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the Chunk Type is not the ABORT chunk type.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if the specified Chunk Length is smaller than the length of the Chunk Header.
    /// </exception>
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if an invalid or unsupported Error Cause Code is provided.
    /// </exception>
    public static SctpAbort Read(BinaryReader binaryReader) =>
        ISctpChunk<SctpAbort>.Read(binaryReader);

    /// <inheritdoc />
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the Chunk Type is not the ABORT chunk type.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if the specified Chunk Length is smaller than the length of the Chunk Header.
    /// </exception>
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if an invalid or unsupported Error Cause Code is provided.
    /// </exception>
    public static SctpAbort Read(Stream stream) =>
        ISctpChunk<SctpAbort>.Read(stream);

    /// <inheritdoc />
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the Chunk Type is not the ABORT chunk type.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if the specified Chunk Length is smaller than the length of the Chunk Header.
    /// </exception>
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if an invalid or unsupported Error Cause Code is provided.
    /// </exception>
    public static Task<SctpAbort> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        ISctpChunk<SctpAbort>.ReadAsync(stream, cancellationToken);

    /// <inheritdoc />
    public ReadOnlyMemory<byte> ToReadOnlyMemory()
    {
        // Add padding if necessary.
        var memory = new Memory<byte>(new byte[this.chunkLength + this.chunkLength % sizeof(uint)]);
        this.Write(memory.Span);
        return memory;
    }

    /// <inheritdoc />
    public void Write(Span<byte> span)
    {
        /* Chunk Header */
        span[0] = (byte)ChunkTypeImplicit;
        span[1] = (byte)this.chunkFlags;
        BinaryPrimitives.WriteUInt16BigEndian(span[2..], this.chunkLength);

        /* Chunk Parameters */
        var offset = sizeof(uint);
        var errorCausesSpan = this.errorCauses.Span;
        for (var i = 0; i < this.errorCauses.Length; i++)
        {
            var errorCause = errorCausesSpan[i];
            errorCause.Write(span[offset..]);
            offset += errorCause.ErrorCauseLength;
        }
    }
}
