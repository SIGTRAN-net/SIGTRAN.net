/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using System.Net;

namespace SigtranNet.Protocols.IP.IPv4.Options.InternetTimestamp;

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
