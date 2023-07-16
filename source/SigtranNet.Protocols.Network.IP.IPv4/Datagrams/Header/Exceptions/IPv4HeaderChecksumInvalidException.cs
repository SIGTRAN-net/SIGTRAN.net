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

using SigtranNet.Protocols.Network.IP.IPv4.Exceptions;

namespace SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header.Exceptions;

/// <summary>
/// An exception that is thrown if an IPv4 header is corrupted or has an invalid checksum.
/// </summary>
internal sealed class IPv4HeaderChecksumInvalidException
    : IPv4Exception
{
    /// <summary>
    /// Initializes a new instance of <see cref="IPv4HeaderChecksumInvalidException" />.
    /// </summary>
    /// <param name="checksum">The invalid checksum.</param>
    internal IPv4HeaderChecksumInvalidException(ushort checksum)
        : base(CreateExceptionMessage(checksum))
    {
        Checksum = checksum;
    }

    /// <summary>
    /// Gets the invalid IPv4 header checksum.
    /// </summary>
    internal ushort Checksum { get; }

    private static string CreateExceptionMessage(ushort checksum) =>
        string.Format(ExceptionMessages.HeaderChecksumInvalid, checksum);
}
