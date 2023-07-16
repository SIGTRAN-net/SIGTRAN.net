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

using SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header.Options;
using SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header.Options.LooseSourceRouting;
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
