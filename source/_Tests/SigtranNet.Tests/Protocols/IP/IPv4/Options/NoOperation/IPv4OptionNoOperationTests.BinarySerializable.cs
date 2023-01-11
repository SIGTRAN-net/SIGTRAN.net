/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.IP.IPv4.Options;
using SigtranNet.Protocols.IP.IPv4.Options.Exceptions;
using SigtranNet.Protocols.IP.IPv4.Options.NoOperation;

namespace SigtranNet.Tests.Protocols.IP.IPv4.Options.NoOperation;

public class IPv4OptionNoOperationTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[] { 1 }),
                new IPv4OptionNoOperation()
            },
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[] { 0b1_00_00001 }),
                new IPv4OptionNoOperation(IPv4OptionType.Copied_Control_NoOperation)
            }
        };

    [Theory(DisplayName = "IPv4OptionNoOperation :: FromReadOnlyMemory")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, IPv4OptionNoOperation expected)
    {
        var actual = IPv4OptionNoOperation.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "IPv4OptionNoOperation :: FromReadOnlyMemory [throws]")]
    public void FromReadOnlyMemoryTestThrows()
    {
        var memory = new ReadOnlyMemory<byte>(new byte[] { 2 });
        Assert.Throws<IPv4OptionInvalidTypeException>(() => IPv4OptionNoOperation.FromReadOnlyMemory(memory));
    }

    [Theory(DisplayName = "IPv4OptionNoOperation :: Read [BinaryReader]")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ReadBinaryReaderTests(ReadOnlyMemory<byte> memory, IPv4OptionNoOperation expected)
    {
        // Arrange
        using var memoryStream = new MemoryStream(memory.ToArray());
        using var binaryReader = new BinaryReader(memoryStream);

        // Act
        var actual = IPv4OptionNoOperation.Read(binaryReader);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "IPv4OptionNoOperation :: Read [Stream]")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ReadStreamTests(ReadOnlyMemory<byte> memory, IPv4OptionNoOperation expected)
    {
        // Arrange
        using var memoryStream = new MemoryStream(memory.ToArray());

        // Act
        var actual = IPv4OptionNoOperation.Read(memoryStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "IPv4OptionNoOperation :: ReadAsync")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal async Task ReadAsyncTests(ReadOnlyMemory<byte> memory, IPv4OptionNoOperation expected)
    {
        // Arrange
        using var memoryStream = new MemoryStream(memory.ToArray());

        // Act
        var actual = await IPv4OptionNoOperation.ReadAsync(memoryStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "IPv4OptionNoOperation :: ToReadOnlyMemory")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTest(ReadOnlyMemory<byte> expected, IPv4OptionNoOperation option)
    {
        var actual = option.ToReadOnlyMemory();
        Assert.Equal(expected.ToArray(), actual.ToArray());
    }

    [Theory(DisplayName = "IPv4OptionNoOperation :: Write [BinaryWriter]")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void WriteBinaryWriterTests(ReadOnlyMemory<byte> expected, IPv4OptionNoOperation option)
    {
        // Arrange
        using var memoryStream = new MemoryStream();
        using var binaryWriter = new BinaryWriter(memoryStream);

        // Act
        option.Write(binaryWriter);

        // Assert
        Assert.Equal(expected.ToArray(), memoryStream.ToArray());
    }

    [Theory(DisplayName = "IPv4OptionNoOperation :: Write [Stream]")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void WriteStreamTests(ReadOnlyMemory<byte> expected, IPv4OptionNoOperation option)
    {
        // Arrange
        using var memoryStream = new MemoryStream();

        // Act
        option.Write(memoryStream);

        // Assert
        Assert.Equal(expected.ToArray(), memoryStream.ToArray());
    }

    [Theory(DisplayName = "IPv4OptionNoOperation :: WriteAsync")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal async Task WriteAsyncTests(ReadOnlyMemory<byte> expected, IPv4OptionNoOperation option)
    {
        // Arrange
        using var memoryStream = new MemoryStream();

        // Act
        await option.WriteAsync(memoryStream);

        // Assert
        Assert.Equal(expected.ToArray(), memoryStream.ToArray());
    }
}
