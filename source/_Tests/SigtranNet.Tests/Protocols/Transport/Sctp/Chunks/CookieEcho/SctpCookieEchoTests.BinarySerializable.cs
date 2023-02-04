/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks;
using SigtranNet.Protocols.Transport.Sctp.Chunks.CookieEcho;

namespace SigtranNet.Tests.Protocols.Transport.Sctp.Chunks.CookieEcho;

public partial class SctpCookieEchoTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    /* Chunk header */
                    (byte)SctpChunkType.StateCookie,
                    0, // No Flags
                    0, 8,

                    /* Cookie */
                    1, 2, 3, 4,
                }),
                new SctpCookieEcho(
                    new ReadOnlyMemory<byte>(
                        new byte[]
                        {
                            1, 2, 3, 4
                        }))
            }
        };

    [Theory(DisplayName = $"{nameof(SctpCookieEcho)} :: {nameof(SctpCookieEcho.FromReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, SctpCookieEcho expected)
    {
        var actual = SctpCookieEcho.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = $"{nameof(SctpCookieEcho)} :: {nameof(SctpCookieEcho.ToReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, SctpCookieEcho chunk)
    {
        var actual = chunk.ToReadOnlyMemory();
        Assert.True(expected.Span.SequenceEqual(actual.Span));
    }
}
