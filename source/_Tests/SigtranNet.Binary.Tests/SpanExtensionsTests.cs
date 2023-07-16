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
