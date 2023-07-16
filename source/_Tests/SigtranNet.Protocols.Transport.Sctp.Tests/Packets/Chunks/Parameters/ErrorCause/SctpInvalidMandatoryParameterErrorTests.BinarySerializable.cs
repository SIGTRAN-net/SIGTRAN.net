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

public partial class SctpInvalidMandatoryParameterErrorTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    (ushort)SctpErrorCauseCode.InvalidMandatoryParameter >> 8,
                    (ushort)SctpErrorCauseCode.InvalidMandatoryParameter & 0xFF,
                    0, 4
                }),
                null
            },
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    (ushort)SctpErrorCauseCode.InvalidStreamIdentifier >> 8,
                    (ushort)SctpErrorCauseCode.InvalidStreamIdentifier & 0xFF,
                    0, 4
                }),
                new SctpErrorCauseCodeInvalidException(SctpErrorCauseCode.InvalidStreamIdentifier)
            },
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    (ushort)SctpErrorCauseCode.InvalidMandatoryParameter >> 8,
                    (ushort)SctpErrorCauseCode.InvalidMandatoryParameter & 0xFF,
                    0, 8
                }),
                new SctpErrorCauseLengthInvalidException(SctpErrorCauseCode.InvalidMandatoryParameter, 8)
            }
        };

    [Theory(DisplayName = "SctpInvalidMandatoryParameterError :: FromReadOnlyMemory")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, Exception? exceptionExpected)
    {
        if (exceptionExpected is not null)
        {
            var exceptionActual = Assert.Throws(exceptionExpected.GetType(), () => SctpInvalidMandatoryParameterError.FromReadOnlyMemory(memory));
            Assert.Equal(exceptionExpected, exceptionActual);
        }
        else
        {
            var actual = SctpInvalidMandatoryParameterError.FromReadOnlyMemory(memory);
            Assert.IsType<SctpInvalidMandatoryParameterError>(actual);
        }
    }

    [Fact(DisplayName = "SctpInvalidMandatoryParameterError :: ToReadOnlyMemory")]
    internal void ToReadOnlyMemoryTest()
    {
        // Arrange
        var expected = new ReadOnlyMemory<byte>(new byte[]
        {
            /* Error Cause Code */
            (ushort)SctpErrorCauseCode.InvalidMandatoryParameter >> 8,
            (ushort)SctpErrorCauseCode.InvalidMandatoryParameter & 0xFF,
            /* Error Cause Length */
            0, 4
        });

        // Act
        var actual = new SctpInvalidMandatoryParameterError().ToReadOnlyMemory();

        // Assert
        Assert.True(expected.Span.SequenceEqual(actual.Span));
    }
}
