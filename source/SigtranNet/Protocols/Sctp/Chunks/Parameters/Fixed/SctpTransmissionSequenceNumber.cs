/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using System.Security.Cryptography;

namespace SigtranNet.Protocols.Sctp.Chunks.Parameters.Fixed;

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
