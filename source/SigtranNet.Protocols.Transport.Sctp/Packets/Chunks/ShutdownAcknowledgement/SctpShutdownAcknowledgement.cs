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

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.ShutdownAcknowledgement;

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
