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

using System.Net;

namespace SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header.Options.InternetTimestamp;

/// <summary>
/// A pair of an IPv4 address and a timestamp (in milliseconds since midnight UT).
/// </summary>
internal readonly struct IPv4OptionInternetTimestampAddressPair
{
    /// <summary>
    /// The IPv4 Internet Address.
    /// </summary>
    internal readonly IPAddress? address;

    /// <summary>
    /// The timestamp (in milliseconds since midnight UT).
    /// </summary>
    internal readonly uint timestamp;

    /// <summary>
    /// Initializes a new instance of <see cref="IPv4OptionInternetTimestampAddressPair" />.
    /// </summary>
    /// <param name="address">The IPv4 Internet Address.</param>
    /// <param name="timestamp">The timestamp (in milliseconds since midnight UT).</param>
    internal IPv4OptionInternetTimestampAddressPair(IPAddress? address, uint timestamp)
    {
        this.address = address;
        this.timestamp = timestamp;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="IPv4OptionInternetTimestampAddressPair" />.
    /// </summary>
    /// <param name="timestamp">The timestamp (in milliseconds since midnight UT).</param>
    internal IPv4OptionInternetTimestampAddressPair(uint timestamp)
    {
        this.timestamp = timestamp;
    }
}
