/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using System.Net;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Variable.Address;

/// <summary>
/// An IPv6 Address Parameter in an SCTP Packet Chunk.
/// </summary>
internal readonly partial struct SctpIPv6AddressParameter : ISctpAddressParameter, ISctpChunkParameterVariableLength<SctpIPv6AddressParameter>
{
    private const SctpChunkParameterType ParameterTypeImplicit = SctpChunkParameterType.IPv6Address;
    private const ushort ParameterLengthImplicit = 5 * sizeof(uint);

    /// <summary>
    /// The value of the IPv6 Address Chunk Parameter.
    /// </summary>
    internal readonly IPAddress ipAddress;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpIPv6AddressParameter" />.
    /// </summary>
    /// <param name="ipAddress">The IP Address.</param>
    internal SctpIPv6AddressParameter(IPAddress ipAddress)
    {
        this.ipAddress = ipAddress;
    }

    SctpChunkParameterType ISctpChunkParameterVariableLength.ParameterType => ParameterTypeImplicit;
    ushort ISctpChunkParameter.ParameterLength => ParameterLengthImplicit;
}
