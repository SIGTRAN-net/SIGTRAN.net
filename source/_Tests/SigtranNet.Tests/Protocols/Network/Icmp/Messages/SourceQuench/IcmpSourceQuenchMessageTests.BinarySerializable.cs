/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Network.Icmp.Messages;
using SigtranNet.Protocols.Network.Icmp.Messages.SourceQuench;
using SigtranNet.Protocols.Network.IP;
using SigtranNet.Protocols.Network.IP.IPv4;
using SigtranNet.Protocols.Network.IP.IPv4.Options;
using System.Net;

namespace SigtranNet.Tests.Protocols.Network.Icmp.Messages.SourceQuench;

public partial class IcmpSourceQuenchMessageTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    /* ICMP Header */
                    (byte)IcmpMessageType.SourceQuench,             // Type
                    0,                                              // Code
                    60395 >> 8, 60395 & 0xFF,                       // Checksum
                    0, 0, 0, 0,                                     // unused

                    /* IPv4 Header original datagram */
                    (byte)IPVersion.IPv4 << 4 | 5,                  // Version | IHL
                    (byte)(IPv4TypeOfService.PrecedenceImmediate
                            | IPv4TypeOfService.LowDelay
                            | IPv4TypeOfService.ReliabilityHigh),   // Type of Service
                    200 >> 8, 200 & 0xFF,                           // Total Length
                    0, 1,                                           // Identification
                    (byte)IPv4Flags.DontFragment << 5 | 0, 1,       // Flags | Fragment Offset
                    20,                                             // Time to Live
                    (byte)IPProtocol.TCP,                           // Protocol
                    53364 >> 8, 53364 & 0xFF,                       // Header Checksum
                    192, 168, 10, 11,                               // Source Address
                    192, 168, 10, 10,                               // Destination Address
                    /* 64 bits of Original Data Datagram */
                    1, 2, 3, 4,
                    5, 6, 7, 8,
                }),
                new IcmpSourceQuenchMessage(
                    ipHeaderOriginal: new IPv4Header(
                        typeOfService: IPv4TypeOfService.PrecedenceImmediate | IPv4TypeOfService.LowDelay | IPv4TypeOfService.ReliabilityHigh,
                        totalLength: 200,
                        identification: 1,
                        flags: IPv4Flags.DontFragment,
                        fragmentOffset: 1,
                        timeToLive: 20,
                        protocol: IPProtocol.TCP,
                        sourceAddress: new IPAddress(new byte[] { 192, 168, 10, 11 }),
                        destinationAddress: new IPAddress(new byte[] { 192, 168, 10, 10 }),
                        options: new ReadOnlyMemory<IIPv4Option>()),
                    originalDataDatagramSample: new ReadOnlyMemory<byte>(new byte[]
                    {
                        1, 2, 3, 4,
                        5, 6, 7, 8
                    }))
            }
        };

    [Theory(DisplayName = $"{nameof(IcmpSourceQuenchMessage)} :: {nameof(IcmpSourceQuenchMessage.FromReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, IcmpSourceQuenchMessage expected)
    {
        var actual = IcmpSourceQuenchMessage.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = $"{nameof(IcmpSourceQuenchMessage)} :: {nameof(IcmpSourceQuenchMessage.ToReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, IcmpSourceQuenchMessage message)
    {
        var actual = message.ToReadOnlyMemory();
        Assert.True(expected.Span.SequenceEqual(actual.Span));
    }
}
