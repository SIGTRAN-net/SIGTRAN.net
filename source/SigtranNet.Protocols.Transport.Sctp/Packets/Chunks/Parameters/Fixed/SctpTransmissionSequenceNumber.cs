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

using System.Security.Cryptography;

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Fixed;

/// <summary>
/// A Transmission Sequence Number (TSN).
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         A 32-bit sequence number used internally by SCTP. One TSN is attached to each chunk containing user data to permit the receiving SCTP endpoint to acknowledge its receipt and detect duplicate deliveries.
///     </code>
/// </remarks>
internal readonly partial struct SctpTransmissionSequenceNumber : ISctpChunkParameter
{
    private const ushort LengthFixed = sizeof(uint);

    /// <summary>
    /// The value of the Transmission Sequence Number (TSN).
    /// </summary>
    internal readonly uint value;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpTransmissionSequenceNumber" />.
    /// </summary>
    /// <param name="value">The value of the Transmission Sequence Number (TSN).</param>
    internal SctpTransmissionSequenceNumber(uint value)
    {
        this.value = value;
    }

    /// <summary>
    /// Generates a random Initial Transmission Sequence Number (I-TSN).
    /// </summary>
    /// <returns>
    /// The generated Initial Transmission Sequence Number (I-TSN).
    /// </returns>
    internal static SctpTransmissionSequenceNumber Generate()
    {
        var memory = new Memory<byte>(new byte[LengthFixed]);
        var generator = RandomNumberGenerator.Create();
        generator.GetBytes(memory.Span);
        return FromReadOnlyMemory(memory);
    }

    ushort ISctpChunkParameter.ParameterLength => LengthFixed;
}
