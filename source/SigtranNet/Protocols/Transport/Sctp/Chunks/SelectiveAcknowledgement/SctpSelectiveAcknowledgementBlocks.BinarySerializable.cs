/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Fixed;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.SelectiveAcknowledgement;

internal readonly partial struct SctpSelectiveAcknowledgementBlocks : IBinarySerializable<SctpSelectiveAcknowledgementBlocks>
{
    /// <inheritdoc />
    public static SctpSelectiveAcknowledgementBlocks FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;
        var numberGapAcknowledgementBlocks = BinaryPrimitives.ReadUInt16BigEndian(memorySpan);
        var numberDuplicateTransmissionSequenceNumbers = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[sizeof(ushort)..]);

        var offset = sizeof(uint);
        var gapAcknowledgementBlocks = new List<SctpGapAcknowledgementBlockRange>();
        for (var i = 0; i < numberGapAcknowledgementBlocks; i++)
        {
            gapAcknowledgementBlocks.Add(SctpGapAcknowledgementBlockRange.FromReadOnlyMemory(memory[offset..]));
            offset += sizeof(uint);
        }

        var duplicateTransmissionSequenceNumbers = new List<SctpTransmissionSequenceNumber>();
        for (var i = 0; i < numberDuplicateTransmissionSequenceNumbers; i++)
        {
            duplicateTransmissionSequenceNumbers.Add(SctpTransmissionSequenceNumber.FromReadOnlyMemory(memory[offset..]));
            offset += sizeof(uint);
        }

        return new(
            gapAcknowledgementBlocks.ToArray(),
            duplicateTransmissionSequenceNumbers.ToArray());
    }

    /// <inheritdoc />
    public static SctpSelectiveAcknowledgementBlocks Read(BinaryReader binaryReader)
    {
        var counts = new ReadOnlyMemory<byte>(binaryReader.ReadBytes(sizeof(uint)));
        var countsSpan = counts.Span;
        var numberGapAcknowledgementBlocks = BinaryPrimitives.ReadUInt16BigEndian(countsSpan);
        var numberDuplicateTransmissionSequenceNumbers = BinaryPrimitives.ReadUInt16BigEndian(countsSpan[sizeof(ushort)..]);

        var dataLength = numberGapAcknowledgementBlocks * sizeof(uint) + numberDuplicateTransmissionSequenceNumbers * sizeof(uint);
        var data = new ReadOnlyMemory<byte>(binaryReader.ReadBytes(dataLength));

        var memory = new Memory<byte>(new byte[sizeof(uint) + dataLength]);
        counts.CopyTo(memory);
        data.CopyTo(memory[sizeof(uint)..]);
        return FromReadOnlyMemory(memory);
    }

    /// <inheritdoc />
    public static SctpSelectiveAcknowledgementBlocks Read(Stream stream)
    {
        var counts = new Memory<byte>(new byte[sizeof(uint)]);
        var countsSpan = counts.Span;
        stream.Read(countsSpan);
        var numberGapAcknowledgementBlocks = BinaryPrimitives.ReadUInt16BigEndian(countsSpan);
        var numberDuplicateTransmissionSequenceNumbers = BinaryPrimitives.ReadUInt16BigEndian(countsSpan[sizeof(ushort)..]);

        var dataLength = numberGapAcknowledgementBlocks * sizeof(uint) + numberDuplicateTransmissionSequenceNumbers * sizeof(uint);
        var memory = new Memory<byte>(new byte[sizeof(uint) + dataLength]);
        counts.CopyTo(memory);
        stream.Read(memory.Span[sizeof(uint)..]);
        return FromReadOnlyMemory(memory);
    }

    /// <inheritdoc />
    public static async Task<SctpSelectiveAcknowledgementBlocks> ReadAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        var counts = new Memory<byte>(new byte[sizeof(uint)]);
        await stream.ReadAsync(counts, cancellationToken);
        var numberGapAcknowledgementBlocks = BinaryPrimitives.ReadUInt16BigEndian(counts.Span);
        var numberDuplicateTransmissionSequenceNumbers = BinaryPrimitives.ReadUInt16BigEndian(counts.Span[sizeof(ushort)..]);

        var dataLength = numberGapAcknowledgementBlocks * sizeof(uint) + numberDuplicateTransmissionSequenceNumbers * sizeof(uint);
        var memory = new Memory<byte>(new byte[sizeof(uint) + dataLength]);
        counts.CopyTo(memory);
        await stream.ReadAsync(memory[sizeof(uint)..], cancellationToken);
        return FromReadOnlyMemory(memory);
    }

    /// <inheritdoc />
    public ReadOnlyMemory<byte> ToReadOnlyMemory()
    {
        var length =
            sizeof(uint)
            + this.gapAcknowledgementBlocks.Length * sizeof(uint)
            + this.duplicateTransmissionSequenceNumbers.Length * sizeof(uint);
        var memory = new Memory<byte>(new byte[length]);
        this.Write(memory.Span);
        return memory;
    }

    /// <inheritdoc />
    public void Write(BinaryWriter writer) =>
        writer.Write(this.ToReadOnlyMemory().Span);

    /// <inheritdoc />
    public void Write(Stream stream) =>
        stream.Write(this.ToReadOnlyMemory().Span);

    public void Write(Span<byte> span)
    {
        var numberGapAcknowledgementBlocks = (ushort)this.gapAcknowledgementBlocks.Length;
        var numberDuplicateTransmissionSequenceNumbers = (ushort)this.duplicateTransmissionSequenceNumbers.Length;
        var gapAcknowledgementBlocksSpan = this.gapAcknowledgementBlocks.Span;
        var duplicateTransmissionSequenceNumbersSpan = this.duplicateTransmissionSequenceNumbers.Span;

        BinaryPrimitives.WriteUInt16BigEndian(span, numberGapAcknowledgementBlocks);
        BinaryPrimitives.WriteUInt16BigEndian(span[sizeof(ushort)..], numberDuplicateTransmissionSequenceNumbers);

        var offset = sizeof(uint);
        for (var i = 0; i < numberGapAcknowledgementBlocks; i++)
        {
            gapAcknowledgementBlocksSpan[i].Write(span[offset..]);
        }
        for (var i = 0; i < numberDuplicateTransmissionSequenceNumbers; i++)
        {
            duplicateTransmissionSequenceNumbersSpan[i].Write(span[offset..]);
        }
    }

    /// <inheritdoc />
    public ValueTask WriteAsync(Stream stream, CancellationToken cancellationToken = default) =>
        stream.WriteAsync(this.ToReadOnlyMemory(), cancellationToken);

    /// <inheritdoc />
    public Task WriteAsync(Memory<byte> memory, CancellationToken cancellationToken = default)
    {
        var me = this;
        return Task.Run(() => me.Write(memory.Span), cancellationToken);
    }
}
