/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;
using SigtranNet.Protocols.Network.Icmp.Messages.Exceptions;
using SigtranNet.Protocols.Network.IP.IPv4;
using System.Buffers.Binary;
using System.Net;

namespace SigtranNet.Protocols.Network.Icmp.Messages.Redirect;

internal readonly partial struct IcmpRedirectMessage
{
    /// <inheritdoc />
    /// <exception cref="IcmpMessageTypeInvalidException">
    /// An <see cref="IcmpMessageTypeInvalidException" /> is thrown if the message type is not equal to <see cref="IcmpMessageType.Redirect" />.
    /// </exception>
    /// <exception cref="IcmpMessageChecksumInvalidException">
    /// An <see cref="IcmpMessageChecksumInvalidException" /> is thrown if the checksum is invalid, or the message is invalid.
    /// </exception>
    public static IcmpRedirectMessage FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var span = memory.Span;

        /* ICMP Header */
        var type = (IcmpMessageType)span[0];
        if (type != IcmpMessageType.Redirect)
            throw new IcmpMessageTypeInvalidException(type);
        var code = (IcmpRedirectCode)span[1];
        var checksum = BinaryPrimitives.ReadUInt16BigEndian(span[sizeof(ushort)..sizeof(uint)]);
        var gatewayInternetAddress = new IPAddress(span[sizeof(uint)..sizeof(ulong)]);
        
        /* Internet Header + 64 bits of Original Data Datagram */
        var offset = sizeof(ulong);
        var ipHeaderOriginal = IPv4Header.FromReadOnlyMemory(memory[offset..]);
        offset += ipHeaderOriginal.internetHeaderLength * sizeof(uint);
        var originalDataDatagram = memory[offset..(offset + sizeof(ulong))];
        offset += sizeof(ulong);

        if (!OnesComplementChecksum16Bit.Validate(memory[0..offset]))
            throw new IcmpMessageChecksumInvalidException(checksum);

        return new(code, gatewayInternetAddress, ipHeaderOriginal, originalDataDatagram);
    }

    /// <inheritdoc />
    /// <exception cref="IcmpMessageTypeInvalidException">
    /// An <see cref="IcmpMessageTypeInvalidException" /> is thrown if the message type is not equal to <see cref="IcmpMessageType.Redirect" />.
    /// </exception>
    /// <exception cref="IcmpMessageChecksumInvalidException">
    /// An <see cref="IcmpMessageChecksumInvalidException" /> is thrown if the checksum is invalid, or the message is invalid.
    /// </exception>
    public static IcmpRedirectMessage Read(BinaryReader binaryReader) =>
        IIcmpMessage<IcmpRedirectMessage>.Read(binaryReader);

    /// <inheritdoc />
    /// <exception cref="IcmpMessageTypeInvalidException">
    /// An <see cref="IcmpMessageTypeInvalidException" /> is thrown if the message type is not equal to <see cref="IcmpMessageType.Redirect" />.
    /// </exception>
    /// <exception cref="IcmpMessageChecksumInvalidException">
    /// An <see cref="IcmpMessageChecksumInvalidException" /> is thrown if the checksum is invalid, or the message is invalid.
    /// </exception>
    public static IcmpRedirectMessage Read(Stream stream) =>
        IIcmpMessage<IcmpRedirectMessage>.Read(stream);

    /// <inheritdoc />
    /// <exception cref="IcmpMessageTypeInvalidException">
    /// An <see cref="IcmpMessageTypeInvalidException" /> is thrown if the message type is not equal to <see cref="IcmpMessageType.Redirect" />.
    /// </exception>
    /// <exception cref="IcmpMessageChecksumInvalidException">
    /// An <see cref="IcmpMessageChecksumInvalidException" /> is thrown if the checksum is invalid, or the message is invalid.
    /// </exception>
    public static Task<IcmpRedirectMessage> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        IIcmpMessage<IcmpRedirectMessage>.ReadAsync(stream, cancellationToken);

    /// <inheritdoc />
    public ReadOnlyMemory<byte> ToReadOnlyMemory()
    {
        var memoryLength =
            sizeof(ulong)
            + this.ipHeaderOriginal.internetHeaderLength * sizeof(uint)
            + sizeof(ulong);
        var memory = new Memory<byte>(new byte[memoryLength]);
        this.Write(memory.Span);
        return memory;
    }

    /// <inheritdoc />
    public void Write(Span<byte> span)
    {
        /* ICMP Header */
        span[0] = (byte)IcmpMessageType.Redirect;
        span[1] = (byte)this.code;
        // 2..4 Checksum - skip in order to calculate.
        this.gatewayInternetAddress.MapToIPv4().TryWriteBytes(span[sizeof(uint)..sizeof(ulong)], out _);

        /* Internet Header + 64 bits of Original Data Datagram */
        var offset = sizeof(ulong);
        this.ipHeaderOriginal.Write(span[offset..]);
        offset += this.ipHeaderOriginal.InternetHeaderLength * sizeof(uint);
        this.originalDataDatagramSample.Span.CopyTo(span[offset..(offset + sizeof(ulong))]);
        offset += sizeof(ulong);

        /* Calculate checksum and insert it. */
        var checksum = OnesComplementChecksum16Bit.Generate(span[0..offset]);
        BinaryPrimitives.WriteUInt16BigEndian(span[sizeof(ushort)..sizeof(uint)], checksum);
    }
}