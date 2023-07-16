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
/// Restart of an Association with New Addresses error cause in an SCTP Packet Chunk.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         An INIT chunk was received on an existing association. But the INIT chunk added addresses to the association that were previously not part of the association. The new addresses are listed in the error cause. This error cause is normally sent as part of an ABORT chunk refusing the INIT chunk (see <a href="https://datatracker.ietf.org/doc/html/rfc9260#sec_handle_duplicate_or_unexpected_chunks">Section 5.2</a>).
///     </code>
/// </remarks>
internal readonly partial struct SctpRestartAssociationWithNewAddressesError : ISctpErrorCauseParameter<SctpRestartAssociationWithNewAddressesError>
{
    private const SctpErrorCauseCode ErrorCauseCodeImplicit = SctpErrorCauseCode.RestartAssociationWithNewAddresses;

    /// <summary>
    /// The Error Cause Length.
    /// </summary>
    internal readonly ushort errorCauseLength;

    /// <summary>
    /// The New Address parameters.
    /// </summary>
    internal readonly ReadOnlyMemory<ISctpAddressParameter> newAddressParameters;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpRestartAssociationWithNewAddressesError" />.
    /// </summary>
    /// <param name="newAddressParameters">The New Address parameters.</param>
    internal SctpRestartAssociationWithNewAddressesError(ReadOnlyMemory<ISctpAddressParameter> newAddressParameters)
    {
        this.errorCauseLength = sizeof(uint);
        var newAddressParametersSpan = newAddressParameters.Span;
        for (var i = 0; i < newAddressParameters.Length; i++)
        {
            this.errorCauseLength += newAddressParametersSpan[i].ParameterLength;
        }

        this.newAddressParameters = newAddressParameters;
    }

    SctpErrorCauseCode ISctpErrorCause.ErrorCauseCode => ErrorCauseCodeImplicit;
    ushort ISctpErrorCause.ErrorCauseLength => this.errorCauseLength;
}
