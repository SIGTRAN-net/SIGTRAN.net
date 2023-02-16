/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;
using SigtranNet.Protocols.Network.Icmp.Messages.Exceptions;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Network.Icmp.Messages.Timestamp;

internal readonly partial struct IcmpTimestampMessage
{
    /// <inheritdoc />
    /// <exception cref="IcmpMessageTypeInvalidException">
    /// An <see cref="IcmpMessageTypeInvalidException" /> is thrown if the message type is not equal to <see cref="IcmpMessageType.Timestamp" /> or <see cref="IcmpMessageType.TimestampReply" />.
    /// </exception>
    /// <exception cref="IcmpMessageChecksumInvalidException">
    /// An <see cref="IcmpMessageChecksumInvalidException" /> is thrown if the checksum is invalid or the message is corrupted.
    /// </exception>
    public static IcmpTimestampMessage FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var span = memory.Span;
        var type = (IcmpMessageType)span[0];
        if (type != IcmpMessageType.Timestamp && type != IcmpMessageType.TimestampReply)
            throw new IcmpMessageTypeInvalidException(type);
        // Code = 0
        var checksum = BinaryPrimitives.ReadUInt16BigEndian(span[2..4]);
        var identifier = BinaryPrimitives.ReadUInt16BigEndian(span[4..6]);
        var sequenceNumber = BinaryPrimitives.ReadUInt16BigEndian(span[6..8]);
        var originateTimestamp = BinaryPrimitives.ReadUInt32BigEndian(span[8..12]);
        var receiveTimestamp = BinaryPrimitives.ReadUInt32BigEndian(span[12..16]);
        var transmitTimestamp = BinaryPrimitives.ReadUInt32BigEndian(span[16..20]);

        if (!OnesComplementChecksum16Bit.Validate(memory[0..20]))
            throw new IcmpMessageChecksumInvalidException(checksum);

        return new(
            type == IcmpMessageType.TimestampReply,
            identifier,
            sequenceNumber,
            originateTimestamp,
            receiveTimestamp,
            transmitTimestamp);
    }

    /// <inheritdoc />
    /// <exception cref="IcmpMessageTypeInvalidException">
    /// An <see cref="IcmpMessageTypeInvalidException" /> is thrown if the message type is not equal to <see cref="IcmpMessageType.Timestamp" /> or <see cref="IcmpMessageType.TimestampReply" />.
    /// </exception>
    /// <exception cref="IcmpMessageChecksumInvalidException">
    /// An <see cref="IcmpMessageChecksumInvalidException" /> is thrown if the checksum is invalid or the message is corrupted.
    /// </exception>
    public static IcmpTimestampMessage Read(BinaryReader binaryReader) =>
        FromReadOnlyMemory(binaryReader.ReadBytes(5 * sizeof(uint)));

    /// <inheritdoc />
    /// <exception cref="IcmpMessageTypeInvalidException">
    /// An <see cref="IcmpMessageTypeInvalidException" /> is thrown if the message type is not equal to <see cref="IcmpMessageType.Timestamp" /> or <see cref="IcmpMessageType.TimestampReply" />.
    /// </exception>
    /// <exception cref="IcmpMessageChecksumInvalidException">
    /// An <see cref="IcmpMessageChecksumInvalidException" /> is thrown if the checksum is invalid or the message is corrupted.
    /// </exception>
    public static IcmpTimestampMessage Read(Stream stream)
    {
        var memory = new Memory<byte>(new byte[5 * sizeof(uint)]);
        stream.Read(memory.Span);
        return FromReadOnlyMemory(memory);
    }

    /// <inheritdoc />
    /// <exception cref="IcmpMessageTypeInvalidException">
    /// An <see cref="IcmpMessageTypeInvalidException" /> is thrown if the message type is not equal to <see cref="IcmpMessageType.Timestamp" /> or <see cref="IcmpMessageType.TimestampReply" />.
    /// </exception>
    /// <exception cref="IcmpMessageChecksumInvalidException">
    /// An <see cref="IcmpMessageChecksumInvalidException" /> is thrown if the checksum is invalid or the message is corrupted.
    /// </exception>
    public static async Task<IcmpTimestampMessage> ReadAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        var memory = new Memory<byte>(new byte[5 * sizeof(uint)]);
        await stream.ReadAsync(memory, cancellationToken);
        return FromReadOnlyMemory(memory);
    }

    /// <inheritdoc />
    public ReadOnlyMemory<byte> ToReadOnlyMemory()
    {
        var memoryLength = 5 * sizeof(uint);
        var memory = new Memory<byte>(new byte[memoryLength]);
        this.Write(memory.Span);
        return memory;
    }

    /// <inheritdoc />
    public void Write(Span<byte> span)
    {
        span[0] = (byte)(this.isReply ? IcmpMessageType.TimestampReply : IcmpMessageType.Timestamp);
        span[1] = 0;
        // 2..4 Skip checksum to calculate later.
        BinaryPrimitives.WriteUInt16BigEndian(span[4..6], this.identifier);
        BinaryPrimitives.WriteUInt16BigEndian(span[6..8], this.sequenceNumber);
        BinaryPrimitives.WriteUInt32BigEndian(span[8..12], this.originateTimestamp);
        BinaryPrimitives.WriteUInt32BigEndian(span[12..16], this.receiveTimestamp);
        BinaryPrimitives.WriteUInt32BigEndian(span[16..20], this.transmitTimestamp);

        // Calculate checksum and insert it.
        var checksum = OnesComplementChecksum16Bit.Generate(span[0..20]);
        BinaryPrimitives.WriteUInt16BigEndian(span[2..4], checksum);
    }
}
