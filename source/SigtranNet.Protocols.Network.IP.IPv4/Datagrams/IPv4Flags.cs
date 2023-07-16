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

namespace SigtranNet.Protocols.Network.IP.IPv4.Datagrams;

/// <summary>
/// Various Control Flags.
/// </summary>
/// <remarks>
///     From RFC 791:
///     <code>
///         Various Control Flags.
///
///         Bit 0: reserved, must be zero
///         Bit 1: (DF) 0 = May Fragment,  1 = Don't Fragment.
///         Bit 2: (MF) 0 = Last Fragment, 1 = More Fragments.
///     </code>
/// </remarks>
[Flags]
internal enum IPv4Flags
{
    /// <summary>
    /// Don't Fragment.
    /// </summary>
    DontFragment = 0b010,

    /// <summary>
    /// More Fragments.
    /// </summary>
    MoreFragments = 0b001,

    /// <summary>
    /// Last Fragment.
    /// </summary>
    LastFragment = 0b000
}
