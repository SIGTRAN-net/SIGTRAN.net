/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Network.IP.IPv4.Options.Exceptions;

namespace SigtranNet.Protocols.Network.IP.IPv4.Options.InternetTimestamp;

/// <summary>
/// An IPv4 option for an Internet Timestamp.
/// </summary>
/// <remarks>
///     From RFC 791:
///     <code>
///         The Timestamp is a right-justified, 32-bit timestamp in
///         milliseconds since midnight UT.If the time is not available in
///         milliseconds or cannot be provided with respect to midnight UT
///         then any time may be inserted as a timestamp provided the high
///         order bit of the timestamp field is set to one to indicate the
///         use of a non-standard value.
///
///         The originating host must compose this option with a large
///         enough timestamp data area to hold all the timestamp information
///         expected.The size of the option does not change due to adding
///         timestamps.  The intitial contents of the timestamp data area
///         must be zero or internet address/zero pairs.
///     </code>
///     <code>
///         The timestamp option is not copied upon fragmentation.  It is
///         carried in the first fragment.Appears at most once in a
///         datagram.
///     </code>
/// </remarks>
internal readonly partial struct IPv4OptionInternetTimestamp : IIPv4Option
{
    /// <summary>
    /// The minimum length (in octets) of an IPv4 Internet Timestamp option.
    /// </summary>
    internal const byte LengthMinimum = 4;

    /// <summary>
    /// The maximum length (in octets) of an IPv4 Internet Timestamp option.
    /// </summary>
    internal const byte LengthMaximum = 40;

    /// <summary>
    /// The minimum value for the pointer.
    /// </summary>
    internal const byte PointerMinimum = 5;

    internal readonly IPv4OptionType optionType;
    internal readonly byte length;
    internal readonly byte pointer;
    internal readonly byte overflow;
    internal readonly IPv4OptionInternetTimestampFlags flag;
    internal readonly ReadOnlyMemory<IPv4OptionInternetTimestampAddressPair> timestamps;

    /// <summary>
    /// Initializes a new instance of <see cref="IPv4OptionInternetTimestamp" />.
    /// </summary>
    /// <param name="optionType">The IPv4 option type.</param>
    /// <param name="length">The length (in octets) of the IPv4 internet timestamp option.</param>
    /// <param name="pointer">The number of octets from the beginning of this option to the end of timestamps plus one (i.e., it points to the octet beginning the space for next timestamp).</param>
    /// <param name="overflow">The Overflow (oflw) [4 bits] is the number of IP modules that cannot register timestamps due to lack of space.</param>
    /// <param name="flags">The Flag (flg) [4 bits].</param>
    /// <param name="timestamps">The timestamps or internet address and timestamp pairs.</param>
    /// <exception cref="IPv4OptionInvalidTypeException">
    /// An <see cref="IPv4OptionInvalidTypeException" /> is thrown if <paramref name="optionType" /> has an invalid value.
    /// </exception>
    /// <exception cref="IPv4OptionInvalidLengthException">
    /// An <see cref="IPv4OptionInvalidLengthException" /> is thrown if <paramref name="length" /> has an invalid value.
    /// </exception>
    /// <exception cref="IPv4OptionInvalidPointerException">
    /// An <see cref="IPv4OptionInvalidPointerException" /> is thrown if <paramref name="pointer" /> has an invalid value.
    /// </exception>
    internal IPv4OptionInternetTimestamp(
        IPv4OptionType optionType,
        byte length,
        byte pointer,
        byte overflow,
        IPv4OptionInternetTimestampFlags flags,
        ReadOnlyMemory<IPv4OptionInternetTimestampAddressPair> timestamps)
    {
        // Guards
        if (optionType is
                not IPv4OptionType.NotCopied_Debugging_InternetTimestamp
                and not IPv4OptionType.Copied_Debugging_InternetTimestamp)
            throw new IPv4OptionInvalidTypeException(optionType);
        if (length is < LengthMinimum or > LengthMaximum)
            throw new IPv4OptionInvalidLengthException(length);
        if (pointer < PointerMinimum)
            throw new IPv4OptionInvalidPointerException(pointer);

        // Fields
        this.optionType = optionType;
        this.length = length;
        this.pointer = pointer;
        this.overflow = overflow;
        this.flag = flags;
        this.timestamps = timestamps;
    }

    byte IIPv4Option.Length => this.length;
}
