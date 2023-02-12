/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Network.Icmp.Messages;
using SigtranNet.Protocols.Network.Icmp.Messages.Echo;

namespace SigtranNet.Tests.Protocols.Network.Icmp.Messages.Echo;

public partial class IcmpEchoMessageTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    (byte)IcmpMessageType.Echo,
                    0,
                    59360 >> 8, 59360 & 0xFF,
                    0, 10,
                    0, 1,
                    1, 2, 3, 4,
                    5, 6, 7, 8,
                }),
                new IcmpEchoMessage(
                    false,
                    10,
                    1,
                    new ReadOnlyMemory<byte>(new byte[]
                    {
                        1, 2, 3, 4,
                        5, 6, 7, 8,
                    }))
            },
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    (byte)IcmpMessageType.EchoReply,
                    0,
                    61408 >> 8, 61408 & 0xFF,
                    0, 10,
                    0, 1,
                    1, 2, 3, 4,
                    5, 6, 7, 8,
                }),
                new IcmpEchoMessage(
                    true,
                    10,
                    1,
                    new ReadOnlyMemory<byte>(new byte[]
                    {
                        1, 2, 3, 4,
                        5, 6, 7, 8,
                    }))
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
