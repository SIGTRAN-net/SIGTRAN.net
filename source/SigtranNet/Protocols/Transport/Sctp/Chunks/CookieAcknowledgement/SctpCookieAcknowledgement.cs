/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.CookieAcknowledgement;

/// <summary>
/// A Cookie Acknowledgement (COOKIE ACK) chunk in an SCTP Packet.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         This chunk is used only during the initialization of an association. It is used to acknowledge the receipt of a COOKIE ECHO chunk. This chunk MUST precede any DATA or SACK chunk sent within the association but MAY be bundled with one or more DATA chunks or SACK chunk's in the same SCTP packet.
///     </code>
/// </remarks>
internal readonly partial struct SctpCookieAcknowledgement : ISctpChunk<SctpCookieAcknowledgement>
{
    private const SctpChunkType ChunkTypeImplicit = SctpChunkType.CookieAcknowledgement;
    private const ushort ChunkLengthImplicit = sizeof(uint);

    /// <summary>
    /// Initializes a new instance of <see cref="SctpCookieAcknowledgement" />.
    /// </summary>
    public SctpCookieAcknowledgement()
    {
    }

    /// <inheritdoc />
    SctpChunkType ISctpChunk.ChunkType => ChunkTypeImplicit;

    /// <inheritdoc />
    /// <remarks>
    ///     COOKIE ACK does not have Chunk Flags.
    /// </remarks>
    byte ISctpChunk.ChunkFlags => 0;

    /// <inheritdoc />
    ushort ISctpChunk.ChunkLength => ChunkLengthImplicit;

    /// <inheritdoc />
    ReadOnlyMemory<ISctpChunkParameter> ISctpChunk.Parameters => new();
}
