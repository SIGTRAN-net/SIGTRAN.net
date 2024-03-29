﻿/*
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
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.Echo;

internal readonly partial struct IcmpEchoMessage
{
    /// <inheritdoc />
    /// <exception cref="IcmpMessageTypeInvalidException">
    /// An <see cref="IcmpMessageTypeInvalidException" /> is thrown if the message type is not equal to <see cref="IcmpMessageType.Echo" /> or <see cref="IcmpMessageType.EchoReply" />.
    /// </exception>
    /// <exception cref="IcmpMessageChecksumInvalidException">
    /// An <see cref="IcmpMessageChecksumInvalidException" /> is thrown if the checksum is invalid or the message is corrupted.
    /// </exception>
    public static IcmpEchoMessage FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var span = memory.Span;
        var type = (IcmpMessageType)span[0];
        if (type != IcmpMessageType.Echo && type != IcmpMessageType.EchoReply)
            throw new IcmpMessageTypeInvalidException(type);
        // Code = 0
        var checksum = BinaryPrimitives.ReadUInt16BigEndian(span[2..4]);
        var identifier = BinaryPrimitives.ReadUInt16BigEndian(span[4..6]);
        var sequenceNumber = BinaryPrimitives.ReadUInt16BigEndian(span[6..8]);
        var data = memory[8..];

        if (!OnesComplementChecksum16Bit.Validate(span))
            throw new IcmpMessageChecksumInvalidException(checksum);

        return new(
            type == IcmpMessageType.EchoReply,
            identifier,
            sequenceNumber,
            data);
    }

    /// <inheritdoc />
    /// <exception cref="IcmpMessageTypeInvalidException">
    /// An <see cref="IcmpMessageTypeInvalidException" /> is thrown if the message type is not equal to <see cref="IcmpMessageType.Echo" /> or <see cref="IcmpMessageType.EchoReply" />.
    /// </exception>
    /// <exception cref="IcmpMessageChecksumInvalidException">
    /// An <see cref="IcmpMessageChecksumInvalidException" /> is thrown if the checksum is invalid or the message is invalid.
    /// </exception>
    public static IcmpEchoMessage Read(BinaryReader binaryReader) =>
        IIcmpMessage<IcmpEchoMessage>.Read(binaryReader);

    /// <inheritdoc />
    /// <exception cref="IcmpMessageTypeInvalidException">
    /// An <see cref="IcmpMessageTypeInvalidException" /> is thrown if the message type is not equal to <see cref="IcmpMessageType.Echo" /> or <see cref="IcmpMessageType.EchoReply" />.
    /// </exception>
    /// <exception cref="IcmpMessageChecksumInvalidException">
    /// An <see cref="IcmpMessageChecksumInvalidException" /> is thrown if the checksum is invalid or the message is invalid.
    /// </exception>
    public static IcmpEchoMessage Read(Stream stream) =>
        IIcmpMessage<IcmpEchoMessage>.Read(stream);

    /// <inheritdoc />
    /// <exception cref="IcmpMessageTypeInvalidException">
    /// An <see cref="IcmpMessageTypeInvalidException" /> is thrown if the message type is not equal to <see cref="IcmpMessageType.Echo" /> or <see cref="IcmpMessageType.EchoReply" />.
    /// </exception>
    /// <exception cref="IcmpMessageChecksumInvalidException">
    /// An <see cref="IcmpMessageChecksumInvalidException" /> is thrown if the checksum is invalid or the message is invalid.
    /// </exception>
    public static Task<IcmpEchoMessage> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        IIcmpMessage<IcmpEchoMessage>.ReadAsync(stream, cancellationToken);

    /// <inheritdoc />
    public ReadOnlyMemory<byte> ToReadOnlyMemory()
    {
        var memoryLength =
            sizeof(ulong)
            + this.data.Length;
        var memory = new Memory<byte>(new byte[memoryLength]);
        this.Write(memory.Span);
        return memory;
    }

    /// <inheritdoc />
    public void Write(Span<byte> span)
    {
        span[0] = (byte)(this.isReply ? IcmpMessageType.EchoReply : IcmpMessageType.Echo);
        span[1] = this.code;
        // 2..4 Skip Checksum
        BinaryPrimitives.WriteUInt16BigEndian(span[4..6], this.identifier);
        BinaryPrimitives.WriteUInt16BigEndian(span[6..8], this.sequenceNumber);
        this.data.Span.CopyTo(span[8..]);

        // Calculate checksum and insert it.
        var checksum = OnesComplementChecksum16Bit.Generate(span[0..(8 + this.data.Length)]);
        BinaryPrimitives.WriteUInt16BigEndian(span[2..4], checksum);
    }
}
