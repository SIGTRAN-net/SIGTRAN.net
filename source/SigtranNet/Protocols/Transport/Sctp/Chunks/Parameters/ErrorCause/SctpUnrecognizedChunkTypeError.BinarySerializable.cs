/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks.Exceptions;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause.Exceptions;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause;

internal readonly partial struct SctpUnrecognizedChunkTypeError
{
    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> that is thrown if the specified Error Cause Code is not <see cref="SctpErrorCauseCode.UnrecognizedChunkType" />.
    /// </exception>
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the chunk type specified in the unrecognized chunk is invalid.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if the chunk length specified in the unrecognized chunk is invalid.
    /// </exception>
    public static SctpUnrecognizedChunkTypeError FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;

        /* Error Cause Header */
        var errorCauseCode = (SctpErrorCauseCode)BinaryPrimitives.ReadUInt16BigEndian(memorySpan);
        if (errorCauseCode != ErrorCauseCodeImplicit)
            throw new SctpErrorCauseCodeInvalidException(errorCauseCode);
        var errorCauseLength = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[sizeof(ushort)..]);

        /* Error Cause Value */
        var unrecognizedChunk = ISctpChunk.FromReadOnlyMemory(memory[sizeof(uint)..errorCauseLength]);

        return new(unrecognizedChunk);
    }

    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> that is thrown if the specified Error Cause Code is not <see cref="SctpErrorCauseCode.UnrecognizedChunkType" />.
    /// </exception>
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the chunk type specified in the unrecognized chunk is invalid.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if the chunk length specified in the unrecognized chunk is invalid.
    /// </exception>
    public static SctpUnrecognizedChunkTypeError Read(BinaryReader reader) =>
        ISctpErrorCauseParameter<SctpUnrecognizedChunkTypeError>.Read(reader);

    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> that is thrown if the specified Error Cause Code is not <see cref="SctpErrorCauseCode.UnrecognizedChunkType" />.
    /// </exception>
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the chunk type specified in the unrecognized chunk is invalid.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if the chunk length specified in the unrecognized chunk is invalid.
    /// </exception>
    public static SctpUnrecognizedChunkTypeError Read(Stream stream) =>
        ISctpErrorCauseParameter<SctpUnrecognizedChunkTypeError>.Read(stream);

    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> that is thrown if the specified Error Cause Code is not <see cref="SctpErrorCauseCode.UnrecognizedChunkType" />.
    /// </exception>
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the chunk type specified in the unrecognized chunk is invalid.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if the chunk length specified in the unrecognized chunk is invalid.
    /// </exception>
    public static Task<SctpUnrecognizedChunkTypeError> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        ISctpErrorCauseParameter<SctpUnrecognizedChunkTypeError>.ReadAsync(stream, cancellationToken);

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
        this.unrecognizedChunk.Write(span[sizeof(uint)..]);
    }
}
