/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;

namespace SigtranNet.Tests.Binary;

public class OnesComplementChecksum16BitTests
{
    /// <summary>
    /// Example from <a href="https://en.wikipedia.org/wiki/Internet_checksum">Internet checksum</a>.
    /// </summary>
    private static readonly ReadOnlyMemory<ushort> Data =
        new(new ushort[]
            {
                0x4500, 0x0073, 0x0000, 0x4000, 0x4011, 0x0000, 0xc0a8, 0x0001,
                0xc0a8, 0x00c7
            });

    /// <summary>
    /// Example from <a href="https://en.wikipedia.org/wiki/Internet_checksum">Internet checksum</a>.
    /// </summary>
    private static readonly ReadOnlyMemory<ushort> DataWithChecksum =
        new(new ushort[]
        {
                0x4500, 0x0073, 0x0000, 0x4000, 0x4011, 0xb861, 0xc0a8, 0x0001,
                0xc0a8, 0x00c7
        });

    [Fact(DisplayName = "OnesComplementChecksum16Bit :: Calculate")]
    internal void CalculateTest()
    {
        const ushort Expected = 0xb861;
        var actual = OnesComplementChecksum16Bit.Calculate(Data);
        Assert.Equal(Expected, actual);
    }

    [Fact(DisplayName = "OnesComplementChecksum16Bit :: Validate")]
    internal void ValidateTest()
    {
        var actual = OnesComplementChecksum16Bit.Validate(DataWithChecksum);
        Assert.True(actual);
    }
}
