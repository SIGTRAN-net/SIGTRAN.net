/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Sctp.Chunks.Parameters.ErrorCause;
using SigtranNet.Protocols.Sctp.Chunks.Parameters.Fixed;

namespace SigtranNet.Tests.Protocols.Sctp.Chunks.Parameters.ErrorCause;

public partial class SctpNoUserDataErrorTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    /* Error Cause Header */
                    (ushort)SctpErrorCauseCode.NoUserData >> 8,
                    (ushort)SctpErrorCauseCode.NoUserData & 0xFF,
                    0, 8,

                    /* Transmission Sequence Number */
                    12345 >> 24,
                    (12345 & 0x00FF0000) >> 16,
                    (12345 & 0x0000FF00) >> 8,
                    12345 & 0xFF,
                }),
                new SctpNoUserDataError(new SctpTransmissionSequenceNumber(12345))
            }
        };

    [Theory(DisplayName = $"{nameof(SctpNoUserDataError)} :: {nameof(SctpNoUserDataError.FromReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, SctpNoUserDataError expected)
    {
        var actual = SctpNoUserDataError.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = $"{nameof(SctpNoUserDataError)} :: {nameof(SctpNoUserDataError.ToReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, SctpNoUserDataError parameter)
    {
        var actual = parameter.ToReadOnlyMemory();
        Assert.True(expected.Span.SequenceEqual(actual.Span));
    }
}
