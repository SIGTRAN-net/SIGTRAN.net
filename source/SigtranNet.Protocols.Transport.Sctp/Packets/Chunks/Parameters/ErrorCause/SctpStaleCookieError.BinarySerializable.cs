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

internal readonly partial struct SctpStaleCookieError
{
    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified Error Cause is invalid.
    /// </exception>
    /// <exception cref="SctpErrorCauseLengthInvalidException">
    /// An <see cref="SctpErrorCauseLengthInvalidException" /> is thrown if the specified Error Cause Length is not equal to the predefined length of a Stale Cookie error cause parameter.
    /// </exception>
    public static SctpStaleCookieError FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;

        /* Parameter Header */
        var errorCauseCode = (SctpErrorCauseCode)BinaryPrimitives.ReadUInt16BigEndian(memorySpan);
        if (errorCauseCode != ErrorCauseCodeImplicit)
            throw new SctpErrorCauseCodeInvalidException(errorCauseCode);
        var parameterLength = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[sizeof(ushort)..]);
        if (parameterLength != ErrorCauseLengthImplicit)
            throw new SctpErrorCauseLengthInvalidException(errorCauseCode, parameterLength);

        /* Measure of Staleness */
        var measureOfStaleness = BinaryPrimitives.ReadUInt32BigEndian(memorySpan[sizeof(uint)..]);

        return new(measureOfStaleness);
    }

    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified Error Cause is invalid.
    /// </exception>
    /// <exception cref="SctpChunkParameterLengthInvalidException">
    /// An <see cref="SctpChunkParameterLengthInvalidException" /> is thrown if the specified parameter length is not equal to the fixed length of a Stale Cookie error cause parameter.
    /// </exception>
    public static SctpStaleCookieError Read(BinaryReader reader) =>
        ISctpErrorCauseParameter<SctpStaleCookieError>.Read(reader);

    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified Error Cause is invalid.
    /// </exception>
    /// <exception cref="SctpChunkParameterLengthInvalidException">
    /// An <see cref="SctpChunkParameterLengthInvalidException" /> is thrown if the specified parameter length is not equal to the fixed length of a Stale Cookie error cause parameter.
    /// </exception>
    public static SctpStaleCookieError Read(Stream stream) =>
        ISctpErrorCauseParameter<SctpStaleCookieError>.Read(stream);

    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified Error Cause is invalid.
    /// </exception>
    /// <exception cref="SctpChunkParameterLengthInvalidException">
    /// An <see cref="SctpChunkParameterLengthInvalidException" /> is thrown if the specified parameter length is not equal to the fixed length of a Stale Cookie error cause parameter.
    /// </exception>
    public static Task<SctpStaleCookieError> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        ISctpErrorCauseParameter<SctpStaleCookieError>.ReadAsync(stream, cancellationToken);

    /// <inheritdoc />
    public ReadOnlyMemory<byte> ToReadOnlyMemory()
    {
        var memory = new Memory<byte>(new byte[ErrorCauseLengthImplicit]);
        this.Write(memory.Span);
        return memory;
    }

    /// <inheritdoc />
    public void Write(Span<byte> span)
    {
        /* Error Cause Header */
        BinaryPrimitives.WriteUInt16BigEndian(span, (ushort)ErrorCauseCodeImplicit);
        BinaryPrimitives.WriteUInt16BigEndian(span[sizeof(ushort)..], ErrorCauseLengthImplicit);

        /* Error Cause Value */
        BinaryPrimitives.WriteUInt32BigEndian(span[sizeof(uint)..], this.measureOfStaleness);
    }
}
