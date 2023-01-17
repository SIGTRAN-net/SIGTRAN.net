/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Sctp.Chunks.Parameters.Fixed;

namespace SigtranNet.Tests.Protocols.Sctp.Chunks.Parameters.Fixed;

public partial class SctpNumberInboundStreamsTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    12345 >> 8,
                    12345 & 0xFF
                }),
                new SctpNumberInboundStreams(12345)
            }
        };

    [Theory(DisplayName = $"{nameof(SctpNumberInboundStreams)} :: {nameof(SctpNumberInboundStreams.FromReadOnlyMemory)}")]
    [MemberData(nameof( ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, SctpNumberInboundStreams expected)
    {
        var actual = SctpNumberInboundStreams.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = $"{nameof(SctpNumberInboundStreams)} :: {nameof(SctpNumberInboundStreams.ToReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, SctpNumberInboundStreams parameter)
    {
        var actual = parameter.ToReadOnlyMemory();
        Assert.True(expected.Span.SequenceEqual(actual.Span));
    }
}
