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

using SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages;
using SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.Timestamp;

namespace SigtranNet.Protocols.Network.IP.IPv4.Icmp.Tests.Messages.Timestamp;

public partial class IcmpTimestampMessageTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            [
                new ReadOnlyMemory<byte>(
                [
                    (byte)IcmpMessageType.Timestamp,
                    0,
                    32186 >> 8, 32186 & 0xFF,
                    0, 20,
                    0, 1,
                    5000 >> 24, (5000 & 0x00FF0000) >> 16, (5000 & 0x0000FF00) >> 8, 5000 & 0xFF,
                    10000 >> 24, (10000 & 0x00FF0000) >> 16, (10000 & 0x0000FF00) >> 8, 10000 & 0xFF,
                    15000 >> 24, (15000 & 0x00FF0000) >> 16, (15000 & 0x0000FF00) >> 8, 15000 & 0xFF,
                ]),
                new IcmpTimestampMessage(
                    false,
                    20,
                    1,
                    5000,
                    10000,
                    15000)
            ],
            new object?[]
            {
                new ReadOnlyMemory<byte>(
                [
                    (byte)IcmpMessageType.TimestampReply,
                    0,
                    31930 >> 8, 31930 & 0xFF,
                    0, 20,
                    0, 1,
                    5000 >> 24, (5000 & 0x00FF0000) >> 16, (5000 & 0x0000FF00) >> 8, 5000 & 0xFF,
                    10000 >> 24, (10000 & 0x00FF0000) >> 16, (10000 & 0x0000FF00) >> 8, 10000 & 0xFF,
                    15000 >> 24, (15000 & 0x00FF0000) >> 16, (15000 & 0x0000FF00) >> 8, 15000 & 0xFF,
                ]),
                new IcmpTimestampMessage(
                    true,
                    20,
                    1,
                    5000,
                    10000,
                    15000)
            }
        };

    [Theory(DisplayName = $"{nameof(IcmpTimestampMessage)} :: {nameof(IcmpTimestampMessage.FromReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, IcmpTimestampMessage expected)
    {
        var actual = IcmpTimestampMessage.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = $"{nameof(IcmpTimestampMessage)} :: {nameof(IcmpTimestampMessage.ToReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, IcmpTimestampMessage message)
    {
        var actual = message.ToReadOnlyMemory();
        Assert.True(expected.Span.SequenceEqual(actual.Span));
    }
}
