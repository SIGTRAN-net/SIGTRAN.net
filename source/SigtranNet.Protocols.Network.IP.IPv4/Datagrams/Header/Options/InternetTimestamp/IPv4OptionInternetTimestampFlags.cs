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

namespace SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header.Options.InternetTimestamp;

/// <summary>
/// The IPv4 Internet Timestamp flags.
/// </summary>
/// <remarks>
///     From RFC 791:
///     <code>
///         The Flag (flg) [4 bits] values are
///         
///             0 -- time stamps only, stored in consecutive 32-bit words,
///
///             1 -- each timestamp is preceded with internet address of the
///                 registering entity,
///
///             3 -- the internet address fields are prespecified. An IP
///                 module only registers its timestamp if it matches its own
///                 address with the next specified internet address.
///     </code>
/// </remarks>
[Flags]
internal enum IPv4OptionInternetTimestampFlags : byte
{
    /// <summary>
    /// Timestamps only.
    /// </summary>
    TimestampsOnly = 0b0000,

    /// <summary>
    /// Each timestamp is preceded with internet address of the registering entity.
    /// </summary>
    InternetAddressPreceded = 0b0001,

    /// <summary>
    /// The internet address fields are prespecified.
    /// </summary>
    InternetAddressPrespecified = 0b0011
}
