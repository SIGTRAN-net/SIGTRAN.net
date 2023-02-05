/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;
using SigtranNet.Protocols.Network.IP.Exceptions;
using SigtranNet.Protocols.Network.IP.IPv4;

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

    /// <summary>
    /// Deserializes an Internet Protocol header from <paramref name="data" />.
    /// </summary>
    /// <param name="data">The data that contains the serialized Internet Protocol header.</param>
    /// <returns>The deserialized Internet Protocol header.</returns>
    /// <exception cref="IPVersionNotSupportedException">
    /// An <see cref="IPVersionNotSupportedException" /> is thrown if the specified Internet Protocol version is not supported.
    /// </exception>
    static IIPHeader FromReadOnlyMemory(ReadOnlyMemory<byte> data)
    {
        var version = (IPVersion)(data.Span[0] >> 4);
        return version switch
        {
            IPVersion.IPv4 => IPv4Header.FromReadOnlyMemory(data),
            IPVersion.IPv6 => throw new IPVersionNotSupportedException(),
            _ => throw new IPVersionNotSupportedException(),
        };
    }
}

/// <summary>
/// An Internet Header.
/// </summary>
/// <typeparam name="THeader">The type of the Internet Header.</typeparam>
internal interface IIPHeader<THeader> : IIPHeader, IBinarySerializable<THeader>
    where THeader : IIPHeader<THeader>
{
}