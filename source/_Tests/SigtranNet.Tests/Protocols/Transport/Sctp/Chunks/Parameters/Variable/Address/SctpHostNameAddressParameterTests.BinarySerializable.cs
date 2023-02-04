/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Variable.Address;
using System.Text;

namespace SigtranNet.Tests.Protocols.Transport.Sctp.Chunks.Parameters.Variable.Address;

public partial class SctpHostNameAddressParameterTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    /* Parameter Header */
                    (ushort)SctpChunkParameterType.HostNameAddress >> 8,
                    (ushort)SctpChunkParameterType.HostNameAddress & 0x00FF,
                    18 >> 8, 18 & 0x00FF, // Length
                }.Concat(Encoding.ASCII.GetBytes("sctp.test.net\0")).ToArray()),
                new SctpHostNameAddressParameter("sctp.test.net")
            }
        };

    [Theory(DisplayName = "SctpHostNameAddressParameter :: FromReadOnlyMemory")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, SctpHostNameAddressParameter expected)
    {
        var actual = SctpHostNameAddressParameter.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "SctpHostNameAddressParameter :: Read [BinaryReader]")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ReadBinaryReaderTests(ReadOnlyMemory<byte> memory, SctpHostNameAddressParameter expected)
    {
        // Arrange
        using var memoryStream = new MemoryStream(memory.ToArray());
        using var binaryReader = new BinaryReader(memoryStream);

        // Act
        var actual = SctpHostNameAddressParameter.Read(binaryReader);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "SctpHostNameAddressParameter :: Read [Stream]")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ReadStreamTests(ReadOnlyMemory<byte> memory, SctpHostNameAddressParameter expected)
    {
        // Arrange
        using var memoryStream = new MemoryStream(memory.ToArray());

        // Act
        var actual = SctpHostNameAddressParameter.Read(memoryStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "SctpHostNameAddressParameter :: ReadAsync")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal async Task ReadAsyncTests(ReadOnlyMemory<byte> memory, SctpHostNameAddressParameter expected)
    {
        // Arrange
        using var memoryStream = new MemoryStream(memory.ToArray());

        // Act
        var actual = await SctpHostNameAddressParameter.ReadAsync(memoryStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "SctpHostNameAddressParameter :: ToReadOnlyMemory")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, SctpHostNameAddressParameter parameter)
    {
        var actual = parameter.ToReadOnlyMemory();
        Assert.Equal(expected.ToArray(), actual.ToArray());
    }

    [Theory(DisplayName = "SctpHostNameAddressParameter :: Write [BinaryWriter]")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void WriteBinaryWriterTests(ReadOnlyMemory<byte> expected, SctpHostNameAddressParameter parameter)
    {
        // Arrange
        using var memoryStream = new MemoryStream();
        using var binaryWriter = new BinaryWriter(memoryStream);

        // Act
        ((IBinarySerializable)parameter).Write(binaryWriter);

        // Assert
        Assert.Equal(expected.ToArray(), memoryStream.ToArray());
    }

    [Theory(DisplayName = "SctpHostNameAddressParameter :: Write [Stream]")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void WriteStreamTests(ReadOnlyMemory<byte> expected, SctpHostNameAddressParameter parameter)
    {
        // Arrange
        using var memoryStream = new MemoryStream();

        // Act
        ((IBinarySerializable)parameter).Write(memoryStream);

        // Assert
        Assert.Equal(expected.ToArray(), memoryStream.ToArray());
    }

    [Theory(DisplayName = "SctpHostNameAddressParameter :: WriteAsync")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal async Task WriteAsyncTests(ReadOnlyMemory<byte> expected, SctpHostNameAddressParameter parameter)
    {
        // Arrange
        using var memoryStream = new MemoryStream();

        // Act
        await ((IBinarySerializable)parameter).WriteAsync(memoryStream);

        // Assert
        Assert.Equal(expected.ToArray(), memoryStream.ToArray());
    }
}
