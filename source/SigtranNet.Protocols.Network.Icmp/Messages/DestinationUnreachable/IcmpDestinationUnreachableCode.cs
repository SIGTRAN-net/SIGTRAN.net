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

namespace SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.DestinationUnreachable;

/// <summary>
/// The code that indicates the reason for the Destination Unreachable message.
/// </summary>
internal enum IcmpDestinationUnreachableCode : byte
{
    /// <summary>
    /// Net Unreachable.
    /// </summary>
    NetUnreachable = 0,

    /// <summary>
    /// Host Unreachable.
    /// </summary>
    HostUnreachable = 1,

    /// <summary>
    /// Protocol Unreachable.
    /// </summary>
    ProtocolUnreachable = 2,

    /// <summary>
    /// Port Unreachable.
    /// </summary>
    PortUnreachable = 3,

    /// <summary>
    /// Fragmentation needed and DF set.
    /// </summary>
    FragmentationNeededAndDFSet = 4,

    /// <summary>
    /// Source route failed.
    /// </summary>
    SourceRouteFailed = 5
}
