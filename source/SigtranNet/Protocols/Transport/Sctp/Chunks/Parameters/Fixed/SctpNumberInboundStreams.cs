/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Fixed;

internal readonly partial struct SctpNumberInboundStreams : ISctpChunkParameter
{
    private const ushort LengthFixed = sizeof(ushort);
    internal readonly ushort value;

    internal SctpNumberInboundStreams(ushort value)
    {
        this.value = value;
    }

    ushort ISctpChunkParameter.ParameterLength => LengthFixed;
}
