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

using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.ErrorCause;

namespace SigtranNet.Protocols.Transport.Sctp.Tests.Packets.Chunks.Parameters.ErrorCause;

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
