/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.IP.IPv4.Options.StreamIdentifier;

/// <summary>
/// An IPv4 Stream Identifier.
/// </summary>
/// <remarks>
///     From RFC 791:
///     <code>
///         This option provides a way for the 16-bit SATNET stream
///         identifier to be carried through networks that do not support
///         the stream concept.
///
///         Must be copied on fragmentation. Appears at most once in a
///         datagram.
///     </code>
/// </remarks>
internal readonly partial struct IPv4OptionStreamIdentifier : IIPv4Option
{
    internal const byte LengthFixed = 4;
    internal readonly IPv4OptionType optionType;
    internal readonly ushort streamIdentifier;

    /// <summary>
    /// Initializes a new instance of <see cref="IPv4OptionStreamIdentifier" />.
    /// </summary>
    /// <param name="optionType">The IPv4 Option Type.</param>
    /// <param name="streamIdentifier">The stream identifier.</param>
    internal IPv4OptionStreamIdentifier(
        IPv4OptionType optionType,
        ushort streamIdentifier)
    {
        this.optionType = optionType;
        this.streamIdentifier = streamIdentifier;
    }

    byte IIPv4Option.Length => LengthFixed;
}
