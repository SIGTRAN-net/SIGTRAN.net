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
