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

namespace SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header.Options;

/// <summary>
/// An IPv4 Option, an optional variable length segment of the IPv4 header.
/// </summary>
internal interface IIPv4Option : IBinarySerializable
{
    /// <summary>
    /// Gets the type of the IPv4 Option.
    /// </summary>
    static IPv4OptionType Type { get; }

    /// <summary>
    /// Gets the length of the IPv4 Option.
    /// </summary>
    byte Length { get; }
}
