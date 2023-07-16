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

using SigtranNet.Binary;
using SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header.Options.Exceptions;
using System.Net;

namespace SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header.Options.RecordRoute;

internal readonly partial struct IPv4OptionRecordRoute : IBinarySerializable<IPv4OptionRecordRoute>
{
    /// <inheritdoc />
    /// <exception cref="IPv4OptionInvalidTypeException">
    /// An <see cref="IPv4OptionInvalidTypeException" /> is thrown if an invalid option type is read.
    /// </exception>
    public static IPv4OptionRecordRoute FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var span = memory.Span;
        var optionType = (IPv4OptionType)span[0];
        if (optionType is
                not IPv4OptionType.NotCopied_Control_RecordRoute
                and not IPv4OptionType.NotCopied_Control_RecordRoute)
            throw new IPv4OptionInvalidTypeException(optionType);

        var length = span[1];
        var pointer = span[2]; // pointer is checked in constructor

        var route = new List<IPAddress>();
        for (var i = 3; i < length; i += 4)
        {
            var ipAddress = new IPAddress(span[i..(i + 4)]);
            route.Add(ipAddress);
        }

        return new(optionType, length, pointer, route.ToArray());
    }

    /// <inheritdoc />
    /// <exception cref="IPv4OptionInvalidTypeException">
    /// An <see cref="IPv4OptionInvalidTypeException" /> is thrown if an invalid option type is read.
    /// </exception>
    public static IPv4OptionRecordRoute Read(BinaryReader reader)
    {
        var optionType = reader.ReadByte();
        if (optionType is
                not (byte)IPv4OptionType.NotCopied_Control_RecordRoute
                and not (byte)IPv4OptionType.NotCopied_Control_RecordRoute)
            throw new IPv4OptionInvalidTypeException((IPv4OptionType)optionType);
        var length = reader.ReadByte();

        var memory = new Memory<byte>(new byte[length]);
        var memorySpan = memory.Span;
        memorySpan[0] = optionType;
        memorySpan[1] = length;

        var route = new Memory<byte>(reader.ReadBytes(length - 2));
        route.CopyTo(memory[2..]);
        return FromReadOnlyMemory(memory);
    }

    /// <inheritdoc />
    /// <exception cref="IPv4OptionInvalidTypeException">
    /// An <see cref="IPv4OptionInvalidTypeException" /> is thrown if an invalid option type is read.
    /// </exception>
    public static IPv4OptionRecordRoute Read(Stream stream)
    {
        var optionType = (byte)stream.ReadByte();
        if (optionType is
                not (byte)IPv4OptionType.NotCopied_Control_RecordRoute
                and not (byte)IPv4OptionType.NotCopied_Control_RecordRoute)
            throw new IPv4OptionInvalidTypeException((IPv4OptionType)optionType);
        var length = (byte)stream.ReadByte();

        var memory = new Memory<byte>(new byte[length]);
        var memorySpan = memory.Span;
        memorySpan[0] = optionType;
        memorySpan[1] = length;

        var route = new Memory<byte>(new byte[length - 2]);
        stream.Read(route.Span);
        route.CopyTo(memory[2..]);
        return FromReadOnlyMemory(memory);
    }

    /// <inheritdoc />
    /// <exception cref="IPv4OptionInvalidTypeException">
    /// An <see cref="IPv4OptionInvalidTypeException" /> is thrown if an invalid option type is read.
    /// </exception>
    public static async Task<IPv4OptionRecordRoute> ReadAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        var optionType = (byte)stream.ReadByte();
        if (optionType is
                not (byte)IPv4OptionType.NotCopied_Control_RecordRoute
                and not (byte)IPv4OptionType.NotCopied_Control_RecordRoute)
            throw new IPv4OptionInvalidTypeException((IPv4OptionType)optionType);
        var length = (byte)stream.ReadByte();

        var memory = new Memory<byte>(new byte[length]);
        memory.Span[0] = optionType;
        memory.Span[1] = length;

        var route = new Memory<byte>(new byte[length - 2]);
        await stream.ReadAsync(route, cancellationToken);
        route.CopyTo(memory[2..]);
        return FromReadOnlyMemory(memory);
    }

    /// <inheritdoc />
    public ReadOnlyMemory<byte> ToReadOnlyMemory()
    {
        var memory = new Memory<byte>(new byte[this.length]);
        this.Write(memory.Span);
        return memory;
    }

    /// <inheritdoc />
    public void Write(Span<byte> span)
    {
        span[0] = (byte)this.optionType;
        span[1] = (byte)this.length;
        span[2] = this.pointer;

        var routeSpan = this.route.Span;
        for (var i = 0; i < routeSpan.Length; i++)
        {
            routeSpan[i].TryWriteBytes(span[(3 + i * sizeof(uint))..], out _);
        }
    }
}
