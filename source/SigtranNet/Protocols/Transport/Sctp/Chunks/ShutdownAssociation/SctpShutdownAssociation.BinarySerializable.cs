/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks.Exceptions;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Fixed;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.ShutdownAssociation;

internal readonly partial struct SctpShutdownAssociation
{
    /// <inheritdoc />
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the specified Chunk Type is not equal to <see cref="SctpChunkType.Shutdown" />.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if the specified Chunk Length is not equal to the length of two unsigned 32-bit integers.
    /// </exception>
    public static SctpShutdownAssociation FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;

        /* Chunk header */
        var chunkType = (SctpChunkType)memorySpan[0];
        if (chunkType != ChunkTypeImplicit)
            throw new SctpChunkTypeInvalidException(chunkType);
        // Skip Flags; SHUTDOWN does not have Chunk Flags.
        var chunkLength = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[sizeof(ushort)..]);
        if (chunkLength != ChunkLengthImplicit)
            throw new SctpChunkLengthInvalidException(chunkLength);

        /* Cumulative TSN Ack */
        var cumulativeTransmissionSequenceNumberAcknowledgement = SctpTransmissionSequenceNumber.FromReadOnlyMemory(memory[sizeof(uint)..chunkLength]);

        return new(cumulativeTransmissionSequenceNumberAcknowledgement);
    }

    /// <inheritdoc />
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the specified Chunk Type is not equal to <see cref="SctpChunkType.Shutdown" />.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if the specified Chunk Length is not equal to the length of two unsigned 32-bit integers.
    /// </exception>
    public static SctpShutdownAssociation Read(BinaryReader binaryReader) =>
        ISctpChunk<SctpShutdownAssociation>.Read(binaryReader);

    /// <inheritdoc />
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the specified Chunk Type is not equal to <see cref="SctpChunkType.Shutdown" />.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if the specified Chunk Length is not equal to the length of two unsigned 32-bit integers.
    /// </exception>
    public static SctpShutdownAssociation Read(Stream stream) =>
        ISctpChunk<SctpShutdownAssociation>.Read(stream);

    /// <inheritdoc />
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the specified Chunk Type is not equal to <see cref="SctpChunkType.Shutdown" />.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if the specified Chunk Length is not equal to the length of two unsigned 32-bit integers.
    /// </exception>
    public static Task<SctpShutdownAssociation> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        ISctpChunk<SctpShutdownAssociation>.ReadAsync(stream, cancellationToken);

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
        /* Chunk header */
        span[0] = (byte)ChunkTypeImplicit;
        // Skip Flags; SHUTDOWN does not have Chunk Flags.
        BinaryPrimitives.WriteUInt16BigEndian(span[sizeof(ushort)..], ChunkLengthImplicit);

        /* Cumulative TSN Ack */
        this.cumulativeTransmissionSequenceNumberAcknowledgement.Write(span[sizeof(uint)..]);
    }
}
