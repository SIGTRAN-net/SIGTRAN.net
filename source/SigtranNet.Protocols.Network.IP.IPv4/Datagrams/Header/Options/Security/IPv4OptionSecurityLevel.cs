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

namespace SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header.Options.Security;

/// <summary>
/// The IPv4 security level.
/// </summary>
/// <remarks>
///     From RFC 791:
///     <code>
///         Specifies one of 16 levels of security (eight of which are
///         reserved for future use).
///     </code>
/// </remarks>
internal enum IPv4OptionSecurityLevel : ushort
{
    Unclassified = 0b00000000_00000000,
    Confidential = 0b11110001_00110101,
    EFTO = 0b01111000_10011010,
    MMMM = 0b10111100_01001101,
    PROG = 0b01011110_00100110,
    Restricted = 0b10101111_00010011,
    Secret = 0b11010111_10001000,
    TopSecret = 0b01101011_11000101
}
