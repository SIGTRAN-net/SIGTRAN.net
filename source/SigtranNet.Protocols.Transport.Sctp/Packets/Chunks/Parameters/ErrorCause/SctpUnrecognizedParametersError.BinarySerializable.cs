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
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Variable;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.ErrorCause;

internal readonly partial struct SctpUnrecognizedParametersError
{
    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified error cause code is not <see cref="SctpErrorCauseCode.UnrecognizedParameters" />.
    /// </exception>
    /// <exception cref="SctpErrorCauseLengthInvalidException">
    /// An <see cref="SctpErrorCauseLengthInvalidException" /> is thrown if the specified error cause length is too short to contain the error cause information and at least one unrecognized parameter.
    /// </exception>
    public static SctpUnrecognizedParametersError FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;

        // Error Cause Header
        var errorCauseCode = (SctpErrorCauseCode)BinaryPrimitives.ReadUInt16BigEndian(memorySpan);
        if (errorCauseCode != ErrorCauseCodeImplicit)
            throw new SctpErrorCauseCodeInvalidException(errorCauseCode);
        var errorCauseLength = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[sizeof(ushort)..]);
        if (errorCauseLength < 2 * sizeof(uint))
            throw new SctpErrorCauseLengthInvalidException(errorCauseCode, errorCauseLength);

        // Unrecognized Parameters
        var unrecognizedParameters = new List<ISctpChunkParameterVariableLength>();
        var offset = sizeof(uint);
        while (offset < errorCauseLength)
        {
            var unrecognizedParameter = ISctpChunkParameterVariableLength.FromReadOnlyMemory(memory[offset..]);
            unrecognizedParameters.Add(unrecognizedParameter);
            offset += unrecognizedParameter.ParameterLength;
        }

        return new(unrecognizedParameters.ToArray());
    }

    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified error cause code is not <see cref="SctpErrorCauseCode.UnrecognizedParameters" />.
    /// </exception>
    /// <exception cref="SctpChunkParameterLengthInvalidException">
    /// An <see cref="SctpChunkParameterLengthInvalidException" /> is thrown if the specified error cause length is too short to contain the error cause information and at least one unrecognized parameter.
    /// </exception>
    public static SctpUnrecognizedParametersError Read(BinaryReader reader) =>
        ISctpErrorCauseParameter<SctpUnrecognizedParametersError>.Read(reader);

    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified error cause code is not <see cref="SctpErrorCauseCode.UnrecognizedParameters" />.
    /// </exception>
    /// <exception cref="SctpChunkParameterLengthInvalidException">
    /// An <see cref="SctpChunkParameterLengthInvalidException" /> is thrown if the specified error cause length is too short to contain the error cause information and at least one unrecognized parameter.
    /// </exception>
    public static SctpUnrecognizedParametersError Read(Stream stream) =>
        ISctpErrorCauseParameter<SctpUnrecognizedParametersError>.Read(stream);

    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified error cause code is not <see cref="SctpErrorCauseCode.UnrecognizedParameters" />.
    /// </exception>
    /// <exception cref="SctpChunkParameterLengthInvalidException">
    /// An <see cref="SctpChunkParameterLengthInvalidException" /> is thrown if the specified error cause length is too short to contain the error cause information and at least one unrecognized parameter.
    /// </exception>
    public static Task<SctpUnrecognizedParametersError> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        ISctpErrorCauseParameter<SctpUnrecognizedParametersError>.ReadAsync(stream, cancellationToken);

    /// <inheritdoc />
    public ReadOnlyMemory<byte> ToReadOnlyMemory()
    {
        var memory = new Memory<byte>(new byte[this.errorCauseLength]);
        this.Write(memory.Span);
        return memory;
    }

    /// <inheritdoc />
    public void Write(Span<byte> span)
    {
        // Error Cause Header
        BinaryPrimitives.WriteUInt16BigEndian(span, (ushort)ErrorCauseCodeImplicit);
        BinaryPrimitives.WriteUInt16BigEndian(span[sizeof(ushort)..], this.errorCauseLength);

        // Unrecognized Parameters
        var offset = sizeof(uint);
        var unrecognizedParametersSpan = this.unrecognizedParameters.Span;
        for (var i = 0; i < unrecognizedParameters.Length; i++)
        {
            unrecognizedParametersSpan[i].Write(span[offset..]);
            offset += unrecognizedParametersSpan[i].ParameterLength;
        }
    }
}
