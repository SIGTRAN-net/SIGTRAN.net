/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Sctp.Chunks;
using SigtranNet.Protocols.Sctp.Chunks.HeartbeatAcknowledgement;
using SigtranNet.Protocols.Sctp.Chunks.Parameters;
using SigtranNet.Protocols.Sctp.Chunks.Parameters.Variable;

namespace SigtranNet.Tests.Protocols.Sctp.Chunks.HeartbeatAcknowledgement;

public partial class SctpHeartbeatAcknowledgementTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    /* Chunk header */
                    (byte)SctpChunkType.HeartbeatAcknowledgement,
                    0,
                    0, 20,

                    /* Heartbeat Information TLV */
                    (ushort)SctpChunkParameterType.HeartbeatInfo >> 8,
                    (ushort)SctpChunkParameterType.HeartbeatInfo & 0xFF,
                    0, 16,
                    1, 2, 3, 4,
                    4, 3, 2, 1,
                    1, 2, 3, 4
                }),
                new SctpHeartbeatAcknowledgement(
                    new SctpHeartbeatInfoParameter(
                        new ReadOnlyMemory<byte>(
                            new byte[]
                            {
                                1, 2, 3, 4,
                                4, 3, 2, 1,
                                1, 2, 3, 4
                            })))
            }
        };

    [Theory(DisplayName = $"{nameof(SctpHeartbeatAcknowledgement)} :: {nameof(SctpHeartbeatAcknowledgement.FromReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, SctpHeartbeatAcknowledgement expected)
    {
        var actual = SctpHeartbeatAcknowledgement.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = $"{nameof(SctpHeartbeatAcknowledgement)} :: {nameof(SctpHeartbeatAcknowledgement.ToReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, SctpHeartbeatAcknowledgement chunk)
    {
        var actual = chunk.ToReadOnlyMemory();
        Assert.True(expected.Span.SequenceEqual(actual.Span));
    }
}
