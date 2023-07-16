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
using SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header;
using SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.Exceptions;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.TimeExceeded;

internal readonly partial struct IcmpTimeExceededMessage
{
    /// <inheritdoc />
    /// <exception cref="IcmpMessageTypeInvalidException">
    /// An <see cref="IcmpMessageTypeInvalidException" /> is thrown if the specified ICMP message type is not equal to <see cref="IcmpMessageType.TimeExceeded" />.
    /// </exception>
    /// <exception cref="IcmpMessageChecksumInvalidException">
    /// An <see cref="IcmpMessageChecksumInvalidException" /> is thrown if the checksum is invalid (or the message is corrupted).
    /// </exception>
    public static IcmpTimeExceededMessage FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var span = memory.Span;
        var type = (IcmpMessageType)span[0];
        if (type != IcmpMessageType.TimeExceeded)
            throw new IcmpMessageTypeInvalidException(type);
        var code = (IcmpTimeExceededCode)span[1];
        var checksum = BinaryPrimitives.ReadUInt16BigEndian(span[sizeof(ushort)..sizeof(uint)]);
        // 4..8 unused

        var offset = sizeof(ulong);
        var ipHeaderOriginal = IPv4Header.FromReadOnlyMemory(memory[offset..]);
        offset += ipHeaderOriginal.InternetHeaderLength * sizeof(uint);
        var originalDataDatagram = memory[offset..(offset + sizeof(ulong))];
        offset += sizeof(ulong);

        if (!OnesComplementChecksum16Bit.Validate(memory[0..offset]))
            throw new IcmpMessageChecksumInvalidException(checksum);

        return new(code, ipHeaderOriginal, originalDataDatagram);
    }

    /// <inheritdoc />
    /// <exception cref="IcmpMessageTypeInvalidException">
    /// An <see cref="IcmpMessageTypeInvalidException" /> is thrown if the specified ICMP message type is not equal to <see cref="IcmpMessageType.TimeExceeded" />.
    /// </exception>
    /// <exception cref="IcmpMessageChecksumInvalidException">
    /// An <see cref="IcmpMessageChecksumInvalidException" /> is thrown if the checksum is invalid (or the message is invalid).
    /// </exception>
    public static IcmpTimeExceededMessage Read(BinaryReader binaryReader) =>
        IIcmpMessage<IcmpTimeExceededMessage>.Read(binaryReader);

    /// <inheritdoc />
    /// <exception cref="IcmpMessageTypeInvalidException">
    /// An <see cref="IcmpMessageTypeInvalidException" /> is thrown if the specified ICMP message type is not equal to <see cref="IcmpMessageType.TimeExceeded" />.
    /// </exception>
    /// <exception cref="IcmpMessageChecksumInvalidException">
    /// An <see cref="IcmpMessageChecksumInvalidException" /> is thrown if the checksum is invalid (or the message is invalid).
    /// </exception>
    public static IcmpTimeExceededMessage Read(Stream stream) =>
        IIcmpMessage<IcmpTimeExceededMessage>.Read(stream);

    /// <inheritdoc />
    /// <exception cref="IcmpMessageTypeInvalidException">
    /// An <see cref="IcmpMessageTypeInvalidException" /> is thrown if the specified ICMP message type is not equal to <see cref="IcmpMessageType.TimeExceeded" />.
    /// </exception>
    /// <exception cref="IcmpMessageChecksumInvalidException">
    /// An <see cref="IcmpMessageChecksumInvalidException" /> is thrown if the checksum is invalid (or the message is invalid).
    /// </exception>
    public static Task<IcmpTimeExceededMessage> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        IIcmpMessage<IcmpTimeExceededMessage>.ReadAsync(stream, cancellationToken);

    /// <inheritdoc />
    public ReadOnlyMemory<byte> ToReadOnlyMemory()
    {
        var memoryLength =
            sizeof(ulong)
            + this.ipHeaderOriginal.InternetHeaderLength * sizeof(uint)
            + sizeof(ulong);
        var memory = new Memory<byte>(new byte[memoryLength]);
        this.Write(memory.Span);
        return memory;
    }

    /// <inheritdoc />
    public void Write(Span<byte> span)
    {
        span[0] = (byte)IcmpMessageType.TimeExceeded;
        span[1] = (byte)this.code;
        // 2..4 Skip checksum in order to calculate it.
        // 4..8 unused (2nd unsigned 32-bit word is unused).
        var offset = sizeof(ulong);
        this.ipHeaderOriginal.Write(span[offset..]);
        offset += this.ipHeaderOriginal.InternetHeaderLength * sizeof(uint);
        this.originalDataDatagramSample.Span.CopyTo(span[offset..]);

        // Calculate and insert the checksum.
        var checksum = OnesComplementChecksum16Bit.Generate(span);
        BinaryPrimitives.WriteUInt16BigEndian(span[sizeof(ushort)..sizeof(uint)], checksum);
    }
}
