/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Network.Icmp.Messages;

/// <summary>
/// The type of the Internet Control Message Protocol (ICMP) message.
/// </summary>
internal enum IcmpMessageType : byte
{
    /// <summary>
    /// Destination Unreachable.
    /// </summary>
    DestinationUnreachable = 3,

    /// <summary>
    /// Time Exceeded.
    /// </summary>
    TimeExceeded = 11
}
