/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Sctp.Chunks.Parameters.ErrorCause;
using SigtranNet.Protocols.Sctp.Chunks.Parameters.ErrorCause.Exceptions;

namespace SigtranNet.Tests.Protocols.Sctp.Chunks.Parameters.ErrorCause;

public partial class SctpOutOfResourceErrorTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    (ushort)SctpErrorCauseCode.OutOfResource >> 8,
                    (ushort)SctpErrorCauseCode.OutOfResource & 0xFF,
                    0, 4
                }),
                null
            },
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    (ushort)SctpErrorCauseCode.StaleCookie >> 8,
                    (ushort)SctpErrorCauseCode.StaleCookie & 0xFF,
                    0, 4
                }),
                new SctpErrorCauseCodeInvalidException(SctpErrorCauseCode.StaleCookie)
            },
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    (ushort)SctpErrorCauseCode.OutOfResource >> 8,
                    (ushort)SctpErrorCauseCode.OutOfResource & 0xFF,
                    0, 5,
                    1
                }),
                new SctpErrorCauseLengthInvalidException(SctpErrorCauseCode.OutOfResource, 5)
            }
        };

    [Theory(DisplayName = "SctpOutOfResourceError :: FromReadOnlyMemory")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, Exception? exception)
    {
        if (exception is not null)
        {
            Assert.Throws(exception.GetType(), () => SctpOutOfResourceError.FromReadOnlyMemory(memory));
        }
        else
        {
            var parameter = SctpOutOfResourceError.FromReadOnlyMemory(memory);
            Assert.IsType<SctpOutOfResourceError>(parameter);
        }
    }

    [Fact(DisplayName = "SctpOutOfResourceError :: ToReadOnlyMemory")]
    internal void ToReadOnlyMemoryTest()
    {
        var memory = new SctpOutOfResourceError().ToReadOnlyMemory();
        Assert.True(memory.Span.SequenceEqual(new Span<byte>(new byte[]
        {
            (ushort)SctpErrorCauseCode.OutOfResource >> 8,
            (ushort)SctpErrorCauseCode.OutOfResource & 0xFF,
            0, 4
        })));
    }
}
