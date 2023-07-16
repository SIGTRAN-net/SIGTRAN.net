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

using SigtranNet.Protocols.Transport.Sctp.Packets.Header;

namespace SigtranNet.Protocols.Transport.Sctp.Tests.Packets.Header;

public class SctpCommonHeaderTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    2000 >> 8, 2000 & 0x00FF,   // Source Port Number
                    2300 >> 8, 2300 & 0x00FF,   // Destination Port Number
                    /* Verification Tag */
                    (byte)(123456u >> 24),
                    (byte)((123456u & 0x00FF0000) >> 16),
                    (byte)((123456u & 0x0000FF00) >> 8),
                    (byte)(123456u & 0x000000FF),
                    /* Checksum */
                    (byte)(654321u >> 24),
                    (byte)((654321u & 0x00FF0000) >> 16),
                    (byte)((654321u & 0x0000FF00) >> 8),
                    (byte)(654321u & 0x000000FF)
                }),
                new SctpCommonHeader(
                    2000,
                    2300,
                    123456u,
                    654321u)
            }
        };

    [Theory(DisplayName = $"{nameof(SctpCommonHeader)} :: {nameof(SctpCommonHeader.FromReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, SctpCommonHeader expected)
    {
        var actual = SctpCommonHeader.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = $"{nameof(SctpCommonHeader)} :: {nameof(SctpCommonHeader.Read)} [BinaryReader]")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ReadBinaryReaderTests(ReadOnlyMemory<byte> memory, SctpCommonHeader expected)
    {
        // Arrange
        using var stream = new MemoryStream(memory.ToArray());
        using var binaryReader = new BinaryReader(stream);

        // Act
        var actual = SctpCommonHeader.Read(binaryReader);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = $"{nameof(SctpCommonHeader)} :: {nameof(SctpCommonHeader.Read)} [Stream]")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ReadStreamTests(ReadOnlyMemory<byte> memory, SctpCommonHeader expected)
    {
        // Arrange
        using var stream = new MemoryStream(memory.ToArray());

        // Act
        var actual = SctpCommonHeader.Read(stream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = $"{nameof(SctpCommonHeader)} :: {nameof(SctpCommonHeader.ReadAsync)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal async Task ReadAsyncTests(ReadOnlyMemory<byte> memory, SctpCommonHeader expected)
    {
        // Arrange
        using var stream = new MemoryStream(memory.ToArray());

        // Act
        var actual = await SctpCommonHeader.ReadAsync(stream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = $"{nameof(SctpCommonHeader)} :: {nameof(SctpCommonHeader.ToReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, SctpCommonHeader header)
    {
        var actual = header.ToReadOnlyMemory();
        Assert.Equal(expected.ToArray(), actual.ToArray());
    }

    [Theory(DisplayName = $"{nameof(SctpCommonHeader)} :: {nameof(SctpCommonHeader.Write)} [BinaryWriter]")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void WriteBinaryWriterTests(ReadOnlyMemory<byte> expected, SctpCommonHeader header)
    {
        // Arrange
        using var stream = new MemoryStream();
        using var binaryWriter = new BinaryWriter(stream);

        // Act
        header.Write(binaryWriter);

        // Assert
        Assert.Equal(expected.ToArray(), stream.ToArray());
    }

    [Theory(DisplayName = $"{nameof(SctpCommonHeader)} :: {nameof(SctpCommonHeader.Write)} [Stream]")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void WriteStreamTests(ReadOnlyMemory<byte> expected, SctpCommonHeader header)
    {
        // Arrange
        using var stream = new MemoryStream();

        // Act
        header.Write(stream);

        // Assert
        Assert.Equal(expected.ToArray(), stream.ToArray());
    }

    [Theory(DisplayName = $"{nameof(SctpCommonHeader)} :: {nameof(SctpCommonHeader.WriteAsync)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal async Task WriteAsyncTests(ReadOnlyMemory<byte> expected, SctpCommonHeader header)
    {
        // Arrange
        using var stream = new MemoryStream();

        // Act
        await header.WriteAsync(stream);

        // Assert
        Assert.Equal(expected.ToArray(), stream.ToArray());
    }
}
