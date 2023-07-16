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
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Variable.Address;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.ErrorCause;

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
