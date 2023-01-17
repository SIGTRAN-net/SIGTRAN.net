/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Sctp.Chunks.Parameters.Variable.Address;

/// <summary>
/// A Host Name Address Parameter in an SCTP Packet Chunk.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         The sender of an INIT chunk or INIT ACK chunk MUST NOT include this parameter. The usage of the Host Name Address parameter is deprecated. The receiver of an INIT chunk or an INIT ACK containing a Host Name Address parameter MUST send an ABORT chunk and MAY include an "Unresolvable Address" error cause.
///     </code>
/// </remarks>
internal readonly partial struct SctpHostNameAddressParameter : ISctpAddressParameter, ISctpChunkParameterVariableLength<SctpHostNameAddressParameter>
{
    private const SctpChunkParameterType ParameterTypeImplicit = SctpChunkParameterType.HostNameAddress;

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
    /// The host name.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         This field contains a host name in "host name syntax" per <a href="https://www.rfc-editor.org/rfc/rfc1123#section-2.1">Section 2.1</a> of [<a href="https://datatracker.ietf.org/doc/html/rfc1123">RFC1123</a>]. The method for resolving the host name is out of scope of SCTP.
    ///
    ///             At least one null terminator is included in the Host Name string and MUST be included in the length.
    ///     </code>
    /// </remarks>
    internal readonly string hostName;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpHostNameAddressParameter" />.
    /// </summary>
    /// <param name="hostName">The host name.</param>
    internal SctpHostNameAddressParameter(string hostName)
    {
        this.parameterLength = (ushort)(sizeof(uint) + hostName.Length + 1);
        this.hostName = hostName;
    }

    SctpChunkParameterType ISctpChunkParameterVariableLength.ParameterType => ParameterTypeImplicit;
    ushort ISctpChunkParameter.ParameterLength => this.parameterLength;
}
