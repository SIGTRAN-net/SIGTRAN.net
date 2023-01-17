/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Sctp.Chunks.Parameters.ErrorCause;

namespace SigtranNet.Tests.Protocols.Sctp.Chunks.Parameters.ErrorCause;

public partial class SctpInvalidStreamIdentifierErrorTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    /* Cause Code */
                    0, 1,
                    /* Cause Length */
                    0, 8,
                    /* Stream Identifier */
                    12345 >> 8, 12345 & 0xFF,
                    /* (Reserved) */
                    0, 0,
                }),
                new SctpInvalidStreamIdentifierError(12345)
            }
        };

    [Theory(DisplayName = "SctpInvalidStreamIdentifierError :: FromReadOnlyMemory")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, SctpInvalidStreamIdentifierError expected)
    {
        var actual = SctpInvalidStreamIdentifierError.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "SctpInvalidStreamIdentifierError :: ToReadOnlyMemory")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, SctpInvalidStreamIdentifierError parameter)
    {
        var actual = parameter.ToReadOnlyMemory();
        Assert.True(expected.Span.SequenceEqual(actual.Span));
    }
}
