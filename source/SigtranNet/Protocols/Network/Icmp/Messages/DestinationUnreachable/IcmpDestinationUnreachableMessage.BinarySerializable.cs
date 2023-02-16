/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;
using SigtranNet.Protocols.Network.Icmp.Messages.Exceptions;
using SigtranNet.Protocols.Network.IP.IPv4;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Network.Icmp.Messages.DestinationUnreachable;

internal readonly partial struct IcmpDestinationUnreachableMessage
{
    /// <inheritdoc />
    /// <exception cref="IcmpMessageTypeInvalidException">
    /// An <see cref="IcmpMessageTypeInvalidException" /> is thrown if the specified message type is not equal to <see cref="IcmpMessageType.DestinationUnreachable" />.
    /// </exception>
    /// <exception cref="IcmpMessageChecksumInvalidException">
    /// An <see cref="IcmpMessageChecksumInvalidException" /> is thrown if the message has an invalid checksum (or the message is corrupted).
    /// </exception>
    public static IcmpDestinationUnreachableMessage FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var span = memory.Span;
        var type = (IcmpMessageType)span[0];
        if (type != IcmpMessageType.DestinationUnreachable)
            throw new IcmpMessageTypeInvalidException(type);
        var code = (IcmpDestinationUnreachableCode)span[1];
        var checksum = BinaryPrimitives.ReadUInt16BigEndian(span[sizeof(ushort)..sizeof(uint)]);
        // 4..8 unused

        var offset = sizeof(ulong);
        var ipHeaderOriginal = IPv4Header.FromReadOnlyMemory(memory[offset..]);
        offset += ipHeaderOriginal.InternetHeaderLength * sizeof(uint);
        var originalDataDatagramSample = memory[offset..(offset + sizeof(ulong))];
        offset += sizeof(ulong);

        if (!OnesComplementChecksum16Bit.Validate(memory[0..offset]))
            throw new IcmpMessageChecksumInvalidException(checksum);
        return new(code, ipHeaderOriginal, originalDataDatagramSample);
    }

    /// <inheritdoc />
    /// <exception cref="IcmpMessageTypeInvalidException">
    /// An <see cref="IcmpMessageTypeInvalidException" /> is thrown if the specified message type is not equal to <see cref="IcmpMessageType.DestinationUnreachable" />.
    /// </exception>
    /// <exception cref="IcmpMessageChecksumInvalidException">
    /// An <see cref="IcmpMessageChecksumInvalidException" /> is thrown if the message has an invalid checksum (or the message is invalid).
    /// </exception>
    public static IcmpDestinationUnreachableMessage Read(BinaryReader binaryReader) =>
        IIcmpMessage<IcmpDestinationUnreachableMessage>.Read(binaryReader);

    /// <inheritdoc />
    /// <exception cref="IcmpMessageTypeInvalidException">
    /// An <see cref="IcmpMessageTypeInvalidException" /> is thrown if the specified message type is not equal to <see cref="IcmpMessageType.DestinationUnreachable" />.
    /// </exception>
    /// <exception cref="IcmpMessageChecksumInvalidException">
    /// An <see cref="IcmpMessageChecksumInvalidException" /> is thrown if the message has an invalid checksum (or the message is invalid).
    /// </exception>
    public static IcmpDestinationUnreachableMessage Read(Stream stream) =>
        IIcmpMessage<IcmpDestinationUnreachableMessage>.Read(stream);

    /// <inheritdoc />
    /// <exception cref="IcmpMessageTypeInvalidException">
    /// An <see cref="IcmpMessageTypeInvalidException" /> is thrown if the specified message type is not equal to <see cref="IcmpMessageType.DestinationUnreachable" />.
    /// </exception>
    /// <exception cref="IcmpMessageChecksumInvalidException">
    /// An <see cref="IcmpMessageChecksumInvalidException" /> is thrown if the message has an invalid checksum (or the message is invalid).
    /// </exception>
    public static Task<IcmpDestinationUnreachableMessage> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        IIcmpMessage<IcmpDestinationUnreachableMessage>.ReadAsync(stream, cancellationToken);

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
        span[0] = (byte)IcmpMessageType.DestinationUnreachable;
        span[1] = (byte)this.code;
        // 2..4 Skip checksum for now in order to calculate it.
        // 4..8 unused (2nd unsigned 32-bit word is unused).
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
