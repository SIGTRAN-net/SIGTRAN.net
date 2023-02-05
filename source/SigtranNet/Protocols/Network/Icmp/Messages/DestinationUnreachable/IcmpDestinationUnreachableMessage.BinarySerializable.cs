﻿/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;
using SigtranNet.Protocols.Network.Icmp.Messages.Exceptions;
using SigtranNet.Protocols.Network.IP;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Network.Icmp.Messages.DestinationUnreachable;

internal readonly partial struct IcmpDestinationUnreachableMessage
{
    /// <inheritdoc />
    /// <exception cref="IcmpMessageTypeInvalidException">
    /// An <see cref="IcmpMessageTypeInvalidException" /> is thrown if the specified message type is not equal to <see cref="IcmpMessageType.DestinationUnreachable" />.
    /// </exception>
    /// <exception cref="IcmpMessageChecksumInvalidException">
    /// An <see cref="IcmpMessageChecksumInvalidException" /> is thrown if the message has an invalid checksum (or the message is invalid).
    /// </exception>
    public static IcmpDestinationUnreachableMessage FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;

        var offset = 0;
        var type = (IcmpMessageType)memorySpan[offset];
        if (type != IcmpMessageType.DestinationUnreachable)
            throw new IcmpMessageTypeInvalidException(type);
        offset++;
        var code = (IcmpDestinationUnreachableCode)memorySpan[offset];
        offset++;
        var checksum = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[offset..]);
        offset += sizeof(ushort);
        // 4..8 unused
        offset += sizeof(uint);
        var ipHeaderOriginal = IIPHeader.FromReadOnlyMemory(memory[offset..]);
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
    public static IcmpDestinationUnreachableMessage Read(BinaryReader binaryReader)
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
    /// An <see cref="IcmpMessageTypeInvalidException" /> is thrown if the specified message type is not equal to <see cref="IcmpMessageType.DestinationUnreachable" />.
    /// </exception>
    /// <exception cref="IcmpMessageChecksumInvalidException">
    /// An <see cref="IcmpMessageChecksumInvalidException" /> is thrown if the message has an invalid checksum (or the message is invalid).
    /// </exception>
    public static IcmpDestinationUnreachableMessage Read(Stream stream)
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
    /// An <see cref="IcmpMessageTypeInvalidException" /> is thrown if the specified message type is not equal to <see cref="IcmpMessageType.DestinationUnreachable" />.
    /// </exception>
    /// <exception cref="IcmpMessageChecksumInvalidException">
    /// An <see cref="IcmpMessageChecksumInvalidException" /> is thrown if the message has an invalid checksum (or the message is invalid).
    /// </exception>
    public static async Task<IcmpDestinationUnreachableMessage> ReadAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        var memoryMainLength = 7 * sizeof(uint);
        var memoryMain = new Memory<byte>(new byte[memoryMainLength]);
        await stream.ReadAsync(memoryMain, cancellationToken);

        var ipHeaderOriginalLength = memoryMain.Span[9] & 0x0F;
        var memoryLength = 2 * sizeof(uint) + ipHeaderOriginalLength * sizeof(uint) + sizeof(ulong);
        var memory = new Memory<byte>(new byte[memoryLength]);
        memoryMain.CopyTo(memory);
        await stream.ReadAsync(memory[memoryMainLength..memoryLength], cancellationToken);

        return FromReadOnlyMemory(memory);
    }

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
        var offset = 0;
        span[offset] = (byte)IcmpMessageType.DestinationUnreachable;
        offset++;
        span[offset] = (byte)this.code;
        offset++;
        // Skip checksum for now in order to calculate it.
        offset += sizeof(ushort);
        // 2nd unsigned 32-bit word is unused.
        offset += sizeof(uint);
        this.ipHeaderOriginal.Write(span[offset..]);
        offset += this.ipHeaderOriginal.InternetHeaderLength * sizeof(uint);
        this.originalDataDatagramSample.Span.CopyTo(span[offset..]);

        // Calculate the checksum and insert it.
        var checksum = OnesComplementChecksum16Bit.Generate(span);
        BinaryPrimitives.WriteUInt16BigEndian(span[sizeof(ushort)..sizeof(uint)], checksum);
    }
}
