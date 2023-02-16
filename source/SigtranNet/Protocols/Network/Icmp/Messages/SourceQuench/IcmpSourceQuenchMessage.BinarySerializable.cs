/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;
using SigtranNet.Protocols.Network.Icmp.Messages.Exceptions;
using SigtranNet.Protocols.Network.IP;
using SigtranNet.Protocols.Network.IP.IPv4;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Network.Icmp.Messages.SourceQuench;

internal readonly partial struct IcmpSourceQuenchMessage
{
    /// <inheritdoc />
    /// <exception cref="IcmpMessageTypeInvalidException">
    /// An <see cref="IcmpMessageTypeInvalidException" /> is thrown if the specified ICMP message type is not equal to <see cref="IcmpMessageType.SourceQuench" />.
    /// </exception>
    /// <exception cref="IcmpMessageChecksumInvalidException">
    /// An <see cref="IcmpMessageChecksumInvalidException" /> is thrown if the checksum is invalid (or the message is corrupted).
    /// </exception>
    public static IcmpSourceQuenchMessage FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var span = memory.Span;
        var type = (IcmpMessageType)span[0];
        if (type != IcmpMessageType.SourceQuench)
            throw new IcmpMessageTypeInvalidException(type);
        // Code is 0.
        var checksum = BinaryPrimitives.ReadUInt16BigEndian(span[sizeof(ushort)..sizeof(uint)]);
        // 4..8 unused

        var offset = sizeof(ulong);
        var ipHeaderOriginal = IPv4Header.FromReadOnlyMemory(memory[offset..]);
        offset += ipHeaderOriginal.InternetHeaderLength * sizeof(uint);
        var originalDataDatagram = memory[offset..(offset + sizeof(ulong))];
        offset += sizeof(ulong);

        if (!OnesComplementChecksum16Bit.Validate(memory[0..offset]))
            throw new IcmpMessageChecksumInvalidException(checksum);

        return new(ipHeaderOriginal, originalDataDatagram);
    }

    /// <inheritdoc />
    /// <exception cref="IcmpMessageTypeInvalidException">
    /// An <see cref="IcmpMessageTypeInvalidException" /> is thrown if the specified ICMP message type is not equal to <see cref="IcmpMessageType.SourceQuench" />.
    /// </exception>
    /// <exception cref="IcmpMessageChecksumInvalidException">
    /// An <see cref="IcmpMessageChecksumInvalidException" /> is thrown if the checksum is invalid (or the message is invalid).
    /// </exception>
    public static IcmpSourceQuenchMessage Read(BinaryReader binaryReader) =>
        IIcmpMessage<IcmpSourceQuenchMessage>.Read(binaryReader);

    /// <inheritdoc />
    /// <exception cref="IcmpMessageTypeInvalidException">
    /// An <see cref="IcmpMessageTypeInvalidException" /> is thrown if the specified ICMP message type is not equal to <see cref="IcmpMessageType.SourceQuench" />.
    /// </exception>
    /// <exception cref="IcmpMessageChecksumInvalidException">
    /// An <see cref="IcmpMessageChecksumInvalidException" /> is thrown if the checksum is invalid (or the message is invalid).
    /// </exception>
    public static IcmpSourceQuenchMessage Read(Stream stream) =>
        IIcmpMessage<IcmpSourceQuenchMessage>.Read(stream);

    /// <inheritdoc />
    /// <exception cref="IcmpMessageTypeInvalidException">
    /// An <see cref="IcmpMessageTypeInvalidException" /> is thrown if the specified ICMP message type is not equal to <see cref="IcmpMessageType.SourceQuench" />.
    /// </exception>
    /// <exception cref="IcmpMessageChecksumInvalidException">
    /// An <see cref="IcmpMessageChecksumInvalidException" /> is thrown if the checksum is invalid (or the message is invalid).
    /// </exception>
    public static Task<IcmpSourceQuenchMessage> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        IIcmpMessage<IcmpSourceQuenchMessage>.ReadAsync(stream, cancellationToken);

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
        span[0] = (byte)IcmpMessageType.SourceQuench;
        span[1] = 0;
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
