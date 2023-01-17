/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Sctp;
using SigtranNet.Protocols.Sctp.Chunks;
using SigtranNet.Protocols.Sctp.Chunks.CookieEcho;
using SigtranNet.Protocols.Sctp.Chunks.PayloadData;

namespace SigtranNet.Tests.Protocols.Sctp;

public partial class SctpPacketTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    /* Common Header */
                    // Source Port Number
                    20000 >> 8,
                    20000 & 0xFF,
                    // Destination Port Number
                    30000 >> 8,
                    30000 & 0xFF,
                    // Verification Tag
                    0, 0, 0, 123,
                    // Checksum
                    1355769883 >> 24,
                    (1355769883 & 0x00FF0000) >> 16,
                    (1355769883 & 0x0000FF00) >> 8,
                    1355769883 & 0xFF,

                    /* Chunks */
                    // COOKIE ECHO
                    (byte)SctpChunkType.StateCookie,
                    0,
                    0, 8,
                    1, 2, 3, 4,

                    // DATA
                    (byte)SctpChunkType.PayloadData,
                    (byte)(SctpPayloadDataFlags.Beginning | SctpPayloadDataFlags.Ending),
                    0, 28,
                    0, 0, 0, 2,
                    12345 >> 8,
                    12345 & 0xFF,
                    0, 5,
                    (byte)((uint)SctpPayloadProtocolIdentifier.M3UA >> 24),
                    (byte)(((uint)SctpPayloadProtocolIdentifier.M3UA & 0x00FF0000) >> 16),
                    (byte)(((uint)SctpPayloadProtocolIdentifier.M3UA & 0x0000FF00) >> 8),
                    (byte)((uint)SctpPayloadProtocolIdentifier.M3UA & 0xFF),
                    1, 2, 3, 4,
                    4, 3, 2, 1,
                    1, 2, 3, 4,
                }),
                new SctpPacket(
                    new SctpCommonHeader(20000, 30000, 123, 1355769883),
                    new ReadOnlyMemory<ISctpChunk>(
                        new ISctpChunk[]
                        {
                            new SctpCookieEcho(new ReadOnlyMemory<byte>(new byte[] { 1, 2, 3, 4 })),
                            new SctpPayloadData(
                                SctpPayloadDataFlags.Beginning | SctpPayloadDataFlags.Ending,
                                2,
                                12345,
                                5,
                                SctpPayloadProtocolIdentifier.M3UA,
                                new ReadOnlyMemory<byte>(new byte[]
                                {
                                    1, 2, 3, 4,
                                    4, 3, 2, 1,
                                    1, 2, 3, 4,
                                }))
                        }))
            },
        };

    [Theory(DisplayName = $"{nameof(SctpPacket)} :: {nameof(SctpPacket.FromReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, SctpPacket expected)
    {
        var actual = SctpPacket.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = $"{nameof(SctpPacket)} :: {nameof(SctpPacket.ToReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, SctpPacket packet)
    {
        var actual = packet.ToReadOnlyMemory();
        Assert.True(expected.Span.SequenceEqual(actual.Span));
    }
}
