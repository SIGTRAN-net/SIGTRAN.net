/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Sctp.Chunks.Parameters.ErrorCause;
using SigtranNet.Protocols.Sctp.Chunks.Parameters.ErrorCause.Exceptions;

namespace SigtranNet.Tests.Protocols.Sctp.Chunks.Parameters.ErrorCause;

public partial class SctpCookieReceivedWhileShuttingDownErrorTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    (ushort)SctpErrorCauseCode.CookieReceivedWhileShuttingDown >> 8,
                    (ushort)SctpErrorCauseCode.CookieReceivedWhileShuttingDown & 0xFF,
                    0, 4
                }),
                null
            },
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    (ushort)SctpErrorCauseCode.ProtocolViolation >> 8,
                    (ushort)SctpErrorCauseCode.ProtocolViolation & 0xFF,
                    0, 4
                }),
                new SctpErrorCauseCodeInvalidException(SctpErrorCauseCode.ProtocolViolation)
            },
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    (ushort)SctpErrorCauseCode.CookieReceivedWhileShuttingDown >> 8,
                    (ushort)SctpErrorCauseCode.CookieReceivedWhileShuttingDown & 0xFF,
                    0, 8
                }),
                new SctpErrorCauseLengthInvalidException(SctpErrorCauseCode.CookieReceivedWhileShuttingDown, 8)
            }
        };

    [Theory(DisplayName = $"{nameof(SctpCookieReceivedWhileShuttingDownError)} :: {nameof(SctpCookieReceivedWhileShuttingDownError.FromReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, Exception? exceptionExpected)
    {
        if (exceptionExpected is null)
        {
            var actual = SctpCookieReceivedWhileShuttingDownError.FromReadOnlyMemory(memory);
            Assert.IsType<SctpCookieReceivedWhileShuttingDownError>(actual);
        }
        else
        {
            var exceptionActual = Assert.Throws(exceptionExpected.GetType(), () => SctpCookieReceivedWhileShuttingDownError.FromReadOnlyMemory(memory));
            Assert.Equal(exceptionExpected, exceptionActual);
        }
    }

    [Fact(DisplayName = $"{nameof(SctpCookieReceivedWhileShuttingDownError)} :: {nameof(SctpCookieReceivedWhileShuttingDownError.ToReadOnlyMemory)}")]
    internal void ToReadOnlyMemoryTest()
    {
        // Arrange
        var expected = new ReadOnlyMemory<byte>(new byte[]
        {
            (ushort)SctpErrorCauseCode.CookieReceivedWhileShuttingDown >> 8,
            (ushort)SctpErrorCauseCode.CookieReceivedWhileShuttingDown & 0xFF,
            0, 4
        });
        var parameter = new SctpCookieReceivedWhileShuttingDownError();

        // Act
        var actual = parameter.ToReadOnlyMemory();

        // Assert
        Assert.True(expected.Span.SequenceEqual(actual.Span));
    }
}
