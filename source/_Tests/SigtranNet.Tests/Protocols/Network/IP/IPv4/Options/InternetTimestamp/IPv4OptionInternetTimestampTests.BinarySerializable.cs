/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;
using SigtranNet.Protocols.Network.IP.IPv4.Options;
using SigtranNet.Protocols.Network.IP.IPv4.Options.InternetTimestamp;
using System.Net;

namespace SigtranNet.Tests.Protocols.Network.IP.IPv4.Options.InternetTimestamp;

public class IPv4OptionInternetTimestampTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    (byte)IPv4OptionType.NotCopied_Debugging_InternetTimestamp, // Option Type
                    12,                                                         // Length
                    5,                                                          // Pointer
                    0,                                                          // Overflow / Flag
                    /* Timespan */
                    20000 >> 24,
                    (20000 & 0x00FF0000) >> 16,
                    (20000 & 0x0000FF00) >> 8,
                    20000 & 0x000000FF,
                    /* Timespan */
                    25000 >> 24,
                    (25000 & 0x00FF0000) >> 16,
                    (25000 & 0x0000FF00) >> 8,
                    25000 & 0x000000FF
                }),
                new IPv4OptionInternetTimestamp(
                    IPv4OptionType.NotCopied_Debugging_InternetTimestamp,
                    12,
                    5,
                    0,
                    IPv4OptionInternetTimestampFlags.TimestampsOnly,
                    new ReadOnlyMemory<IPv4OptionInternetTimestampAddressPair>(new IPv4OptionInternetTimestampAddressPair[]
                    {
                        new(20000),
                        new(25000)
                    }))
            },
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    (byte)IPv4OptionType.Copied_Debugging_InternetTimestamp,    // Option Type
                    20,                                                         // Length
                    5,                                                          // Pointer
                    (2 << 4) + 1,                                               // Overflow / Flag
                    /* Address */
                    192, 168, 10, 10,
                    /* Timestamp */
                    21000 >> 24,
                    (21000 & 0x00FF0000) >> 16,
                    (21000 & 0x0000FF00) >> 8,
                    21000 & 0x000000FF,
                    /* Address */
                    192, 168, 10, 11,
                    /* Timestamp */
                    26000 >> 24,
                    (26000 & 0x00FF0000) >> 16,
                    (26000 & 0x0000FF00) >> 8,
                    26000 & 0x000000FF
                }),
                new IPv4OptionInternetTimestamp(
                    IPv4OptionType.Copied_Debugging_InternetTimestamp,
                    20,
                    5,
                    2,
                    IPv4OptionInternetTimestampFlags.InternetAddressPreceded,
                    new ReadOnlyMemory<IPv4OptionInternetTimestampAddressPair>(new IPv4OptionInternetTimestampAddressPair[]
                    {
                        new(new IPAddress(new byte[] { 192, 168, 10, 10 }), 21000),
                        new(new IPAddress(new byte[] { 192, 168, 10, 11 }), 26000)
                    }))
            }
        };

    [Theory(DisplayName = "IPv4OptionInternetTimestamp :: FromReadOnlyMemory")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, IPv4OptionInternetTimestamp expected)
    {
        var actual = IPv4OptionInternetTimestamp.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "IPv4OptionInternetTimestamp :: Read [BinaryReader]")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ReadBinaryReaderTests(ReadOnlyMemory<byte> memory, IPv4OptionInternetTimestamp expected)
    {
        // Arrange
        using var memoryStream = new MemoryStream(memory.ToArray());
        using var binaryReader = new BinaryReader(memoryStream);

        // Act
        var actual = IPv4OptionInternetTimestamp.Read(binaryReader);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "IPv4OptionInternetTimestamp :: Read [Stream]")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ReadStreamTests(ReadOnlyMemory<byte> memory, IPv4OptionInternetTimestamp expected)
    {
        // Arrange
        using var memoryStream = new MemoryStream(memory.ToArray());

        // Act
        var actual = IPv4OptionInternetTimestamp.Read(memoryStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "IPv4OptionInternetTimestamp :: ReadAsync")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal async Task ReadStreamAsyncTests(ReadOnlyMemory<byte> memory, IPv4OptionInternetTimestamp expected)
    {
        // Arrange
        using var memoryStream = new MemoryStream(memory.ToArray());

        // Act
        var actual = await IPv4OptionInternetTimestamp.ReadAsync(memoryStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "IPv4OptionInternetTimestamp :: ToReadOnlyMemory")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, IPv4OptionInternetTimestamp option)
    {
        var actual = option.ToReadOnlyMemory();
        Assert.Equal(expected.ToArray(), actual.ToArray());
    }

    [Theory(DisplayName = "IPv4OptionInternetTimestamp :: Write [BinaryWriter]")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void WriteBinaryWriterTests(ReadOnlyMemory<byte> expected, IPv4OptionInternetTimestamp option)
    {
        // Arrange
        using var memoryStream = new MemoryStream();
        using var binaryWriter = new BinaryWriter(memoryStream);

        // Act
        ((IBinarySerializable)option).Write(binaryWriter);

        // Assert
        Assert.Equal(expected.ToArray(), memoryStream.ToArray());
    }

    [Theory(DisplayName = "IPv4OptionInternetTimestamp :: Write [Stream]")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void WriteStreamTests(ReadOnlyMemory<byte> expected, IPv4OptionInternetTimestamp option)
    {
        // Arrange
        using var memoryStream = new MemoryStream();

        // Act
        ((IBinarySerializable)option).Write(memoryStream);

        // Assert
        Assert.Equal(expected.ToArray(), memoryStream.ToArray());
    }

    [Theory(DisplayName = "IPv4OptionInternetTimestamp :: WriteAsync")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal async Task WriteAsyncTests(ReadOnlyMemory<byte> expected, IPv4OptionInternetTimestamp option)
    {
        // Arrange
        using var memoryStream = new MemoryStream();

        // Act
        await ((IBinarySerializable)option).WriteAsync(memoryStream);

        // Assert
        Assert.Equal(expected.ToArray(), memoryStream.ToArray());
    }
}
