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

namespace SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.Redirect;

/// <summary>
/// A code for a Redirect Message in the Internet Control Message Protocol (ICMP).
/// </summary>
internal enum IcmpRedirectCode
{
    /// <summary>
    /// Redirect datagrams for the Network.
    /// </summary>
    Network = 0,

    /// <summary>
    /// Redirect datagrams for the Host.
    /// </summary>
    Host = 1,

    /// <summary>
    /// Redirect datagrams for the Type of Service and Network.
    /// </summary>
    TypeOfService_Network = 2,

    /// <summary>
    /// Redirect datagrams for the Type of Service and Host.
    /// </summary>
    TypeOfService_Host = 3
}
