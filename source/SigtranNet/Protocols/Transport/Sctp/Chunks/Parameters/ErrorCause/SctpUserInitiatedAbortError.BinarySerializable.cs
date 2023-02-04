/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause.Exceptions;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause;

internal readonly partial struct SctpUserInitiatedAbortError
{
    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified Error Cause Code is not equal to <see cref="SctpErrorCauseCode.UserInitiatedAbort" />.
    /// </exception>
    /// <exception cref="SctpErrorCauseLengthInvalidException">
    /// An <see cref="SctpErrorCauseLengthInvalidException" /> is thrown if the specified Error Cause Length is less than the size of the Error Cause header.
    /// </exception>
    public static SctpUserInitiatedAbortError FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;

        /* Error Cause header */
        var errorCauseCode = (SctpErrorCauseCode)BinaryPrimitives.ReadUInt16BigEndian(memorySpan);
        if (errorCauseCode != ErrorCauseCodeImplicit)
            throw new SctpErrorCauseCodeInvalidException(errorCauseCode);
        var errorCauseLength = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[sizeof(ushort)..]);
        if (errorCauseLength < sizeof(uint))
            throw new SctpErrorCauseLengthInvalidException(errorCauseCode, errorCauseLength);

        /* Upper Layer Abort Reason */
        var upperLayerAbortReason = new Memory<byte>(new byte[errorCauseLength - sizeof(uint)]);
        memory[sizeof(uint)..errorCauseLength].CopyTo(upperLayerAbortReason);

        return new(upperLayerAbortReason);
    }

    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified Error Cause Code is not equal to <see cref="SctpErrorCauseCode.UserInitiatedAbort" />.
    /// </exception>
    /// <exception cref="SctpErrorCauseLengthInvalidException">
    /// An <see cref="SctpErrorCauseLengthInvalidException" /> is thrown if the specified Error Cause Length is less than the size of the Error Cause header.
    /// </exception>
    public static SctpUserInitiatedAbortError Read(BinaryReader reader) =>
        ISctpErrorCauseParameter<SctpUserInitiatedAbortError>.Read(reader);

    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified Error Cause Code is not equal to <see cref="SctpErrorCauseCode.UserInitiatedAbort" />.
    /// </exception>
    /// <exception cref="SctpErrorCauseLengthInvalidException">
    /// An <see cref="SctpErrorCauseLengthInvalidException" /> is thrown if the specified Error Cause Length is less than the size of the Error Cause header.
    /// </exception>
    public static SctpUserInitiatedAbortError Read(Stream stream) =>
        ISctpErrorCauseParameter<SctpUserInitiatedAbortError>.Read(stream);

    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified Error Cause Code is not equal to <see cref="SctpErrorCauseCode.UserInitiatedAbort" />.
    /// </exception>
    /// <exception cref="SctpErrorCauseLengthInvalidException">
    /// An <see cref="SctpErrorCauseLengthInvalidException" /> is thrown if the specified Error Cause Length is less than the size of the Error Cause header.
    /// </exception>
    public static Task<SctpUserInitiatedAbortError> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        ISctpErrorCauseParameter<SctpUserInitiatedAbortError>.ReadAsync(stream, cancellationToken);

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
        /* Error Cause header */
        BinaryPrimitives.WriteUInt16BigEndian(span, (ushort)ErrorCauseCodeImplicit);
        BinaryPrimitives.WriteUInt16BigEndian(span[sizeof(ushort)..], this.errorCauseLength);

        /* Upper Layer Abort Reason */
        this.upperLayerAbortReason.Span.CopyTo(span[sizeof(uint)..]);
    }
}
