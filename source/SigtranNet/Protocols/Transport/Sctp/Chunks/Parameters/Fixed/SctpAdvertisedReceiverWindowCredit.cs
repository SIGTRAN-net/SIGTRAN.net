/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Exceptions;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Fixed;

/// <summary>
/// An SCTP chunk's Advertised Receiver Window Credit (a_rwnd) parameter.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         This value represents the dedicated buffer space, in number of bytes, the sender of the INIT chunk has reserved in association with this window.
///
///             The Advertised Receiver Window Credit MUST NOT be smaller than 1500.
///
///             A receiver of an INIT chunk with the a_rwnd value set to a value smaller than 1500 MUST discard the packet, SHOULD send a packet in response containing an ABORT chunk and using the Initiate Tag as the Verification Tag, and MUST NOT change the state of any existing association.
///
///             During the life of the association, this buffer space SHOULD NOT be reduced (i.e., dedicated buffers ought not to be taken away from this association); however, an endpoint MAY change the value of a_rwnd it sends in SACK chunks.
///     </code>
/// </remarks>
internal readonly partial struct SctpAdvertisedReceiverWindowCredit : ISctpChunkParameter
{
    private const ushort LengthFixed = sizeof(uint);
    private const uint ValueMinimum = 1500;

    /// <summary>
    /// The Advertised Receiver Window Credit (a_rwnd) value.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         This value represents the dedicated buffer space, in number of bytes, the sender of the INIT chunk has reserved in association with this window.
    ///
    ///             The Advertised Receiver Window Credit MUST NOT be smaller than 1500.
    ///
    ///             A receiver of an INIT chunk with the a_rwnd value set to a value smaller than 1500 MUST discard the packet, SHOULD send a packet in response containing an ABORT chunk and using the Initiate Tag as the Verification Tag, and MUST NOT change the state of any existing association.
    ///
    ///             During the life of the association, this buffer space SHOULD NOT be reduced (i.e., dedicated buffers ought not to be taken away from this association); however, an endpoint MAY change the value of a_rwnd it sends in SACK chunks.
    ///     </code>
    /// </remarks>
    internal readonly uint value;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpAdvertisedReceiverWindowCredit" />.
    /// </summary>
    /// <param name="value">The value of the Advertised Receiver Window Credit (a_rwnd).</param>
    /// <exception cref="SctpAdvertisedReceiverWindowCreditInvalidException">
    /// An <see cref="SctpAdvertisedReceiverWindowCreditInvalidException" /> is thrown if <paramref name="value" /> has an invalid Advertised Receiver Window Credit (a_rwnd) value.
    /// </exception>
    internal SctpAdvertisedReceiverWindowCredit(uint value)
    {
        // Guards
        if (value < ValueMinimum)
            throw new SctpAdvertisedReceiverWindowCreditInvalidException();

        // Fields
        this.value = value;
    }

    ushort ISctpChunkParameter.ParameterLength => LengthFixed;
}
