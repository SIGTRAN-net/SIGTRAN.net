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
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Variable.Address;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.ErrorCause;

internal readonly partial struct SctpRestartAssociationWithNewAddressesError
{
    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified Error Cause Code is not equal to <see cref="SctpErrorCauseCode.RestartAssociationWithNewAddresses" />.
    /// </exception>
    /// <exception cref="SctpErrorCauseLengthInvalidException">
    /// An <see cref="SctpErrorCauseLengthInvalidException" /> is thrown if the specified Error Cause Length is less than the Error Cause header and at least one Address parameter.
    /// </exception>
    public static SctpRestartAssociationWithNewAddressesError FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;

        /* Error Cause header */
        var errorCauseCode = (SctpErrorCauseCode)BinaryPrimitives.ReadUInt16BigEndian(memorySpan);
        if (errorCauseCode != ErrorCauseCodeImplicit)
            throw new SctpErrorCauseCodeInvalidException(errorCauseCode);
        var errorCauseLength = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[sizeof(ushort)..]);
        if (errorCauseLength < 3 * sizeof(uint))
            throw new SctpErrorCauseLengthInvalidException(errorCauseCode, errorCauseLength);

        /* New Address TLVs */
        var newAddressParameters = new List<ISctpAddressParameter>();
        var offset = sizeof(uint);
        while (offset < errorCauseLength)
        {
            var newAddressParameter = ISctpAddressParameter.FromReadOnlyMemory(memory[offset..]);
            newAddressParameters.Add(newAddressParameter);
            offset += newAddressParameter.ParameterLength;
        }
        
        return new(newAddressParameters.ToArray());
    }

    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified Error Cause Code is not equal to <see cref="SctpErrorCauseCode.RestartAssociationWithNewAddresses" />.
    /// </exception>
    /// <exception cref="SctpErrorCauseLengthInvalidException">
    /// An <see cref="SctpErrorCauseLengthInvalidException" /> is thrown if the specified Error Cause Length is less than the Error Cause header and at least one Address parameter.
    /// </exception>
    public static SctpRestartAssociationWithNewAddressesError Read(BinaryReader reader) =>
        ISctpErrorCauseParameter<SctpRestartAssociationWithNewAddressesError>.Read(reader);

    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified Error Cause Code is not equal to <see cref="SctpErrorCauseCode.RestartAssociationWithNewAddresses" />.
    /// </exception>
    /// <exception cref="SctpErrorCauseLengthInvalidException">
    /// An <see cref="SctpErrorCauseLengthInvalidException" /> is thrown if the specified Error Cause Length is less than the Error Cause header and at least one Address parameter.
    /// </exception>
    public static SctpRestartAssociationWithNewAddressesError Read(Stream stream) =>
        ISctpErrorCauseParameter<SctpRestartAssociationWithNewAddressesError>.Read(stream);

    /// <inheritdoc />
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified Error Cause Code is not equal to <see cref="SctpErrorCauseCode.RestartAssociationWithNewAddresses" />.
    /// </exception>
    /// <exception cref="SctpErrorCauseLengthInvalidException">
    /// An <see cref="SctpErrorCauseLengthInvalidException" /> is thrown if the specified Error Cause Length is less than the Error Cause header and at least one Address parameter.
    /// </exception>
    public static Task<SctpRestartAssociationWithNewAddressesError> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        ISctpErrorCauseParameter<SctpRestartAssociationWithNewAddressesError>.ReadAsync(stream, cancellationToken);

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

        /* New Address TLVs */
        var offset = sizeof(uint);
        var newAddressParametersSpan = this.newAddressParameters.Span;
        for (var i = 0; i < newAddressParametersSpan.Length; i++)
        {
            newAddressParametersSpan[i].Write(span[offset..]);
            offset += newAddressParametersSpan[i].ParameterLength;
        }
    }
}
