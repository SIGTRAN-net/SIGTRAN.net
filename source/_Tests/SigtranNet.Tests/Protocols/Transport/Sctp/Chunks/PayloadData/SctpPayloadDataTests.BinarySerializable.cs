/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;
using SigtranNet.Protocols.Transport.Sctp.Chunks;
using SigtranNet.Protocols.Transport.Sctp.Chunks.PayloadData;

namespace SigtranNet.Tests.Protocols.Transport.Sctp.Chunks.PayloadData;

public class SctpPayloadDataTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    (byte)SctpChunkType.PayloadData,                                        // Chunk Type
                    (byte)(SctpPayloadDataFlags.Beginning | SctpPayloadDataFlags.Ending),   // Chunk Flags
                    22 >> 8, 22 & 0x00FF,                                                   // Chunk Length
                    /* TSN */
                    (byte)(2u >> 24),
                    (byte)((2u & 0x00FF0000) >> 16),
                    (byte)((2u & 0x0000FF00) >> 8),
                    (byte)(2u & 0x000000FF),
                    /* Stream Identifier */
                    1 >> 8, 1 & 0x00FF,
                    /* Stream Sequence Number */
                    35 >> 8, 35 & 0x00FF,
                    /* Payload Protocol Identifier */
                    (byte)((uint)SctpPayloadProtocolIdentifier._3GPP_M3AP >> 24),
                    (byte)(((uint)SctpPayloadProtocolIdentifier._3GPP_M3AP & 0x00FF0000) >> 16),
                    (byte)(((uint)SctpPayloadProtocolIdentifier._3GPP_M3AP & 0x0000FF00) >> 8),
                    (byte)((uint)SctpPayloadProtocolIdentifier._3GPP_M3AP & 0x000000FF),
                    /* User Data */
                    1, 2, 3, 4,
                    5, 6, 0, 0
                }),
                new SctpPayloadData(
                    flags: SctpPayloadDataFlags.Beginning | SctpPayloadDataFlags.Ending,
                    transmissionSequenceNumber: 2u,
                    streamIdentifier: 1,
                    streamSequenceNumber: 35,
                    payloadProtocolIdentifier: SctpPayloadProtocolIdentifier._3GPP_M3AP,
                    userData: new ReadOnlyMemory<byte>(new byte[] { 1, 2, 3, 4, 5, 6 }))
            }
        };

    [Theory(DisplayName = $"{nameof(SctpPayloadData)} :: {nameof(SctpPayloadData.FromReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemory(ReadOnlyMemory<byte> memory, SctpPayloadData expected)
    {
        var actual = SctpPayloadData.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = $"{nameof(SctpPayloadData)} :: {nameof(SctpPayloadData.ToReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ReadBinaryReaderTests(ReadOnlyMemory<byte> memory, SctpPayloadData expected)
    {
        // Arrange
        using var memoryStream = new MemoryStream(memory.ToArray());
        using var binaryReader = new BinaryReader(memoryStream);

        // Act
        var actual = SctpPayloadData.Read(binaryReader);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "SctpPayloadData :: Read [Stream]")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ReadStreamTests(ReadOnlyMemory<byte> memory, SctpPayloadData expected)
    {
        // Arrange
        using var memoryStream = new MemoryStream(memory.ToArray());

        // Act
        var actual = SctpPayloadData.Read(memoryStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "SctpPayloadData :: ReadAsync")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal async Task ReadAsyncTests(ReadOnlyMemory<byte> memory, SctpPayloadData expected)
    {
        // Arrange
        using var memoryStream = new MemoryStream(memory.ToArray());

        // Act
        var actual = await SctpPayloadData.ReadAsync(memoryStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "SctpPayloadData :: ToReadOnlyMemory")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemory(ReadOnlyMemory<byte> expected, SctpPayloadData chunk)
    {
        var actual = chunk.ToReadOnlyMemory();
        Assert.Equal(expected.ToArray(), actual.ToArray());
    }

    [Theory(DisplayName = "SctpPayloadData :: Write [BinaryWriter]")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void WriteBinaryWriterTests(ReadOnlyMemory<byte> expected, SctpPayloadData chunk)
    {
        // Arrange
        using var memoryStream = new MemoryStream();
        using var binaryWriter = new BinaryWriter(memoryStream);

        // Act
        ((IBinarySerializable)chunk).Write(binaryWriter);

        // Assert
        Assert.Equal(expected.ToArray(), memoryStream.ToArray());
    }

    [Theory(DisplayName = "SctpPayloadData :: Write [Stream]")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void WriteStreamTests(ReadOnlyMemory<byte> expected, SctpPayloadData chunk)
    {
        // Arrange
        using var memoryStream = new MemoryStream();

        // Act
        ((IBinarySerializable)chunk).Write(memoryStream);

        // Assert
        Assert.Equal(expected.ToArray(), memoryStream.ToArray());
    }

    [Theory(DisplayName = "SctpPayloadData :: WriteAsync")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal async Task WriteAsyncTests(ReadOnlyMemory<byte> expected, SctpPayloadData chunk)
    {
        // Arrange
        using var memoryStream = new MemoryStream();

        // Act
        await ((IBinarySerializable)chunk).WriteAsync(memoryStream);

        // Assert
        Assert.Equal(expected.ToArray(), memoryStream.ToArray());
    }
}
