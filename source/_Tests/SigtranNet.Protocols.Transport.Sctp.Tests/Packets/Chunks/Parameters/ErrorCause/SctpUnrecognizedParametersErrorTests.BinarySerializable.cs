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
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Variable;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Variable.Address;
using System.Net;

namespace SigtranNet.Protocols.Transport.Sctp.Tests.Packets.Chunks.Parameters.ErrorCause;

public partial class SctpUnrecognizedParametersErrorTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    /* Error Cause Header */
                    (ushort)SctpErrorCauseCode.UnrecognizedParameters >> 8,
                    (ushort)SctpErrorCauseCode.UnrecognizedParameters & 0xFF,
                    20 >> 8,
                    20 & 0xFF,

                    /* IPv4 Address Parameter */
                    (ushort)SctpChunkParameterType.IPv4Address >> 8,
                    (ushort)SctpChunkParameterType.IPv4Address & 0xFF,
                    0, 8,
                    192, 168, 10, 10,

                    /* Invalid Mandatory Parameter */
                    (ushort)SctpChunkParameterType.HeartbeatInfo >> 8,
                    (ushort)SctpChunkParameterType.HeartbeatInfo & 0xFF,
                    0, 8,
                    1, 2, 3, 4
                }),
                new SctpUnrecognizedParametersError(new ISctpChunkParameterVariableLength[]
                {
                    new SctpIPv4AddressParameter(new IPAddress(new byte[] { 192, 168, 10, 10 })),
                    new SctpHeartbeatInfoParameter(new byte[] { 1, 2, 3, 4 })
                })
            }
        };

    [Theory(DisplayName = $"{nameof(SctpUnrecognizedParametersError)} :: {nameof(SctpUnrecognizedParametersError.FromReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, SctpUnrecognizedParametersError expected)
    {
        var actual = SctpUnrecognizedParametersError.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = $"{nameof(SctpUnrecognizedParametersError)} :: {nameof(SctpUnrecognizedParametersError.ToReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, SctpUnrecognizedParametersError parameter)
    {
        var actual = parameter.ToReadOnlyMemory();
        Assert.True(expected.Span.SequenceEqual(actual.Span));
    }
}
