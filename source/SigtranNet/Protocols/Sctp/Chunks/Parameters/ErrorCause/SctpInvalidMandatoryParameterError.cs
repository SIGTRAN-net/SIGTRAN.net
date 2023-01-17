/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Sctp.Chunks.Parameters.ErrorCause;

/// <summary>
/// An Invalid Mandatory Parameter error cause in an SCTP Packet Chunk.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         This error cause is returned to the originator of an INIT or INIT ACK chunk when one of the mandatory parameters is set to an invalid value.
///     </code>
/// </remarks>
internal readonly partial struct SctpInvalidMandatoryParameterError : ISctpErrorCauseParameter<SctpInvalidMandatoryParameterError>
{
    private const SctpErrorCauseCode ErrorCauseCodeImplicit = SctpErrorCauseCode.InvalidMandatoryParameter;
    private const ushort ParameterLengthFixed = sizeof(uint);

    /// <summary>
    /// Initializes a new instance of <see cref="SctpInvalidMandatoryParameterError" />.
    /// </summary>
    public SctpInvalidMandatoryParameterError()
    {
    }

    SctpErrorCauseCode ISctpErrorCause.ErrorCauseCode => ErrorCauseCodeImplicit;
    ushort ISctpErrorCause.ErrorCauseLength => ParameterLengthFixed;
}
