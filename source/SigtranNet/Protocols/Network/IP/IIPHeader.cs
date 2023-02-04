/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;

namespace SigtranNet.Protocols.Network.IP;

/// <summary>
/// An Internet Header.
/// </summary>
internal interface IIPHeader : IBinarySerializable
{
    /// <summary>
    /// Gets the length (number of unsigned 32-bit words) of the Internet Header.
    /// </summary>
    byte InternetHeaderLength { get; }
}

/// <summary>
/// An Internet Header.
/// </summary>
/// <typeparam name="THeader">The type of the Internet Header.</typeparam>
internal interface IIPHeader<THeader> : IIPHeader, IBinarySerializable<THeader>
    where THeader : IIPHeader<THeader>
{
}