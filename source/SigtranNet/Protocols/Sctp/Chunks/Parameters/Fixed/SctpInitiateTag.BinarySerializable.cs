/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Sctp.Chunks.Parameters.Fixed;

internal readonly partial struct SctpInitiateTag : IBinarySerializable<SctpInitiateTag>
{
    /// <inheritdoc />
    public static SctpInitiateTag FromReadOnlyMemory(ReadOnlyMemory<byte> memory) =>
        new(BinaryPrimitives.ReadUInt32BigEndian(memory.Span));

    /// <inheritdoc />
    public static SctpInitiateTag Read(BinaryReader reader) =>
        FromReadOnlyMemory(new ReadOnlyMemory<byte>(reader.ReadBytes(LengthFixed)));

    /// <inheritdoc />
    public static SctpInitiateTag Read(Stream stream)
    {
        var memory = new Memory<byte>(new byte[LengthFixed]);
        stream.Read(memory.Span);
        return FromReadOnlyMemory(memory);
    }

    /// <inheritdoc />
    public static async Task<SctpInitiateTag> ReadAsync(Stream stream, CancellationToken cancellationToken = default)
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
}
