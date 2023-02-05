/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Network.Icmp.Messages.TimeExceeded;

/// <summary>
/// A code of an Internet Control Message Protocol (ICMP) message.
/// </summary>
internal enum IcmpTimeExceededCode : byte
{
    /// <summary>
    /// Time to Live exceeded in transit.
    /// </summary>
    TimeToLiveExceededInTransit = 0,

    /// <summary>
    /// Fragment reassembly time exceeded.
    /// </summary>
    FragmentReassemblyTimeExceeded = 1,
}
