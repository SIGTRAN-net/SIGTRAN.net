/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Sctp.Chunks.Parameters.ErrorCause;

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
