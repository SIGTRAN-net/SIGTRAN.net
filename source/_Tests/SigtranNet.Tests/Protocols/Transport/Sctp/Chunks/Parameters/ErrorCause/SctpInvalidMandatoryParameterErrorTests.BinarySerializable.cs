/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause.Exceptions;

namespace SigtranNet.Tests.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause;

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
