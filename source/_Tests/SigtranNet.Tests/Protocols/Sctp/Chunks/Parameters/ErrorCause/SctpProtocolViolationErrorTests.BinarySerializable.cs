/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Sctp.Chunks.Parameters.ErrorCause;

namespace SigtranNet.Tests.Protocols.Sctp.Chunks.Parameters.ErrorCause;

public partial class SctpProtocolViolationErrorTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    /* Error Cause header */
                    (ushort)SctpErrorCauseCode.ProtocolViolation >> 8,
                    (ushort)SctpErrorCauseCode.ProtocolViolation & 0xFF,
                    0, 8,

                    /* Additional Information */
                    1, 2, 3, 4,
                }),
                new SctpProtocolViolationError(
                    new ReadOnlyMemory<byte>(new byte[]
                    {
                        1, 2, 3, 4
                    }))
            }
        };

    [Theory(DisplayName = $"{nameof(SctpProtocolViolationError)} :: {nameof(SctpProtocolViolationError.FromReadOnlyMemory)}")]
    [MemberData(nameof( ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, SctpProtocolViolationError expected)
    {
        var actual = SctpProtocolViolationError.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = $"{nameof(SctpProtocolViolationError)} :: {nameof(SctpProtocolViolationError.ToReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, SctpProtocolViolationError parameter)
    {
        var actual = parameter.ToReadOnlyMemory();
        Assert.True(expected.Span.SequenceEqual(actual.Span));
    }
}
