/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Sctp.Chunks.SelectiveAcknowledgement;

internal readonly partial struct SctpGapAcknowledgementBlockRange : IBinarySerializable<SctpGapAcknowledgementBlockRange>
{
    /// <inheritdoc />
    public static SctpGapAcknowledgementBlockRange FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;
        var start = BinaryPrimitives.ReadUInt16BigEndian(memorySpan);
        var end = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[sizeof(ushort)..]);
        return new(start, end);
    }

    /// <inheritdoc />
    public static SctpGapAcknowledgementBlockRange Read(BinaryReader binaryReader) =>
        FromReadOnlyMemory(new ReadOnlyMemory<byte>(binaryReader.ReadBytes(sizeof(uint))));

    /// <inheritdoc />
    public static SctpGapAcknowledgementBlockRange Read(Stream stream)
    {
        var memory = new Memory<byte>(new byte[sizeof(uint)]);
        var memorySpan = memory.Span;
        stream.Read(memorySpan);
        return FromReadOnlyMemory(memory);
    }

    /// <inheritdoc />
    public static async Task<SctpGapAcknowledgementBlockRange> ReadAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        var memory = new Memory<byte>(new byte[sizeof(uint)]);
        await stream.ReadAsync(memory, cancellationToken);
        return FromReadOnlyMemory(memory);
    }

    /// <inheritdoc />
    public ReadOnlyMemory<byte> ToReadOnlyMemory()
    {
        var memory = new Memory<byte>(new byte[sizeof(uint)]);
        this.Write(memory.Span);
        return memory;
    }

    /// <inheritdoc />
    public void Write(BinaryWriter writer) =>
        writer.Write(this.ToReadOnlyMemory().Span);

    /// <inheritdoc />
    public void Write(Stream stream) =>
        stream.Write(this.ToReadOnlyMemory().Span);

    /// <inheritdoc />
    public void Write(Span<byte> span)
    {
        BinaryPrimitives.WriteUInt16BigEndian(span, this.start);
        BinaryPrimitives.WriteUInt16BigEndian(span[sizeof(ushort)..], this.end);
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
