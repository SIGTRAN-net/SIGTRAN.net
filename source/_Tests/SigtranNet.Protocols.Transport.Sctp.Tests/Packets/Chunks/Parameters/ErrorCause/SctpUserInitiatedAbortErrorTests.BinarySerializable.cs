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

using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.ErrorCause;

namespace SigtranNet.Protocols.Transport.Sctp.Tests.Packets.Chunks.Parameters.ErrorCause;

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
