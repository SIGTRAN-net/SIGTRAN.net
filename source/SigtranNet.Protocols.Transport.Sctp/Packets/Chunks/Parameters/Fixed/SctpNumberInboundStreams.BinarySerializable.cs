/*
 * This file is part of SIGTRAN.net.
 * 
 * SIGTRAN.net is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, 
 * either version 3 of the License, or (at your option) any later version.
 * 
 * SIGTRAN.net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. 
 * See the GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License along with SIGTRAN.net. If not, see <https://www.gnu.org/licenses/>.
 */

using SigtranNet.Binary;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Fixed;

internal readonly partial struct SctpNumberInboundStreams : IBinarySerializable<SctpNumberInboundStreams>
{
    /// <inheritdoc />
    public static SctpNumberInboundStreams FromReadOnlyMemory(ReadOnlyMemory<byte> memory) =>
        new(BinaryPrimitives.ReadUInt16BigEndian(memory.Span));

    /// <inheritdoc />
    public static SctpNumberInboundStreams Read(BinaryReader reader) =>
        new(BinaryPrimitives.ReadUInt16BigEndian(new ReadOnlyMemory<byte>(reader.ReadBytes(LengthFixed)).Span));

    /// <inheritdoc />
    public static SctpNumberInboundStreams Read(Stream stream)
    {
        var memory = new Memory<byte>(new byte[LengthFixed]);
        stream.Read(memory.Span);
        return FromReadOnlyMemory(memory);
    }

    /// <inheritdoc />
    public static async Task<SctpNumberInboundStreams> ReadAsync(Stream stream, CancellationToken cancellationToken = default)
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
        BinaryPrimitives.WriteUInt16BigEndian(span, this.value);
    }

    /// <inheritdoc />
    public Task WriteAsync(Memory<byte> memory, CancellationToken cancellationToken = default)
    {
        this.Write(memory.Span);
        return Task.CompletedTask;
    }
}
