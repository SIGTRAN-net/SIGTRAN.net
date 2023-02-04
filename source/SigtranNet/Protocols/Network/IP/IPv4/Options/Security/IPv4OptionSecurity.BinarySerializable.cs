/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;
using SigtranNet.Protocols.Network.IP.IPv4.Options.Exceptions;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Network.IP.IPv4.Options.Security;

internal readonly partial struct IPv4OptionSecurity : IBinarySerializable<IPv4OptionSecurity>
{
    /// <inheritdoc />
    /// <exception cref="IPv4OptionInvalidTypeException">
    /// An <see cref="IPv4OptionInvalidTypeException" /> is thrown if the specified IPv4 option type is not "Security".
    /// </exception>
    /// <exception cref="IPv4OptionInvalidLengthException">
    /// An <see cref="IPv4OptionInvalidLengthException" /> is thrown if the specified length in the IPv4 is not the expected value 11.
    /// </exception>
    public static IPv4OptionSecurity FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var optionType = (IPv4OptionType)memory.Span[0];
        if (optionType is not IPv4OptionType.NotCopied_Control_Security and not IPv4OptionType.Copied_Control_Security)
            throw new IPv4OptionInvalidTypeException(optionType);
        var length = memory.Span[1];
        if (length != LengthFixed)
            throw new IPv4OptionInvalidLengthException(length);

        var securityLevel = (IPv4OptionSecurityLevel)BinaryPrimitives.ReadUInt16BigEndian(memory.Span[2..4]);
        var compartments = BinaryPrimitives.ReadUInt16BigEndian(memory.Span[4..6]);
        var handlingRestrictions = BinaryPrimitives.ReadUInt16BigEndian(memory.Span[6..8]);

        var tcc = new Span<byte>(new byte[sizeof(uint)]);
        memory.Span[8..11].CopyTo(tcc);
        var transmissionControlCode = BinaryPrimitives.ReadUInt32BigEndian(tcc) >> 8;

        return new(
            optionType,
            securityLevel,
            compartments,
            handlingRestrictions,
            transmissionControlCode);
    }

    /// <inheritdoc />
    public static IPv4OptionSecurity Read(BinaryReader reader) => FromReadOnlyMemory(new ReadOnlyMemory<byte>(reader.ReadBytes(LengthFixed)));

    /// <inheritdoc />
    public static IPv4OptionSecurity Read(Stream stream)
    {
        var buffer = new byte[LengthFixed];
        stream.Read(buffer);
        return FromReadOnlyMemory(buffer);
    }

    /// <inheritdoc />
    public static async Task<IPv4OptionSecurity> ReadAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        var buffer = new byte[LengthFixed];
        await stream.ReadAsync(buffer, cancellationToken);
        return FromReadOnlyMemory(buffer);
    }

    /// <inheritdoc />
    public ReadOnlyMemory<byte> ToReadOnlyMemory()
    {
        var result = new Memory<byte>(new byte[LengthFixed]);
        this.Write(result.Span);
        return result;
    }

    /// <inheritdoc />
    public void Write(Span<byte> span)
    {
        span[0] = (byte)this.optionType;
        span[1] = LengthFixed;
        BinaryPrimitives.WriteUInt16BigEndian(span[2..], (ushort)this.securityLevel);
        BinaryPrimitives.WriteUInt16BigEndian(span[4..], this.compartments);
        BinaryPrimitives.WriteUInt16BigEndian(span[6..], this.handlingRestrictions);

        var tcc = new Span<byte>(new byte[sizeof(uint)]);
        BinaryPrimitives.WriteUInt32BigEndian(tcc, this.transmissionControlCode << 8);
        span[8] = tcc[0];
        span[9] = tcc[1];
        span[10] = tcc[2];
    }
}
