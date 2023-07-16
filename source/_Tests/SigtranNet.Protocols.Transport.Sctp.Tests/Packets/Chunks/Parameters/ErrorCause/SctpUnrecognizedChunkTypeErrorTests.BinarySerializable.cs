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
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.HeartbeatRequest;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.ErrorCause;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Variable;

namespace SigtranNet.Protocols.Transport.Sctp.Tests.Packets.Chunks.Parameters.ErrorCause;

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
