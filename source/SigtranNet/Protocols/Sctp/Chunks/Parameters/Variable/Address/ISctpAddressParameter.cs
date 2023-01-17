/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Sctp.Chunks.Parameters.Exceptions;
using System.Buffers.Binary;
using System.Runtime.InteropServices;

namespace SigtranNet.Protocols.Sctp.Chunks.Parameters.Variable.Address;

/// <summary>
/// An optional or variable-length SCTP Packet Chunk that specifies an address (IP or host).
/// </summary>
internal interface ISctpAddressParameter : ISctpChunkParameterVariableLength
{
    private static readonly Dictionary<SctpChunkParameterType, Func<ReadOnlyMemory<byte>, ISctpAddressParameter>> Deserializers =
        new Dictionary<SctpChunkParameterType, Func<ReadOnlyMemory<byte>, ISctpAddressParameter>>
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
