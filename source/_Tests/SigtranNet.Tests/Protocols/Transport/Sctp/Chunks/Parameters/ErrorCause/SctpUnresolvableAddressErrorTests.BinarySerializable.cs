/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Variable.Address;
using System.Net;

namespace SigtranNet.Tests.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause;

public partial class SctpUnresolvableAddressErrorTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    /* Error Cause Code */
                    (ushort)SctpErrorCauseCode.UnresolvableAddress >> 8,
                    (ushort)SctpErrorCauseCode.UnresolvableAddress & 0xFF,
                    /* Error Cause Length */
                    12 >> 8,
                    12 & 0xFF,
                    /* Unresolvable Address */
                    (ushort)SctpChunkParameterType.IPv4Address >> 8,
                    (ushort)SctpChunkParameterType.IPv4Address & 0xFF,
                    0, 8,
                    192, 168, 1, 2
                }),
                new SctpUnresolvableAddressError(
                    new SctpIPv4AddressParameter(new IPAddress(new byte[] { 192, 168, 1, 2 })))
            }
        };

    [Theory(DisplayName = "SctpUnresolvableAddressError :: FromReadOnlyMemory")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, SctpUnresolvableAddressError expected)
    {
        var actual = SctpUnresolvableAddressError.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "SctpUnresolvableAddressError :: ToReadOnlyMemory")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, SctpUnresolvableAddressError parameter)
    {
        var actual = parameter.ToReadOnlyMemory();
        Assert.True(expected.Span.SequenceEqual(actual.Span));
    }
}
