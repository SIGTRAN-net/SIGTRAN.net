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

namespace SigtranNet.Tests.Binary;

public class OnesComplementChecksum16BitTests
{
    /// <summary>
    /// Example from <a href="https://en.wikipedia.org/wiki/Internet_checksum">Internet checksum</a>.
    /// </summary>
    private static readonly ReadOnlyMemory<byte> Data =
        new(new byte[]
            {
                0x45, 0x00, 0x00, 0x73, 0x00, 0x00, 0x40, 0x00, 0x40, 0x11, 0x00, 0x00, 0xc0, 0xa8, 0x00, 0x01,
                0xc0, 0xa8, 0x00, 0xc7
            });

    /// <summary>
    /// Example from <a href="https://en.wikipedia.org/wiki/Internet_checksum">Internet checksum</a>.
    /// </summary>
    private static readonly ReadOnlyMemory<byte> DataWithChecksum =
        new(new byte[]
        {
                0x45, 0x00, 0x00, 0x73, 0x00, 0x00, 0x40, 0x00, 0x40, 0x11, 0xb8, 0x61, 0xc0, 0xa8, 0x00, 0x01,
                0xc0, 0xa8, 0x00, 0xc7
        });

    [Fact(DisplayName = $"{nameof(OnesComplementChecksum16Bit)} :: {nameof(OnesComplementChecksum16Bit.Generate)}")]
    internal void GenerateTest()
    {
        const ushort Expected = 0xb861;
        var actual = OnesComplementChecksum16Bit.Generate(Data);
        Assert.Equal(Expected, actual);
    }

    [Fact(DisplayName = $"{nameof(OnesComplementChecksum16Bit)} :: {nameof(OnesComplementChecksum16Bit.Validate)}")]
    internal void ValidateTest()
    {
        var actual = OnesComplementChecksum16Bit.Validate(DataWithChecksum);
        Assert.True(actual);
    }
}
