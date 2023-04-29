/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;

namespace SigtranNet.Tests.Binary;

public sealed class SpanExtensionsTests
{
    public static readonly IEnumerable<object?[]> ConcatParameters =
        new[]
        {
            new object?[]
            {
                new Memory<byte>(new byte[]
                {
                    1, 2, 3
                }),
                new Memory<byte>(new byte[]
                {
                    4, 5, 6
                }),
                new Memory<byte>(new byte[]
                {
                    1, 2, 3, 4, 5, 6
                })
            },
            new object?[]
            {
                new Memory<byte>(new byte[]
                {
                    6, 5, 4, 3, 2, 1
                }),
                new Memory<byte>(new byte[]
                {
                    12, 11, 10, 9, 8, 7
                }),
                new Memory<byte>(new byte[]
                {
                    6, 5, 4, 3, 2, 1, 12, 11, 10, 9, 8, 7
                })
            }
        };

    [Theory(DisplayName = "SpanExtensions :: Concat")]
    [MemberData(nameof(ConcatParameters))]
    public void ConcatTests(Memory<byte> first, Memory<byte> second, Memory<byte> expected)
    {
        // Arrange
        var firstSpan = first.Span;
        var secondSpan = second.Span;
        var expectedSpan = expected.Span;

        // Act
        var actualSpan = firstSpan.Concat(secondSpan);

        // Assert
        Assert.True(expectedSpan.SequenceEqual(actualSpan));
    }
}
