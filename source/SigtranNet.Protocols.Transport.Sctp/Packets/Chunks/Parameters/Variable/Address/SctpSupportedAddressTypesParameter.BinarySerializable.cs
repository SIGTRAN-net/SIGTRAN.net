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

using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Exceptions;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Variable;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Variable.Address;

internal readonly partial struct SctpSupportedAddressTypesParameter
{
    /// <inheritdoc />
    public static SctpSupportedAddressTypesParameter FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;

        /* Parameter Header */
        var parameterType = (SctpChunkParameterType)BinaryPrimitives.ReadUInt16BigEndian(memorySpan);
        if (parameterType != ParameterTypeImplicit)
            throw new SctpChunkParameterTypeInvalidException(parameterType);
        var parameterLength = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[2..]);

        /* Parameter Value */
        var offset = 4;
        var supportedAddressTypes = new List<SctpChunkParameterType>();
        while (offset < parameterLength)
        {
            var supportedAddressType = (SctpChunkParameterType)BinaryPrimitives.ReadUInt16BigEndian(memorySpan[offset..]);
            supportedAddressTypes.Add(supportedAddressType);
            offset += sizeof(ushort);
        }

        return new(supportedAddressTypes.ToArray());
    }

    /// <inheritdoc />
    public static SctpSupportedAddressTypesParameter Read(BinaryReader binaryReader) =>
        ISctpChunkParameterVariableLength<SctpSupportedAddressTypesParameter>.Read(binaryReader);

    /// <inheritdoc />
    public static SctpSupportedAddressTypesParameter Read(Stream stream) =>
        ISctpChunkParameterVariableLength<SctpSupportedAddressTypesParameter>.Read(stream);

    /// <inheritdoc />
    public static Task<SctpSupportedAddressTypesParameter> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        ISctpChunkParameterVariableLength<SctpSupportedAddressTypesParameter>.ReadAsync(stream, cancellationToken);

    /// <inheritdoc />
    public ReadOnlyMemory<byte> ToReadOnlyMemory()
    {
        var memory = new Memory<byte>(new byte[this.parameterLength]);
        this.Write(memory.Span);
        return memory;
    }

    /// <inheritdoc />
    public void Write(Span<byte> span)
    {
        /* Parameter Header */
        BinaryPrimitives.WriteUInt16BigEndian(span, (ushort)ParameterTypeImplicit);
        BinaryPrimitives.WriteUInt16BigEndian(span[sizeof(ushort)..], this.parameterLength);

        /* Parameter Value */
        var offset = sizeof(uint);
        var supportedAddressTypesSpan = this.supportedAddressTypes.Span;
        for (var i = 0; i < this.supportedAddressTypes.Length; i++)
        {
            BinaryPrimitives.WriteUInt16BigEndian(span[offset..], (ushort)supportedAddressTypesSpan[i]);
            offset += sizeof(ushort);
        }
    }
}
