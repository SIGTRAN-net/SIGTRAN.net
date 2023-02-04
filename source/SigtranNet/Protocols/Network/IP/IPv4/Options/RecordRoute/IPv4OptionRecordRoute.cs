/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Network.IP.IPv4.Options.Exceptions;
using System.Net;

namespace SigtranNet.Protocols.Network.IP.IPv4.Options.RecordRoute;

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
