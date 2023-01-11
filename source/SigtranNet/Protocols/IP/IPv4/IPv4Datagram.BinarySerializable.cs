/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;

namespace SigtranNet.Protocols.IP.IPv4;

internal readonly partial struct IPv4Datagram : IBinarySerializable<IPv4Datagram>
{
    /// <inheritdoc />
    public static IPv4Datagram FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var header = IPv4Header.FromReadOnlyMemory(memory);
        var payload = memory[header.internetHeaderLength..header.totalLength];
        return new(header, payload);
    }

    /// <inheritdoc />
    public static IPv4Datagram Read(BinaryReader reader)
    {
        var header = IPv4Header.Read(reader);
        var payloadLength = header.totalLength - header.internetHeaderLength;
        var payload = reader.ReadBytes(payloadLength);
        return new(header, payload);
    }

    /// <inheritdoc />
    public static IPv4Datagram Read(Stream stream)
    {
        var header = IPv4Header.Read(stream);
        var payloadLength = header.totalLength - header.internetHeaderLength;
        var payload = new byte[payloadLength];
        stream.Read(payload);
        return new(header, payload);
    }

    /// <inheritdoc />
    public static async Task<IPv4Datagram> ReadAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        var header = await IPv4Header.ReadAsync(stream, cancellationToken);
        var payloadLength = header.totalLength - header.internetHeaderLength;
        var payload = new byte[payloadLength];
        await stream.ReadAsync(payload, cancellationToken);
        return new(header, payload);
    }

    /// <inheritdoc />
    public ReadOnlyMemory<byte> ToReadOnlyMemory()
    {
        var headerMemory = this.header.ToReadOnlyMemory();
        var result = new Memory<byte>(new byte[headerMemory.Length + this.payload.Length]);
        headerMemory.CopyTo(result);
        this.payload.CopyTo(result[headerMemory.Length..]);
        return result;
    }

    /// <inheritdoc />
    public void Write(BinaryWriter writer) => writer.Write(this.ToReadOnlyMemory().Span);

    /// <inheritdoc />
    public void Write(Stream stream) => stream.Write(this.ToReadOnlyMemory().Span);

    /// <inheritdoc />
    public ValueTask WriteAsync(Stream stream, CancellationToken cancellationToken = default) =>
        stream.WriteAsync(this.ToReadOnlyMemory(), cancellationToken);
}
