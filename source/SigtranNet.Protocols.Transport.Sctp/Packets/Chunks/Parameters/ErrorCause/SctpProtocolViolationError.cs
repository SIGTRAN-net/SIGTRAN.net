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

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.ErrorCause;

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
