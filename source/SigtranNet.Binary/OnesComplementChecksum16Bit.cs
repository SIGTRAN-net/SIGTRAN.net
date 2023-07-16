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

using System.Buffers.Binary;

namespace SigtranNet.Binary;

/// <summary>
/// A One's Complement Checksum algorithm for an unsigned 16-bit checksum.
/// </summary>
public static class OnesComplementChecksum16Bit
{
    /// <summary>
    /// Generates the checksum based on the provided data.
    /// </summary>
    /// <param name="data">The data for which to calculate the checksum.</param>
    /// <returns>The calculated checksum.</returns>
    public static ushort Generate(ReadOnlyMemory<byte> data) =>
        Generate(data.Span);

    /// <summary>
    /// Generates the checksum based on the provided data.
    /// </summary>
    /// <param name="data">The data for which to calculate the checksum.</param>
    /// <returns>The calculated checksum.</returns>
    public static ushort Generate(ReadOnlySpan<byte> data)
    {
        uint checksum = 0u;
        for (var i = 0; i < data.Length; i += sizeof(ushort))
        {
            checksum += BinaryPrimitives.ReadUInt16BigEndian(data[i..(i + sizeof(ushort))]);
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
    public static bool Validate(ReadOnlyMemory<byte> data) =>
        Validate(data.Span);

    /// <summary>
    /// Validates the checksum, presuming that the checksum is already in the data.
    /// </summary>
    /// <param name="data">The data to validate.</param>
    /// <returns>A <see langword="bool" /> that indicates whether the checksum is valid.</returns>
    public static bool Validate(ReadOnlySpan<byte> data)
    {
        uint checksum = 0u;
        for (var i = 0; i < data.Length; i += sizeof(ushort))
        {
            checksum += BinaryPrimitives.ReadUInt16BigEndian(data[i..(i + sizeof(ushort))]);
        }
        var carry = checksum & 0xffff0000;
        checksum -= carry;
        checksum += carry >> 16;
        return checksum == 0xffff; // skip one's complement for efficiency
    }
}
