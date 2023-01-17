/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Sctp.Chunks.Parameters.ErrorCause;

namespace SigtranNet.Tests.Protocols.Sctp.Chunks.Parameters.ErrorCause;

public partial class SctpStaleCookieErrorTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    /* Parameter Header */
                    (ushort)SctpErrorCauseCode.StaleCookie >> 8,
                    (ushort)SctpErrorCauseCode.StaleCookie & 0xFF,
                    0, 8,
                    /* Measure of Staleness */
                    2345678 >> 24,
                    (2345678 & 0x00FF0000) >> 16,
                    (2345678 & 0x0000FF00) >> 8,
                    2345678 & 0xFF,
                }),
                new SctpStaleCookieError(2345678)
            }
        };

    [Theory(DisplayName = "SctpStaleCookieError :: FromReadOnlyMemory")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, SctpStaleCookieError expected)
    {
        var actual = SctpStaleCookieError.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "SctpStaleCookieError :: ToReadOnlyMemory")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, SctpStaleCookieError parameter)
    {
        var actual = parameter.ToReadOnlyMemory();
        Assert.True(expected.Span.SequenceEqual(actual.Span));
    }
}
