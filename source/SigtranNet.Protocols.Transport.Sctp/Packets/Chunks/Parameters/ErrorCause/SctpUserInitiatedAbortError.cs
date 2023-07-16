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

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.ErrorCause;

/// <summary>
/// A User-Initiated Abort error cause in an SCTP Packet Chunk.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         This error cause MAY be included in ABORT chunks that are sent because of an upper-layer request. The upper layer can specify an Upper Layer Abort Reason that is transported by SCTP transparently and MAY be delivered to the upper-layer protocol at the peer.
///     </code>
/// </remarks>
internal readonly partial struct SctpUserInitiatedAbortError : ISctpErrorCauseParameter<SctpUserInitiatedAbortError>
{
    private const SctpErrorCauseCode ErrorCauseCodeImplicit = SctpErrorCauseCode.UserInitiatedAbort;

    /// <summary>
    /// The Error Cause Length.
    /// </summary>
    internal readonly ushort errorCauseLength;

    /// <summary>
    /// The Upper Layer Abort Reason.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         The upper layer can specify an Upper Layer Abort Reason that is transported by SCTP transparently and MAY be delivered to the upper-layer protocol at the peer.
    ///     </code>
    /// </remarks>
    internal readonly ReadOnlyMemory<byte> upperLayerAbortReason;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpUserInitiatedAbortError" />.
    /// </summary>
    /// <param name="upperLayerAbortReason">
    /// The upper layer abort reason.
    /// </param>
    internal SctpUserInitiatedAbortError(ReadOnlyMemory<byte> upperLayerAbortReason)
    {
        this.errorCauseLength = (ushort)(sizeof(uint) + upperLayerAbortReason.Length);
        this.upperLayerAbortReason = upperLayerAbortReason;
    }

    SctpErrorCauseCode ISctpErrorCause.ErrorCauseCode => ErrorCauseCodeImplicit;
    ushort ISctpErrorCause.ErrorCauseLength => this.errorCauseLength;
}
