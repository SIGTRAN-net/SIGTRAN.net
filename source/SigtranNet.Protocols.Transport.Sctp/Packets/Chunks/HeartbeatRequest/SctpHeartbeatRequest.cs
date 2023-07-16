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

using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Variable;

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.HeartbeatRequest;

/// <summary>
/// A Heartbeat Request (HB) SCTP Packet Chunk.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         An endpoint <b>SHOULD</b> send a HEARTBEAT (HB) chunk to its peer endpoint to probe the reachability of a particular destination transport address defined in the present association.
///
///         The parameter field contains the Heartbeat Information, which is a variable-length opaque data structure understood only by the sender.
///     </code>
/// </remarks>
internal readonly partial struct SctpHeartbeatRequest : ISctpChunk<SctpHeartbeatRequest>
{
    private const SctpChunkType ChunkTypeImplicit = SctpChunkType.HeartbeatRequest;

    /// <summary>
    /// The Chunk Length.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         This value represents the size of the chunk in bytes, including the Chunk Type, Chunk Flags, Chunk Length, and Chunk Value fields. Therefore, if the Chunk Value field is zero-length, the Length field will be set to 4. The Chunk Length field does not count any chunk padding. However, it does include any padding of variable-length parameters other than the last parameter in the chunk.
    /// 
    ///             Note: A robust implementation is expected to accept the chunk whether or not the final padding has been included in the Chunk Length.
    ///     </code>
    /// </remarks>
    internal readonly ushort chunkLength;

    /// <summary>
    /// The Heartbeat Information parameter.
    /// </summary>
    /// <remarks>
    ///     <code>
    ///         The parameter field contains the Heartbeat Information, which is a variable-length opaque data structure understood only by the sender.
    ///     </code>
    /// </remarks>
    internal readonly SctpHeartbeatInfoParameter heartbeatInformation;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpHeartbeatRequest" />.
    /// </summary>
    /// <param name="heartbeatInformation">The heartbeat information.</param>
    internal SctpHeartbeatRequest(SctpHeartbeatInfoParameter heartbeatInformation)
    {
        this.chunkLength = (ushort)(sizeof(uint) + heartbeatInformation.parameterLength);
        this.heartbeatInformation = heartbeatInformation;
    }

    /// <inheritdoc />
    SctpChunkType ISctpChunk.ChunkType => ChunkTypeImplicit;

    /// <inheritdoc />
    /// <remarks>
    /// The HEARTBEAT chunk does not have flags.
    /// </remarks>
    byte ISctpChunk.ChunkFlags => 0;

    /// <inheritdoc />
    ushort ISctpChunk.ChunkLength => this.chunkLength;

    /// <inheritdoc />
    ReadOnlyMemory<ISctpChunkParameter> ISctpChunk.Parameters =>
        new(new ISctpChunkParameter[] { this.heartbeatInformation });
}
