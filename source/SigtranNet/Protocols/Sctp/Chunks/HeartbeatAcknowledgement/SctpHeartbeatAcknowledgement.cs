﻿/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Sctp.Chunks.Parameters;
using SigtranNet.Protocols.Sctp.Chunks.Parameters.Variable;

namespace SigtranNet.Protocols.Sctp.Chunks.HeartbeatAcknowledgement;

/// <summary>
/// The Heartbeat Acknowledgement (HEARTBEAT ACK) chunk.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         An endpoint MUST send this chunk to its peer endpoint as a response to a HEARTBEAT chunk (see <a href="https://datatracker.ietf.org/doc/html/rfc9260#sec_path_heartbeat">Section 8.3</a>). A packet containing the HEARTBEAT ACK chunk is always sent to the source IP address of the IP datagram containing the HEARTBEAT chunk to which this HEARTBEAT ACK chunk is responding.
///     </code>
/// </remarks>
internal readonly partial struct SctpHeartbeatAcknowledgement : ISctpChunk<SctpHeartbeatAcknowledgement>
{
    private const SctpChunkType ChunkTypeImplicit = SctpChunkType.HeartbeatAcknowledgement;

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
    /// The Heartbeat Information.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         This field MUST contain the Heartbeat Info parameter (as defined in <a href="https://datatracker.ietf.org/doc/html/rfc9260#sec_heartbeat_chunk">Section 3.3.5</a>) of the Heartbeat Request to which this Heartbeat Acknowledgement is responding.
    ///     </code>
    ///     <code>
    ///         The parameter field contains a variable-length opaque data structure.
    ///     </code>
    /// </remarks>
    internal readonly SctpHeartbeatInfoParameter heartbeatInformation;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpHeartbeatAcknowledgement" />.
    /// </summary>
    /// <param name="heartbeatInformation">The Heartbeat Information.</param>
    internal SctpHeartbeatAcknowledgement(SctpHeartbeatInfoParameter heartbeatInformation)
    {
        this.chunkLength = (ushort)(sizeof(uint) + heartbeatInformation.parameterLength);
        this.heartbeatInformation = heartbeatInformation;
    }

    /// <inheritdoc />
    SctpChunkType ISctpChunk.ChunkType => ChunkTypeImplicit;

    /// <inheritdoc />
    /// <remarks>
    ///     The HEARTBEAT ACK chunk does not have Chunk Flags.
    /// </remarks>
    byte ISctpChunk.ChunkFlags => 0;

    /// <inheritdoc />
    ushort ISctpChunk.ChunkLength => this.chunkLength;

    /// <inheritdoc />
    ReadOnlyMemory<ISctpChunkParameter> ISctpChunk.Parameters =>
        new(new ISctpChunkParameter[] { this.heartbeatInformation });
}
