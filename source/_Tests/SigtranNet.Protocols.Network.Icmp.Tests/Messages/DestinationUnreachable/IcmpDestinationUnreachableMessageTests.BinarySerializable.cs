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
using SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header;
using SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header.Options;
using SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages;
using SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.DestinationUnreachable;
using System.Net;

namespace SigtranNet.Protocols.Network.IP.IPv4.Icmp.Tests.Messages.DestinationUnreachable;

public partial class IcmpDestinationUnreachableMessageTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(
                [
                    /* ICMP Destination Unreachable Message */
                    (byte)IcmpMessageType.DestinationUnreachable,                                   // Type
                    (byte)IcmpDestinationUnreachableCode.ProtocolUnreachable,                       // Code
                    60649 >> 8, 60649 & 0xFF,                                                       // Checksum
                    0, 0, 0, 0,                                                                     // unused

                    // IP Header Original
                    (byte)InternetProtocolVersion.IPv4 << 4 | 5,                                                  // Version | Internet Header Length
                    (byte)(IPv4TypeOfService.PrecedenceCriticEcp
                            | IPv4TypeOfService.ThroughputHigh
                            | IPv4TypeOfService.ReliabilityHigh),                                   // Type of Service
                    200 >> 8, 200 & 0xFF,                                                           // Total Length
                    0, 1,                                                                           // Identification
                    (byte)IPv4Flags.DontFragment << 5 | 0, 0,                                       // Flags ; Fragment Offset
                    64,                                                                             // Time to Live
                    (byte)InternetProtocol.SCTP,                                                          // Protocol
                    41887 >> 8, 41887 & 0xFF,                                                       // Header Checksum
                    192, 168, 10, 11,                                                               // Source Address
                    192, 168, 10, 10,                                                               // Destination Address
                    // 64 bits of Original Data Datagram
                    1, 2, 3, 4,
                    5, 6, 7, 8,
                ]),
                new IcmpDestinationUnreachableMessage(
                    IcmpDestinationUnreachableCode.ProtocolUnreachable,
                    new IPv4Header(
                        IPv4TypeOfService.PrecedenceCriticEcp
                            | IPv4TypeOfService.ThroughputHigh
                            | IPv4TypeOfService.ReliabilityHigh,
                        200,
                        1,
                        IPv4Flags.DontFragment,
                        0,
                        64,
                        InternetProtocol.SCTP,
                        new IPAddress(new byte[] { 192, 168, 10, 11 }),
                        new IPAddress(new byte[] { 192, 168, 10, 10 }),
                        new ReadOnlyMemory<IIPv4Option>()),
                    new ReadOnlyMemory<byte>([1, 2, 3, 4, 5, 6, 7, 8]))
            }
        };

    [Theory(DisplayName = $"{nameof(IcmpDestinationUnreachableMessage)} :: {nameof(IcmpDestinationUnreachableMessage.FromReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, IcmpDestinationUnreachableMessage expected)
    {
        var actual = IcmpDestinationUnreachableMessage.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = $"{nameof(IcmpDestinationUnreachableMessage)} :: {nameof(IcmpDestinationUnreachableMessage.ToReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, IcmpDestinationUnreachableMessage message)
    {
        var actual = message.ToReadOnlyMemory();
        Assert.True(expected.Span.SequenceEqual(actual.Span));
    }
}
