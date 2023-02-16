/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Network.Icmp.Messages;
using SigtranNet.Protocols.Network.Icmp.Messages.Timestamp;

namespace SigtranNet.Tests.Protocols.Network.Icmp.Messages.Timestamp;

public partial class IcmpTimestampMessageTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    (byte)IcmpMessageType.Timestamp,
                    0,
                    32186 >> 8, 32186 & 0xFF,
                    0, 20,
                    0, 1,
                    5000 >> 24, (5000 & 0x00FF0000) >> 16, (5000 & 0x0000FF00) >> 8, 5000 & 0xFF,
                    10000 >> 24, (10000 & 0x00FF0000) >> 16, (10000 & 0x0000FF00) >> 8, 10000 & 0xFF,
                    15000 >> 24, (15000 & 0x00FF0000) >> 16, (15000 & 0x0000FF00) >> 8, 15000 & 0xFF,
                }),
                new IcmpTimestampMessage(
                    false,
                    20,
                    1,
                    5000,
                    10000,
                    15000)
            },
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    (byte)IcmpMessageType.TimestampReply,
                    0,
                    31930 >> 8, 31930 & 0xFF,
                    0, 20,
                    0, 1,
                    5000 >> 24, (5000 & 0x00FF0000) >> 16, (5000 & 0x0000FF00) >> 8, 5000 & 0xFF,
                    10000 >> 24, (10000 & 0x00FF0000) >> 16, (10000 & 0x0000FF00) >> 8, 10000 & 0xFF,
                    15000 >> 24, (15000 & 0x00FF0000) >> 16, (15000 & 0x0000FF00) >> 8, 15000 & 0xFF,
                }),
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
