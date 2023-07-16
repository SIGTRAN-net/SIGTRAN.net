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

namespace SigtranNet.Protocols.Transport.Sctp.Tests.Packets.Chunks.Parameters.ErrorCause;

public partial class SctpStaleCookieErrorTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    /* Parameter Header */
                    (ushort)SctpErrorCauseCode.StaleCookie >> 8,
                    (ushort)SctpErrorCauseCode.StaleCookie & 0xFF,
                    0, 8,
                    /* Measure of Staleness */
                    2345678 >> 24,
                    (2345678 & 0x00FF0000) >> 16,
                    (2345678 & 0x0000FF00) >> 8,
                    2345678 & 0xFF,
                }),
                new SctpStaleCookieError(2345678)
            }
        };

    [Theory(DisplayName = "SctpStaleCookieError :: FromReadOnlyMemory")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, SctpStaleCookieError expected)
    {
        var actual = SctpStaleCookieError.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = "SctpStaleCookieError :: ToReadOnlyMemory")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, SctpStaleCookieError parameter)
    {
        var actual = parameter.ToReadOnlyMemory();
        Assert.True(expected.Span.SequenceEqual(actual.Span));
    }
}
