/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.PayloadData;

/// <summary>
/// Flags for an SCTP Payload Data chunk.
/// </summary>
[Flags]
internal enum SctpPayloadDataFlags : byte
{
    /// <summary>
    /// (I)mmediate
    /// </summary>
    /// <remarks>
    /// From RFC 9260:
    ///     <code>
    ///         The (I)mmediate bit MAY be set by the sender whenever the sender of a DATA chunk can benefit from the corresponding SACK chunk being sent back without delay.
    ///     </code>
    /// </remarks>
    Immediate = 0b1000,

    /// <summary>
    /// (U)nordered
    /// </summary>
    /// <remarks>
    /// From RFC 9260:
    ///     <code>
    ///         The (U)nordered bit, if set to 1, indicates that this is an unordered DATA chunk, and there is no Stream Sequence Number assigned to this DATA chunk. Therefore, the receiver MUST ignore the Stream Sequence Number field.
    ///     </code>
    /// </remarks>
    Unordered = 0b0100,

    /// <summary>
    /// (B)eginning
    /// </summary>
    /// <remarks>
    /// From RFC 9260:
    ///     <code>
    ///         The (B)eginning fragment bit, if set, indicates the first fragment of a user message.
    ///     </code>
    /// </remarks>
    Beginning = 0b0010,

    /// <summary>
    /// (E)nding
    /// </summary>
    /// <remarks>
    /// From RFC 9260:
    ///     <code>
    ///         The (E)nding fragment bit, if set, indicates the last fragment of a user message.
    ///     </code>
    /// </remarks>
    Ending = 0b0001
}
