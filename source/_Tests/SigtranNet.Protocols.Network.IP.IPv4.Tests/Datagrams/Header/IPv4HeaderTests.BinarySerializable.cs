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

using SigtranNet.Binary;
using SigtranNet.Protocols.Network.IP;
using SigtranNet.Protocols.Network.IP.IPv4.Datagrams;
using SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header;
using SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header.Options;
using SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header.Options.LooseSourceRouting;
using SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header.Options.NoOperation;
using System.Net;

namespace SigtranNet.Tests.Protocols.Network.IP.IPv4;

public partial class IPv4HeaderTests
{
    public static readonly IEnumerable<object?[]> IPv4HeaderParameters =
        new[]
        {
            new object?[]
            {
                new byte[]
                {
                    0b0100_0101,                                        // Version, IHL
                    (byte)(IPv4TypeOfService.PrecedenceCriticEcp
                        | IPv4TypeOfService.ThroughputHigh
                        | IPv4TypeOfService.ReliabilityHigh),           // Type of Service
                    576 >> 8, 576 & 0xF0,                               // Total Length
                    0, 1,                                               // Identification
                    0b001_00000, 12,                                    // Various Control Flags, Fragment Offset
                    5,                                                  // Time to Live (TTL)
                    (byte)InternetProtocol.UDP,                               // Protocol
                    21322 >> 8, 21322 & 0xFF,                           // Header Checksum
                    127, 0, 0, 1,                                       // Source Address,
                    192, 168, 0, 1,                                     // Destination Address
                },
                new IPv4Header(
                    typeOfService: IPv4TypeOfService.PrecedenceCriticEcp | IPv4TypeOfService.ThroughputHigh | IPv4TypeOfService.ReliabilityHigh,
                    totalLength: 576,
                    identification: 1,
                    flags: IPv4Flags.MoreFragments,
                    fragmentOffset: 12,
                    timeToLive: 5,
                    protocol: InternetProtocol.UDP,
                    sourceAddress: new IPAddress(new byte[] { 127, 0, 0, 1 }),
                    destinationAddress: new IPAddress(new byte[] { 192, 168, 0, 1 }),
                    options: new ReadOnlyMemory<IIPv4Option>())
            },
            new object?[]
            {
                new byte[]
                {
                    0b0100_1000,                                        // Version, IHL
                    (byte)(IPv4TypeOfService.PrecedenceCriticEcp
                        | IPv4TypeOfService.ThroughputHigh
                        | IPv4TypeOfService.ReliabilityHigh),           // Type of Service
                    576 >> 8, 576 & 0xF0,                               // Total Length
                    0, 2,                                               // Identification
                    0b000_00000, 15,                                    // Various Control Flags, Fragment Offset
                    3,                                                  // Time to Live (TTL)
                    (byte)InternetProtocol.ICMP,                              // Protocol
                    39137 >> 8, 39137 & 0xFF,                           // Header Checksum
                    192, 168, 21, 22,                                   // Source Address,
                    192, 168, 21, 23,                                   // Destination Address
                    /* Options */
                    (byte)IPv4OptionType.NotCopied_Control_LooseSourceRouting,
                    11,
                    4,
                    192, 168, 10, 10,
                    192, 168, 10, 11,
                    1
                },
                new IPv4Header(
                    typeOfService: IPv4TypeOfService.PrecedenceCriticEcp | IPv4TypeOfService.ThroughputHigh | IPv4TypeOfService.ReliabilityHigh,
                    totalLength: 576,
                    identification: 2,
                    flags: IPv4Flags.LastFragment,
                    fragmentOffset: 15,
                    timeToLive: 3,
                    protocol: InternetProtocol.ICMP,
                    sourceAddress: new IPAddress(new byte[] { 192, 168, 21, 22 }),
                    destinationAddress: new IPAddress(new byte[] { 192, 168, 21, 23 }),
                    options: new ReadOnlyMemory<IIPv4Option>(new IIPv4Option[]
                    {
                        new IPv4OptionLooseSourceRecordRoute(
                            IPv4OptionType.NotCopied_Control_LooseSourceRouting,
                            4,
                            new ReadOnlyMemory<IPAddress>(new IPAddress[]
                            {
                                new IPAddress(new byte[] { 192, 168, 10, 10 }),
                                new IPAddress(new byte[] { 192, 168, 10, 11 })
                            })),
                        new IPv4OptionNoOperation()
                    }))
            }
        };

    [Theory(DisplayName = "IPv4Header :: Read")]
    [MemberData(nameof(IPv4HeaderParameters))]
    internal void ReadTests(byte[] data, IPv4Header expected)
    {
        // Arrange
        using var memoryStream = new MemoryStream(data);
        using var binaryReader = new BinaryReader(memoryStream);

        // Act
        var actual = IPv4Header.Read(binaryReader);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "IPv4Header :: Write")]
    [MemberData(nameof(IPv4HeaderParameters))]
    internal void WriteTests(byte[] expected, IPv4Header header)
    {
        // Arrange
        using var memoryStream = new MemoryStream();
        using var binaryWriter = new BinaryWriter(memoryStream);

        // Act
        ((IBinarySerializable)header).Write(binaryWriter);
        binaryWriter.Flush();
        var actual = memoryStream.ToArray();

        // Assert
        Assert.Equal(expected, actual);
    }
}
