/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using System.Net.NetworkInformation;

namespace SigtranNet.Protocols.Network.IP.IPv4.Controllers;

/// <summary>
/// Configuration options for an <see cref="IPv4Controller" />.
/// </summary>
internal sealed class IPv4ControllerOptions
{
    private const byte TimeToLiveDefault = 64; // Recommended by RFC 1700.

    /// <summary>
    /// Initializes a new instance of <see cref="IPv4ControllerOptions" />.
    /// </summary>
    internal IPv4ControllerOptions(
        NetworkInterface? networkInterfacePreferred = null,
        byte? timeToLive = null)
    {
        this.NetworkInterfacePreferred = networkInterfacePreferred;
        this.TimeToLive = timeToLive ?? TimeToLiveDefault;
    }

    /// <summary>
    /// Gets the preferred Network Interface to use for sending and receiving IPv4 datagrams.
    /// </summary>
    internal NetworkInterface? NetworkInterfacePreferred { get; }

    /// <summary>
    /// Gets the Time to Live (TTL).
    /// </summary>
    internal byte TimeToLive { get; }
}
