/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;
using SigtranNet.Protocols.IP.Exceptions;
using SigtranNet.Protocols.IP.IPv4.Exceptions;
using SigtranNet.Protocols.IP.IPv4.Options;
using SigtranNet.Protocols.IP.IPv4.Options.Exceptions;
using SigtranNet.Protocols.IP.IPv4.Options.InternetTimestamp;
using SigtranNet.Protocols.IP.IPv4.Options.LooseSourceRouting;
using SigtranNet.Protocols.IP.IPv4.Options.NoOperation;
using SigtranNet.Protocols.IP.IPv4.Options.Security;
using SigtranNet.Protocols.IP.IPv4.Options.StreamIdentifier;
using SigtranNet.Protocols.IP.IPv4.Options.StrictSourceRouting;
using System.Buffers.Binary;
using System.Net;

namespace SigtranNet.Protocols.IP.IPv4;

internal readonly partial struct IPv4Header : IBinarySerializable<IPv4Header>
{
    private const int MinimumLength = sizeof(uint) * 5;

    private static void ReadBase(
        ReadOnlyMemory<byte> memory,
        out byte internetHeaderLength,
        out IPv4TypeOfService typeOfService,
        out ushort totalLength,
        out ushort identification,
        out IPv4Flags flags,
        out ushort fragmentOffset,
        out byte timeToLive,
        out IPProtocol protocol,
        out ushort headerChecksum,
        out IPAddress sourceAddress,
        out IPAddress destinationAddress)
    {
        var span = memory.Span;

        // Version and IHL
        var versionInternetHeaderLength = span[0];
        var version = (IPVersion)(versionInternetHeaderLength >> 4);
        if (version != IPVersion.IPv4)
            throw new IPVersionNotSupportedException();
        internetHeaderLength = (byte)(versionInternetHeaderLength & 0x0F);

        // Type of Service
        typeOfService = (IPv4TypeOfService)span[1];

        // Total Length
        totalLength = BinaryPrimitives.ReadUInt16BigEndian(span[2..4]);

        // Identification
        identification = BinaryPrimitives.ReadUInt16BigEndian(span[4..6]);

        // Flags and Fragment Offset
        var flagsFragmentOffset = BinaryPrimitives.ReadUInt16BigEndian(span[6..8]);
        flags = (IPv4Flags)(flagsFragmentOffset >> 13);
        fragmentOffset = (ushort)(flagsFragmentOffset & 0x1FFF);

        // Time to Live (TTL)
        timeToLive = span[8];

        // Protocol
        protocol = (IPProtocol)span[9];

        // Header Checksum
        headerChecksum = BinaryPrimitives.ReadUInt16BigEndian(span[10..12]);

        sourceAddress = new IPAddress(span[12..16]);
        destinationAddress = new IPAddress(span[16..20]);
    }

    private static ReadOnlyMemory<IIPv4Option> ReadOptions(ReadOnlyMemory<byte> memory)
    {
        var offset = 0;
        var options = new List<IIPv4Option>();

        while (offset < memory.Length)
        {
            var optionType = (IPv4OptionType)memory.Span[offset];
            switch (optionType)
            {
                case IPv4OptionType.NotCopied_Control_EndOfOptionList:
                case IPv4OptionType.Copied_Control_EndOfOptionList:
                    return options.ToArray();
                case IPv4OptionType.NotCopied_Control_NoOperation:
                case IPv4OptionType.Copied_Control_NoOperation:
                    {
                        options.Add(IPv4OptionNoOperation.FromReadOnlyMemory(memory[offset..]));
                        offset++;
                        continue;
                    }
                case IPv4OptionType.NotCopied_Control_Security:
                case IPv4OptionType.Copied_Control_Security:
                    {
                        var securityOption = IPv4OptionSecurity.FromReadOnlyMemory(memory[offset..]);
                        options.Add(securityOption);
                        offset += IPv4OptionSecurity.LengthFixed;
                        break;
                    }
                case IPv4OptionType.NotCopied_Control_LooseSourceRouting:
                case IPv4OptionType.Copied_Control_LooseSourceRouting:
                    {
                        var looseSourceRouting = IPv4OptionLooseSourceRecordRoute.FromReadOnlyMemory(memory[offset..]);
                        options.Add(looseSourceRouting);
                        offset += looseSourceRouting.length;
                        break;
                    }
                case IPv4OptionType.NotCopied_Control_StrictSourceRouting:
                case IPv4OptionType.Copied_Control_StrictSourceRouting:
                    {
                        var strictSourceRouting = IPv4OptionStrictSourceRecordRoute.FromReadOnlyMemory(memory[offset..]);
                        options.Add(strictSourceRouting);
                        offset += strictSourceRouting.length;
                        break;
                    }
                case IPv4OptionType.NotCopied_Control_StreamIdentifier:
                case IPv4OptionType.Copied_Control_StreamIdentifier:
                    {
                        var streamIdentifier = IPv4OptionStreamIdentifier.FromReadOnlyMemory(memory[offset..]);
                        options.Add(streamIdentifier);
                        offset += IPv4OptionStreamIdentifier.LengthFixed;
                        break;
                    }
                case IPv4OptionType.NotCopied_Debugging_InternetTimestamp:
                case IPv4OptionType.Copied_Debugging_InternetTimestamp:
                    {
                        var internetTimestamp = IPv4OptionInternetTimestamp.FromReadOnlyMemory(memory[offset..]);
                        options.Add(internetTimestamp);
                        offset += internetTimestamp.length;
                        break;
                    }
                default:
                    throw new IPv4OptionInvalidTypeException(optionType);
            }
        }

        return options.ToArray();
    }

    /// <inheritdoc />
    /// <exception cref="IPv4HeaderIncompleteException">
    /// An <see cref="IPv4HeaderIncompleteException" /> is thrown if <paramref name="memory" /> does not have the expected length to read the IPv4 header.
    /// </exception>
    /// <exception cref="IPVersionNotSupportedException">
    /// An <see cref="IPVersionNotSupportedException" /> is thrown if the specified IP version is not supported.
    /// </exception>
    /// <exception cref="IPv4OptionInvalidTypeException">
    /// An <see cref="IPv4OptionInvalidTypeException" /> is thrown if the specified option type is invalid.
    /// </exception>
    public static IPv4Header FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        ReadBase(
            memory,
            out var internetHeaderLength,
            out var typeOfService,
            out var totalLength,
            out var identification,
            out var flags,
            out var fragmentOffset,
            out var timeToLive,
            out var protocol,
            out var headerChecksum,
            out var sourceAddress,
            out var destinationAddress);

        var internetHeaderLengthOctets = internetHeaderLength * sizeof(uint);
        if (internetHeaderLengthOctets > memory.Length)
            throw new IPv4HeaderIncompleteException(internetHeaderLength);

        var options =
            internetHeaderLengthOctets > MinimumLength
                ? ReadOptions(memory[MinimumLength..])
                : new ReadOnlyMemory<IIPv4Option>();

        return new(
            internetHeaderLength,
            typeOfService,
            totalLength,
            identification,
            flags,
            fragmentOffset,
            timeToLive,
            protocol,
            headerChecksum,
            sourceAddress,
            destinationAddress,
            options);
    }

    /// <inheritdoc />
    /// <exception cref="IPVersionNotSupportedException">
    /// An <see cref="IPVersionNotSupportedException" /> is thrown if the specified IP version is not supported.
    /// </exception>
    /// <exception cref="IPv4OptionInvalidTypeException">
    /// An <see cref="IPv4OptionInvalidTypeException" /> is thrown if the specified option type is invalid.
    /// </exception>
    public static IPv4Header Read(BinaryReader reader)
    {
        var headerBase = new Memory<byte>(reader.ReadBytes(MinimumLength));
        var internetHeaderLength = (headerBase.Span[0] & 0b0000_1111) * sizeof(uint);
        if (internetHeaderLength == MinimumLength)
            return FromReadOnlyMemory(headerBase);

        var memoryExtended = new Memory<byte>(new byte[internetHeaderLength]);
        headerBase.CopyTo(memoryExtended);
        var headerOptions = new Memory<byte>(reader.ReadBytes(internetHeaderLength - MinimumLength));
        headerOptions.CopyTo(memoryExtended[MinimumLength..]);

        return FromReadOnlyMemory(memoryExtended);
    }

    /// <inheritdoc />
    /// <exception cref="IPVersionNotSupportedException">
    /// An <see cref="IPVersionNotSupportedException" /> is thrown if the specified IP version is not supported.
    /// </exception>
    /// <exception cref="IPv4OptionInvalidTypeException">
    /// An <see cref="IPv4OptionInvalidTypeException" /> is thrown if the specified option type is invalid.
    /// </exception>
    public static IPv4Header Read(Stream stream)
    {
        var headerBase = new Memory<byte>(new byte[MinimumLength]);
        var headerBaseSpan = headerBase.Span;
        stream.Read(headerBaseSpan);
        var internetHeaderLength = (headerBaseSpan[0] & 0b0000_1111) * sizeof(uint);
        if (internetHeaderLength == MinimumLength)
            return FromReadOnlyMemory(headerBase);

        var memoryExtended = new Memory<byte>(new byte[internetHeaderLength]);
        headerBase.CopyTo(memoryExtended);
        var headerOptions = new Memory<byte>(new byte[internetHeaderLength - MinimumLength]);
        stream.Read(headerOptions.Span);
        headerOptions.CopyTo(memoryExtended[MinimumLength..]);

        return FromReadOnlyMemory(memoryExtended);
    }

    /// <inheritdoc />
    /// <exception cref="IPVersionNotSupportedException">
    /// An <see cref="IPVersionNotSupportedException" /> is thrown if the specified IP version is not supported.
    /// </exception>
    /// <exception cref="IPv4OptionInvalidTypeException">
    /// An <see cref="IPv4OptionInvalidTypeException" /> is thrown if the specified option type is invalid.
    /// </exception>
    public static async Task<IPv4Header> ReadAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        var headerBase = new Memory<byte>(new byte[MinimumLength]);
        await stream.ReadAsync(headerBase, cancellationToken);

        var internetHeaderLength = (headerBase.Span[0] & 0b0000_1111) * sizeof(uint);
        if (internetHeaderLength == MinimumLength)
            return FromReadOnlyMemory(headerBase);

        var memoryExtended = new Memory<byte>(new byte[internetHeaderLength]);
        headerBase.CopyTo(memoryExtended);
        var headerOptions = new Memory<byte>(new byte[internetHeaderLength - MinimumLength]);
        await stream.ReadAsync(headerOptions, cancellationToken);
        headerOptions.CopyTo(memoryExtended[MinimumLength..]);

        return FromReadOnlyMemory(memoryExtended);
    }

    /// <inheritdoc />
    public ReadOnlyMemory<byte> ToReadOnlyMemory()
    {
        var data = new Memory<byte>(new byte[this.internetHeaderLength * sizeof(uint)]);
        this.Write(data.Span);
        return data;
    }

    /// <inheritdoc />
    public void Write(Span<byte> span)
    {
        var versionInternetHeaderLength =
            (byte)(((byte)IPVersion.IPv4 << 4)
            + this.internetHeaderLength);
        span[0] = versionInternetHeaderLength;
        span[1] = (byte)this.typeOfService;
        BinaryPrimitives.WriteUInt16BigEndian(span[2..], this.totalLength);
        BinaryPrimitives.WriteUInt16BigEndian(span[4..], this.identification);

        var flagsFragmentOffset = (ushort)(((ushort)this.flags << 13) + this.fragmentOffset);
        BinaryPrimitives.WriteUInt16BigEndian(span[6..], flagsFragmentOffset);

        span[8] = this.timeToLive;
        span[9] = (byte)this.protocol;
        BinaryPrimitives.WriteUInt16BigEndian(span[10..], this.headerChecksum);

        this.sourceAddress.MapToIPv4().TryWriteBytes(span[12..], out _);
        this.destinationAddress.MapToIPv4().TryWriteBytes(span[16..], out _);

        var offset = 20;
        foreach (var option in this.options.Span)
        {
            var optionData = option.ToReadOnlyMemory();
            optionData.Span.CopyTo(span[offset..]);
            offset += optionData.Length;
        }
    }
}
