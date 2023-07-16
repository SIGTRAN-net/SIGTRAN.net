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

using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Fixed;

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.ErrorCause;

/// <summary>
/// A No User Data error cause parameter in an SCTP Packet Chunk.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         This error cause is returned to the originator of a DATA chunk if a received DATA chunk has no user data.
///     </code>
/// </remarks>
internal readonly partial struct SctpNoUserDataError : ISctpErrorCauseParameter<SctpNoUserDataError>
{
    private const SctpErrorCauseCode ErrorCauseCodeImplicit = SctpErrorCauseCode.NoUserData;
    private const ushort ErrorCauseLengthImplicit = 2 * sizeof(uint);

    /// <summary>
    /// The Transmission Sequence Number (TSN) of the DATA chunk that has no data.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         This parameter contains the TSN of the DATA chunk received with no User Data field.
    ///     </code>
    ///     <code>
    ///         This cause code is normally returned in an ABORT chunk (see <a href="https://datatracker.ietf.org/doc/html/rfc9260#sec_acknowledgements_of_reception_of_data_chunks">Section 6.2</a>).
    ///     </code>
    /// </remarks>
    internal readonly SctpTransmissionSequenceNumber transmissionSequenceNumber;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpNoUserDataError" />.
    /// </summary>
    /// <param name="transmissionSequenceNumber">The Transmission Sequence Number (TSN) of the DATA chunk that has no data.</param>
    internal SctpNoUserDataError(SctpTransmissionSequenceNumber transmissionSequenceNumber)
    {
        this.transmissionSequenceNumber = transmissionSequenceNumber;
    }

    SctpErrorCauseCode ISctpErrorCause.ErrorCauseCode => ErrorCauseCodeImplicit;
    ushort ISctpErrorCause.ErrorCauseLength => ErrorCauseLengthImplicit;
}
