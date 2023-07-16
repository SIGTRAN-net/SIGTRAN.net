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

using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Exceptions;
using System.Buffers.Binary;
using System.Runtime.InteropServices;

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Variable.Address;

/// <summary>
/// An optional or variable-length SCTP Packet Chunk that specifies an address (IP or host).
/// </summary>
internal interface ISctpAddressParameter : ISctpChunkParameterVariableLength
{
    private static readonly Dictionary<SctpChunkParameterType, Func<ReadOnlyMemory<byte>, ISctpAddressParameter>> Deserializers =
        new()
        {
            { SctpChunkParameterType.HostNameAddress, memory => SctpHostNameAddressParameter.FromReadOnlyMemory(memory) },
            { SctpChunkParameterType.IPv4Address, memory => SctpIPv4AddressParameter.FromReadOnlyMemory(memory) },
            { SctpChunkParameterType.IPv6Address, memory => SctpIPv6AddressParameter.FromReadOnlyMemory(memory) },
        };

    /// <summary>
    /// Deserializes an SCTP Packet Chunk parameter that specifies an address (IP or host).
    /// </summary>
    /// <param name="memory">The memory that contains the serialized parameter.</param>
    /// <returns>The deserialized parameter.</returns>
    /// <exception cref="SctpChunkParameterTypeInvalidException">
    /// An <see cref="SctpChunkParameterTypeInvalidException" /> is thrown if the specified parameter type is not an address parameter type.
    /// </exception>
    static new ISctpAddressParameter FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;

        var parameterType = (SctpChunkParameterType)BinaryPrimitives.ReadUInt16BigEndian(memorySpan);
        ref var deserializer = ref CollectionsMarshal.GetValueRefOrNullRef(Deserializers, parameterType);
        if (deserializer is null)
            throw new SctpChunkParameterTypeInvalidException(parameterType);
        return deserializer(memory);
    }
}
