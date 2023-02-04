/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause;

/// <summary>
/// A Cookie Received While Shutting Down Error cause parameter in an SCTP Packet Chunk.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         A COOKIE ECHO chunk was received while the endpoint was in the SHUTDOWN-ACK-SENT state. This error is usually returned in an ERROR chunk bundled with the retransmitted SHUTDOWN ACK chunk.
///     </code>
/// </remarks>
internal readonly partial struct SctpCookieReceivedWhileShuttingDownError : ISctpErrorCauseParameter<SctpCookieReceivedWhileShuttingDownError>
{
    private const SctpErrorCauseCode ErrorCauseCodeImplicit = SctpErrorCauseCode.CookieReceivedWhileShuttingDown;
    private const ushort ErrorCauseLengthImplicit = sizeof(uint);

    /// <summary>
    /// Initializes a new instance of <see cref="SctpCookieReceivedWhileShuttingDownError" />.
    /// </summary>
    public SctpCookieReceivedWhileShuttingDownError()
    {
    }

    SctpErrorCauseCode ISctpErrorCause.ErrorCauseCode => ErrorCauseCodeImplicit;
    ushort ISctpErrorCause.ErrorCauseLength => ErrorCauseLengthImplicit;
}
