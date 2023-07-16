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

using SigtranNet.Binary;
using System.Buffers.Binary;

namespace SigtranNet.Tests.Binary;

public class CRC32cChecksumTests
{
    public static readonly IEnumerable<object?[]> Samples =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(
                    Enumerable
                        .Empty<byte>()
                        .Concat(new byte[] { 0, 0, 0, 0 })
                        .Concat(new byte[] { 0x8C, 0x28, 0xB2, 0x8A })
                        .ToArray()),
                4,
                0x8C28B28A
            },
            new object?[]
            {
                new ReadOnlyMemory<byte>(
                    Enumerable
                        .Empty<byte>()
                        .Concat(new byte[] { 1, 2, 3, 4 })
                        .Concat(new byte[] { 0xF3, 0xE5, 0x82, 0x5E })
                        .Concat(new byte[] { 4, 3, 2, 1 })
                        .ToArray()),
                4,
                0xF3E5825E
            }
        };

    [Theory(DisplayName = $"{nameof(CRC32cChecksum)} :: {nameof(CRC32cChecksum.Generate)}")]
    [MemberData(nameof(Samples))]
    internal void GenerateTests(ReadOnlyMemory<byte> data, int checksumIndex, uint expected)
    {
        var actual = CRC32cChecksum.Generate(data, checksumIndex);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = $"{nameof(CRC32cChecksum)} :: {nameof(CRC32cChecksum.Validate)}")]
    [MemberData(nameof(Samples))]
    internal void ValidateTests(ReadOnlyMemory<byte> data, int checksumIndex, uint expected)
    {
        var actual = BinaryPrimitives.ReadUInt32BigEndian(data.Span[checksumIndex..]);
        var isValid = CRC32cChecksum.Validate(data, checksumIndex);
        Assert.True(isValid);
        Assert.Equal(expected, actual);
    }
}
