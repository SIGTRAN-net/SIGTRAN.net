/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Sctp.Chunks.Parameters;
using SigtranNet.Protocols.Sctp.Chunks.Parameters.ErrorCause;
using SigtranNet.Protocols.Sctp.Chunks.Parameters.Variable.Address;
using System.Net;

namespace SigtranNet.Tests.Protocols.Sctp.Chunks.Parameters.ErrorCause;

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
