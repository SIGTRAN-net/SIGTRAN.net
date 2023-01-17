/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Sctp;

/// <summary>
/// An SCTP packet's common header.
/// </summary>
internal readonly partial struct SctpCommonHeader
{
    /// <summary>
    /// The Source Port Number.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         This is the SCTP sender's port number. It can be used by the receiver in combination with the source IP address, the SCTP Destination Port Number, and possibly the destination IP address to identify the association to which this packet belongs. The Source Port Number 0 MUST NOT be used.
    ///     </code>
    /// </remarks>
    internal readonly ushort sourcePortNumber;

    /// <summary>
    /// The Destination Port Number.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         This is the SCTP port number to which this packet is destined. The receiving host will use this port number to de-multiplex the SCTP packet to the correct receiving endpoint/application. The Destination Port Number 0 MUST NOT be used.
    ///     </code>
    /// </remarks>
    internal readonly ushort destinationPortNumber;

    /// <summary>
    /// The Verification Tag.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         The receiver of an SCTP packet uses the Verification Tag to validate the sender of this packet. On transmit, the value of the Verification Tag MUST be set to the value of the Initiate Tag received from the peer endpoint during the association initialization, with the following exceptions:
    ///
    ///         -   A packet containing an INIT chunk MUST have a zero Verification Tag.
    ///         -   A packet containing a SHUTDOWN COMPLETE chunk with the T bit set MUST have the Verification Tag copied from the packet with the SHUTDOWN ACK chunk.
    ///         -   A packet containing an ABORT chunk MAY have the Verification Tag copied from the packet that caused the ABORT chunk to be sent.
    ///     </code>
    /// </remarks>
    internal readonly uint verificationTag;

    /// <summary>
    /// The Checksum.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         This field contains the checksum of the SCTP packet.
    ///     </code>
    /// </remarks>
    internal readonly uint checksum;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpCommonHeader" />.
    /// </summary>
    /// <param name="sourcePortNumber">The source port number.</param>
    /// <param name="destinationPortNumber">The destination port number.</param>
    /// <param name="verificationTag">The verification tag.</param>
    /// <param name="checksum">The checksum.</param>
    internal SctpCommonHeader(
        ushort sourcePortNumber,
        ushort destinationPortNumber,
        uint verificationTag,
        uint checksum)
    {
        this.sourcePortNumber = sourcePortNumber;
        this.destinationPortNumber = destinationPortNumber;
        this.verificationTag = verificationTag;
        this.checksum = checksum;
    }
}
