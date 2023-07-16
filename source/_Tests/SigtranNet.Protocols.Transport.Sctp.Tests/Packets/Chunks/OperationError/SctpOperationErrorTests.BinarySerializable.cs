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
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.OperationError;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.ErrorCause;

namespace SigtranNet.Protocols.Transport.Sctp.Tests.Packets.Chunks.OperationError;

public partial class SctpOperationErrorTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    /* Chunk header */
                    (byte)SctpChunkType.OperationError,
                    0,
                    0, 20,

                    /* Error Causes */
                    (ushort)SctpErrorCauseCode.InvalidStreamIdentifier >> 8,
                    (ushort)SctpErrorCauseCode.InvalidStreamIdentifier & 0xFF,
                    0, 8,
                    12345 >> 8,
                    12345 & 0xFF,
                    0, 0, // (Reserved)

                    (ushort)SctpErrorCauseCode.StaleCookie >> 8,
                    (ushort)SctpErrorCauseCode.StaleCookie & 0xFF,
                    0, 8,
                    54321 >> 24,
                    (54321 & 0x00FF0000) >> 16,
                    (54321 & 0x0000FF00) >> 8,
                    54321 & 0xFF,
                }),
                new SctpOperationError(
                    new ReadOnlyMemory<ISctpErrorCause>(
                        new ISctpErrorCause[]
                        {
                            new SctpInvalidStreamIdentifierError(12345),
                            new SctpStaleCookieError(54321)
                        }))
            }
        };

    [Theory(DisplayName = $"{nameof(SctpOperationError)} :: {nameof(SctpOperationError.FromReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, SctpOperationError expected)
    {
        var actual = SctpOperationError.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = $"{nameof(SctpOperationError)} :: {nameof(SctpOperationError.ToReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, SctpOperationError errorCause)
    {
        var actual = errorCause.ToReadOnlyMemory();
        Assert.True(expected.Span.SequenceEqual(actual.Span));
    }
}
