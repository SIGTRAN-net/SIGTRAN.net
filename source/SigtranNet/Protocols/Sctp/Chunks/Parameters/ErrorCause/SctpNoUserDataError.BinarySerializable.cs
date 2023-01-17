/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;
using SigtranNet.Protocols.Sctp.Chunks.Parameters.ErrorCause.Exceptions;
using SigtranNet.Protocols.Sctp.Chunks.Parameters.Exceptions;
using SigtranNet.Protocols.Sctp.Chunks.Parameters.Fixed;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Sctp.Chunks.Parameters.ErrorCause;

internal readonly partial struct SctpNoUserDataError
{
    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified Error Cause Code is not <see cref="SctpErrorCauseCode.NoUserData" />.
    /// </exception>
    /// <exception cref="SctpErrorCauseLengthInvalidException">
    /// An <see cref="SctpErrorCauseLengthInvalidException" /> is thrown if the specified Error Cause Length is not the predefined length for a No User Data error parameter.
    /// </exception>
    public static SctpNoUserDataError FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;

        // Error Cause Header
        var errorCauseCode = (SctpErrorCauseCode)BinaryPrimitives.ReadUInt16BigEndian(memorySpan);
        if (errorCauseCode != ErrorCauseCodeImplicit)
            throw new SctpErrorCauseCodeInvalidException(errorCauseCode);
        var errorCauseLength = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[sizeof(ushort)..]);
        if (errorCauseLength != ErrorCauseLengthImplicit)
            throw new SctpErrorCauseLengthInvalidException(errorCauseCode, errorCauseLength);

        // Transmission Sequence Number (TSN)
        var transmissionSequenceNumber = SctpTransmissionSequenceNumber.FromReadOnlyMemory(memory[sizeof(uint)..]);

        return new(transmissionSequenceNumber);
    }

    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified Error Cause Code is not <see cref="SctpErrorCauseCode.NoUserData" />.
    /// </exception>
    /// <exception cref="SctpChunkParameterLengthInvalidException">
    /// An <see cref="SctpChunkParameterLengthInvalidException" /> is thrown if the specified Error Cause Length is not the predefined length for a No User Data error parameter.
    /// </exception>
    public static SctpNoUserDataError Read(BinaryReader reader) =>
        ISctpErrorCauseParameter<SctpNoUserDataError>.Read(reader);

    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified Error Cause Code is not <see cref="SctpErrorCauseCode.NoUserData" />.
    /// </exception>
    /// <exception cref="SctpChunkParameterLengthInvalidException">
    /// An <see cref="SctpChunkParameterLengthInvalidException" /> is thrown if the specified Error Cause Length is not the predefined length for a No User Data error parameter.
    /// </exception>
    public static SctpNoUserDataError Read(Stream stream) =>
        ISctpErrorCauseParameter<SctpNoUserDataError>.Read(stream);

    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified Error Cause Code is not <see cref="SctpErrorCauseCode.NoUserData" />.
    /// </exception>
    /// <exception cref="SctpChunkParameterLengthInvalidException">
    /// An <see cref="SctpChunkParameterLengthInvalidException" /> is thrown if the specified Error Cause Length is not the predefined length for a No User Data error parameter.
    /// </exception>
    public static Task<SctpNoUserDataError> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        ISctpErrorCauseParameter<SctpNoUserDataError>.ReadAsync(stream, cancellationToken);

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
        this.transmissionSequenceNumber.Write(span[sizeof(uint)..]);
    }
}
