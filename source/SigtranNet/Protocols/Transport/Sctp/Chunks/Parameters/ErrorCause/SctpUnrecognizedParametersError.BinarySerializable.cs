/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause.Exceptions;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Exceptions;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Variable;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause;

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
