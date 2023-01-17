/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Sctp.Chunks.Parameters;

namespace SigtranNet.Protocols.Sctp.Chunks.ShutdownComplete;

/// <summary>
/// A Shutdown Complete (SHUTDOWN COMPLETE) chunk in an SCTP Packet.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         This chunk MUST be used to acknowledge the receipt of the SHUTDOWN ACK chunk at the completion of the shutdown process; see <a href="https://datatracker.ietf.org/doc/html/rfc9260#sec_shutdown_of_an_association">Section 9.2</a> for details.
///
///         The SHUTDOWN COMPLETE chunk has no parameters.
///     </code>
/// </remarks>
internal readonly partial struct SctpShutdownComplete : ISctpChunk<SctpShutdownComplete>
{
    private const SctpChunkType ChunkTypeImplicit = SctpChunkType.ShutdownComplete;
    private const ushort ChunkLengthImplicit = sizeof(uint);

    /// <summary>
    /// The Chunk Flags.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         T bit: 1 bit
    ///         The T bit is set to 0 if the sender filled in the Verification Tag expected by the peer. If the Verification Tag is reflected, the T bit MUST be set to 1. Reflecting means that the sent Verification Tag is the same as the received one.
    ///     </code>
    ///     <code>
    ///         Note: Special rules apply to this chunk for verification; please see <a href="https://datatracker.ietf.org/doc/html/rfc9260#sec_exceptions_in_verification_tag_rules">Section 8.5.1</a> for details. 
    ///     </code>
    /// </remarks>
    internal readonly SctpShutdownCompleteFlags chunkFlags;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpShutdownComplete" />.
    /// </summary>
    /// <param name="chunkFlags">The Chunk Flags.</param>
    internal SctpShutdownComplete(SctpShutdownCompleteFlags chunkFlags)
    {
        this.chunkFlags = chunkFlags;
    }

    /// <inheritdoc />
    SctpChunkType ISctpChunk.ChunkType => ChunkTypeImplicit;

    /// <inheritdoc />
    byte ISctpChunk.ChunkFlags => (byte)this.chunkFlags;

    /// <inheritdoc />
    ushort ISctpChunk.ChunkLength => ChunkLengthImplicit;

    /// <inheritdoc />
    ReadOnlyMemory<ISctpChunkParameter> ISctpChunk.Parameters => new();
}
