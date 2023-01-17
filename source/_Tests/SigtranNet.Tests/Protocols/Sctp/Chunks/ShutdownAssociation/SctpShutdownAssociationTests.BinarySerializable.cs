/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Sctp.Chunks;
using SigtranNet.Protocols.Sctp.Chunks.Exceptions;
using SigtranNet.Protocols.Sctp.Chunks.Parameters.Fixed;
using SigtranNet.Protocols.Sctp.Chunks.ShutdownAssociation;
using System.ComponentModel.DataAnnotations.Schema;

namespace SigtranNet.Tests.Protocols.Sctp.Chunks.ShutdownAssociation;

public partial class SctpShutdownAssociationTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    /* Chunk header */
                    (byte)SctpChunkType.Shutdown,
                    0,
                    0, 8,

                    /* Cumulative TSN Ack */
                    12345 >> 24,
                    (12345 & 0x00FF0000) >> 16,
                    (12345 & 0x0000FF00) >> 8,
                    12345 & 0xFF
                }),
                null
            },
            new object?[]
            {
                 new ReadOnlyMemory<byte>(new byte[]
                 {
                     /* Chunk header */
                     (byte)SctpChunkType.StateCookie,
                     0,
                     0, 8,

                     /* Cumulative TSN Ack */
                     12345 >> 24,
                     (12345 & 0x00FF0000) >> 16,
                     (12345 & 0x0000FF00) >> 8,
                     12345 & 0xFF
                 }),
                 new SctpChunkTypeInvalidException(SctpChunkType.StateCookie)
            },
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    /* Chunk header */
                    (byte)SctpChunkType.Shutdown,
                    0,
                    0, 12,

                    /* Cumulative TSN Ack */
                    12345 >> 24,
                    (12345 & 0x00FF0000) >> 16,
                    (12345 & 0x0000FF00) >> 8,
                    12345 & 0xFF
                }),
                new SctpChunkLengthInvalidException(12)
            }
        };

    [Theory(DisplayName = $"{nameof(SctpShutdownAssociation)} :: {nameof(SctpShutdownAssociation.FromReadOnlyMemory)}")]
    [MemberData(nameof( ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, Exception? exceptionExpected)
    {
        if (exceptionExpected is null)
        {
            var actual = SctpShutdownAssociation.FromReadOnlyMemory(memory);
            Assert.IsType<SctpShutdownAssociation>(actual);
        }
        else
        {
            var exceptionActual = Assert.Throws(exceptionExpected.GetType(), () => SctpShutdownAssociation.FromReadOnlyMemory(memory));
            Assert.Equal(exceptionExpected, exceptionActual);
        }
    }

    [Fact(DisplayName = $"{nameof(SctpShutdownAssociation)} :: {nameof(SctpShutdownAssociation.ToReadOnlyMemory)}")]
    internal void ToReadOnlyMemoryTest()
    {
        // Arrange
        var expected = new ReadOnlyMemory<byte>(new byte[]
        {
            /* Chunk header */
            (byte)SctpChunkType.Shutdown,
            0,
            0, 8,

            /* Cumulative TSN Ack */
            12345 >> 24,
            (12345 & 0x00FF0000) >> 16,
            (12345 & 0x0000FF00) >> 8,
            12345 & 0xFF
        });
        var chunk = new SctpShutdownAssociation(new SctpTransmissionSequenceNumber(12345));

        // Act
        var actual = chunk.ToReadOnlyMemory();

        // Assert
        Assert.True(expected.Span.SequenceEqual(actual.Span));
    }
}
