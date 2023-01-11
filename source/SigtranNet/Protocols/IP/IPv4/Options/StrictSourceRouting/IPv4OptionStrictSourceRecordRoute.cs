/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.IP.IPv4.Options.Exceptions;
using System.Net;

namespace SigtranNet.Protocols.IP.IPv4.Options.StrictSourceRouting;

/// <summary>
/// An IPv4 Strict Source and Record Route option.
/// </summary>
/// <remarks>
///     From RFC 791:
///     <code>
///         The strict source and record route (SSRR) option provides a
///         means for the source of an internet datagram to supply routing
///         information to be used by the gateways in forwarding the
///         datagram to the destination, and to record the route
///         information.
///     </code>
///     <code>
///         Must be copied on fragmentation.  Appears at most once in a 
///         datagram.
///     </code>
/// </remarks>
internal readonly partial struct IPv4OptionStrictSourceRecordRoute : IIPv4Option
{
    private const byte PointerMinimumValue = 4;

    internal readonly IPv4OptionType optionType;
    internal readonly byte length;
    internal readonly byte pointer;
    internal readonly ReadOnlyMemory<IPAddress> route;

    /// <summary>
    /// Initializes a new instance of <see cref="IPv4OptionStrictSourceRecordRoute" />.
    /// </summary>
    /// <param name="optionType">The IPv4 option type, including the 'copied' flag.</param>
    /// <param name="pointer">The pointer into the route data indicating the octet which begins the next source address to be processed.</param>
    /// <param name="route">The route data.</param>
    /// <exception cref="IPv4OptionInvalidTypeException">
    /// An <see cref="IPv4OptionInvalidTypeException" /> is thrown if <paramref name="optionType" /> is an invalid option type.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// An <see cref="ArgumentOutOfRangeException" /> is thrown if <paramref name="pointer" /> has a lower value than the minimum value 4.
    /// </exception>
    internal IPv4OptionStrictSourceRecordRoute(
        IPv4OptionType optionType,
        byte pointer,
        ReadOnlyMemory<IPAddress> route)
    {
        // Guards
        if (optionType is
                not IPv4OptionType.NotCopied_Control_StrictSourceRouting
                and not IPv4OptionType.Copied_Control_StrictSourceRouting)
            throw new IPv4OptionInvalidTypeException(optionType);
        if (pointer < PointerMinimumValue)
            throw new ArgumentOutOfRangeException(nameof(pointer));

        // Fields
        this.optionType = optionType;
        this.length = (byte)(3 + (route.Length * sizeof(uint)));
        this.pointer = pointer;
        this.route = route;
    }

    byte IIPv4Option.Length => this.length;
}
