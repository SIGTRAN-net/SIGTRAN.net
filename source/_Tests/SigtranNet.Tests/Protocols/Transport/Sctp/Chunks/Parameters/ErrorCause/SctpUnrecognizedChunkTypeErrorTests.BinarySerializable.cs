/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks;
using SigtranNet.Protocols.Transport.Sctp.Chunks.HeartbeatRequest;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Variable;

namespace SigtranNet.Tests.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause;

public partial class SctpUnrecognizedChunkTypeErrorTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    /* Error Cause Code */
                    (ushort)SctpErrorCauseCode.UnrecognizedChunkType >> 8,
                    (ushort)SctpErrorCauseCode.UnrecognizedChunkType & 0xFF,
                    /* Error Cause Length */
                    16 >> 8, 16 & 0xFF,
                    /* Unrecognized Chunk */
                    (byte)SctpChunkType.HeartbeatRequest,
                    0,
                    12 >> 8, 12 & 0xFF,
                    0, 1, 0, 8,
                    1, 2, 3, 4
                }),
                new SctpUnrecognizedChunkTypeError(
                    new SctpHeartbeatRequest(
                        new SctpHeartbeatInfoParameter(new byte[]
                        {
                            1, 2, 3, 4
                        })))
            }
        };

    [Theory(DisplayName = "SctpUnrecognizedChunkTypeError :: FromReadOnlyMemory")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, SctpUnrecognizedChunkTypeError expected)
    {
        var actual = SctpUnrecognizedChunkTypeError.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "SctpUnrecognizedChunkTypeError :: ToReadOnlyMemory")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, SctpUnrecognizedChunkTypeError parameter)
    {
        var actual = parameter.ToReadOnlyMemory();
        Assert.True(expected.Span.SequenceEqual(actual.Span));
    }
}
