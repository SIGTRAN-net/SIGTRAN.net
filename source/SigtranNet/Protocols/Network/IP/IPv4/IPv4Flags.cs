/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Network.IP.IPv4;

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
