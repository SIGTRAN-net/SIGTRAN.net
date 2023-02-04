/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Variable.Address;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause;

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
