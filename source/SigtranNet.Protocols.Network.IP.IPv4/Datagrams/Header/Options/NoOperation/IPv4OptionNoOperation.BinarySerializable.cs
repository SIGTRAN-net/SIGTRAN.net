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
using SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header.Options;
using SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header.Options.Exceptions;

namespace SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header.Options.NoOperation;

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
    public ReadOnlyMemory<byte> ToReadOnlyMemory() =>
        new(new byte[] { (byte)this.optionType });

    /// <inheritdoc />
    public void Write(Span<byte> span)
    {
        span[0] = (byte)this.optionType;
    }

    /// <inheritdoc />
    public Task WriteAsync(Memory<byte> memory, CancellationToken cancellationToken = default)
    {
        memory.Span[0] = (byte)this.optionType;
        return Task.CompletedTask;
    }
}
