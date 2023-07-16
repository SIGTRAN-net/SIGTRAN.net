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

using SigtranNet.Protocols.Network.Datagrams;

namespace SigtranNet.Protocols.Network;

/// <summary>
/// Processes a protocol in the OSI Network Layer.
/// </summary>
/// <typeparam name="TDatagram">The type of datagram.</typeparam>
public interface INetworkProcessor<TDatagram> : IProtocolProcessor<TDatagram>
    where TDatagram : INetworkDatagram<TDatagram>
{
}
