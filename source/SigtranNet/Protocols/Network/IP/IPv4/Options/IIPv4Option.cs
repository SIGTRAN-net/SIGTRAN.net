/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;

namespace SigtranNet.Protocols.Network.IP.IPv4.Options;

/// <summary>
/// An IPv4 Option, an optional variable length segment of the IPv4 header.
/// </summary>
internal interface IIPv4Option : IBinarySerializable
{
    /// <summary>
    /// Gets the type of the IPv4 Option.
    /// </summary>
    static IPv4OptionType Type { get; }

    /// <summary>
    /// Gets the length of the IPv4 Option.
    /// </summary>
    byte Length { get; }
}
