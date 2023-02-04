/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks.Exceptions;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Fixed;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.SelectiveAcknowledgement;

internal readonly partial struct SctpSelectiveAcknowledgement
{
    /// <inheritdoc />
    public static SctpSelectiveAcknowledgement FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var offset = 0;
        var memorySpan = memory.Span;
        var chunkType = (SctpChunkType)memorySpan[offset];
        if (chunkType != ChunkTypeImplicit)
            throw new SctpChunkTypeInvalidException(chunkType);
        // Skip Chunk Flags; the SACK chunk does not have Chunk Flags.
        offset += sizeof(ushort);
        var chunkLength = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[offset..]);
        if (chunkLength < ChunkLengthMinimum)
            throw new SctpChunkLengthInvalidException(chunkLength);
        offset += sizeof(ushort);

        var cumulativeTransmissionSequenceNumberAcknowledgement = SctpTransmissionSequenceNumber.FromReadOnlyMemory(memory[offset..]);
        offset += sizeof(uint);
        var advertisedReceiverWindowCredit = SctpAdvertisedReceiverWindowCredit.FromReadOnlyMemory(memory[offset..]);
        offset += sizeof(uint);
        var blocks = SctpSelectiveAcknowledgementBlocks.FromReadOnlyMemory(memory[offset..]);

        return new(
            cumulativeTransmissionSequenceNumberAcknowledgement,
            advertisedReceiverWindowCredit,
            blocks);
    }

    /// <inheritdoc />
    public static SctpSelectiveAcknowledgement Read(BinaryReader binaryReader) =>
        ISctpChunk<SctpSelectiveAcknowledgement>.Read(binaryReader);

    /// <inheritdoc />
    public static SctpSelectiveAcknowledgement Read(Stream stream) =>
        ISctpChunk<SctpSelectiveAcknowledgement>.Read(stream);

    /// <inheritdoc />
    public static Task<SctpSelectiveAcknowledgement> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        ISctpChunk<SctpSelectiveAcknowledgement>.ReadAsync(stream, cancellationToken);

    /// <inheritdoc />
    public ReadOnlyMemory<byte> ToReadOnlyMemory()
    {
        var memory = new Memory<byte>(new byte[this.chunkLength + this.chunkLength % sizeof(uint)]);
        this.Write(memory.Span);
        return memory;
    }

    /// <inheritdoc />
    public void Write(Span<byte> span)
    {
        var offset = 0;
        span[offset] = (byte)ChunkTypeImplicit;
        // Skip Chunk Flags; SACK does not have Chunk Flags.
        offset += sizeof(ushort);
        BinaryPrimitives.WriteUInt16BigEndian(span[offset..], this.chunkLength);
        offset += sizeof(ushort);
        this.cumulativeTransmissionSequenceNumberAcknowledgement.Write(span[offset..]);
        offset += sizeof(uint);
        this.advertisedReceiverWindowCredit.Write(span[offset..]);
        offset += sizeof(uint);
        this.blocks.Write(span[offset..]);
    }
}
