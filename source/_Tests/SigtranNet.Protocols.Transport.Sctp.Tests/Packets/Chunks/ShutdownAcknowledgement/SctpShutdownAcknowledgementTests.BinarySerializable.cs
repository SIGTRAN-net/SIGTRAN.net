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
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.ShutdownAcknowledgement;

namespace SigtranNet.Protocols.Transport.Sctp.Tests.Packets.Chunks.ShutdownAcknowledgement;

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
