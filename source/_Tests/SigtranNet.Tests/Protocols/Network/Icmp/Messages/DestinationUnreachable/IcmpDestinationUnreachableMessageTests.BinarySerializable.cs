/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Network.Icmp.Messages;
using SigtranNet.Protocols.Network.Icmp.Messages.DestinationUnreachable;
using SigtranNet.Protocols.Network.IP;
using SigtranNet.Protocols.Network.IP.IPv4;
using SigtranNet.Protocols.Network.IP.IPv4.Options;
using System.Net;

namespace SigtranNet.Tests.Protocols.Network.Icmp.Messages.DestinationUnreachable;

public partial class IcmpDestinationUnreachableMessageTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    /* ICMP Destination Unreachable Message */
                    (byte)IcmpMessageType.DestinationUnreachable,                                   // Type
                    (byte)IcmpDestinationUnreachableCode.ProtocolUnreachable,                       // Code
                    60649 >> 8, 60649 & 0xFF,                                                       // Checksum
                    0, 0, 0, 0,                                                                     // unused

                    // IP Header Original
                    (byte)IPVersion.IPv4 << 4 | 5,                                                  // Version | Internet Header Length
                    (byte)(IPv4TypeOfService.PrecedenceCriticEcp
                            | IPv4TypeOfService.ThroughputHigh
                            | IPv4TypeOfService.ReliabilityHigh),                                   // Type of Service
                    200 >> 8, 200 & 0xFF,                                                           // Total Length
                    0, 1,                                                                           // Identification
                    (byte)IPv4Flags.DontFragment << 5 | 0, 0,                                       // Flags ; Fragment Offset
                    64,                                                                             // Time to Live
                    (byte)IPProtocol.SCTP,                                                          // Protocol
                    41887 >> 8, 41887 & 0xFF,                                                       // Header Checksum
                    192, 168, 10, 11,                                                               // Source Address
                    192, 168, 10, 10,                                                               // Destination Address
                    // 64 bits of Original Data Datagram
                    1, 2, 3, 4,
                    5, 6, 7, 8,
                }),
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
                        IPProtocol.SCTP,
                        new IPAddress(new byte[] { 192, 168, 10, 11 }),
                        new IPAddress(new byte[] { 192, 168, 10, 10 }),
                        new ReadOnlyMemory<IIPv4Option>()),
                    new ReadOnlyMemory<byte>(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 }))
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
