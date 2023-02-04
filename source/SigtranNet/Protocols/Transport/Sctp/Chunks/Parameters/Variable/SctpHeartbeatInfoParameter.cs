/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Variable;

/// <summary>
/// A Heartbeat Info Parameter for a 'HEARTBEAT' SCTP Packet Chunk.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         The parameter field contains the Heartbeat Information, which is a variable-length opaque data structure understood only by the sender.
///     </code>
/// </remarks>
internal readonly partial struct SctpHeartbeatInfoParameter : ISctpChunkParameterVariableLength<SctpHeartbeatInfoParameter>
{
    private const SctpChunkParameterType ParameterTypeImplicit = SctpChunkParameterType.HeartbeatInfo;

    /// <summary>
    /// The Parameter Length.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         The Parameter Length field contains the size of the parameter in bytes, including the Parameter Type, Parameter Length, and Parameter Value fields. Thus, a parameter with a zero-length Parameter Value field would have a Parameter Length field of 4. The Parameter Length does not include any padding bytes.
    ///     </code>
    /// </remarks>
    internal readonly ushort parameterLength;

    /// <summary>
    /// The heartbeat information.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         The Sender-Specific Heartbeat Info field <b>SHOULD</b> include information about the sender's current time when this HEARTBEAT chunk is sent and the destination transport address to which this HEARTBEAT chunk is sent (see <a href="https://datatracker.ietf.org/doc/html/rfc9260#sec_path_heartbeat">Section 8.3</a>). This information is simply reflected back by the receiver in the HEARTBEAT ACK chunk (see <a href="https://datatracker.ietf.org/doc/html/rfc9260#sec_heartbeat_ack_chunk">Section 3.3.6</a>). Note also that the HEARTBEAT chunk is both for reachability checking and for path verification (see <a href="https://datatracker.ietf.org/doc/html/rfc9260#sec_path_verifiation">Section 5.4</a>). When a HEARTBEAT chunk is being used for path verification purposes, it <b>MUST</b> include a random nonce of length 64 bits or longer ([<a href="https://datatracker.ietf.org/doc/html/rfc4086">RFC4086</a>] provides some information on randomness guidelines).
    ///     </code>
    /// </remarks>
    internal readonly ReadOnlyMemory<byte> heartbeatInformation;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpHeartbeatInfoParameter" />.
    /// </summary>
    /// <param name="heartbeatInformation">The heartbeat information.</param>
    internal SctpHeartbeatInfoParameter(ReadOnlyMemory<byte> heartbeatInformation)
    {
        this.parameterLength = (ushort)(sizeof(uint) + heartbeatInformation.Length);
        this.heartbeatInformation = heartbeatInformation;
    }

    SctpChunkParameterType ISctpChunkParameterVariableLength.ParameterType => ParameterTypeImplicit;

    ushort ISctpChunkParameter.ParameterLength => this.parameterLength;
}
