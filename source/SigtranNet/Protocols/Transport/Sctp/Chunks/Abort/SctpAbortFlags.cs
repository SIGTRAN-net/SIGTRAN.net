/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Abort;

/// <summary>
/// The Chunk Flags of an ABORT chunk in an SCTP Packet.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         Reserved: 7 bits
///
///         T bit: 1 bit
///         The T bit is set to 0 if the sender filled in the Verification Tag expected by the peer.If the Verification Tag is reflected, the T bit MUST be set to 1. Reflecting means that the sent Verification Tag is the same as the received one
///     </code>
/// </remarks>
[Flags]
internal enum SctpAbortFlags
{
    /// <summary>
    /// The sender filled in the Verification Tag expected by the peer.
    /// </summary>
    VerificationTagAsExpected = 0x0,

    /// <summary>
    /// The sent Verification Tag is the same as the received one.
    /// </summary>
    VerificationTagReflected = 0x1
}
