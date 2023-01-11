/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.IP.IPv4.Options.InternetTimestamp;

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
///             3 -- the internet address fields are prespecified.An IP
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
