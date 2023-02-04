/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;
using System;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Fixed;

internal readonly partial struct SctpAdvertisedReceiverWindowCredit : IBinarySerializable<SctpAdvertisedReceiverWindowCredit>
{
    /// <inheritdoc />
    public static SctpAdvertisedReceiverWindowCredit FromReadOnlyMemory(ReadOnlyMemory<byte> memory) =>
        new(BinaryPrimitives.ReadUInt32BigEndian(memory.Span));

    /// <inheritdoc />
    public static SctpAdvertisedReceiverWindowCredit Read(BinaryReader reader) =>
        new(BinaryPrimitives.ReadUInt32BigEndian(new ReadOnlyMemory<byte>(reader.ReadBytes(LengthFixed)).Span));

    /// <inheritdoc />
    public static SctpAdvertisedReceiverWindowCredit Read(Stream stream)
    {
        var memory = new Memory<byte>(new byte[LengthFixed]);
        stream.Read(memory.Span);
        return FromReadOnlyMemory(memory);
    }

    /// <inheritdoc />
    public static async Task<SctpAdvertisedReceiverWindowCredit> ReadAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        var memory = new Memory<byte>(new byte[LengthFixed]);
        await stream.ReadAsync(memory, cancellationToken);
        return FromReadOnlyMemory(memory);
    }

    /// <inheritdoc />
    public ReadOnlyMemory<byte> ToReadOnlyMemory()
    {
        var memory = new Memory<byte>(new byte[LengthFixed]);
        this.Write(memory.Span);
        return memory;
    }

    /// <inheritdoc />
    public void Write(Span<byte> span)
    {
        BinaryPrimitives.WriteUInt32BigEndian(span, this.value);
    }

    /// <inheritdoc />
    public Task WriteAsync(Memory<byte> memory, CancellationToken cancellationToken = default)
    {
        this.Write(memory.Span);
        return Task.CompletedTask;
    }
}
