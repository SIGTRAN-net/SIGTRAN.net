/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;
using SigtranNet.Protocols.IP.IPv4.Options.Exceptions;

namespace SigtranNet.Protocols.IP.IPv4.Options.NoOperation;

internal readonly partial struct IPv4OptionNoOperation : IBinarySerializable<IPv4OptionNoOperation>
{
    /// <inheritdoc />
    /// <exception cref="IPv4OptionInvalidTypeException">
    /// An <see cref="IPv4OptionInvalidTypeException" /> is thrown if a 'No Operation' IPv4 option is expected, but not found.
    /// </exception>
    public static IPv4OptionNoOperation FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var optionType = (IPv4OptionType)memory.Span[0];
        return new(optionType);
    }

    /// <inheritdoc />
    /// <exception cref="IPv4OptionInvalidTypeException">
    /// An <see cref="IPv4OptionInvalidTypeException" /> is thrown if a 'No Operation' IPv4 option is expected, but not found.
    /// </exception>
    public static IPv4OptionNoOperation Read(BinaryReader reader)
    {
        var optionType = (IPv4OptionType)reader.ReadByte();
        return new(optionType);
    }

    /// <inheritdoc />
    /// <exception cref="IPv4OptionInvalidTypeException">
    /// An <see cref="IPv4OptionInvalidTypeException" /> is thrown if a 'No Operation' IPv4 option is expected, but not found.
    /// </exception>
    public static IPv4OptionNoOperation Read(Stream stream)
    {
        var optionType = (IPv4OptionType)stream.ReadByte();
        return new(optionType);
    }

    /// <inheritdoc />
    /// <exception cref="IPv4OptionInvalidTypeException">
    /// An <see cref="IPv4OptionInvalidTypeException" /> is thrown if a 'No Operation' IPv4 option is expected, but not found.
    /// </exception>
    public static Task<IPv4OptionNoOperation> ReadAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        var optionType = (IPv4OptionType)stream.ReadByte();
        return Task.FromResult(new IPv4OptionNoOperation(optionType));
    }

    /// <inheritdoc />
    public ReadOnlyMemory<byte> ToReadOnlyMemory() => new(new byte[] { (byte)this.optionType });

    /// <inheritdoc />
    public void Write(BinaryWriter writer) => writer.Write(this.ToReadOnlyMemory().Span);

    /// <inheritdoc />
    public void Write(Stream stream) => stream.Write(this.ToReadOnlyMemory().Span);

    /// <inheritdoc />
    public ValueTask WriteAsync(Stream stream, CancellationToken cancellationToken = default) =>
        stream.WriteAsync(this.ToReadOnlyMemory(), cancellationToken);
}
