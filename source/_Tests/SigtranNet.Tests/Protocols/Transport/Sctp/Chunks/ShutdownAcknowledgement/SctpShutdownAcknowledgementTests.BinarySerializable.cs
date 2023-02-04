/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Exceptions;
using SigtranNet.Protocols.Transport.Sctp.Chunks.ShutdownAcknowledgement;

namespace SigtranNet.Tests.Protocols.Transport.Sctp.Chunks.ShutdownAcknowledgement;

public partial class SctpShutdownAcknowledgementTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    (byte)SctpChunkType.ShutdownAcknowledgement,
                    0, // No Flags
                    0, 4,
                }),
                null
            },
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    (byte)SctpChunkType.Shutdown,
                    0, // No Flags
                    0, 4,
                }),
                new SctpChunkTypeInvalidException(SctpChunkType.Shutdown)
            },
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    (byte)SctpChunkType.ShutdownAcknowledgement,
                    0, // No Flags
                    0, 8,
                }),
                new SctpChunkLengthInvalidException(8)
            }
        };

    [Theory(DisplayName = $"{nameof(SctpShutdownAcknowledgement)} :: {nameof(SctpShutdownAcknowledgement.FromReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, Exception? exceptionExpected)
    {
        if (exceptionExpected is null)
        {
            var actual = SctpShutdownAcknowledgement.FromReadOnlyMemory(memory);
            Assert.IsType<SctpShutdownAcknowledgement>(actual);
        }
        else
        {
            var exceptionActual = Assert.Throws(exceptionExpected.GetType(), () => SctpShutdownAcknowledgement.FromReadOnlyMemory(memory));
            Assert.Equal(exceptionExpected, exceptionActual);
        }
    }

    [Fact(DisplayName = $"{nameof(SctpShutdownAcknowledgement)} :: {nameof(SctpShutdownAcknowledgement.ToReadOnlyMemory)}")]
    internal void ToReadOnlyMemoryTest()
    {
        // Arrange
        var expected = new ReadOnlyMemory<byte>(new byte[]
        {
            (byte)SctpChunkType.ShutdownAcknowledgement,
            0,
            0, 4,
        });
        var chunk = new SctpShutdownAcknowledgement();

        // Act
        var actual = chunk.ToReadOnlyMemory();

        // Assert
        Assert.True(expected.Span.SequenceEqual(actual.Span));
    }
}
