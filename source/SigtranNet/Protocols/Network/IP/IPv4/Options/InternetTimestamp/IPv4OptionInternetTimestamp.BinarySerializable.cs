/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;
using System.Buffers.Binary;
using System.Net;

namespace SigtranNet.Protocols.Network.IP.IPv4.Options.InternetTimestamp;

internal readonly partial struct IPv4OptionInternetTimestamp : IBinarySerializable<IPv4OptionInternetTimestamp>
{
    /// <inheritdoc />
    public static IPv4OptionInternetTimestamp FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;
        var optionType = (IPv4OptionType)memorySpan[0];
        var length = memorySpan[1];
        var pointer = memorySpan[2];
        var overflow = (byte)(memorySpan[3] >> 4);
        var flags = (IPv4OptionInternetTimestampFlags)(memorySpan[3] & 0x0F);

        if (length == LengthMinimum)
            return new(optionType, length, pointer, overflow, flags, new ReadOnlyMemory<IPv4OptionInternetTimestampAddressPair>());

        var timestamps = new List<IPv4OptionInternetTimestampAddressPair>();
        if (flags.HasFlag(IPv4OptionInternetTimestampFlags.InternetAddressPreceded))
        {
            for (var i = 4; i < memorySpan.Length; i += 2 * sizeof(uint))
            {
                var address = new IPAddress(memorySpan[i..(i + sizeof(uint))]);
                var timestamp = BinaryPrimitives.ReadUInt32BigEndian(memorySpan[(i + sizeof(uint))..(i + 2 * sizeof(uint))]);
                timestamps.Add(new IPv4OptionInternetTimestampAddressPair(address, timestamp));
            }
        }
        else
        {
            for (var i = 4; i < memorySpan.Length; i += sizeof(uint))
            {
                var timestamp = BinaryPrimitives.ReadUInt32BigEndian(memorySpan[i..(i + sizeof(uint))]);
                timestamps.Add(new IPv4OptionInternetTimestampAddressPair(timestamp));
            }
        }
        return new(optionType, length, pointer, overflow, flags, timestamps.ToArray());
    }

    /// <inheritdoc />
    public static IPv4OptionInternetTimestamp Read(BinaryReader reader)
    {
        var optionType = reader.ReadByte();
        var length = reader.ReadByte();

        var memory = new Memory<byte>(new byte[length]);
        var memorySpan = memory.Span;
        memorySpan[0] = optionType;
        memorySpan[1] = length;

        var rest = new Memory<byte>(reader.ReadBytes(length - 2));
        rest.CopyTo(memory[2..]);
        return FromReadOnlyMemory(memory);
    }

    /// <inheritdoc />
    public static IPv4OptionInternetTimestamp Read(Stream stream)
    {
        var optionType = (byte)stream.ReadByte();
        var length = (byte)stream.ReadByte();

        var memory = new Memory<byte>(new byte[length]);
        var memorySpan = memory.Span;
        memorySpan[0] = optionType;
        memorySpan[1] = length;
        stream.Read(memorySpan[2..]);
        return FromReadOnlyMemory(memory);
    }

    /// <inheritdoc />
    public static async Task<IPv4OptionInternetTimestamp> ReadAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        var optionType = (byte)stream.ReadByte();
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
        var memorySpan = memory.Span;
        this.Write(memorySpan);
        return memory;
    }

    /// <inheritdoc />
    public void Write(Span<byte> span)
    {
        span[0] = (byte)this.optionType;
        span[1] = this.length;
        span[2] = this.pointer;
        span[3] = (byte)((this.overflow << 4) + (byte)this.flag);

        var timestampsSpan = this.timestamps.Span;
        for (var i = 0; i < timestampsSpan.Length; i++)
        {
            if (flag.HasFlag(IPv4OptionInternetTimestampFlags.InternetAddressPreceded))
            {
                if (timestampsSpan[i].address is { } address)
                    address.MapToIPv4().TryWriteBytes(span[(4 + (i * 2 * sizeof(uint)))..], out _);
                BinaryPrimitives.WriteUInt32BigEndian(span[(4 + (i * 2 * sizeof(uint)) + sizeof(uint))..], timestampsSpan[i].timestamp);
            }
            else
            {
                BinaryPrimitives.WriteUInt32BigEndian(span[(4 + (i * sizeof(uint)))..], timestampsSpan[i].timestamp);
            }
        }
    }
}
