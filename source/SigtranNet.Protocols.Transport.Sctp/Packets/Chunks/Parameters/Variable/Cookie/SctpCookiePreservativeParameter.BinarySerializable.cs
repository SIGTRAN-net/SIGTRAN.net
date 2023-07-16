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

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Variable.Cookie;

internal readonly partial struct SctpCookiePreservativeParameter
{
    /// <inheritdoc />
    public static SctpCookiePreservativeParameter FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;

        /* Parameter Header */
        var parameterType = (SctpChunkParameterType)BinaryPrimitives.ReadUInt16BigEndian(memorySpan);
        if (parameterType != ParameterTypeImplicit)
            throw new SctpChunkParameterTypeInvalidException(parameterType);
        var parameterLength = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[2..]);
        if (parameterLength != ParameterLengthImplicit)
            throw new SctpChunkParameterLengthInvalidException(parameterType, parameterLength);

        /* Parameter Value */
        var suggestedCookieLifeSpanIncrement = BinaryPrimitives.ReadUInt32BigEndian(memorySpan[4..]);

        return new(suggestedCookieLifeSpanIncrement);
    }

    /// <inheritdoc />
    public static SctpCookiePreservativeParameter Read(BinaryReader binaryReader) =>
        ISctpChunkParameterVariableLength<SctpCookiePreservativeParameter>.Read(binaryReader);

    /// <inheritdoc />
    public static SctpCookiePreservativeParameter Read(Stream stream) =>
        ISctpChunkParameterVariableLength<SctpCookiePreservativeParameter>.Read(stream);

    /// <inheritdoc />
    public static Task<SctpCookiePreservativeParameter> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        ISctpChunkParameterVariableLength<SctpCookiePreservativeParameter>.ReadAsync(stream, cancellationToken);

    /// <inheritdoc />
    public ReadOnlyMemory<byte> ToReadOnlyMemory()
    {
        var memory = new Memory<byte>(new byte[ParameterLengthImplicit]);
        this.Write(memory.Span);
        return memory;
    }

    /// <inheritdoc />
    public void Write(Span<byte> span)
    {
        BinaryPrimitives.WriteUInt16BigEndian(span, (ushort)ParameterTypeImplicit);
        BinaryPrimitives.WriteUInt16BigEndian(span[2..], ParameterLengthImplicit);
        BinaryPrimitives.WriteUInt32BigEndian(span[4..], this.suggestedCookieLifeSpanIncrement);
    }
}
