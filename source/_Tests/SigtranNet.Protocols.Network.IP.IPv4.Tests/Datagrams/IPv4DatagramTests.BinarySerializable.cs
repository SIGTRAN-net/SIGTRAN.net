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

using SigtranNet.Protocols.Network.IP.IPv4.Datagrams;
using SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header.Options;
using System.Net;

namespace SigtranNet.Protocols.Network.IP.IPv4.Tests.Datagrams;

public partial class IPv4DatagramTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    /* Internet Header */
                    (byte)InternetProtocolVersion.IPv4 << 4 | 5,                                                      // Version + IHL,
                    (byte)(IPv4TypeOfService.PrecedenceCriticEcp
                            | IPv4TypeOfService.ThroughputHigh
                            | IPv4TypeOfService.ReliabilityHigh),                                       // Type of Service
                    32 >> 8, 32 & 0xFF,                                                                 // Total Length
                    200 >> 8, 200 & 0xFF,                                                               // Identification
                    (byte)IPv4Flags.DontFragment << 5, 15 & 0xFF,                                       // Flags + Fragment Offset
                    5,                                                                                  // Time to Live
                    (byte)InternetProtocol.ExperimentationAndTesting_253,                                     // Protocol
                    64682 >> 8, 64682 & 0xFF,                                                           // Header Checksum
                    172, 0, 0, 1,                                                                       // Source Address
                    192, 168, 10, 10,                                                                   // Destination Address

                    /* Payload */
                    1, 2, 3, 4,
                    4, 3, 2, 1,
                    1, 2, 3, 4,
                }),
                new IPv4Datagram(
                        typeOfService:
                            IPv4TypeOfService.PrecedenceCriticEcp
                            | IPv4TypeOfService.ThroughputHigh
                            | IPv4TypeOfService.ReliabilityHigh,
                        identification: 200,
                        flags: IPv4Flags.DontFragment,
                        fragmentOffset: 15,
                        timeToLive: 5,
                        protocol: InternetProtocol.ExperimentationAndTesting_253,
                        sourceAddress: new IPAddress(new byte[] { 172, 0, 0, 1 }),
                        destinationAddress: new IPAddress(new byte[] { 192, 168, 10, 10 }),
                        options: new ReadOnlyMemory<IIPv4Option>(),
                        payload: new ReadOnlyMemory<byte>(new byte[]
                        {
                            1, 2, 3, 4,
                            4, 3, 2, 1,
                            1, 2, 3, 4
                        })
                    )
            }
        };

    [Theory(DisplayName = $"{nameof(IPv4Datagram)} :: {nameof(IPv4Datagram.FromReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, IPv4Datagram expected)
    {
        var actual = IPv4Datagram.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = $"{nameof(IPv4Datagram)} :: {nameof(IPv4Datagram.ToReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, IPv4Datagram datagram)
    {
        var actual = datagram.ToReadOnlyMemory();
        Assert.True(expected.Span.SequenceEqual(actual.Span));
    }
}
