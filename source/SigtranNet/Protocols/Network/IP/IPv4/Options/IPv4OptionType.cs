/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Network.IP.IPv4.Options;

/// <summary>
/// The type of an optional segment in the IPv4 header.
/// </summary>
/// <remarks>
///     From RFC 791:
///     <code>
///         The option-type octet is viewed as having 3 fields:
/// 
///             1 bit copied flag,
///             2 bits option class,
///             5 bits option number.
/// 
///         The copied flag indicates that this option is copied into all
///         fragments on fragmentation.
/// 
///             0 = not copied
///             1 = copied
/// 
///         The option classes are:
/// 
///             0 = control
///             1 = reserved for future use
///             2 = debugging and measurement
///             3 = reserved for future use
///     </code>
/// </remarks>
internal enum IPv4OptionType : byte
{
    NotCopied_Control_EndOfOptionList =     0b0_00_00000,
    NotCopied_Control_NoOperation =         0b0_00_00001,
    NotCopied_Control_Security =            0b0_00_00010,
    NotCopied_Control_LooseSourceRouting =  0b0_00_00011,
    NotCopied_Control_StrictSourceRouting = 0b0_00_01001,
    NotCopied_Control_RecordRoute =         0b0_00_00111,
    NotCopied_Control_StreamIdentifier =            0b0_00_01000,
    NotCopied_Debugging_InternetTimestamp = 0b0_10_00100,

    Copied_Control_EndOfOptionList =        0b1_00_00000,
    Copied_Control_NoOperation =            0b1_00_00001,
    Copied_Control_Security =               0b1_00_00010,
    Copied_Control_LooseSourceRouting =     0b1_00_00011,
    Copied_Control_StrictSourceRouting =    0b1_00_01001,
    Copied_Control_RecordRoute =            0b1_00_00111,
    Copied_Control_StreamIdentifier =               0b1_00_01000,
    Copied_Debugging_InternetTimestamp =    0b1_10_00100,
}
