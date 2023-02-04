/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause.Exceptions;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Exceptions;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Variable.Address;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause;

internal readonly partial struct SctpUnresolvableAddressError
{
    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified Error Cause Code is not <see cref="SctpErrorCauseCode.UnresolvableAddress" />.
    /// </exception>
    /// <exception cref="SctpChunkParameterTypeInvalidException">
    /// An <see cref="SctpChunkParameterTypeInvalidException" /> is thrown if the parameter type specified in the Unresolvable Address field is invalid, unsupported or unrecognized.
    /// </exception>
    public static SctpUnresolvableAddressError FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;

        /* Error Cause Header */
        var errorCauseCode = (SctpErrorCauseCode)BinaryPrimitives.ReadUInt16BigEndian(memorySpan);
        if (errorCauseCode != ErrorCauseCodeImplicit)
            throw new SctpErrorCauseCodeInvalidException(errorCauseCode);
        var errorCauseLength = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[sizeof(ushort)..]);

        /* Error Cause Value */
        var unresolvableAddress = ISctpAddressParameter.FromReadOnlyMemory(memory[sizeof(uint)..errorCauseLength]);

        /* Result */
        return new(unresolvableAddress);
    }

    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified Error Cause Code is not <see cref="SctpErrorCauseCode.UnresolvableAddress" />.
    /// </exception>
    /// <exception cref="SctpChunkParameterTypeInvalidException">
    /// An <see cref="SctpChunkParameterTypeInvalidException" /> is thrown if the parameter type specified in the Unresolvable Address field is invalid, unsupported or unrecognized.
    /// </exception>
    public static SctpUnresolvableAddressError Read(BinaryReader reader) =>
        ISctpErrorCauseParameter<SctpUnresolvableAddressError>.Read(reader);

    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified Error Cause Code is not <see cref="SctpErrorCauseCode.UnresolvableAddress" />.
    /// </exception>
    /// <exception cref="SctpChunkParameterTypeInvalidException">
    /// An <see cref="SctpChunkParameterTypeInvalidException" /> is thrown if the parameter type specified in the Unresolvable Address field is invalid, unsupported or unrecognized.
    /// </exception>
    public static SctpUnresolvableAddressError Read(Stream stream) =>
        ISctpErrorCauseParameter<SctpUnresolvableAddressError>.Read(stream);

    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified Error Cause Code is not <see cref="SctpErrorCauseCode.UnresolvableAddress" />.
    /// </exception>
    /// <exception cref="SctpChunkParameterTypeInvalidException">
    /// An <see cref="SctpChunkParameterTypeInvalidException" /> is thrown if the parameter type specified in the Unresolvable Address field is invalid, unsupported or unrecognized.
    /// </exception>
    public static Task<SctpUnresolvableAddressError> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        ISctpErrorCauseParameter<SctpUnresolvableAddressError>.ReadAsync(stream, cancellationToken);

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
        this.unresolvableAddress.Write(span[sizeof(uint)..]);
    }
}
