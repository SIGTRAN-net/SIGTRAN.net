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

using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Exceptions;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Fixed;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.ShutdownAssociation;

namespace SigtranNet.Protocols.Transport.Sctp.Tests.Packets.Chunks.ShutdownAssociation;

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
    [MemberData(nameof(ReadOnlyMemoryParameters))]
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
