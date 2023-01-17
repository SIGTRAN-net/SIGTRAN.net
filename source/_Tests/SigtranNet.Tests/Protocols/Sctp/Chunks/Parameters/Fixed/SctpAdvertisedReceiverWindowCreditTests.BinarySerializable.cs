/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Sctp.Chunks.Parameters;
using SigtranNet.Protocols.Sctp.Chunks.Parameters.Fixed;

namespace SigtranNet.Tests.Protocols.Sctp.Chunks.Parameters.Fixed;

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
