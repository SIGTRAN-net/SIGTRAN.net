/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;
using SigtranNet.Protocols.IP.IPv4.Options.Exceptions;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.IP.IPv4.Options.StreamIdentifier;

internal readonly partial struct IPv4OptionStreamIdentifier : IBinarySerializable<IPv4OptionStreamIdentifier>
{
    /// <inheritdoc />
    /// <exception cref="IPv4OptionInvalidLengthException">
    /// An <see cref="IPv4OptionInvalidLengthException" /> is thrown if an invalid length was specified in the option.
    /// </exception>
    public static IPv4OptionStreamIdentifier FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;
        var optionType = (IPv4OptionType)memorySpan[0];
        var length = memorySpan[1];
        if (length != LengthFixed)
            throw new IPv4OptionInvalidLengthException(length);
        var streamIdentifier = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[2..]);
        return new(optionType, streamIdentifier);
    }

    /// <inheritdoc />
    /// <exception cref="IPv4OptionInvalidLengthException">
    /// An <see cref="IPv4OptionInvalidLengthException" /> is thrown if an invalid length was specified in the option.
    /// </exception>
    public static IPv4OptionStreamIdentifier Read(BinaryReader reader) => FromReadOnlyMemory(reader.ReadBytes(LengthFixed));

    /// <inheritdoc />
    /// <exception cref="IPv4OptionInvalidLengthException">
    /// An <see cref="IPv4OptionInvalidLengthException" /> is thrown if an invalid length was specified in the option.
    /// </exception>
    public static IPv4OptionStreamIdentifier Read(Stream stream)
    {
        var memory = new Memory<byte>(new byte[LengthFixed]);
        stream.Read(memory.Span);
        return FromReadOnlyMemory(memory);
    }

    /// <inheritdoc />
    /// <exception cref="IPv4OptionInvalidLengthException">
    /// An <see cref="IPv4OptionInvalidLengthException" /> is thrown if an invalid length was specified in the option.
    /// </exception>
    public static async Task<IPv4OptionStreamIdentifier> ReadAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        var memory = new Memory<byte>(new byte[LengthFixed]);
        await stream.ReadAsync(memory, cancellationToken);
        return FromReadOnlyMemory(memory);
    }

    /// <inheritdoc />
    public ReadOnlyMemory<byte> ToReadOnlyMemory()
    {
        var memory = new Memory<byte>(new byte[LengthFixed]);
        var memorySpan = memory.Span;
        memorySpan[0] = (byte)this.optionType;
        memorySpan[1] = LengthFixed;
        BinaryPrimitives.WriteUInt16BigEndian(memorySpan[2..], this.streamIdentifier);
        return memory;
    }

    /// <inheritdoc />
    public void Write(BinaryWriter writer) =>
        writer.Write(this.ToReadOnlyMemory().Span);

    /// <inheritdoc />
    public void Write(Stream stream) =>
        stream.Write(this.ToReadOnlyMemory().Span);

    /// <inheritdoc />
    public ValueTask WriteAsync(Stream stream, CancellationToken cancellationToken = default) =>
        stream.WriteAsync(this.ToReadOnlyMemory(), cancellationToken);
}
