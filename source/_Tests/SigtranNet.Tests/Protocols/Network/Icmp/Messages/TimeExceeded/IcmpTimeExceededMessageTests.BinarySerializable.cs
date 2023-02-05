/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Network.Icmp.Messages;
using SigtranNet.Protocols.Network.Icmp.Messages.TimeExceeded;
using SigtranNet.Protocols.Network.IP;
using SigtranNet.Protocols.Network.IP.IPv4;
using SigtranNet.Protocols.Network.IP.IPv4.Options;
using System.Net;

namespace SigtranNet.Tests.Protocols.Network.Icmp.Messages.TimeExceeded;

public partial class IcmpTimeExceededMessageTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    /* ICMP Time Exceeded message */
                    (byte)IcmpMessageType.TimeExceeded,
                    (byte)IcmpTimeExceededCode.FragmentReassemblyTimeExceeded,
                    58602 >> 8, 58602 & 0xFF,
                    0, 0, 0, 0,

                    /* Internet header of the original datagram */
                    (byte)IPVersion.IPv4 << 4 | 5,
                    (byte)(IPv4TypeOfService.PrecedencePriority | IPv4TypeOfService.ThroughputHigh | IPv4TypeOfService.ReliabilityHigh),
                    300 >> 8, 300 & 0xFF,
                    0, 1,
                    (byte)IPv4Flags.DontFragment << 5 | 0, 0,
                    64,
                    (byte)IPProtocol.SCTP,
                    41915 >> 8, 41915 & 0xFF,
                    192, 168, 10, 11,
                    192, 168, 10, 10,
                    // First 64 bits of the Original Data Datagram.
                    1, 2, 3, 4,
                    5, 6, 7, 8,
                }),
                new IcmpTimeExceededMessage(
                    IcmpTimeExceededCode.FragmentReassemblyTimeExceeded,
                    new IPv4Header(
                        IPv4TypeOfService.PrecedencePriority | IPv4TypeOfService.ThroughputHigh | IPv4TypeOfService.ReliabilityHigh,
                        300,
                        1,
                        IPv4Flags.DontFragment,
                        0,
                        64,
                        IPProtocol.SCTP,
                        new IPAddress(new byte[] { 192, 168, 10, 11 }),
                        new IPAddress(new byte[] { 192, 168, 10, 10 }),
                        new ReadOnlyMemory<IIPv4Option>()),
                    new ReadOnlyMemory<byte>(new byte[]
                    {
                        1, 2, 3, 4,
                        5, 6, 7, 8
                    }))
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
