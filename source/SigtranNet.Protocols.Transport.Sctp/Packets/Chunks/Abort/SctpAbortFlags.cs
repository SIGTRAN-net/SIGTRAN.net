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

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Abort;

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
