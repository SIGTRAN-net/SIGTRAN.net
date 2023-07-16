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
using SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.Exceptions;
using SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.ParameterProblem;

internal readonly partial struct IcmpParameterProblemMessage
{
    /// <inheritdoc />
    /// <exception cref="IcmpMessageTypeInvalidException">
    /// An <see cref="IcmpMessageTypeInvalidException" /> is thrown if the specified ICMP message type is not equal to <see cref="IcmpMessageType.ParameterProblem" />.
    /// </exception>
    /// <exception cref="IcmpMessageChecksumInvalidException">
    /// An <see cref="IcmpMessageChecksumInvalidException" /> is thrown if the checksum is invalid (or the message is corrupted).
    /// </exception>
    public static IcmpParameterProblemMessage FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var span = memory.Span;
        var type = (IcmpMessageType)span[0];
        if (type != IcmpMessageType.ParameterProblem)
            throw new IcmpMessageTypeInvalidException(IcmpMessageType.ParameterProblem);
        // Skip Code, because currently 0 is the only value.
        var checksum = BinaryPrimitives.ReadUInt16BigEndian(span[sizeof(ushort)..sizeof(uint)]);
        var pointer = span[sizeof(uint)];

        var offset = sizeof(ulong);
        var ipHeaderOriginal = IPv4Header.FromReadOnlyMemory(memory[offset..]);
        offset += ipHeaderOriginal.InternetHeaderLength * sizeof(uint);
        var originalDataDatagramSample = memory[offset..(offset + sizeof(ulong))];
        offset += sizeof(ulong);

        if (!OnesComplementChecksum16Bit.Validate(memory[0..offset]))
            throw new IcmpMessageChecksumInvalidException(checksum);

        return new(pointer, ipHeaderOriginal, originalDataDatagramSample);
    }

    /// <inheritdoc />
    /// <exception cref="IcmpMessageTypeInvalidException">
    /// An <see cref="IcmpMessageTypeInvalidException" /> is thrown if the specified ICMP message type is not equal to <see cref="IcmpMessageType.ParameterProblem" />.
    /// </exception>
    /// <exception cref="IcmpMessageChecksumInvalidException">
    /// An <see cref="IcmpMessageChecksumInvalidException" /> is thrown if the checksum is invalid (or the message is invalid).
    /// </exception>
    public static IcmpParameterProblemMessage Read(BinaryReader binaryReader) =>
        IIcmpMessage<IcmpParameterProblemMessage>.Read(binaryReader);

    /// <inheritdoc />
    /// <exception cref="IcmpMessageTypeInvalidException">
    /// An <see cref="IcmpMessageTypeInvalidException" /> is thrown if the specified ICMP message type is not equal to <see cref="IcmpMessageType.ParameterProblem" />.
    /// </exception>
    /// <exception cref="IcmpMessageChecksumInvalidException">
    /// An <see cref="IcmpMessageChecksumInvalidException" /> is thrown if the checksum is invalid (or the message is invalid).
    /// </exception>
    public static IcmpParameterProblemMessage Read(Stream stream) =>
        IIcmpMessage<IcmpParameterProblemMessage>.Read(stream);

    /// <inheritdoc />
    /// <exception cref="IcmpMessageTypeInvalidException">
    /// An <see cref="IcmpMessageTypeInvalidException" /> is thrown if the specified ICMP message type is not equal to <see cref="IcmpMessageType.ParameterProblem" />.
    /// </exception>
    /// <exception cref="IcmpMessageChecksumInvalidException">
    /// An <see cref="IcmpMessageChecksumInvalidException" /> is thrown if the checksum is invalid (or the message is invalid).
    /// </exception>
    public static Task<IcmpParameterProblemMessage> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        IIcmpMessage<IcmpParameterProblemMessage>.ReadAsync(stream, cancellationToken);

    /// <inheritdoc />
    public ReadOnlyMemory<byte> ToReadOnlyMemory()
    {
        var messageLength =
            2 * sizeof(uint)
            + this.ipHeaderOriginal.InternetHeaderLength * sizeof(uint)
            + sizeof(ulong);
        var memory = new Memory<byte>(new byte[messageLength]);
        this.Write(memory.Span);
        return memory;
    }

    /// <inheritdoc />
    public void Write(Span<byte> span)
    {
        span[0] = (byte)IcmpMessageType.ParameterProblem;
        span[1] = 0;
        // 2..4 Skip checksum for now in order to calculate it.
        span[4] = this.pointer;
        // 5..8 unused
        var offset = sizeof(ulong);
        var ipHeaderOriginalLength = this.ipHeaderOriginal.InternetHeaderLength * sizeof(uint);
        this.ipHeaderOriginal.Write(span[offset..(offset + ipHeaderOriginalLength)]);
        offset += ipHeaderOriginalLength;
        this.originalDataDatagramSample.Span.CopyTo(span[offset..(offset + sizeof(ulong))]);

        // Calculate the checksum and insert it.
        var checksum = OnesComplementChecksum16Bit.Generate(span);
        BinaryPrimitives.WriteUInt16BigEndian(span[sizeof(ushort)..sizeof(uint)], checksum);
    }
}
