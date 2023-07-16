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

using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.ErrorCause;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Variable.Address;

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.ErrorCause;

/// <summary>
/// An Unresolvable Address error parameter in an SCTP Packet Chunk.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         Indicates that the sender is not able to resolve the specified address parameter (e.g., type of address is not supported by the sender). This is usually sent in combination with or within an ABORT chunk.
///     </code>
/// </remarks>
internal readonly partial struct SctpUnresolvableAddressError : ISctpErrorCauseParameter<SctpUnresolvableAddressError>
{
    private const SctpErrorCauseCode ErrorCauseCodeImplicit = SctpErrorCauseCode.UnresolvableAddress;

    /// <summary>
    /// The Cause Length.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         Set to the size of the parameter in bytes, including the Cause Code, Cause Length, and Cause-Specific Information fields.
    ///     </code>
    /// </remarks>
    internal readonly ushort errorCauseLength;

    /// <summary>
    /// The Unresolvable Address.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         The Unresolvable Address field contains the complete Type, Length, and Value of the address parameter (or Host Name parameter) that contains the unresolvable address or host name.
    ///     </code>
    /// </remarks>
    internal readonly ISctpAddressParameter unresolvableAddress;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpUnresolvableAddressError" />.
    /// </summary>
    /// <param name="unresolvableAddress">The Unresolvable Address.</param>
    internal SctpUnresolvableAddressError(ISctpAddressParameter unresolvableAddress)
    {
        this.errorCauseLength = (ushort)(sizeof(uint) + unresolvableAddress.ParameterLength);
        this.unresolvableAddress = unresolvableAddress;
    }

    SctpErrorCauseCode ISctpErrorCause.ErrorCauseCode => ErrorCauseCodeImplicit;
    ushort ISctpErrorCause.ErrorCauseLength => this.errorCauseLength;
}
