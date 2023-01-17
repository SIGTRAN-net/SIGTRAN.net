/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Binary;

/// <summary>
/// A One's Complement Checksum algorithm for an unsigned 16-bit checksum.
/// </summary>
internal static class OnesComplementChecksum16Bit
{
    /// <summary>
    /// Generates the checksum based on the provided data.
    /// </summary>
    /// <param name="data">The data for which to calculate the checksum.</param>
    /// <returns>The calculated checksum.</returns>
    internal static ushort Generate(ReadOnlyMemory<ushort> data)
    {
        uint checksum = 0u;
        foreach (var chunk in data.Span)
        {
            checksum += chunk;
        }
        var carry = checksum & 0xffff0000;
        checksum -= carry;
        checksum += carry >> 16;
        if (checksum > ushort.MaxValue)
        {
            carry = checksum & 0xffff0000;
            checksum -= carry;
            checksum += carry >> 16;
        }
        return (ushort)~checksum; // one's complement
    }

    /// <summary>
    /// Validates the checksum, presuming that the checksum is already in the data.
    /// </summary>
    /// <param name="data">The data to validate.</param>
    /// <returns>A <see langword="bool" /> that indicates whether the checksum is valid.</returns>
    internal static bool Validate(ReadOnlyMemory<ushort> data)
    {
        uint checksum = 0u;
        foreach (var chunk in data.Span)
        {
            checksum += chunk;
        }
        var carry = checksum & 0xffff0000;
        checksum -= carry;
        checksum += carry >> 16;
        return checksum == 0xffff; // skip one's complement for efficiency
    }
}
