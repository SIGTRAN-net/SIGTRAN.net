/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Network.Icmp.Messages;
using SigtranNet.Protocols.Network.Icmp.Messages.Information;

namespace SigtranNet.Tests.Protocols.Network.Icmp.Messages.Information;

public partial class IcmpInformationMessageTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    (byte)IcmpMessageType.InformationRequest,
                    0,
                    61674 >> 8, 61674 & 0xFF,
                    20 >> 8, 20 & 0xFF,
                    1 >> 8, 1 & 0xFF,
                }),
                new IcmpInformationMessage(
                    false,
                    20,
                    1)
            },
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    (byte)IcmpMessageType.InformationReply,
                    0,
                    61418 >> 8, 61418 & 0xFF,
                    20 >> 8, 20 & 0xFF,
                    1 >> 8, 1 & 0xFF,
                }),
                new IcmpInformationMessage(
                    true,
                    20,
                    1)
            }
        };

    [Theory(DisplayName = $"{nameof(IcmpInformationMessage)} :: {nameof(IcmpInformationMessage.FromReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, IcmpInformationMessage expected)
    {
        var actual = IcmpInformationMessage.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = $"{nameof(IcmpInformationMessage)} :: {nameof(IcmpInformationMessage.ToReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, IcmpInformationMessage message)
    {
        var actual = message.ToReadOnlyMemory();
        Assert.True(expected.Span.SequenceEqual(actual.Span));
    }
}
