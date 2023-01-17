/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Sctp.Chunks.Parameters.Variable.Address;

namespace SigtranNet.Protocols.Sctp.Chunks.Parameters.ErrorCause;

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
