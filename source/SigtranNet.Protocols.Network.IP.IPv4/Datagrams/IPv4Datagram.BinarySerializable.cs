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

using SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header;

namespace SigtranNet.Protocols.Network.IP.IPv4.Datagrams;

internal readonly partial struct IPv4Datagram
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
