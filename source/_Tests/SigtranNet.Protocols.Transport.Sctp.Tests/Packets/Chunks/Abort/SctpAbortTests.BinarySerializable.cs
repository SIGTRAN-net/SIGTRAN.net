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
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Abort;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.ErrorCause;

namespace SigtranNet.Protocols.Transport.Sctp.Tests.Packets.Chunks.Abort;

public partial class SctpAbortTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    /* Chunk header */
                    (byte)SctpChunkType.Abort,
                    (byte)SctpAbortFlags.VerificationTagReflected,
                    0, 16,

                    /* Cookie Received While Shutting Down */
                    (ushort)SctpErrorCauseCode.CookieReceivedWhileShuttingDown >> 8,
                    (ushort)SctpErrorCauseCode.CookieReceivedWhileShuttingDown & 0xFF,
                    0, 4,

                    /* Invalid Stream Identifier */
                    (ushort)SctpErrorCauseCode.InvalidStreamIdentifier >> 8,
                    (ushort)SctpErrorCauseCode.InvalidStreamIdentifier & 0xFF,
                    0, 8,
                    20000 >> 8,
                    20000 & 0xFF,
                    0, // (reserved)
                    0, // (reserved)

                }),
                new SctpAbort(
                    SctpAbortFlags.VerificationTagReflected,
                    new ReadOnlyMemory<ISctpErrorCause>(
                        new ISctpErrorCause[]
                        {
                            new SctpCookieReceivedWhileShuttingDownError(),
                            new SctpInvalidStreamIdentifierError(20000)
                        }))
            }
        };

    [Theory(DisplayName = $"{nameof(SctpAbort)} :: {nameof(SctpAbort.FromReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, SctpAbort expected)
    {
        var actual = SctpAbort.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = $"{nameof(SctpAbort)} :: {nameof(SctpAbort.ToReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, SctpAbort chunk)
    {
        var actual = chunk.ToReadOnlyMemory();
        Assert.True(expected.Span.SequenceEqual(actual.Span));
    }
}
