/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using System.Net;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Variable.Address;

/// <summary>
/// An IPv4 Address Parameter in an SCTP Packet Chunk.
/// </summary>
internal readonly partial struct SctpIPv4AddressParameter : ISctpAddressParameter, ISctpChunkParameterVariableLength<SctpIPv4AddressParameter>
{
    private const SctpChunkParameterType ParameterTypeImplicit = SctpChunkParameterType.IPv4Address;
    private const ushort ParameterLengthImplicit = 2 * sizeof(uint);

    /// <summary>
    /// The value of the IPv4 Address Chunk Parameter.
    /// </summary>
    internal readonly IPAddress ipAddress;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpIPv4AddressParameter" />.
    /// </summary>
    /// <param name="ipAddress">The IP Address.</param>
    internal SctpIPv4AddressParameter(IPAddress ipAddress)
    {
        this.ipAddress = ipAddress;
    }

    SctpChunkParameterType ISctpChunkParameterVariableLength.ParameterType => ParameterTypeImplicit;

    ushort ISctpChunkParameter.ParameterLength => ParameterLengthImplicit;
}
