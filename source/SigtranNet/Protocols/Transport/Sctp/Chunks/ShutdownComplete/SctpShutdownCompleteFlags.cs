/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.ShutdownComplete;

/// <summary>
/// The Shutdown Complete (SHUTDOWN COMPLETE) Chunk Flags.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         T bit: 1 bit
///         The T bit is set to 0 if the sender filled in the Verification Tag expected by the peer. If the Verification Tag is reflected, the T bit MUST be set to 1. Reflecting means that the sent Verification Tag is the same as the received one.
///     </code>
///     <code>
///         Note: Special rules apply to this chunk for verification; please see <a href="https://datatracker.ietf.org/doc/html/rfc9260#sec_exceptions_in_verification_tag_rules">Section 8.5.1</a> for details. 
///     </code>
/// </remarks>
[Flags]
internal enum SctpShutdownCompleteFlags : byte
{
    /// <summary>
    /// The sender filed in the Verification Tag expected by the peer.
    /// </summary>
    VerificationTagExpected = 0b0000_0000,

    /// <summary>
    /// The Verification Tag is reflected. Reflecting means that the sent Verification Tag is the same as the received one.
    /// </summary>
    VerificationTagReflected = 0b0000_0001
}
