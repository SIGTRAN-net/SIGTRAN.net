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

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Header;

internal readonly partial struct SctpCommonHeader : IBinarySerializable<SctpCommonHeader>
{
    private const int Length =
        sizeof(ushort) * 2
        + sizeof(uint) * 2;

    /// <inheritdoc />
    public static SctpCommonHeader FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;
        var sourcePortNumber = BinaryPrimitives.ReadUInt16BigEndian(memorySpan);
        var destinationPortNumber = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[2..]);
        var verificationTag = BinaryPrimitives.ReadUInt32BigEndian(memorySpan[4..]);
        var checksum = BinaryPrimitives.ReadUInt32BigEndian(memorySpan[8..]);
        return new(sourcePortNumber, destinationPortNumber, verificationTag, checksum);
    }

    /// <inheritdoc />
    public static SctpCommonHeader Read(BinaryReader reader)
    {
        var memory = new ReadOnlyMemory<byte>(reader.ReadBytes(Length));
        return FromReadOnlyMemory(memory);
    }

    /// <inheritdoc />
    public static SctpCommonHeader Read(Stream stream)
    {
        var memory = new Memory<byte>(new byte[Length]);
        stream.Read(memory.Span);
        return FromReadOnlyMemory(memory);
    }

    /// <inheritdoc />
    public static async Task<SctpCommonHeader> ReadAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        var memory = new Memory<byte>(new byte[Length]);
        await stream.ReadAsync(memory, cancellationToken);
        return FromReadOnlyMemory(memory);
    }

    /// <inheritdoc />
    public ReadOnlyMemory<byte> ToReadOnlyMemory()
    {
        var memory = new Memory<byte>(new byte[Length]);
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
        BinaryPrimitives.WriteUInt16BigEndian(span, this.sourcePortNumber);
        BinaryPrimitives.WriteUInt16BigEndian(span[2..], this.destinationPortNumber);
        BinaryPrimitives.WriteUInt32BigEndian(span[4..], this.verificationTag);
        BinaryPrimitives.WriteUInt32BigEndian(span[8..], this.checksum);
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
