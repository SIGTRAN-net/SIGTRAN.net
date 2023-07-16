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
using SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header.Options.LooseSourceRouting;
using System.Net;

namespace SigtranNet.Tests.Protocols.Network.IP.IPv4.Options.LooseSourceRouting;

public partial class IPv4OptionLooseSourceRecordRouteTest
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    (byte)IPv4OptionType.NotCopied_Control_LooseSourceRouting,      // Option Type
                    15,                                                             // Length
                    4,                                                              // Pointer
                    192, 168, 10, 10,
                    192, 168, 10, 11,
                    192, 168, 10, 12
                }),
                new IPv4OptionLooseSourceRecordRoute(
                    optionType: IPv4OptionType.NotCopied_Control_LooseSourceRouting,
                    pointer: 4,
                    new ReadOnlyMemory<IPAddress>(new IPAddress[]
                    {
                        new IPAddress(new byte[] { 192, 168, 10, 10 }),
                        new IPAddress(new byte[] { 192, 168, 10, 11 }),
                        new IPAddress(new byte[] { 192, 168, 10, 12 })
                    }))
            }
        };

    [Theory(DisplayName = "IPv4OptionLooseSourceRecordRoute :: FromReadOnlyMemory")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, IPv4OptionLooseSourceRecordRoute expected)
    {
        var actual = IPv4OptionLooseSourceRecordRoute.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "IPv4OptionLooseSourceRecordRoute :: Read [BinaryReader]")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ReadBinaryReaderTests(ReadOnlyMemory<byte> memory, IPv4OptionLooseSourceRecordRoute expected)
    {
        // Arrange
        using var binaryReader = new BinaryReader(new MemoryStream(memory.ToArray()));

        // Act
        var actual = IPv4OptionLooseSourceRecordRoute.Read(binaryReader);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "IPv4OptionLooseSourceRecordRoute :: Read [Stream]")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ReadStreamTests(ReadOnlyMemory<byte> memory, IPv4OptionLooseSourceRecordRoute expected)
    {
        // Arrange
        using var memoryStream = new MemoryStream(memory.ToArray());

        // Act
        var actual = IPv4OptionLooseSourceRecordRoute.Read(memoryStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "IPv4OptionLooseSourceRecordRoute :: ReadAsync")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal async Task ReadAsyncTests(ReadOnlyMemory<byte> memory, IPv4OptionLooseSourceRecordRoute expected)
    {
        // Arrange
        using var memoryStream = new MemoryStream(memory.ToArray());

        // Act
        var actual = await IPv4OptionLooseSourceRecordRoute.ReadAsync(memoryStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "IPv4OptionLooseSourceRecordRoute :: ToReadOnlyMemory")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, IPv4OptionLooseSourceRecordRoute option)
    {
        var actual = option.ToReadOnlyMemory();
        Assert.Equal(expected.ToArray(), actual.ToArray());
    }

    [Theory(DisplayName = "IPv4OptionLooseSourceRecordRoute :: Write [BinaryWriter]")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void WriteBinaryWriterTests(ReadOnlyMemory<byte> expected, IPv4OptionLooseSourceRecordRoute option)
    {
        // Arrange
        using var memoryStream = new MemoryStream();
        using var binaryWriter = new BinaryWriter(memoryStream);

        // Act
        ((IBinarySerializable)option).Write(binaryWriter);

        // Assert
        Assert.Equal(expected.ToArray(), memoryStream.ToArray());
    }

    [Theory(DisplayName = "IPv4OptionLooseSourceRecordRoute :: Write [Stream]")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void WriteStreamTests(ReadOnlyMemory<byte> expected, IPv4OptionLooseSourceRecordRoute option)
    {
        // Arrange
        using var memoryStream = new MemoryStream();

        // Act
        ((IBinarySerializable)option).Write(memoryStream);

        // Assert
        Assert.Equal(expected.ToArray(), memoryStream.ToArray());
    }

    [Theory(DisplayName = "IPv4OptionLooseSourceRecordRoute :: WriteAsync")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal async Task WriteAsyncTests(ReadOnlyMemory<byte> expected, IPv4OptionLooseSourceRecordRoute option)
    {
        // Arrange
        using var memoryStream = new MemoryStream();

        // Act
        await ((IBinarySerializable)option).WriteAsync(memoryStream);

        // Assert
        Assert.Equal(expected.ToArray(), memoryStream.ToArray());
    }
}
