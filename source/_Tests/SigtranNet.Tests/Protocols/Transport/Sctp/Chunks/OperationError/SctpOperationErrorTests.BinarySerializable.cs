/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks;
using SigtranNet.Protocols.Transport.Sctp.Chunks.OperationError;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause;

namespace SigtranNet.Tests.Protocols.Transport.Sctp.Chunks.OperationError;

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
