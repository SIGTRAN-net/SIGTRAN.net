/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Variable.Address;
using System.Net;

namespace SigtranNet.Tests.Protocols.Transport.Sctp.Chunks.Parameters.Variable.Address;

public class SctpIPv4AddressParameterTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    /* Chunk Parameter Type */
                    (ushort)SctpChunkParameterType.IPv4Address >> 8, (ushort)SctpChunkParameterType.IPv4Address & 0x00FF,
                    /* Chunk Parameter Length */
                    0, 8,
                    /* IPv4 Address */
                    192, 168, 12, 12
                }),
                new SctpIPv4AddressParameter(new IPAddress(new byte[] { 192, 168, 12, 12 }))
            }
        };

    [Theory(DisplayName = "IPv4AddressParameter :: FromReadOnlyMemory")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, SctpIPv4AddressParameter expected)
    {
        var actual = SctpIPv4AddressParameter.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "IPv4AddressParameter :: ToReadOnlyMemory")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, SctpIPv4AddressParameter parameter)
    {
        var actual = parameter.ToReadOnlyMemory();
        Assert.Equal(expected.ToArray(), actual.ToArray());
    }
}
