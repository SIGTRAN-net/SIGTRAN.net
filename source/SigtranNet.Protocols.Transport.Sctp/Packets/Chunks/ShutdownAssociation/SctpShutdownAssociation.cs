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

using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Fixed;

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.ShutdownAssociation;

/// <summary>
/// A Shutdown Association (SHUTDOWN) chunk.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         An endpoint in an association MUST use this chunk to initiate a graceful close of the association with its peer.
///     </code>
///     <code>
///         Note: Since the SHUTDOWN chunk does not contain Gap Ack Blocks, it cannot be used to acknowledge TSNs received out of order. In a SACK chunk, lack of Gap Ack Blocks that were previously included indicates that the data receiver reneged on the associated DATA chunks.
/// 
///         Since the SHUTDOWN chunk does not contain Gap Ack Blocks, the receiver of the SHUTDOWN chunk MUST NOT interpret the lack of a Gap Ack Block as a renege. (See <a href="https://datatracker.ietf.org/doc/html/rfc9260#sec_acknowledgements_of_reception_of_data_chunks">Section 6.2</a> for information on reneging.)
///
///         The sender of the SHUTDOWN chunk MAY bundle a SACK chunk to indicate any gaps in the received TSNs.
///     </code>
/// </remarks>
internal readonly partial struct SctpShutdownAssociation : ISctpChunk<SctpShutdownAssociation>
{
    private const SctpChunkType ChunkTypeImplicit = SctpChunkType.Shutdown;
    private const ushort ChunkLengthImplicit = 2 * sizeof(uint);

    /// <summary>
    /// The Cumultative Transmission Sequence Number Acknowledgement (Cumulative TSN Ack).
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         The largest TSN, such that all TSNs smaller than or equal to it have been received and the next one has not been received.
    ///     </code>
    /// </remarks>
    internal readonly SctpTransmissionSequenceNumber cumulativeTransmissionSequenceNumberAcknowledgement;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpShutdownAssociation" />.
    /// </summary>
    /// <param name="cumulativeTransmissionSequenceNumberAcknowledgement">
    /// The Cumulative Transmission Sequence Number Acknowledgement.
    /// </param>
    internal SctpShutdownAssociation(SctpTransmissionSequenceNumber cumulativeTransmissionSequenceNumberAcknowledgement)
    {
        this.cumulativeTransmissionSequenceNumberAcknowledgement = cumulativeTransmissionSequenceNumberAcknowledgement;
    }

    /// <inheritdoc />
    SctpChunkType ISctpChunk.ChunkType => ChunkTypeImplicit;

    /// <inheritdoc />
    /// <remarks>
    ///     SHUTDOWN does not have Chunk Flags.
    /// </remarks>
    byte ISctpChunk.ChunkFlags => 0;

    /// <inheritdoc />
    ushort ISctpChunk.ChunkLength => ChunkLengthImplicit;

    /// <inheritdoc />
    ReadOnlyMemory<ISctpChunkParameter> ISctpChunk.Parameters => new();
}
