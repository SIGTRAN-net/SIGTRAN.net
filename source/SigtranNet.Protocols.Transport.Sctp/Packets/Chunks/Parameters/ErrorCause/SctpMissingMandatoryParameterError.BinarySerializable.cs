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

using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.ErrorCause.Exceptions;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Exceptions;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.ErrorCause;

internal readonly partial struct SctpMissingMandatoryParameterError
{
    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified Error Cause Code is not "Missing Mandatory Parameter".
    /// </exception>
    /// <exception cref="SctpErrorCauseLengthInvalidException">
    /// An <see cref="SctpErrorCauseLengthInvalidException" /> is thrown if the specified Error Cause Length is less than the length of the Error Cause header.
    /// </exception>
    public static SctpMissingMandatoryParameterError FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;
        var offset = 0;

        /* Parameter Header */
        var errorCauseCode = (SctpErrorCauseCode)BinaryPrimitives.ReadUInt16BigEndian(memorySpan);
        if (errorCauseCode != ErrorCauseCodeImplicit)
            throw new SctpErrorCauseCodeInvalidException(errorCauseCode);
        offset += sizeof(ushort);
        var parameterLength = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[offset..]);
        if (parameterLength < ErrorCauseLengthMinimum)
            throw new SctpErrorCauseLengthInvalidException(errorCauseCode, parameterLength);
        offset += sizeof(ushort);
        var numberMissingParameters = BinaryPrimitives.ReadUInt32BigEndian(memorySpan[offset..]);

        /* Missing Parameter Types */
        offset += sizeof(uint);
        var missingParameterTypes = new List<SctpChunkParameterType>();
        for (var i = 0; i < numberMissingParameters; i++)
        {
            missingParameterTypes.Add((SctpChunkParameterType)BinaryPrimitives.ReadUInt16BigEndian(memorySpan[offset..]));
            offset += sizeof(ushort);
        }

        return new(missingParameterTypes.ToArray());
    }

    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified Error Cause Code is not "Missing Mandatory Parameter".
    /// </exception>
    /// <exception cref="SctpChunkParameterLengthInvalidException">
    /// An <see cref="SctpChunkParameterLengthInvalidException" /> is thrown if the specified parameter length is less than the length of the parameter header.
    /// </exception>
    public static SctpMissingMandatoryParameterError Read(BinaryReader reader) =>
        ISctpErrorCauseParameter<SctpMissingMandatoryParameterError>.Read(reader);

    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified Error Cause Code is not "Missing Mandatory Parameter".
    /// </exception>
    /// <exception cref="SctpChunkParameterLengthInvalidException">
    /// An <see cref="SctpChunkParameterLengthInvalidException" /> is thrown if the specified parameter length is less than the length of the parameter header.
    /// </exception>
    public static SctpMissingMandatoryParameterError Read(Stream stream) =>
        ISctpErrorCauseParameter<SctpMissingMandatoryParameterError>.Read(stream);

    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified Error Cause Code is not "Missing Mandatory Parameter".
    /// </exception>
    /// <exception cref="SctpChunkParameterLengthInvalidException">
    /// An <see cref="SctpChunkParameterLengthInvalidException" /> is thrown if the specified parameter length is less than the length of the parameter header.
    /// </exception>
    public static Task<SctpMissingMandatoryParameterError> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        ISctpErrorCauseParameter<SctpMissingMandatoryParameterError>.ReadAsync(stream, cancellationToken);

    /// <inheritdoc />
    public ReadOnlyMemory<byte> ToReadOnlyMemory()
    {
        var memory = new Memory<byte>(new byte[this.causeLength]);
        this.Write(memory.Span);
        return memory;
    }

    /// <inheritdoc />
    public void Write(Span<byte> span)
    {
        /* Parameter Header */
        var offset = 0;
        BinaryPrimitives.WriteUInt16BigEndian(span, (ushort)ErrorCauseCodeImplicit);
        offset += sizeof(ushort);
        BinaryPrimitives.WriteUInt16BigEndian(span[offset..], this.causeLength);
        offset += sizeof(ushort);
        BinaryPrimitives.WriteUInt32BigEndian(span[offset..], (uint)this.missingParameterTypes.Length);
        offset += sizeof(uint);

        /* Missing Parameter Types */
        var missingParameterTypesSpan = this.missingParameterTypes.Span;
        for (var i = 0; i < this.missingParameterTypes.Length; i++)
        {
            BinaryPrimitives.WriteUInt16BigEndian(span[offset..], (ushort)missingParameterTypesSpan[i]);
            offset += sizeof(ushort);
        }
    }
}
