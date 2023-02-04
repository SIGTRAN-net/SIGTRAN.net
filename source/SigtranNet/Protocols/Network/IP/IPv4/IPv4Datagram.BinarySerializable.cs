/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;

namespace SigtranNet.Protocols.Network.IP.IPv4;

internal readonly partial struct IPv4Datagram : IBinarySerializable<IPv4Datagram>
{
    /// <inheritdoc />
    public static IPv4Datagram FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var header = IPv4Header.FromReadOnlyMemory(memory);
        var payload = memory[(header.internetHeaderLength * sizeof(uint))..header.totalLength];
        return new(header, payload);
    }

    /// <inheritdoc />
    public static IPv4Datagram Read(BinaryReader reader)
    {
        var header = IPv4Header.Read(reader);
        var payloadLength = header.totalLength - (header.internetHeaderLength * sizeof(uint));
        var payload = reader.ReadBytes(payloadLength);
        return new(header, payload);
    }

    /// <inheritdoc />
    public static IPv4Datagram Read(Stream stream)
    {
        var header = IPv4Header.Read(stream);
        var payloadLength = header.totalLength - (header.internetHeaderLength * sizeof(uint));
        var payload = new byte[payloadLength];
        stream.Read(payload);
        return new(header, payload);
    }

    /// <inheritdoc />
    public static async Task<IPv4Datagram> ReadAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        var header = await IPv4Header.ReadAsync(stream, cancellationToken);
        var payloadLength = header.totalLength - (header.internetHeaderLength * sizeof(uint));
        var payload = new byte[payloadLength];
        await stream.ReadAsync(payload, cancellationToken);
        return new(header, payload);
    }

    /// <inheritdoc />
    public ReadOnlyMemory<byte> ToReadOnlyMemory()
    {
        var memory = new Memory<byte>(new byte[this.header.totalLength]);
        this.Write(memory.Span);
        return memory;
    }

    /// <inheritdoc />
    public void Write(Span<byte> span)
    {
        this.header.Write(span);
        this.payload.Span.CopyTo(span[(this.header.internetHeaderLength * sizeof(uint))..this.header.totalLength]);
    }

    /// <inheritdoc />
    public async Task WriteAsync(Memory<byte> memory, CancellationToken cancellationToken = default)
    {
        await this.header.WriteAsync(memory, cancellationToken);
        this.payload.CopyTo(memory[(this.header.internetHeaderLength * sizeof(uint))..this.header.totalLength]);
    }
}
