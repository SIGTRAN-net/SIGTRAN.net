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

using SigtranNet.Protocols.Transport.Sctp.Packets;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.CookieEcho;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.PayloadData;
using SigtranNet.Protocols.Transport.Sctp.Packets.Header;

namespace SigtranNet.Protocols.Transport.Sctp.Tests.Packets;

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
