/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;
using SigtranNet.Protocols.Network.IP.IPv4.Options.Exceptions;
using System.Net;

namespace SigtranNet.Protocols.Network.IP.IPv4.Options.LooseSourceRouting;

internal readonly partial struct IPv4OptionLooseSourceRecordRoute : IBinarySerializable<IPv4OptionLooseSourceRecordRoute>
{
    /// <inheritdoc />
    /// <exception cref="IPv4OptionInvalidTypeException">
    /// An <see cref="IPv4OptionInvalidTypeException" /> is thrown if an invalid option type is read.
    /// </exception>
    public static IPv4OptionLooseSourceRecordRoute FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var span = memory.Span;
        var optionType = (IPv4OptionType)span[0];
        if (optionType is
                not IPv4OptionType.NotCopied_Control_LooseSourceRouting
                and not IPv4OptionType.Copied_Control_LooseSourceRouting)
            throw new IPv4OptionInvalidTypeException(optionType);

        var length = span[1];
        var pointer = span[2]; // pointer is checked in constructor

        var route = new List<IPAddress>();
        for (var i = 3; i < length; i += 4)
        {
            var ipAddress = new IPAddress(span[i..(i + 4)]);
            route.Add(ipAddress);
        }

        return new(optionType, pointer, route.ToArray());
    }

    /// <inheritdoc />
    /// <exception cref="IPv4OptionInvalidTypeException">
    /// An <see cref="IPv4OptionInvalidTypeException" /> is thrown if an invalid option type is read.
    /// </exception>
    public static IPv4OptionLooseSourceRecordRoute Read(BinaryReader reader)
    {
        var optionType = reader.ReadByte();
        if (optionType is
                not (byte)IPv4OptionType.NotCopied_Control_LooseSourceRouting
                and not (byte)IPv4OptionType.Copied_Control_LooseSourceRouting)
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
    public static IPv4OptionLooseSourceRecordRoute Read(Stream stream)
    {
        var optionType = (byte)stream.ReadByte();
        if (optionType is
                not (byte)IPv4OptionType.NotCopied_Control_LooseSourceRouting
                and not (byte)IPv4OptionType.Copied_Control_LooseSourceRouting)
            throw new IPv4OptionInvalidTypeException((IPv4OptionType)optionType);
        var length = (byte)stream.ReadByte();

        var memory = new Memory<byte>(new byte[length]);
        var memorySpan = memory.Span;
        memorySpan[0] = optionType;
        memorySpan[1] = length;
        stream.Read(memorySpan[2..]);
        return FromReadOnlyMemory(memory);
    }

    /// <inheritdoc />
    /// <exception cref="IPv4OptionInvalidTypeException">
    /// An <see cref="IPv4OptionInvalidTypeException" /> is thrown if an invalid option type is read.
    /// </exception>
    public static async Task<IPv4OptionLooseSourceRecordRoute> ReadAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        var optionType = (byte)stream.ReadByte();
        if (optionType is
                not (byte)IPv4OptionType.NotCopied_Control_LooseSourceRouting
                and not (byte)IPv4OptionType.Copied_Control_LooseSourceRouting)
            throw new IPv4OptionInvalidTypeException((IPv4OptionType)optionType);
        var length = (byte)stream.ReadByte();

        var memory = new Memory<byte>(new byte[length]);
        memory.Span[0] = optionType;
        memory.Span[1] = length;
        await stream.ReadAsync(memory[2..], cancellationToken);
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
