/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Sctp.Chunks;
using SigtranNet.Protocols.Sctp.Chunks.HeartbeatRequest;
using SigtranNet.Protocols.Sctp.Chunks.Parameters;
using SigtranNet.Protocols.Sctp.Chunks.Parameters.Variable;

namespace SigtranNet.Tests.Protocols.Sctp.Chunks.HeartbeatRequest;

public partial class SctpHeartbeatRequestTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    /* Chunk header */
                    (byte)SctpChunkType.HeartbeatRequest,
                    0,
                    0, 20,

                    /* Heartbeat Information TLV */
                    (ushort)SctpChunkParameterType.HeartbeatInfo >> 8,
                    (ushort)SctpChunkParameterType.HeartbeatInfo & 0xFF,
                    0, 16,
                    1, 2, 3, 4,
                    4, 3, 2, 1,
                    1, 2, 3, 4,
                }),
                new SctpHeartbeatRequest(
                    new SctpHeartbeatInfoParameter(
                        new ReadOnlyMemory<byte>(
                            new byte[]
                            {
                                1, 2, 3, 4,
                                4, 3, 2, 1,
                                1, 2, 3, 4,
                            })))
            }
        };

    [Theory(DisplayName = $"{nameof(SctpHeartbeatRequest)} :: {nameof(SctpHeartbeatRequest.FromReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, SctpHeartbeatRequest expected)
    {
        var actual = SctpHeartbeatRequest.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = $"{nameof(SctpHeartbeatRequest)} :: {nameof(SctpHeartbeatRequest.ToReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, SctpHeartbeatRequest chunk)
    {
        var actual = chunk.ToReadOnlyMemory();
        Assert.True(expected.Span.SequenceEqual(actual.Span));
    }
}
