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
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.HeartbeatAcknowledgement;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Variable;

namespace SigtranNet.Protocols.Transport.Sctp.Tests.Packets.Chunks.HeartbeatAcknowledgement;

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
