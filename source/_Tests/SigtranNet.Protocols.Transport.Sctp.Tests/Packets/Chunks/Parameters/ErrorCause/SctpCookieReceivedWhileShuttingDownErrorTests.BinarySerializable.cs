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

using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.ErrorCause;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.ErrorCause.Exceptions;

namespace SigtranNet.Protocols.Transport.Sctp.Tests.Packets.Chunks.Parameters.ErrorCause;

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
