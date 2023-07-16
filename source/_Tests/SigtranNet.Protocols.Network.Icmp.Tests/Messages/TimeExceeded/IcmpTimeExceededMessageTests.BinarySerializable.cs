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
using SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.TimeExceeded;
using System.Net;

namespace SigtranNet.Protocols.Network.IP.IPv4.Icmp.Tests.Messages.TimeExceeded;

public partial class IcmpTimeExceededMessageTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(
                [
                    /* ICMP Time Exceeded message */
                    (byte)IcmpMessageType.TimeExceeded,
                    (byte)IcmpTimeExceededCode.FragmentReassemblyTimeExceeded,
                    58602 >> 8, 58602 & 0xFF,
                    0, 0, 0, 0,

                    /* Internet header of the original datagram */
                    (byte)InternetProtocolVersion.IPv4 << 4 | 5,
                    (byte)(IPv4TypeOfService.PrecedencePriority | IPv4TypeOfService.ThroughputHigh | IPv4TypeOfService.ReliabilityHigh),
                    300 >> 8, 300 & 0xFF,
                    0, 1,
                    (byte)IPv4Flags.DontFragment << 5 | 0, 0,
                    64,
                    (byte)InternetProtocol.SCTP,
                    41915 >> 8, 41915 & 0xFF,
                    192, 168, 10, 11,
                    192, 168, 10, 10,
                    // First 64 bits of the Original Data Datagram.
                    1, 2, 3, 4,
                    5, 6, 7, 8,
                ]),
                new IcmpTimeExceededMessage(
                    IcmpTimeExceededCode.FragmentReassemblyTimeExceeded,
                    new IPv4Header(
                        IPv4TypeOfService.PrecedencePriority | IPv4TypeOfService.ThroughputHigh | IPv4TypeOfService.ReliabilityHigh,
                        300,
                        1,
                        IPv4Flags.DontFragment,
                        0,
                        64,
                        InternetProtocol.SCTP,
                        new IPAddress(new byte[] { 192, 168, 10, 11 }),
                        new IPAddress(new byte[] { 192, 168, 10, 10 }),
                        new ReadOnlyMemory<IIPv4Option>()),
                    new ReadOnlyMemory<byte>(
                    [
                        1, 2, 3, 4,
                        5, 6, 7, 8
                    ]))
            }
        };

    [Theory(DisplayName = $"{nameof(IcmpTimeExceededMessage)} :: {nameof(IcmpTimeExceededMessage.FromReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, IcmpTimeExceededMessage expected)
    {
        var actual = IcmpTimeExceededMessage.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = $"{nameof(IcmpTimeExceededMessage)} :: {nameof(IcmpTimeExceededMessage.ToReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, IcmpTimeExceededMessage message)
    {
        var actual = message.ToReadOnlyMemory();
        Assert.True(expected.Span.SequenceEqual(actual.Span));
    }
}
