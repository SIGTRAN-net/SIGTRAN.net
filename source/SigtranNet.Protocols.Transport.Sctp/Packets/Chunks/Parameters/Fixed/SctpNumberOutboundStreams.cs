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

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Fixed;

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
