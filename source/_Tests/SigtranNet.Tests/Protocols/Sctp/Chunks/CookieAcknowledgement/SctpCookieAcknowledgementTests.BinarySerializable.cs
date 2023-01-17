/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Sctp.Chunks;
using SigtranNet.Protocols.Sctp.Chunks.CookieAcknowledgement;

namespace SigtranNet.Tests.Protocols.Sctp.Chunks.CookieAcknowledgement;

public partial class SctpCookieAcknowledgementTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    (byte)SctpChunkType.CookieAcknowledgement,
                    0,
                    0, 4
                }),
                null
            }
        };

    [Theory(DisplayName = $"{nameof(SctpCookieAcknowledgement)} :: {nameof(SctpCookieAcknowledgement.FromReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, Exception? exceptionExpected)
    {
        if (exceptionExpected is null)
        {
            var actual = SctpCookieAcknowledgement.FromReadOnlyMemory(memory);
            Assert.IsType<SctpCookieAcknowledgement>(actual);
        }
        else
        {
            var exceptionActual = Assert.Throws(exceptionExpected.GetType(), () => SctpCookieAcknowledgement.FromReadOnlyMemory(memory));
            Assert.Equal(exceptionExpected, exceptionActual);
        }
    }

    [Fact(DisplayName = $"{nameof(SctpCookieAcknowledgement)} :: {nameof(SctpCookieAcknowledgement.ToReadOnlyMemory)}")]
    internal void ToReadOnlyMemoryTest()
    {
        // Arrange
        var expected = new ReadOnlyMemory<byte>(new byte[]
        {
            (byte)SctpChunkType.CookieAcknowledgement,
            0,
            0, 4
        });
        var chunk = new SctpCookieAcknowledgement();

        // Act
        var actual = chunk.ToReadOnlyMemory();

        // Assert
        Assert.True(expected.Span.SequenceEqual(actual.Span));
    }
}
