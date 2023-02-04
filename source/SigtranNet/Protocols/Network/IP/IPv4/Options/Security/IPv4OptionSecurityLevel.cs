/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Network.IP.IPv4.Options.Security;

/// <summary>
/// The IPv4 security level.
/// </summary>
/// <remarks>
///     From RFC 791:
///     <code>
///         Specifies one of 16 levels of security (eight of which are
///         reserved for future use).
///     </code>
/// </remarks>
internal enum IPv4OptionSecurityLevel : ushort
{
    Unclassified = 0b00000000_00000000,
    Confidential = 0b11110001_00110101,
    EFTO = 0b01111000_10011010,
    MMMM = 0b10111100_01001101,
    PROG = 0b01011110_00100110,
    Restricted = 0b10101111_00010011,
    Secret = 0b11010111_10001000,
    TopSecret = 0b01101011_11000101
}
