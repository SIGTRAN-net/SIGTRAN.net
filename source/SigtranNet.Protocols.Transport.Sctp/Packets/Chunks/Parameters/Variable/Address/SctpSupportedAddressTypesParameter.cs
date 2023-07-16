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

using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Variable;

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Variable.Address;

/// <summary>
/// A Supported Address Types Parameter for an SCTP Packet Chunk.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         The sender of the INIT chunk uses this parameter to list all the address types it can support.
///     </code>
/// </remarks>
internal readonly partial struct SctpSupportedAddressTypesParameter : ISctpChunkParameterVariableLength<SctpSupportedAddressTypesParameter>
{
    private const SctpChunkParameterType ParameterTypeImplicit = SctpChunkParameterType.SupportedAddressTypes;

    /// <summary>
    /// The Parameter Length.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         The Parameter Length field contains the size of the parameter in bytes, including the Parameter Type, Parameter Length, and Parameter Value fields. Thus, a parameter with a zero-length Parameter Value field would have a Parameter Length field of 4. The Parameter Length does not include any padding bytes.
    ///     </code>
    /// </remarks>
    internal readonly ushort parameterLength;

    /// <summary>
    /// The supported address types.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         This is filled with the type value of the corresponding address TLV (e.g., 5 for indicating IPv4, and 6 for indicating IPv6). The value indicating the Host Name Address parameter MUST NOT be used when sending this parameter and MUST be ignored when receiving this parameter.
    ///     </code>
    /// </remarks>
    internal readonly ReadOnlyMemory<SctpChunkParameterType> supportedAddressTypes;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpSupportedAddressTypesParameter" />.
    /// </summary>
    /// <param name="supportedAddressTypes">The supported address types.</param>
    internal SctpSupportedAddressTypesParameter(ReadOnlyMemory<SctpChunkParameterType> supportedAddressTypes)
    {
        this.parameterLength = (ushort)(sizeof(uint) + supportedAddressTypes.Length * sizeof(ushort));
        this.supportedAddressTypes = supportedAddressTypes;
    }

    SctpChunkParameterType ISctpChunkParameterVariableLength.ParameterType => ParameterTypeImplicit;
    ushort ISctpChunkParameter.ParameterLength => this.parameterLength;
}
