/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Network.Icmp.Messages.Redirect;

/// <summary>
/// A code for a Redirect Message in the Internet Control Message Protocol (ICMP).
/// </summary>
internal enum IcmpRedirectCode
{
    /// <summary>
    /// Redirect datagrams for the Network.
    /// </summary>
    Network = 0,

    /// <summary>
    /// Redirect datagrams for the Host.
    /// </summary>
    Host = 1,

    /// <summary>
    /// Redirect datagrams for the Type of Service and Network.
    /// </summary>
    TypeOfService_Network = 2,

    /// <summary>
    /// Redirect datagrams for the Type of Service and Host.
    /// </summary>
    TypeOfService_Host = 3
}
