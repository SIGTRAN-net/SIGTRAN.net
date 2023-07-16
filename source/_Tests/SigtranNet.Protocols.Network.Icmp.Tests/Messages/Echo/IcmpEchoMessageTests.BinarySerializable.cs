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
using SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.Echo;

namespace SigtranNet.Protocols.Network.IP.IPv4.Icmp.Tests.Messages.Echo;

public partial class IcmpEchoMessageTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            [
                new ReadOnlyMemory<byte>(
                [
                    (byte)IcmpMessageType.Echo,
                    0,
                    59360 >> 8, 59360 & 0xFF,
                    0, 10,
                    0, 1,
                    1, 2, 3, 4,
                    5, 6, 7, 8,
                ]),
                new IcmpEchoMessage(
                    false,
                    10,
                    1,
                    new ReadOnlyMemory<byte>(
                    [
                        1, 2, 3, 4,
                        5, 6, 7, 8,
                    ]))
            ],
            new object?[]
            {
                new ReadOnlyMemory<byte>(
                [
                    (byte)IcmpMessageType.EchoReply,
                    0,
                    61408 >> 8, 61408 & 0xFF,
                    0, 10,
                    0, 1,
                    1, 2, 3, 4,
                    5, 6, 7, 8,
                ]),
                new IcmpEchoMessage(
                    true,
                    10,
                    1,
                    new ReadOnlyMemory<byte>(
                    [
                        1, 2, 3, 4,
                        5, 6, 7, 8,
                    ]))
            }
        };

    [Theory(DisplayName = $"{nameof(IcmpEchoMessage)} :: {nameof(IcmpEchoMessage.FromReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, IcmpEchoMessage expected)
    {
        var actual = IcmpEchoMessage.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = $"{nameof(IcmpEchoMessage)} :: {nameof(IcmpEchoMessage.ToReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, IcmpEchoMessage message)
    {
        var actual = message.ToReadOnlyMemory();
        Assert.True(expected.Span.SequenceEqual(actual.Span));
    }
}
