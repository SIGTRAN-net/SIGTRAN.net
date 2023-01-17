/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Sctp.Chunks.Parameters;
using SigtranNet.Protocols.Sctp.Chunks.Parameters.Variable.Cookie;

namespace SigtranNet.Tests.Protocols.Sctp.Chunks.Parameters.Variable.Cookie;

public class SctpStateCookieParameterTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    /* Parameter Header */
                    (ushort)SctpChunkParameterType.StateCookie >> 8,
                    (ushort)SctpChunkParameterType.StateCookie & 0xFF,
                    0, 12,
                    /* Parameter Value */
                    1, 2, 3, 4,
                    5, 6, 7, 8
                }),
                new SctpStateCookieParameter(new ReadOnlyMemory<byte>(new byte[]
                {
                    1, 2, 3, 4,
                    5, 6, 7, 8
                }))
            }
        };

    [Theory(DisplayName = "SctpStateCookieParameter :: FromReadOnlyMemory")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, SctpStateCookieParameter expected)
    {
        var actual = SctpStateCookieParameter.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "SctpStateCookieParameter :: ToReadOnlyMemory")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, SctpStateCookieParameter parameter)
    {
        var actual = parameter.ToReadOnlyMemory();
        Assert.Equal(expected.ToArray(), actual.ToArray());
    }
}
