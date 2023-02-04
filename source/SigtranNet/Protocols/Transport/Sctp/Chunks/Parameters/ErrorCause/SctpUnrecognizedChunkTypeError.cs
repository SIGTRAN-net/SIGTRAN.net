/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause;

/// <summary>
/// An Unrecognized Chunk Type error cause parameter in an SCTP Packet Chunk.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         This error cause is returned to the originator of the chunk if the receiver does not understand the chunk and the upper bits of the 'Chunk Type' are set to 01 or 11.
///     </code>
/// </remarks>
internal readonly partial struct SctpUnrecognizedChunkTypeError : ISctpErrorCauseParameter<SctpUnrecognizedChunkTypeError>
{
    private const SctpErrorCauseCode ErrorCauseCodeImplicit = SctpErrorCauseCode.UnrecognizedChunkType;

    /// <summary>
    /// The Cause Length.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         Set to the size of the parameter in bytes, including the Cause Code, Cause Length, and Cause-Specific Information fields.
    ///     </code>
    /// </remarks>
    internal readonly ushort errorCauseLength;

    /// <summary>
    /// The Unrecognized Chunk.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         The Unrecognized Chunk field contains the unrecognized chunk from the SCTP packet complete with Chunk Type, Chunk Flags, and Chunk Length.
    ///     </code>
    /// </remarks>
    internal readonly ISctpChunk unrecognizedChunk;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpUnrecognizedChunkTypeError" />.
    /// </summary>
    /// <param name="unrecognizedChunk">The Unrecognized Chunk.</param>
    internal SctpUnrecognizedChunkTypeError(ISctpChunk unrecognizedChunk)
    {
        this.errorCauseLength = (ushort)(sizeof(uint) + unrecognizedChunk.ChunkLength);
        this.unrecognizedChunk = unrecognizedChunk;
    }

    SctpErrorCauseCode ISctpErrorCause.ErrorCauseCode => ErrorCauseCodeImplicit;
    ushort ISctpErrorCause.ErrorCauseLength => this.errorCauseLength;
}
