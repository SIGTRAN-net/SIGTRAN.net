/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.IP.IPv4.Options;
using SigtranNet.Protocols.IP.IPv4.Options.Security;

namespace SigtranNet.Tests.Protocols.IP.IPv4.Options.Security;

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
