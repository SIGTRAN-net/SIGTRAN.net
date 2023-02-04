/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause;

/// <summary>
/// A Protocol Violation Error Cause in an SCTP Packet Chunk.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         This error cause MAY be included in ABORT chunks that are sent because an SCTP endpoint detects a protocol violation of the peer that is not covered by the error causes described in Sections <a href="https://datatracker.ietf.org/doc/html/rfc9260#sec_invalid_stream_identifier_cause">3.3.10.1</a> - <a href="https://datatracker.ietf.org/doc/html/rfc9260#sec_user_initiated_abort_cause">3.3.10.12</a>. An implementation MAY provide additional information specifying what kind of protocol violation has been detected.
///     </code>
/// </remarks>
internal readonly partial struct SctpProtocolViolationError : ISctpErrorCauseParameter<SctpProtocolViolationError>
{
    private const SctpErrorCauseCode ErrorCauseCodeImplicit = SctpErrorCauseCode.ProtocolViolation;

    /// <summary>
    /// The Error Cause Length.
    /// </summary>
    internal readonly ushort errorCauseLength;

    /// <summary>
    /// The Additional Information.
    /// </summary>
    internal readonly ReadOnlyMemory<byte> additionalInformation;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpProtocolViolationError" />.
    /// </summary>
    /// <param name="additionalInformation">
    /// The Additional Information.
    /// </param>
    internal SctpProtocolViolationError(ReadOnlyMemory<byte> additionalInformation)
    {
        this.errorCauseLength = (ushort)(sizeof(uint) + additionalInformation.Length);
        this.additionalInformation = additionalInformation;
    }

    SctpErrorCauseCode ISctpErrorCause.ErrorCauseCode => ErrorCauseCodeImplicit;
    ushort ISctpErrorCause.ErrorCauseLength => this.errorCauseLength;
}
