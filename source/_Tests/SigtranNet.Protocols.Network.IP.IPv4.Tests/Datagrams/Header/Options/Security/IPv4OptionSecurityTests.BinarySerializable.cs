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
using SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header.Options.Security;

namespace SigtranNet.Protocols.Network.IP.IPv4.Tests.Datagrams.Header.Options.Security;

public class IPv4OptionSecurityTests
{
    public static readonly IEnumerable<object?[]> OptionSecurityParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(
                    new byte[]
                    {
                        (byte)IPv4OptionType.Copied_Control_Security,   // Option Type
                        11,                                             // Length
                        0b11010111, 0b10001000,                         // Security Level
                        0, 2,                                           // Compartments
                        0, 3,                                           // Handling Restrictions
                        0, 0, 4 }),                                     // Transmission Control Code
                new IPv4OptionSecurity(
                    optionType: IPv4OptionType.Copied_Control_Security,
                    securityLevel: IPv4OptionSecurityLevel.Secret,
                    compartments: 2,
                    handlingRestrictions: 3,
                    transmissionControlCode: 4)
            }
        };

    [Theory(DisplayName = "IPv4OptionSecurity :: FromReadOnlyMemory")]
    [MemberData(nameof(OptionSecurityParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, IPv4OptionSecurity expected)
    {
        var actual = IPv4OptionSecurity.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "IPv4OptionSecurity :: ToReadOnlyMemory")]
    [MemberData(nameof(OptionSecurityParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, IPv4OptionSecurity option)
    {
        var actual = option.ToReadOnlyMemory();
        Assert.Equal(expected.ToArray(), actual.ToArray());
    }
}
