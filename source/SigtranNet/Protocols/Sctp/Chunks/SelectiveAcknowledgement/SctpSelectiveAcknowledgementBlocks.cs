/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Sctp.Chunks.Parameters.Fixed;

namespace SigtranNet.Protocols.Sctp.Chunks.SelectiveAcknowledgement;

/// <summary>
/// The Selective Acknowledgement blocks and duplicate Transmission Sequence Numbers.
/// </summary>
internal readonly partial struct SctpSelectiveAcknowledgementBlocks
{
    /// <summary>
    /// The Gap Acknowledgement Blocks (Gap Ack Blocks).
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         These fields contain the Gap Ack Blocks. They are repeated for each Gap Ack Block up to the number of Gap Ack Blocks defined in the Number of Gap Ack Blocks field. All DATA chunks with TSNs greater than or equal to (Cumulative TSN Ack + Gap Ack Block Start) and less than or equal to (Cumulative TSN Ack + Gap Ack Block End) of each Gap Ack Block are assumed to have been received correctly.
    ///     </code>
    /// </remarks>
    internal readonly ReadOnlyMemory<SctpGapAcknowledgementBlockRange> gapAcknowledgementBlocks;

    /// <summary>
    /// The duplicate TSNs.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         Indicates the number of times a TSN was received in duplicate since the last SACK chunk was sent. Every time a receiver gets a duplicate TSN (before sending the SACK chunk), it adds it to the list of duplicates. The duplicate count is reinitialized to zero after sending each SACK chunk.
    /// 
    ///             For example, if a receiver were to get the TSN 19 three times, it would list 19 twice in the outbound SACK chunk.After sending the SACK chunk, if it received yet one more TSN 19, it would list 19 as a duplicate once in the next outgoing SACK chunk.
    ///     </code>
    /// </remarks>
    internal readonly ReadOnlyMemory<SctpTransmissionSequenceNumber> duplicateTransmissionSequenceNumbers;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpSelectiveAcknowledgementBlocks" />.
    /// </summary>
    /// <param name="gapAcknowledgementBlocks">The Gap Acknowledgement Blocks (Gap Ack Blocks).</param>
    /// <param name="duplicateTransmissionSequenceNumbers">The duplicate Transmission Sequence Numbers (Duplicate TSNs).</param>
    internal SctpSelectiveAcknowledgementBlocks(
        ReadOnlyMemory<SctpGapAcknowledgementBlockRange> gapAcknowledgementBlocks,
        ReadOnlyMemory<SctpTransmissionSequenceNumber> duplicateTransmissionSequenceNumbers)
    {
        this.gapAcknowledgementBlocks = gapAcknowledgementBlocks;
        this.duplicateTransmissionSequenceNumbers = duplicateTransmissionSequenceNumbers;
    }
}
