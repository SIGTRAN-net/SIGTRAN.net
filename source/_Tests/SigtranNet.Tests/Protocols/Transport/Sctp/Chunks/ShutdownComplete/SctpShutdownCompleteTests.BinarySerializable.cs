/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks;
using SigtranNet.Protocols.Transport.Sctp.Chunks.ShutdownComplete;

namespace SigtranNet.Tests.Protocols.Transport.Sctp.Chunks.ShutdownComplete;

public partial class SctpShutdownCompleteTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    (byte)SctpChunkType.ShutdownComplete,
                    (byte)SctpShutdownCompleteFlags.VerificationTagExpected,
                    0, 4
                }),
                new SctpShutdownComplete(SctpShutdownCompleteFlags.VerificationTagExpected)
            },
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    (byte)SctpChunkType.ShutdownComplete,
                    (byte)SctpShutdownCompleteFlags.VerificationTagReflected,
                    0, 4
                }),
                new SctpShutdownComplete(SctpShutdownCompleteFlags.VerificationTagReflected)
            }
        };

    [Theory(DisplayName = $"{nameof(SctpShutdownComplete)} :: {nameof(SctpShutdownComplete.FromReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemory(ReadOnlyMemory<byte> memory, SctpShutdownComplete expected)
    {
        var actual = SctpShutdownComplete.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = $"{nameof(SctpShutdownComplete)} :: {nameof(SctpShutdownComplete.FromReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemory(ReadOnlyMemory<byte> expected, SctpShutdownComplete chunk)
    {
        var actual = chunk.ToReadOnlyMemory();
        Assert.True(expected.Span.SequenceEqual(actual.Span));
    }
}
