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

using SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header.Options.Exceptions;
using System.Net;

namespace SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header.Options.RecordRoute;

/// <summary>
/// An IPv4 Record Route.
/// </summary>
/// <remarks>
///     From RFC 791:
///     <code>
///         The record route option provides a means to record the route of
///         an internet datagram.
///     </code>
///     <code>
///         Not copied on fragmentation, goes in first fragment only.
///         Appears at most once in a datagram.
///     </code>
/// </remarks>
internal readonly partial struct IPv4OptionRecordRoute : IIPv4Option
{
    internal readonly IPv4OptionType optionType;
    internal readonly byte length;
    internal readonly byte pointer;
    internal readonly ReadOnlyMemory<IPAddress> route;

    internal IPv4OptionRecordRoute(
        IPv4OptionType optionType,
        byte length,
        byte pointer,
        ReadOnlyMemory<IPAddress> route)
    {
        // Guards
        if (optionType is
                not IPv4OptionType.NotCopied_Control_RecordRoute
                and not IPv4OptionType.Copied_Control_RecordRoute)
            throw new IPv4OptionInvalidTypeException(optionType);
        if (length is < 3 || ((length - 3) % 4 > 0)) // must be the 3 required octets and any 4-octet addresses.
            throw new ArgumentOutOfRangeException(nameof(length));

        // Fields
        this.optionType = optionType;
        this.length = length;
        this.pointer = pointer;
        this.route = route;
    }

    byte IIPv4Option.Length => this.length;
}
