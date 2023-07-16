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

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.PayloadData;

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
