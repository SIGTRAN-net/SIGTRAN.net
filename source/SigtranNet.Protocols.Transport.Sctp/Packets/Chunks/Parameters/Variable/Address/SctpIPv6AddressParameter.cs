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

using System.Net;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Variable;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Variable.Address;

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Variable.Address;

/// <summary>
/// An IPv6 Address Parameter in an SCTP Packet Chunk.
/// </summary>
internal readonly partial struct SctpIPv6AddressParameter : ISctpAddressParameter, ISctpChunkParameterVariableLength<SctpIPv6AddressParameter>
{
    private const SctpChunkParameterType ParameterTypeImplicit = SctpChunkParameterType.IPv6Address;
    private const ushort ParameterLengthImplicit = 5 * sizeof(uint);

    /// <summary>
    /// The value of the IPv6 Address Chunk Parameter.
    /// </summary>
    internal readonly IPAddress ipAddress;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpIPv6AddressParameter" />.
    /// </summary>
    /// <param name="ipAddress">The IP Address.</param>
    internal SctpIPv6AddressParameter(IPAddress ipAddress)
    {
        this.ipAddress = ipAddress;
    }

    SctpChunkParameterType ISctpChunkParameterVariableLength.ParameterType => ParameterTypeImplicit;
    ushort ISctpChunkParameter.ParameterLength => ParameterLengthImplicit;
}
