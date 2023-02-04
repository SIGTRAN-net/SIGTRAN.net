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
