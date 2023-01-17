/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Sctp.Chunks.Parameters;

namespace SigtranNet.Protocols.Sctp.Chunks.ShutdownAcknowledgement;

/// <summary>
/// A Shutdown Acknowledgement (SHUTDOWN ACK) chunk in an SCTP Packet.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         This chunk MUST be used to acknowledge the receipt of the SHUTDOWN chunk at the completion of the shutdown process; see <a href="https://datatracker.ietf.org/doc/html/rfc9260#sec_shutdown_of_an_association">Section 9.2</a> for details.
///     </code>
///     <code>
///         The SHUTDOWN ACK chunk has no parameters.
///     </code>
/// </remarks>
internal readonly partial struct SctpShutdownAcknowledgement : ISctpChunk<SctpShutdownAcknowledgement>
{
    private const SctpChunkType ChunkTypeImplicit = SctpChunkType.ShutdownAcknowledgement;
    private const ushort ChunkLengthImplicit = sizeof(uint);

    /// <summary>
    /// Initializes a new instance of <see cref="SctpShutdownAcknowledgement" />.
    /// </summary>
    public SctpShutdownAcknowledgement()
    {
    }

    /// <inheritdoc />
    SctpChunkType ISctpChunk.ChunkType => ChunkTypeImplicit;

    /// <inheritdoc />
    /// <remarks>
    ///     SHUTDOWN ACK does not have Chunk Flags.
    /// </remarks>
    byte ISctpChunk.ChunkFlags => 0;

    /// <inheritdoc />
    ushort ISctpChunk.ChunkLength => ChunkLengthImplicit;

    /// <inheritdoc />
    ReadOnlyMemory<ISctpChunkParameter> ISctpChunk.Parameters => new();
}
