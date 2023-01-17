/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Sctp.Chunks.Parameters.Exceptions;
using System.Security.Cryptography;

namespace SigtranNet.Protocols.Sctp.Chunks.Parameters.Fixed;

/// <summary>
/// An SCTP Initiate Tag chunk parameter.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         The receiver of the INIT chunk (the responding end) records the value of the Initiate Tag parameter. This value MUST be placed into the Verification Tag field of every SCTP packet that the receiver of the INIT chunk transmits within this association.
///
///             The Initiate Tag is allowed to have any value except 0. See Section 5.3.1 for more on the selection of the tag value.
///
///             If the value of the Initiate Tag in a received INIT chunk is found to be 0, the receiver MUST silently discard the packet.
///     </code>
/// </remarks>
internal readonly partial struct SctpInitiateTag : ISctpChunkParameter
{
    private const ushort LengthFixed = sizeof(uint);

    /// <summary>
    /// The value of the SCTP Initiate Tag.
    /// </summary>
    internal readonly uint value;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpInitiateTag" />.
    /// </summary>
    /// <param name="value">The value of the SCTP Initiate Tag.</param>
    /// <exception cref="SctpInitiateTagInvalidException">
    /// An <see cref="SctpInitiateTagInvalidException" /> is thrown if <paramref name="value" /> is an invalid Initiate Tag value.
    /// </exception>
    internal SctpInitiateTag(uint value)
    {
        // Guards
        if (value == 0)
            throw new SctpInitiateTagInvalidException();

        // Fields
        this.value = value;
    }

    /// <summary>
    /// Generates a random SCTP Initiate Tag.
    /// </summary>
    /// <returns>
    /// A random SCTP Initiate Tag.
    /// </returns>
    internal static SctpInitiateTag Generate()
    {
        var memory = new Memory<byte>(new byte[LengthFixed]);
        var generator = RandomNumberGenerator.Create();
        generator.GetNonZeroBytes(memory.Span);
        return FromReadOnlyMemory(memory);
    }

    /// <inheritdoc />
    ushort ISctpChunkParameter.ParameterLength => LengthFixed;
}
