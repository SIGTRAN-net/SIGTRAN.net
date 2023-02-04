/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause.Exceptions;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Exceptions;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause;

internal readonly partial struct SctpOutOfResourceError
{
    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the Error Cause Code specified is not equal to <see cref="SctpErrorCauseCode.OutOfResource" />.
    /// </exception>
    /// <exception cref="SctpErrorCauseLengthInvalidException">
    /// An <see cref="SctpErrorCauseLengthInvalidException" /> is thrown if the Error Cause Length is not equal to the predefined length of an Out of Resource error cause parameter.
    /// </exception>
    public static SctpOutOfResourceError FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;

        var errorCauseCode = (SctpErrorCauseCode)BinaryPrimitives.ReadUInt16BigEndian(memorySpan);
        if (errorCauseCode != ErrorCauseCodeImplicit)
            throw new SctpErrorCauseCodeInvalidException(errorCauseCode);
        var parameterLength = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[sizeof(ushort)..]);
        if (parameterLength != ErrorCauseLengthImplicit)
            throw new SctpErrorCauseLengthInvalidException(errorCauseCode, parameterLength);

        return new SctpOutOfResourceError();
    }

    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the Error Cause Code specified is not equal to <see cref="SctpErrorCauseCode.OutOfResource" />.
    /// </exception>
    /// <exception cref="SctpChunkParameterLengthInvalidException">
    /// An <see cref="SctpChunkParameterLengthInvalidException" /> is thrown if the parameter length is not equal to the predefined length of an Out of Resource error cause parameter.
    /// </exception>
    public static SctpOutOfResourceError Read(BinaryReader reader) =>
        ISctpErrorCauseParameter<SctpOutOfResourceError>.Read(reader);

    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the Error Cause Code specified is not equal to <see cref="SctpErrorCauseCode.OutOfResource" />.
    /// </exception>
    /// <exception cref="SctpChunkParameterLengthInvalidException">
    /// An <see cref="SctpChunkParameterLengthInvalidException" /> is thrown if the parameter length is not equal to the predefined length of an Out of Resource error cause parameter.
    /// </exception>
    public static SctpOutOfResourceError Read(Stream stream) =>
        ISctpErrorCauseParameter<SctpOutOfResourceError>.Read(stream);

    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the Error Cause Code specified is not equal to <see cref="SctpErrorCauseCode.OutOfResource" />.
    /// </exception>
    /// <exception cref="SctpChunkParameterLengthInvalidException">
    /// An <see cref="SctpChunkParameterLengthInvalidException" /> is thrown if the parameter length is not equal to the predefined length of an Out of Resource error cause parameter.
    /// </exception>
    public static Task<SctpOutOfResourceError> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        ISctpErrorCauseParameter<SctpOutOfResourceError>.ReadAsync(stream, cancellationToken);

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
        BinaryPrimitives.WriteUInt16BigEndian(span, (ushort)ErrorCauseCodeImplicit);
        BinaryPrimitives.WriteUInt16BigEndian(span[sizeof(ushort)..], ErrorCauseLengthImplicit);
    }

    /// <inheritdoc />
    public Task WriteAsync(Memory<byte> memory, CancellationToken cancellationToken = default)
    {
        this.Write(memory.Span);
        return Task.CompletedTask;
    }
}
