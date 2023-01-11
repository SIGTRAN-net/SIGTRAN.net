/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.IP.IPv4.Options;
using SigtranNet.Protocols.IP.IPv4.Options.StreamIdentifier;

namespace SigtranNet.Tests.Protocols.IP.IPv4.Options.StreamIdentifier;

public class IPv4OptionStreamIdentifierTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    (byte)IPv4OptionType.NotCopied_Control_StreamIdentifier,        // Option Type
                    4,                                                              // Length
                    102 >> 8, 102 & 0x00FF                                          // Stream Identifier
                }),
                new IPv4OptionStreamIdentifier(
                    IPv4OptionType.NotCopied_Control_StreamIdentifier,
                    102)
            },
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    (byte)IPv4OptionType.Copied_Control_StreamIdentifier,           // Option Type
                    4,                                                              // Length
                    105 >> 8, 105 & 0x00FF                                          // Stream Identifier
                }),
                new IPv4OptionStreamIdentifier(
                    IPv4OptionType.Copied_Control_StreamIdentifier,
                    105)
            }
        };

    [Theory(DisplayName = "IPv4OptionStreamIdentifier :: FromReadOnlyMemory")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, IPv4OptionStreamIdentifier expected)
    {
        var actual = IPv4OptionStreamIdentifier.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "IPv4OptionStreamIdentifier :: ToReadOnlyMemory")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, IPv4OptionStreamIdentifier option)
    {
        var actual = option.ToReadOnlyMemory();
        Assert.Equal(expected.ToArray(), actual.ToArray());
    }
}
