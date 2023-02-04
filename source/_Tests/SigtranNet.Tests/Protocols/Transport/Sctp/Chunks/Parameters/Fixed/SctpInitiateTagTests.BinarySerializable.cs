/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Fixed;

namespace SigtranNet.Tests.Protocols.Transport.Sctp.Chunks.Parameters.Fixed;

public class SctpInitiateTagTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    (byte)(23u >> 24),
                    (byte)((23u & 0x00FF0000) >> 16),
                    (byte)((23u & 0x0000FF00) >> 8),
                    (byte)(23u & 0x000000FF)
                }),
                new SctpInitiateTag(23u)
            }
        };

    [Theory(DisplayName = "SctpInitiateTag :: FromReadOnlyMemory")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, SctpInitiateTag expected)
    {
        var actual = SctpInitiateTag.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "SctpInitiateTag :: ToReadOnlyMemory")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, SctpInitiateTag parameter)
    {
        var actual = parameter.ToReadOnlyMemory();
        Assert.Equal(expected.ToArray(), actual.ToArray());
    }
}
