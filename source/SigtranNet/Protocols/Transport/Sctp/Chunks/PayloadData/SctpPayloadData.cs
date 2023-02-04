/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks.Exceptions;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.PayloadData;

/// <summary>
/// A Payload Data chunk in an SCTP packet.
/// </summary>
internal readonly partial struct SctpPayloadData : ISctpChunk<SctpPayloadData>
{
    /// <summary>
    /// The SCTP Payload Data chunk type.
    /// </summary>
    internal const SctpChunkType ChunkTypeImplicit = SctpChunkType.PayloadData;

    /// <summary>
    /// The SCTP Payload Data flags.
    /// </summary>
    internal readonly SctpPayloadDataFlags chunkFlags;

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
    /// The Transmission Sequence Number (TSN).
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         A 32-bit sequence number used internally by SCTP. One TSN is attached to each chunk containing user data to permit the receiving SCTP endpoint to acknowledge its receipt and detect duplicate deliveries.
    ///     </code>
    /// </remarks>
    internal readonly uint transmissionSequenceNumber;

    /// <summary>
    /// The Stream Identifier (S).
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         Identifies the stream to which the following user data belongs.
    ///     </code>
    /// </remarks>
    internal readonly ushort streamIdentifier;

    /// <summary>
    /// The Stream Sequence Number (n).
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         This value represents the Stream Sequence Number of the following user data within the stream S. Valid range is 0 to 65535.
    ///
    ///             When a user message is fragmented by SCTP for transport, the same Stream Sequence Number MUST be carried in each of the fragments of the message.
    ///     </code>
    /// </remarks>
    internal readonly ushort streamSequenceNumber;

    /// <summary>
    /// The Payload Protocol Identifier.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         This value represents an application (or upper layer) specified protocol identifier. This value is passed to SCTP by its upper layer and sent to its peer. This identifier is not used by SCTP but can be used by certain network entities, as well as by the peer application, to identify the type of information being carried in this DATA chunk. This field MUST be sent even in fragmented DATA chunks (to make sure it is available for agents in the middle of the network). Note that this field is not touched by an SCTP implementation; the upper layer is responsible for the host to network byte order conversion of this field.
    ///
    ///             The value 0 indicates that no application identifier is specified by the upper layer for this payload data.
    ///     </code>
    /// </remarks>
    internal readonly SctpPayloadProtocolIdentifier payloadProtocolIdentifier;

    /// <summary>
    /// The User Data.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         This is the payload user data. The implementation MUST pad the end of the data to a 4-byte boundary with all zero bytes. Any padding MUST NOT be included in the Length field. A sender MUST never add more than 3 bytes of padding.
    ///     </code>
    /// </remarks>
    internal readonly ReadOnlyMemory<byte> userData;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpPayloadData" />.
    /// </summary>
    /// <param name="flags">The payload data flags.</param>
    /// <param name="transmissionSequenceNumber">The transmission sequence number (TSN).</param>
    /// <param name="streamIdentifier">The stream identifier.</param>
    /// <param name="streamSequenceNumber">The stream sequence number.</param>
    /// <param name="payloadProtocolIdentifier">The payload protocol identifier.</param>
    /// <param name="userData">The user data.</param>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if the user data is empty.
    /// </exception>
    internal SctpPayloadData(
        SctpPayloadDataFlags flags,
        uint transmissionSequenceNumber,
        ushort streamIdentifier,
        ushort streamSequenceNumber,
        SctpPayloadProtocolIdentifier payloadProtocolIdentifier,
        ReadOnlyMemory<byte> userData)
    {
        // Guards
        if (userData.Length == 0)
            throw new SctpChunkLengthInvalidException(16);

        // Fields
        this.chunkFlags = flags;
        this.chunkLength = (ushort)(16u + userData.Length);
        this.transmissionSequenceNumber = transmissionSequenceNumber;
        this.streamIdentifier = streamIdentifier;
        this.streamSequenceNumber = streamSequenceNumber;
        this.payloadProtocolIdentifier = payloadProtocolIdentifier;
        this.userData = userData;
    }

    SctpChunkType ISctpChunk.ChunkType => ChunkTypeImplicit;

    byte ISctpChunk.ChunkFlags => (byte)this.chunkFlags;

    ushort ISctpChunk.ChunkLength => this.chunkLength;

    /// <inheritdoc />
    /// <remarks>
    /// DATA chunks do not have chunk parameters.
    /// </remarks>
    ReadOnlyMemory<ISctpChunkParameter> ISctpChunk.Parameters =>
        ReadOnlyMemory<ISctpChunkParameter>.Empty;
}
