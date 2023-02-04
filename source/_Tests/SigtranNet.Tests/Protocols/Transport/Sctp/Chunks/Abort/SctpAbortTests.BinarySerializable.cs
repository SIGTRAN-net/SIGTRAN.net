/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Abort;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause;

namespace SigtranNet.Tests.Protocols.Transport.Sctp.Chunks.Abort;

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
