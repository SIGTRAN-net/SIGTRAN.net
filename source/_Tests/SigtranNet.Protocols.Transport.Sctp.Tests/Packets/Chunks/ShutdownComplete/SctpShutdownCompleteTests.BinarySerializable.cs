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
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.ShutdownComplete;

namespace SigtranNet.Protocols.Transport.Sctp.Tests.Packets.Chunks.ShutdownComplete;

public partial class SctpShutdownCompleteTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    (byte)SctpChunkType.ShutdownComplete,
                    (byte)SctpShutdownCompleteFlags.VerificationTagExpected,
                    0, 4
                }),
                new SctpShutdownComplete(SctpShutdownCompleteFlags.VerificationTagExpected)
            },
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    (byte)SctpChunkType.ShutdownComplete,
                    (byte)SctpShutdownCompleteFlags.VerificationTagReflected,
                    0, 4
                }),
                new SctpShutdownComplete(SctpShutdownCompleteFlags.VerificationTagReflected)
            }
        };

    [Theory(DisplayName = $"{nameof(SctpShutdownComplete)} :: {nameof(SctpShutdownComplete.FromReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemory(ReadOnlyMemory<byte> memory, SctpShutdownComplete expected)
    {
        var actual = SctpShutdownComplete.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = $"{nameof(SctpShutdownComplete)} :: {nameof(SctpShutdownComplete.FromReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemory(ReadOnlyMemory<byte> expected, SctpShutdownComplete chunk)
    {
        var actual = chunk.ToReadOnlyMemory();
        Assert.True(expected.Span.SequenceEqual(actual.Span));
    }
}
