/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Sctp.Chunks.SelectiveAcknowledgement;

/// <summary>
/// A range of a Gap Acknowledgement Block (Gap Ack Block).
/// </summary>
internal readonly partial struct SctpGapAcknowledgementBlockRange
{
    /// <summary>
    /// The start of the Gap Acknowledgement Block (Gap Ack Block) range.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         Indicates the Start offset TSN for this Gap Ack Block. To calculate the actual TSN number, the Cumulative TSN Ack is added to this offset number. This calculated TSN identifies the lowest TSN in this Gap Ack Block that has been received.
    ///     </code>
    /// </remarks>
    internal readonly ushort start;

    /// <summary>
    /// The end of the Gap Acknowledgement Block (Gap Ack Block) range.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         Indicates the End offset TSN for this Gap Ack Block. To calculate the actual TSN number, the Cumulative TSN Ack is added to this offset number. This calculated TSN identifies the highest TSN in this Gap Ack Block that has been received.
    ///     </code>
    /// </remarks>
    internal readonly ushort end;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpGapAcknowledgementBlockRange" />.
    /// </summary>
    /// <param name="start">The start of the Gap Acknowledgement Block (Gap Ack Block) range.</param>
    /// <param name="end">The end of the Gap Acknowledgement Block (Gap Ack Block) range.</param>
    internal SctpGapAcknowledgementBlockRange(ushort start, ushort end)
    {
        this.start = start;
        this.end = end;
    }
}
