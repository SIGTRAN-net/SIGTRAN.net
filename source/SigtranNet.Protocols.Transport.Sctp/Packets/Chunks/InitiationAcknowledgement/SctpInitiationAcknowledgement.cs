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

using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Exceptions;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Fixed;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Variable;

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.InitiationAcknowledgement;

/// <summary>
/// An Initiation Acknowledgement (INIT ACK) chunk in an SCTP Packet.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         The INIT ACK chunk is used to acknowledge the initiation of an SCTP association.
///     </code>
/// </remarks>
internal readonly partial struct SctpInitiationAcknowledgement : ISctpChunk<SctpInitiationAcknowledgement>
{
    private const SctpChunkType ChunkTypeImplicit = SctpChunkType.InitiationAcknowledgement;
    internal const ushort ChunkLengthMinimum = 5 * sizeof(uint);

    private static readonly IReadOnlySet<SctpChunkParameterType> SupportedParameterTypes =
        new HashSet<SctpChunkParameterType>
        {
                SctpChunkParameterType.StateCookie,
                SctpChunkParameterType.IPv4Address,
                SctpChunkParameterType.IPv6Address,
                SctpChunkParameterType.UnrecognizedParameter,
                SctpChunkParameterType.HostNameAddress
        };

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
    /// The Initiate Tag.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         The receiver of the INIT ACK chunk records the value of the Initiate Tag parameter. This value MUST be placed into the Verification Tag field of every SCTP packet that the receiver of the INIT ACK chunk transmits within this association.
    ///
    ///             The Initiate Tag MUST NOT take the value 0. See <a href="https://datatracker.ietf.org/doc/html/rfc9260#sec_selection_of_tag_value">Section 5.3.1</a> for more on the selection of the Initiate Tag value.
    /// 
    ///             If an endpoint in the COOKIE-WAIT state receives an INIT ACK chunk with the Initiate Tag set to 0, it MUST destroy the TCB and SHOULD send an ABORT chunk with the T bit set.If such an INIT ACK chunk is received in any state other than CLOSED or COOKIE-WAIT, it SHOULD be discarded silently (see <a href="https://datatracker.ietf.org/doc/html/rfc9260?fbclid=IwAR3rRB_0bpnZF9SvbUwPY4DKe3iZ0q9FIWQ-uwqokBd8Tx01PgEu6cIO_s0#sec_unexpected_init_ack">Section 5.2.3</a>).
    ///     </code>
    /// </remarks>
    internal readonly SctpInitiateTag initiateTag;

    /// <summary>
    /// The Advertised Reciever Window Credit (a_rwnd).
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         This value represents the dedicated buffer space, in number of bytes, the sender of the INIT ACK chunk has reserved in association with this window.
    /// 
    ///             The Advertised Receiver Window Credit MUST NOT be smaller than 1500.
    /// 
    ///             A receiver of an INIT ACK chunk with the a_rwnd value set to a value smaller than 1500 MUST discard the packet, SHOULD send a packet in response containing an ABORT chunk and using the Initiate Tag as the Verification Tag, and MUST NOT change the state of any existing association.
    /// 
    ///             During the life of the association, this buffer space SHOULD NOT be reduced (i.e., dedicated buffers ought not to be taken away from this association); however, an endpoint MAY change the value of a_rwnd it sends in SACK chunks.
    ///     </code>
    /// </remarks>
    internal readonly SctpAdvertisedReceiverWindowCredit advertisedReceiverWindowCredit;

    /// <summary>
    /// The Number of Outbound Streams (OS).
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         Defines the number of outbound streams the sender of this INIT ACK chunk wishes to create in this association. The value of 0 MUST NOT be used, and the value MUST NOT be greater than the MIS value sent in the INIT chunk.
    /// 
    ///             If an endpoint in the COOKIE-WAIT state receives an INIT ACK chunk with the OS value set to 0, it MUST destroy the TCB and SHOULD send an ABORT chunk.If such an INIT ACK chunk is received in any state other than CLOSED or COOKIE-WAIT, it SHOULD be discarded silently (see <a href="https://datatracker.ietf.org/doc/html/rfc9260#sec_unexpected_init_ack">Section 5.2.3</a>).
    ///     </code>
    /// </remarks>
    internal readonly SctpNumberOutboundStreams numberOutboundStreams;

    /// <summary>
    /// The Number of Inbound Streams (MIS).
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         Defines the maximum number of streams the sender of this INIT ACK chunk allows the peer end to create in this association. The value 0 MUST NOT be used.
    /// 
    ///             Note: There is no negotiation of the actual number of streams, but instead the two endpoints will use the min(requested, offered). See <a href="https://datatracker.ietf.org/doc/html/rfc9260#sec_handle_stream_parameters">Section 5.1.1</a> for details.
    /// 
    ///             If an endpoint in the COOKIE-WAIT state receives an INIT ACK chunk with the MIS value set to 0, it MUST destroy the TCB and SHOULD send an ABORT chunk.If such an INIT ACK chunk is received in any state other than CLOSED or COOKIE-WAIT, it SHOULD be discarded silently (see <a href="https://datatracker.ietf.org/doc/html/rfc9260#sec_unexpected_init_ack">Section 5.2.3</a>).
    ///     </code>
    /// </remarks>
    internal readonly SctpNumberInboundStreams numberInboundStreams;

    /// <summary>
    /// The Initial Transmission Sequence Number (I-TSN).
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         Defines the TSN that the sender of the INIT ACK chunk will use initially. The valid range is from 0 to 4294967295 and the Initial TSN SHOULD be set to a random value in that range. The methods described in [<a href="https://datatracker.ietf.org/doc/html/rfc4086">RFC4086</a>] can be used for the Initial TSN randomization.
    ///     </code>
    /// </remarks>
    internal readonly SctpTransmissionSequenceNumber initialTransmissionSequenceNumber;
    
    /// <summary>
    /// Any number of optional variable-length parameters that are valid for an INIT ACK chunk.
    /// </summary>
    internal readonly ReadOnlyMemory<ISctpChunkParameterVariableLength> variableLengthParameters;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpInitiationAcknowledgement" />.
    /// </summary>
    /// <param name="initiateTag">The Initiate Tag parameter.</param>
    /// <param name="advertisedReceiverWindowCredit">The Advertised Receiver Window Credit parameter.</param>
    /// <param name="numberOutboundStreams">The Number of Outbound Streams parameter.</param>
    /// <param name="numberInboundStreams">The Number of Inbound Streams parameter.</param>
    /// <param name="initialTransmissionSequenceNumber">The Initial Transmission Sequence Number (I-TSN) parameter.</param>
    /// <param name="variableLengthParameters">Any optional variable-length parameters.</param>
    /// <exception cref="SctpStateCookieMissingException">
    /// An <see cref="SctpStateCookieMissingException" /> is thrown if a State Cookie parameter is not provided.
    /// </exception>
    internal SctpInitiationAcknowledgement(
        SctpInitiateTag initiateTag,
        SctpAdvertisedReceiverWindowCredit advertisedReceiverWindowCredit,
        SctpNumberOutboundStreams numberOutboundStreams,
        SctpNumberInboundStreams numberInboundStreams,
        SctpTransmissionSequenceNumber initialTransmissionSequenceNumber,
        ReadOnlyMemory<ISctpChunkParameterVariableLength> variableLengthParameters)
    {
        // Guards
        this.chunkLength = 5 * sizeof(uint);
        var hasStateCookie = false;
        var variableLengthParametersSpan = variableLengthParameters.Span;
        for (var i = 0; i < variableLengthParameters.Length; i++)
        {
            if (variableLengthParametersSpan[i].ParameterType == SctpChunkParameterType.StateCookie)
                hasStateCookie = true;
            chunkLength += variableLengthParametersSpan[i].ParameterLength;
        }
        if (!hasStateCookie)
            throw new SctpStateCookieMissingException();

        // Fields
        this.initiateTag = initiateTag;
        this.advertisedReceiverWindowCredit = advertisedReceiverWindowCredit;
        this.numberOutboundStreams = numberOutboundStreams;
        this.numberInboundStreams = numberInboundStreams;
        this.initialTransmissionSequenceNumber = initialTransmissionSequenceNumber;
        this.variableLengthParameters = variableLengthParameters;
    }

    SctpChunkType ISctpChunk.ChunkType => ChunkTypeImplicit;

    /// <inheritdoc />
    /// <remarks>
    /// The Initiation Acknowledgement (INIT ACK) chunk does not have flags.
    /// </remarks>
    byte ISctpChunk.ChunkFlags => 0;

    ushort ISctpChunk.ChunkLength => this.chunkLength;

    ReadOnlyMemory<ISctpChunkParameter> ISctpChunk.Parameters
    {
        get
        {
            var memory = new Memory<ISctpChunkParameter>(new ISctpChunkParameter[5 + this.variableLengthParameters.Length]);
            var memorySpan = memory.Span;
            memorySpan[0] = this.initiateTag;
            memorySpan[1] = this.advertisedReceiverWindowCredit;
            memorySpan[2] = this.numberOutboundStreams;
            memorySpan[3] = this.numberInboundStreams;
            memorySpan[4] = this.initialTransmissionSequenceNumber;

            var variableLengthParameterSpan = this.variableLengthParameters.Span;
            for (var i = 0; i < this.variableLengthParameters.Length; i++)
            {
                memorySpan[i] = variableLengthParameterSpan[i];
            }

            return memory;
        }
    }
}
