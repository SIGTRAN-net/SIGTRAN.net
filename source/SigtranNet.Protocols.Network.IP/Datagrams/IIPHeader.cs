/*
 * This file is part of SIGTRAN.net.
 * 
 * SIGTRAN.net is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, 
 * either version 3 of the License, or (at your option) any later version.
 * 
 * SIGTRAN.net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. 
 * See the GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License along with SIGTRAN.net. If not, see <https://www.gnu.org/licenses/>.
 */

using SigtranNet.Binary;

namespace SigtranNet.Protocols.Network.IP.Datagrams;

/// <summary>
/// An Internet Header.
/// </summary>
public interface IIPHeader : IBinarySerializable
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
public interface IIPHeader<THeader> : IIPHeader, IBinarySerializable<THeader>
    where THeader : IIPHeader<THeader>
{
}