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

using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters;

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.CookieAcknowledgement;

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
