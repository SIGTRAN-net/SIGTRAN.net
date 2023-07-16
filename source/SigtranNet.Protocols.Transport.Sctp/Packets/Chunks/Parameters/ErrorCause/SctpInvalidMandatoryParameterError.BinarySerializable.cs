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

internal readonly partial struct SctpInvalidMandatoryParameterError
{
    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified Error Cause Code is not equal to <see cref="SctpErrorCauseCode.InvalidMandatoryParameter" />.
    /// </exception>
    /// <exception cref="SctpErrorCauseLengthInvalidException">
    /// An <see cref="SctpErrorCauseLengthInvalidException" /> is thrown if the specified Error Cause length is not equal to the length of an unsigned 32-bit integer.
    /// </exception>
    public static SctpInvalidMandatoryParameterError FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;

        var errorCauseCode = (SctpErrorCauseCode)BinaryPrimitives.ReadUInt16BigEndian(memorySpan);
        if (errorCauseCode != ErrorCauseCodeImplicit)
            throw new SctpErrorCauseCodeInvalidException(errorCauseCode);
        var errorCauseLength = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[sizeof(ushort)..]);
        if (errorCauseLength != ParameterLengthFixed)
            throw new SctpErrorCauseLengthInvalidException(errorCauseCode, errorCauseLength);

        return new SctpInvalidMandatoryParameterError();
    }

    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified Error Cause Code is not equal to <see cref="SctpErrorCauseCode.InvalidMandatoryParameter" />.
    /// </exception>
    /// <exception cref="SctpChunkParameterLengthInvalidException">
    /// An <see cref="SctpChunkParameterLengthInvalidException" /> is thrown if the specified Error Cause length is not equal to the length of an unsigned 32-bit integer.
    /// </exception>
    public static SctpInvalidMandatoryParameterError Read(BinaryReader reader) =>
        ISctpErrorCauseParameter<SctpInvalidMandatoryParameterError>.Read(reader);

    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified Error Cause Code is not equal to <see cref="SctpErrorCauseCode.InvalidMandatoryParameter" />.
    /// </exception>
    /// <exception cref="SctpChunkParameterLengthInvalidException">
    /// An <see cref="SctpChunkParameterLengthInvalidException" /> is thrown if the specified Error Cause length is not equal to the length of an unsigned 32-bit integer.
    /// </exception>
    public static SctpInvalidMandatoryParameterError Read(Stream stream) =>
        ISctpErrorCauseParameter<SctpInvalidMandatoryParameterError>.Read(stream);

    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified Error Cause Code is not equal to <see cref="SctpErrorCauseCode.InvalidMandatoryParameter" />.
    /// </exception>
    /// <exception cref="SctpChunkParameterLengthInvalidException">
    /// An <see cref="SctpChunkParameterLengthInvalidException" /> is thrown if the specified Error Cause length is not equal to the length of an unsigned 32-bit integer.
    /// </exception>
    public static Task<SctpInvalidMandatoryParameterError> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        ISctpErrorCauseParameter<SctpInvalidMandatoryParameterError>.ReadAsync(stream, cancellationToken);

    /// <inheritdoc />
    public ReadOnlyMemory<byte> ToReadOnlyMemory()
    {
        var memory = new Memory<byte>(new byte[ParameterLengthFixed]);
        this.Write(memory.Span);
        return memory;
    }

    /// <inheritdoc />
    public void Write(Span<byte> span)
    {
        BinaryPrimitives.WriteUInt16BigEndian(span, (ushort)ErrorCauseCodeImplicit);
        BinaryPrimitives.WriteUInt16BigEndian(span[sizeof(ushort)..], ParameterLengthFixed);
    }
}
