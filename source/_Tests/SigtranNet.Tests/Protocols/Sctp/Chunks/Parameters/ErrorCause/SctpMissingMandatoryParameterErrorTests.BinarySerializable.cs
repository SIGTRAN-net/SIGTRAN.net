/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Sctp.Chunks.Parameters;
using SigtranNet.Protocols.Sctp.Chunks.Parameters.ErrorCause;

namespace SigtranNet.Tests.Protocols.Sctp.Chunks.Parameters.ErrorCause;

public partial class SctpMissingMandatoryParameterErrorTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    /* Cause Code */
                    (ushort)SctpErrorCauseCode.MissingMandatoryParameter >> 8,
                    (ushort)SctpErrorCauseCode.MissingMandatoryParameter & 0xFF,
                    /* Cause Length */
                    10 >> 8, 10 & 0xFF,
                    /* Number of Missing Parameters */
                    1 >> 24, (1 & 0x00FF0000) >> 16, (1 & 0x0000FF00) >> 8, 1 & 0xFF,
                    /* Missing Parameter Types */
                    (ushort)SctpChunkParameterType.StateCookie >> 8,
                    (ushort)SctpChunkParameterType.StateCookie & 0xFF,
                }),
                new SctpMissingMandatoryParameterError(
                    new SctpChunkParameterType[]
                    {
                        SctpChunkParameterType.StateCookie
                    })
            }
        };

    [Theory(DisplayName = "SctpMissingMandatoryParameterError :: FromReadOnlyMemory")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, SctpMissingMandatoryParameterError expected)
    {
        var actual = SctpMissingMandatoryParameterError.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "SctpMissingMandatoryParameterError :: ToReadOnlyMemory")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, SctpMissingMandatoryParameterError parameter)
    {
        var actual = parameter.ToReadOnlyMemory();
        Assert.True(expected.Span.SequenceEqual(actual.Span));
    }
}
