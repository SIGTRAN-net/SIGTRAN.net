/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Fixed;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.SelectiveAcknowledgement;

/// <summary>
/// A Selective Acknowledgement (SACK) chunk.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         This chunk is sent to the peer endpoint to acknowledge received DATA chunks and to inform the peer endpoint of gaps in the received subsequences of DATA chunks as represented by their TSNs.
///
///         The SACK chunk MUST contain the Cumulative TSN Ack, Advertised Receiver Window Credit (a_rwnd), Number of Gap Ack Blocks, and Number of Duplicate TSNs fields.
///     </code>
/// </remarks>
internal readonly partial struct SctpSelectiveAcknowledgement : ISctpChunk<SctpSelectiveAcknowledgement>
{
    private const SctpChunkType ChunkTypeImplicit = SctpChunkType.SelectiveAcknowledgement;
    private const ushort ChunkLengthMinimum = 4 * sizeof(uint);

    /// <summary>
    /// The Chunk Length.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         This value represents the size of the chunk in bytes, including the Chunk Type, Chunk Flags, Chunk Length, and Chunk Value fields. Therefore, if the Chunk Value field is zero-length, the Length field will be set to 4. The Chunk Length field does not count any chunk padding. However, it does include any padding of variable-length parameters other than the last parameter in the chunk.
    /// 
    ///             Note: A robust implementation is expected to accept the chunk whether or not the final padding has been included in the Chunk Length.
    ///     </code>
    /// </remarks>
    internal readonly ushort chunkLength;

    /// <summary>
    /// The Cumulative Transmission Sequence Number Acknowledgement (Cumulative TSN Ack).
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         The largest TSN, such that all TSNs smaller than or equal to it have been received and the next one has not been received. In the case where no DATA chunk has been received, this value is set to the peer's Initial TSN minus one.
    ///     </code>
    /// </remarks>
    internal readonly SctpTransmissionSequenceNumber cumulativeTransmissionSequenceNumberAcknowledgement;

    /// <summary>
    /// The Advertised Receiver Window Credit (a_rwnd).
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         This field indicates the updated receive buffer space in bytes of the sender of this SACK chunk; see <a href="https://datatracker.ietf.org/doc/html/rfc9260#sec_processing_of_received_sack">Section 6.2.1</a> for details.
    ///     </code>
    /// </remarks>
    internal readonly SctpAdvertisedReceiverWindowCredit advertisedReceiverWindowCredit;

    /// <summary>
    /// The Selective Acknowledgement blocks.
    /// </summary>
    internal readonly SctpSelectiveAcknowledgementBlocks blocks;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpSelectiveAcknowledgement" />.
    /// </summary>
    /// <param name="cumulativeTransmissionSequenceNumberAcknowledgement">The Cumulative TSN Ack.</param>
    /// <param name="advertisedReceiverWindowCredit">The Advertised Receiver Window Credit (a_rwnd).</param>
    /// <param name="blocks">The Selective Acknowledgement blocks.</param>
    internal SctpSelectiveAcknowledgement(
        SctpTransmissionSequenceNumber cumulativeTransmissionSequenceNumberAcknowledgement,
        SctpAdvertisedReceiverWindowCredit advertisedReceiverWindowCredit,
        SctpSelectiveAcknowledgementBlocks blocks)
    {
        this.chunkLength =
            (ushort)(
                4 * sizeof(uint)
                + blocks.gapAcknowledgementBlocks.Length * sizeof(uint)
                + blocks.duplicateTransmissionSequenceNumbers.Length * sizeof(uint));

        this.cumulativeTransmissionSequenceNumberAcknowledgement = cumulativeTransmissionSequenceNumberAcknowledgement;
        this.advertisedReceiverWindowCredit = advertisedReceiverWindowCredit;
        this.blocks = blocks;
    }

    /// <inheritdoc />
    SctpChunkType ISctpChunk.ChunkType => ChunkTypeImplicit;

    /// <inheritdoc />
    /// <remarks>
    /// A Selective Acknowledgement (SACK) chunk does not have flags.
    /// </remarks>
    byte ISctpChunk.ChunkFlags => 0;

    /// <inheritdoc />
    ushort ISctpChunk.ChunkLength => this.chunkLength;

    /// <inheritdoc />
    ReadOnlyMemory<ISctpChunkParameter> ISctpChunk.Parameters =>
        new(new ISctpChunkParameter[]
        {
            this.cumulativeTransmissionSequenceNumberAcknowledgement,
            this.advertisedReceiverWindowCredit
        });
}
