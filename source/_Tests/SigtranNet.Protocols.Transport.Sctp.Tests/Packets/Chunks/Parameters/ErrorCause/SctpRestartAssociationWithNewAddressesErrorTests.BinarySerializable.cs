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
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Variable.Address;
using System.Net;

namespace SigtranNet.Protocols.Transport.Sctp.Tests.Packets.Chunks.Parameters.ErrorCause;

public partial class SctpRestartAssociationWithNewAddressesErrorTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    /* Error Cause header */
                    (ushort)SctpErrorCauseCode.RestartAssociationWithNewAddresses >> 8,
                    (ushort)SctpErrorCauseCode.RestartAssociationWithNewAddresses & 0xFF,
                    0, 20,

                    /* New Address TLVs */
                    (ushort)SctpChunkParameterType.IPv4Address >> 8,
                    (ushort)SctpChunkParameterType.IPv4Address & 0xFF,
                    0, 8,
                    192, 168, 20, 20,

                    (ushort)SctpChunkParameterType.IPv4Address >> 8,
                    (ushort)SctpChunkParameterType.IPv4Address & 0xFF,
                    0, 8,
                    192, 168, 22, 22
                }),
                new SctpRestartAssociationWithNewAddressesError(
                    new ReadOnlyMemory<ISctpAddressParameter>(
                        new ISctpAddressParameter[]
                        {
                            new SctpIPv4AddressParameter(new IPAddress(new byte[] { 192, 168, 20, 20 })),
                            new SctpIPv4AddressParameter(new IPAddress(new byte[] { 192, 168, 22, 22 }))
                        }))
            }
        };

    [Theory(DisplayName = $"{nameof(SctpRestartAssociationWithNewAddressesError)} :: {nameof(SctpRestartAssociationWithNewAddressesError.FromReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, SctpRestartAssociationWithNewAddressesError expected)
    {
        var actual = SctpRestartAssociationWithNewAddressesError.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = $"{nameof(SctpRestartAssociationWithNewAddressesError)} :: {nameof(SctpRestartAssociationWithNewAddressesError.ToReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, SctpRestartAssociationWithNewAddressesError parameter)
    {
        var actual = parameter.ToReadOnlyMemory();
        Assert.True(expected.Span.SequenceEqual(actual.Span));
    }
}
