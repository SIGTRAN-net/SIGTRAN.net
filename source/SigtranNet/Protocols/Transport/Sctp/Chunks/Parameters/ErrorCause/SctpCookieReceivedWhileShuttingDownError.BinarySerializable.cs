/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause.Exceptions;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Exceptions;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause;

internal readonly partial struct SctpCookieReceivedWhileShuttingDownError
{
    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified error cause code is not equal to <see cref="SctpErrorCauseCode.CookieReceivedWhileShuttingDown" />.
    /// </exception>
    /// <exception cref="SctpErrorCauseLengthInvalidException">
    /// An <see cref="SctpErrorCauseLengthInvalidException" /> is thrown if the specified error cause length is not equal to the size of an unsigned 32-bit integer.
    /// </exception>
    public static SctpCookieReceivedWhileShuttingDownError FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;
        var errorCauseCode = (SctpErrorCauseCode)BinaryPrimitives.ReadUInt16BigEndian(memorySpan);
        if (errorCauseCode != ErrorCauseCodeImplicit)
            throw new SctpErrorCauseCodeInvalidException(errorCauseCode);
        var errorCauseLength = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[sizeof(ushort)..]);
        if (errorCauseLength != ErrorCauseLengthImplicit)
            throw new SctpErrorCauseLengthInvalidException(errorCauseCode, errorCauseLength);
        return new SctpCookieReceivedWhileShuttingDownError();
    }

    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified error cause code is not equal to <see cref="SctpErrorCauseCode.CookieReceivedWhileShuttingDown" />.
    /// </exception>
    /// <exception cref="SctpChunkParameterLengthInvalidException">
    /// An <see cref="SctpChunkParameterLengthInvalidException" /> is thrown if the specified error cause length is not equal to the size of an unsigned 32-bit integer.
    /// </exception>
    public static SctpCookieReceivedWhileShuttingDownError Read(BinaryReader reader) =>
        ISctpErrorCauseParameter<SctpCookieReceivedWhileShuttingDownError>.Read(reader);

    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified error cause code is not equal to <see cref="SctpErrorCauseCode.CookieReceivedWhileShuttingDown" />.
    /// </exception>
    /// <exception cref="SctpChunkParameterLengthInvalidException">
    /// An <see cref="SctpChunkParameterLengthInvalidException" /> is thrown if the specified error cause length is not equal to the size of an unsigned 32-bit integer.
    /// </exception>
    public static SctpCookieReceivedWhileShuttingDownError Read(Stream stream) =>
        ISctpErrorCauseParameter<SctpCookieReceivedWhileShuttingDownError>.Read(stream);

    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified error cause code is not equal to <see cref="SctpErrorCauseCode.CookieReceivedWhileShuttingDown" />.
    /// </exception>
    /// <exception cref="SctpChunkParameterLengthInvalidException">
    /// An <see cref="SctpChunkParameterLengthInvalidException" /> is thrown if the specified error cause length is not equal to the size of an unsigned 32-bit integer.
    /// </exception>
    public static Task<SctpCookieReceivedWhileShuttingDownError> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        ISctpErrorCauseParameter<SctpCookieReceivedWhileShuttingDownError>.ReadAsync(stream, cancellationToken);

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
}
