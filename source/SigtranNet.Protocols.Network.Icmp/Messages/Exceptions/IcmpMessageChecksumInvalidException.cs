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

using SigtranNet.Protocols.Network.IP.IPv4.Icmp.Exceptions;

namespace SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.Exceptions;

/// <summary>
/// An exception that is thrown if an Internet Control Message Protocol (ICMP) message checksum is invalid.
/// </summary>
internal sealed class IcmpMessageChecksumInvalidException : IcmpException
{
    /// <summary>
    /// Initializes a new instance of <see cref="IcmpMessageTypeInvalidException" />.
    /// </summary>
    /// <param name="checksum">The invalid message checksum.</param>
    internal IcmpMessageChecksumInvalidException(ushort checksum)
        : base(CreateExceptionMessage(checksum))
    {
        this.Checksum = checksum;
    }

    /// <summary>
    /// Gets the invalid message checksum.
    /// </summary>
    internal ushort Checksum { get; }

    private static string CreateExceptionMessage(ushort checksum) =>
        string.Format(ExceptionMessages.MessageChecksumInvalid, checksum);
}
