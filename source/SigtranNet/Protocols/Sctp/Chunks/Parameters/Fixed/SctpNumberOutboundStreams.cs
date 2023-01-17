/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Sctp.Chunks.Parameters.Fixed;

/// <summary>
/// The Number of Outbound Streams.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         Defines the number of outbound streams the sender of this INIT chunk wishes to create in this association. The value of 0 MUST NOT be used.
///
///             A receiver of an INIT chunk with the OS value set to 0 MUST discard the packet, SHOULD send a packet in response containing an ABORT chunk and using the Initiate Tag as the Verification Tag, and MUST NOT change the state of any existing association.
///     </code>
/// </remarks>
internal readonly partial struct SctpNumberOutboundStreams : ISctpChunkParameter
{
    private const ushort LengthFixed = sizeof(ushort);

    /// <summary>
    /// The value of the Number of Outbound Streams SCTP chunk parameter.
    /// </summary>
    internal readonly ushort value;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpNumberOutboundStreams" />.
    /// </summary>
    /// <param name="value">The value of the Number of Outbound Streams.</param>
    internal SctpNumberOutboundStreams(ushort value)
    {
        this.value = value;
    }

    ushort ISctpChunkParameter.ParameterLength => LengthFixed;
}
