/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Sctp.Chunks.Exceptions;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Sctp.Chunks.ShutdownComplete;

internal readonly partial struct SctpShutdownComplete
{
    /// <inheritdoc />
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the specified Chunk Type is not equal to <see cref="SctpChunkType.ShutdownComplete" />.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if the specified Chunk Length is not equal to an unsigned 32-bit integer.
    /// </exception>
    public static SctpShutdownComplete FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;

        var chunkType = (SctpChunkType)memorySpan[0];
        if (chunkType != ChunkTypeImplicit)
            throw new SctpChunkTypeInvalidException(chunkType);
        var chunkFlags = (SctpShutdownCompleteFlags)memorySpan[1];
        var chunkLength = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[sizeof(ushort)..ChunkLengthImplicit]);
        if (chunkLength != ChunkLengthImplicit)
            throw new SctpChunkLengthInvalidException(chunkLength);

        return new(chunkFlags);
    }

    /// <inheritdoc />
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the specified Chunk Type is not equal to <see cref="SctpChunkType.ShutdownComplete" />.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if the specified Chunk Length is not equal to an unsigned 32-bit integer.
    /// </exception>
    public static SctpShutdownComplete Read(BinaryReader binaryReader) =>
        ISctpChunk<SctpShutdownComplete>.Read(binaryReader);

    /// <inheritdoc />
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the specified Chunk Type is not equal to <see cref="SctpChunkType.ShutdownComplete" />.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if the specified Chunk Length is not equal to an unsigned 32-bit integer.
    /// </exception>
    public static SctpShutdownComplete Read(Stream stream) =>
        ISctpChunk<SctpShutdownComplete>.Read(stream);

    /// <inheritdoc />
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the specified Chunk Type is not equal to <see cref="SctpChunkType.ShutdownComplete" />.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if the specified Chunk Length is not equal to an unsigned 32-bit integer.
    /// </exception>
    public static Task<SctpShutdownComplete> ReadAsync(Stream stream, CancellationToken cancellationToken) =>
        ISctpChunk<SctpShutdownComplete>.ReadAsync(stream, cancellationToken);

    /// <inheritdoc />
    public ReadOnlyMemory<byte> ToReadOnlyMemory()
    {
        var memory = new Memory<byte>(new byte[ChunkLengthImplicit]);
        this.Write(memory.Span);
        return memory;
    }

    /// <inheritdoc />
    public void Write(Span<byte> span)
    {
        span[0] = (byte)ChunkTypeImplicit;
        span[1] = (byte)this.chunkFlags;
        BinaryPrimitives.WriteUInt16BigEndian(span[sizeof(ushort)..ChunkLengthImplicit], ChunkLengthImplicit);
    }
}
