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

using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Fixed;

namespace SigtranNet.Protocols.Transport.Sctp.Tests.Packets.Chunks.Parameters.Fixed;

public class SctpAdvertisedReceiverWindowCreditTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    (byte)(23000u >> 24),
                    (byte)((23000u & 0x00FF0000) >> 16),
                    (byte)((23000u & 0x0000FF00) >> 8),
                    (byte)(23000u & 0x000000FF)
                }),
                new SctpAdvertisedReceiverWindowCredit(23000)
            }
        };

    [Theory(DisplayName = "SctpAdvertisedReceiverWindowCredit :: FromReadOnlyMemory")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, SctpAdvertisedReceiverWindowCredit expected)
    {
        var actual = SctpAdvertisedReceiverWindowCredit.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "SctpAdvertisedReceiverWindowCredit :: ToReadOnlyMemory")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, SctpAdvertisedReceiverWindowCredit parameter)
    {
        var actual = parameter.ToReadOnlyMemory();
        Assert.Equal(expected.ToArray(), actual.ToArray());
    }
}
