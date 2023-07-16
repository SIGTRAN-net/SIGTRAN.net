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
using SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header.Options.StreamIdentifier;

namespace SigtranNet.Protocols.Network.IP.IPv4.Tests.Datagrams.Header.Options.StreamIdentifier;

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
