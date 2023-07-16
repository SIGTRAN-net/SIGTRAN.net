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

using SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages;
using SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.Information;

namespace SigtranNet.Protocols.Network.IP.IPv4.Icmp.Tests.Messages.Information;

public partial class IcmpInformationMessageTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            [
                new ReadOnlyMemory<byte>(
                [
                    (byte)IcmpMessageType.InformationRequest,
                    0,
                    61674 >> 8, 61674 & 0xFF,
                    20 >> 8, 20 & 0xFF,
                    1 >> 8, 1 & 0xFF,
                ]),
                new IcmpInformationMessage(
                    false,
                    20,
                    1)
            ],
            new object?[]
            {
                new ReadOnlyMemory<byte>(
                [
                    (byte)IcmpMessageType.InformationReply,
                    0,
                    61418 >> 8, 61418 & 0xFF,
                    20 >> 8, 20 & 0xFF,
                    1 >> 8, 1 & 0xFF,
                ]),
                new IcmpInformationMessage(
                    true,
                    20,
                    1)
            }
        };

    [Theory(DisplayName = $"{nameof(IcmpInformationMessage)} :: {nameof(IcmpInformationMessage.FromReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, IcmpInformationMessage expected)
    {
        var actual = IcmpInformationMessage.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = $"{nameof(IcmpInformationMessage)} :: {nameof(IcmpInformationMessage.ToReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, IcmpInformationMessage message)
    {
        var actual = message.ToReadOnlyMemory();
        Assert.True(expected.Span.SequenceEqual(actual.Span));
    }
}
