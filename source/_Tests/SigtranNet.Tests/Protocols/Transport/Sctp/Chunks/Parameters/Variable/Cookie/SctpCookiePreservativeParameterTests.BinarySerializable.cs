/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Variable.Cookie;

namespace SigtranNet.Tests.Protocols.Transport.Sctp.Chunks.Parameters.Variable.Cookie;

public class SctpCookiePreservativeParameterTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    (ushort)SctpChunkParameterType.CookiePreservative >> 8,
                    (ushort)SctpChunkParameterType.CookiePreservative & 0xFF,
                    0, 8,
                    3200 >> 24, (3200 & 0x00FF0000) >> 16, (3200 & 0x0000FF00) >> 8, 3200 & 0xFF
                }),
                new SctpCookiePreservativeParameter(3200)
            }
        };

    [Theory(DisplayName = "SctpCookiePreservativeParameter :: FromReadOnlyMemory")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, SctpCookiePreservativeParameter expected)
    {
        var actual = SctpCookiePreservativeParameter.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "SctpCookiePreservativeParameter :: ToReadOnlyMemory")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, SctpCookiePreservativeParameter parameter)
    {
        var actual = parameter.ToReadOnlyMemory();
        Assert.Equal(expected.ToArray(), actual.ToArray());
    }
}
