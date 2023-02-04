/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause;

namespace SigtranNet.Tests.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause;

public partial class SctpUserInitiatedAbortErrorTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    /* Error Cause header */
                    (ushort)SctpErrorCauseCode.UserInitiatedAbort >> 8,
                    (ushort)SctpErrorCauseCode.UserInitiatedAbort & 0xFF,
                    0, 8,

                    /* Upper Layer Abort Reason */
                    1, 2, 3, 4
                }),
                new SctpUserInitiatedAbortError(
                    new ReadOnlyMemory<byte>(
                        new byte[]
                        {
                            1, 2, 3, 4
                        }))
            },
        };

    [Theory(DisplayName = $"{nameof(SctpUserInitiatedAbortError)} :: {nameof(SctpUserInitiatedAbortError.FromReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, SctpUserInitiatedAbortError expected)
    {
        var actual = SctpUserInitiatedAbortError.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = $"{nameof(SctpUserInitiatedAbortError)} :: {nameof(SctpUserInitiatedAbortError.ToReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, SctpUserInitiatedAbortError parameter)
    {
        var actual = parameter.ToReadOnlyMemory();
        Assert.True(expected.Span.SequenceEqual(actual.Span));
    }
}
