/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Network.IP.IPv4.Options;
using SigtranNet.Protocols.Network.IP.IPv4.Options.LooseSourceRouting;
using System.Net;

namespace SigtranNet.Tests.Protocols.Network.IP.IPv4.Options.LooseSourceRouting;

public partial class IPv4OptionLooseSourceRecordRouteTest
{
    public static readonly IEnumerable<object?[]> EqualsParameters =
        new[]
        {
            new object?[]
            {
                new IPv4OptionLooseSourceRecordRoute(
                    IPv4OptionType.NotCopied_Control_LooseSourceRouting,
                    4,
                    new ReadOnlyMemory<IPAddress>(new IPAddress[]
                    {
                        new IPAddress(new byte[] { 192, 168, 1, 20 })
                    })),
                null,
                false
            },
            new object?[]
            {
                new IPv4OptionLooseSourceRecordRoute(
                    IPv4OptionType.NotCopied_Control_LooseSourceRouting,
                    4,
                    new ReadOnlyMemory<IPAddress>(new IPAddress[]
                    {
                        new IPAddress(new byte[] { 192, 168, 1, 20 }),
                        new IPAddress(new byte[] { 192, 168, 1, 21 })
                    })),
                new IPv4OptionLooseSourceRecordRoute(
                    IPv4OptionType.NotCopied_Control_LooseSourceRouting,
                    4,
                    new ReadOnlyMemory<IPAddress>(new IPAddress[]
                    {
                        new IPAddress(new byte[] { 192, 168, 1, 20 }),
                        new IPAddress(new byte[] { 192, 168, 1, 21 })
                    })),
                true
            },
            new object?[]
            {
                new IPv4OptionLooseSourceRecordRoute(
                    IPv4OptionType.NotCopied_Control_LooseSourceRouting,
                    4,
                    new ReadOnlyMemory<IPAddress>(new IPAddress[]
                    {
                        new IPAddress(new byte[] { 192, 168, 1, 20 }),
                        new IPAddress(new byte[] { 192, 168, 1, 21 })
                    })),
                new IPv4OptionLooseSourceRecordRoute(
                    IPv4OptionType.Copied_Control_LooseSourceRouting,
                    4,
                    new ReadOnlyMemory<IPAddress>(new IPAddress[]
                    {
                        new IPAddress(new byte[] { 192, 168, 1, 21 }),
                        new IPAddress(new byte[] { 192, 168, 1, 20 })
                    })),
                false
            }
        };

    [Theory(DisplayName = "IPv4OptionLooseSourceRecordRoute :: Equals")]
    [MemberData(nameof(EqualsParameters))]
    internal void EqualsTests(IPv4OptionLooseSourceRecordRoute option, object? other, bool expected)
    {
        var actual = option.Equals(other);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "IPv4OptionLooseSourceRecordRoute :: ==")]
    [MemberData(nameof(EqualsParameters))]
    internal void EqualsOperatorTests(IPv4OptionLooseSourceRecordRoute option, object? other, bool expected)
    {
        var actualLhs = option == other;
        var actualRhs = other == option;
        Assert.Equal(expected, actualLhs);
        Assert.Equal(expected, actualRhs);
    }

    [Theory(DisplayName = "IPv4OptionLooseSourceRecordRoute :: !=")]
    [MemberData(nameof(EqualsParameters))]
    internal void NotEqualsOperatorTests(IPv4OptionLooseSourceRecordRoute option, object? other, bool expectedInverse)
    {
        var expected = !expectedInverse;
        var actualLhs = option != other;
        var actualRhs = other != option;
        Assert.Equal(expected, actualLhs);
        Assert.Equal(expected, actualRhs);
    }
}
