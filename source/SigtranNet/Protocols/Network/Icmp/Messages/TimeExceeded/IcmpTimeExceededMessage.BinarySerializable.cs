/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;
using SigtranNet.Protocols.Network.Icmp.Messages.Exceptions;
using SigtranNet.Protocols.Network.IP;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Network.Icmp.Messages.TimeExceeded;

internal readonly partial struct IcmpTimeExceededMessage
{
    /// <inheritdoc />
    /// <exception cref="IcmpMessageTypeInvalidException">
    /// An <see cref="IcmpMessageTypeInvalidException" /> is thrown if the specified ICMP message type is not equal to <see cref="IcmpMessageType.TimeExceeded" />.
    /// </exception>
    /// <exception cref="IcmpMessageChecksumInvalidException">
    /// An <see cref="IcmpMessageChecksumInvalidException" /> is thrown if the checksum is invalid (or the message is invalid).
    /// </exception>
    public static IcmpTimeExceededMessage FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;
        var type = (IcmpMessageType)memorySpan[0];
        if (type != IcmpMessageType.TimeExceeded)
            throw new IcmpMessageTypeInvalidException(type);
        var code = (IcmpTimeExceededCode)memorySpan[1];
        var checksum = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[sizeof(ushort)..sizeof(uint)]);
        // 4..8 unused
        var offset = 2 * sizeof(uint);
        var ipHeaderOriginal = IIPHeader.FromReadOnlyMemory(memory[offset..]);
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
    public static IcmpTimeExceededMessage Read(BinaryReader binaryReader)
    {
        var memoryMainLength = 7 * sizeof(uint);
        var memoryMain = new Memory<byte>(new byte[memoryMainLength]);
        var memoryMainSpan = memoryMain.Span;
        binaryReader.Read(memoryMainSpan);

        var ipHeaderOriginalLength = memoryMainSpan[9] & 0x0F;
        var memoryLength = 2 * sizeof(uint) + ipHeaderOriginalLength * sizeof(uint) + sizeof(ulong);
        var memory = new Memory<byte>(new byte[memoryLength]);
        memoryMain.CopyTo(memory);
        binaryReader.Read(memory.Span[memoryMainLength..memoryLength]);

        return FromReadOnlyMemory(memory);
    }

    /// <inheritdoc />
    /// <exception cref="IcmpMessageTypeInvalidException">
    /// An <see cref="IcmpMessageTypeInvalidException" /> is thrown if the specified ICMP message type is not equal to <see cref="IcmpMessageType.TimeExceeded" />.
    /// </exception>
    /// <exception cref="IcmpMessageChecksumInvalidException">
    /// An <see cref="IcmpMessageChecksumInvalidException" /> is thrown if the checksum is invalid (or the message is invalid).
    /// </exception>
    public static IcmpTimeExceededMessage Read(Stream stream)
    {
        var memoryMainLength = 7 * sizeof(uint);
        var memoryMain = new Memory<byte>(new byte[memoryMainLength]);
        var memoryMainSpan = memoryMain.Span;
        stream.Read(memoryMainSpan);

        var ipHeaderOriginalLength = memoryMainSpan[9] & 0x0F;
        var memoryLength = 2 * sizeof(uint) + ipHeaderOriginalLength * sizeof(uint) + sizeof(ulong);
        var memory = new Memory<byte>(new byte[memoryLength]);
        memoryMain.CopyTo(memory);
        stream.Read(memory.Span[memoryMainLength..memoryLength]);

        return FromReadOnlyMemory(memory);
    }

    /// <inheritdoc />
    /// <exception cref="IcmpMessageTypeInvalidException">
    /// An <see cref="IcmpMessageTypeInvalidException" /> is thrown if the specified ICMP message type is not equal to <see cref="IcmpMessageType.TimeExceeded" />.
    /// </exception>
    /// <exception cref="IcmpMessageChecksumInvalidException">
    /// An <see cref="IcmpMessageChecksumInvalidException" /> is thrown if the checksum is invalid (or the message is invalid).
    /// </exception>
    public static async Task<IcmpTimeExceededMessage> ReadAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        var memoryMainLength = 7 * sizeof(uint);
        var memoryMain = new Memory<byte>(new byte[memoryMainLength]);
        await stream.ReadAsync(memoryMain, cancellationToken);

        var ipHeaderOriginalLength = memoryMain.Span[9] & 0x0F;
        var memoryLength = 2 * sizeof(uint) * ipHeaderOriginalLength * sizeof(uint) + sizeof(ulong);
        var memory = new Memory<byte>(new byte[memoryLength]);
        memoryMain.CopyTo(memory);
        await stream.ReadAsync(memory[memoryMainLength..memoryLength], cancellationToken);

        return FromReadOnlyMemory(memory);
    }

    /// <inheritdoc />
    public ReadOnlyMemory<byte> ToReadOnlyMemory()
    {
        var memoryLength =
            2 * sizeof(uint)
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
        // 4..8 unused
        var offset = 2 * sizeof(uint);
        this.ipHeaderOriginal.Write(span[offset..]);
        offset += this.ipHeaderOriginal.InternetHeaderLength * sizeof(uint);
        this.originalDataDatagramSample.Span.CopyTo(span[offset..]);

        // Calculate and insert the checksum.
        var checksum = OnesComplementChecksum16Bit.Generate(span);
        BinaryPrimitives.WriteUInt16BigEndian(span[2..4], checksum);
    }
}
