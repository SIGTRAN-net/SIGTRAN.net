/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Network.Icmp.Messages.DestinationUnreachable;

/// <summary>
/// The code that indicates the reason for the Destination Unreachable message.
/// </summary>
internal enum IcmpDestinationUnreachableCode : byte
{
    /// <summary>
    /// Net Unreachable.
    /// </summary>
    NetUnreachable = 0,

    /// <summary>
    /// Host Unreachable.
    /// </summary>
    HostUnreachable = 1,

    /// <summary>
    /// Protocol Unreachable.
    /// </summary>
    ProtocolUnreachable = 2,

    /// <summary>
    /// Port Unreachable.
    /// </summary>
    PortUnreachable = 3,

    /// <summary>
    /// Fragmentation needed and DF set.
    /// </summary>
    FragmentationNeededAndDFSet = 4,

    /// <summary>
    /// Source route failed.
    /// </summary>
    SourceRouteFailed = 5
}
