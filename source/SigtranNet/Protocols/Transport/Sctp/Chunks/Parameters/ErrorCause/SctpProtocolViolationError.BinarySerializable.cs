/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause.Exceptions;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause;

internal readonly partial struct SctpProtocolViolationError
{
    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified Error Cause Code is not equal to <see cref="SctpErrorCauseCode.ProtocolViolation" />.
    /// </exception>
    /// <exception cref="SctpErrorCauseLengthInvalidException">
    /// An <see cref="SctpErrorCauseLengthInvalidException" /> is thrown if the specified Error Cause Length is less than the Error Cause header.
    /// </exception>
    public static SctpProtocolViolationError FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;

        /* Error Cause header */
        var errorCauseCode = (SctpErrorCauseCode)BinaryPrimitives.ReadUInt16BigEndian(memorySpan);
        if (errorCauseCode != ErrorCauseCodeImplicit)
            throw new SctpErrorCauseCodeInvalidException(errorCauseCode);
        var errorCauseLength = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[sizeof(ushort)..]);
        if (errorCauseLength < sizeof(uint))
            throw new SctpErrorCauseLengthInvalidException(errorCauseCode, errorCauseLength);

        /* Additional Information */
        var additionalInformation = new Memory<byte>(new byte[errorCauseLength - sizeof(uint)]);
        memory[sizeof(uint)..errorCauseLength].CopyTo(additionalInformation);

        return new(additionalInformation);
    }

    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified Error Cause Code is not equal to <see cref="SctpErrorCauseCode.ProtocolViolation" />.
    /// </exception>
    /// <exception cref="SctpErrorCauseLengthInvalidException">
    /// An <see cref="SctpErrorCauseLengthInvalidException" /> is thrown if the specified Error Cause Length is less than the Error Cause header.
    /// </exception>
    public static SctpProtocolViolationError Read(BinaryReader reader) =>
        ISctpErrorCauseParameter<SctpProtocolViolationError>.Read(reader);

    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified Error Cause Code is not equal to <see cref="SctpErrorCauseCode.ProtocolViolation" />.
    /// </exception>
    /// <exception cref="SctpErrorCauseLengthInvalidException">
    /// An <see cref="SctpErrorCauseLengthInvalidException" /> is thrown if the specified Error Cause Length is less than the Error Cause header.
    /// </exception>
    public static SctpProtocolViolationError Read(Stream stream) =>
        ISctpErrorCauseParameter<SctpProtocolViolationError>.Read(stream);

    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified Error Cause Code is not equal to <see cref="SctpErrorCauseCode.ProtocolViolation" />.
    /// </exception>
    /// <exception cref="SctpErrorCauseLengthInvalidException">
    /// An <see cref="SctpErrorCauseLengthInvalidException" /> is thrown if the specified Error Cause Length is less than the Error Cause header.
    /// </exception>
    public static Task<SctpProtocolViolationError> ReadAsync(Stream stream, CancellationToken cancellationToken) =>
        ISctpErrorCauseParameter<SctpProtocolViolationError>.ReadAsync(stream, cancellationToken);

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
        BinaryPrimitives.WriteUInt16BigEndian(span, (ushort)ErrorCauseCodeImplicit);
        BinaryPrimitives.WriteUInt16BigEndian(span[sizeof(ushort)..], this.errorCauseLength);
        this.additionalInformation.Span.CopyTo(span[sizeof(uint)..this.errorCauseLength]);
    }
}
