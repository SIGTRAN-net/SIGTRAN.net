/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause;

/// <summary>
/// An Error Cause Code of an SCTP error.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         Sections <a href="https://datatracker.ietf.org/doc/html/rfc9260#sec_invalid_stream_identifier_cause">3.3.10.1</a> - <a href="https://datatracker.ietf.org/doc/html/rfc9260#sec_protocol_violation_cause">3.3.10.13</a> define error causes for SCTP. Guidelines for the IETF to define new error cause values are discussed in <a href="https://datatracker.ietf.org/doc/html/rfc9260#sec_ietf_defined_additional_error_causes">Section 15.4</a>.
///     </code>
/// </remarks>
internal enum SctpErrorCauseCode : ushort
{
    /// <summary>
    /// Invalid Stream Identifier.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         Indicates that the endpoint received a DATA chunk sent using a nonexistent stream.
    ///     </code>
    /// </remarks>
    InvalidStreamIdentifier = 1,

    /// <summary>
    /// Missing Mandatory Parameter.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         Indicates that one or more mandatory TLV parameters are missing in a received INIT or INIT ACK chunk.
    ///     </code>
    /// </remarks>
    MissingMandatoryParameter = 2,

    /// <summary>
    /// Stale Cookie.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         Indicates the receipt of a valid State Cookie that has expired.
    ///     </code>
    /// </remarks>
    StaleCookie = 3,

    /// <summary>
    /// Out of Resource.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         Indicates that the sender is out of resource. This is usually sent in combination with or within an ABORT chunk.
    ///     </code>
    /// </remarks>
    OutOfResource = 4,

    /// <summary>
    /// Unresolvable Address.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         Indicates that the sender is not able to resolve the specified address parameter (e.g., type of address is not supported by the sender). This is usually sent in combination with or within an ABORT chunk.
    ///     </code>
    /// </remarks>
    UnresolvableAddress = 5,

    /// <summary>
    /// Unrecognized Chunk Type.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         This error cause is returned to the originator of the chunk if the receiver does not understand the chunk and the upper bits of the 'Chunk Type' are set to 01 or 11.
    ///     </code>
    /// </remarks>
    UnrecognizedChunkType = 6,

    /// <summary>
    /// Invalid Mandatory Parameter.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         This error cause is returned to the originator of an INIT or INIT ACK chunk when one of the mandatory parameters is set to an invalid value.
    ///     </code>
    /// </remarks>
    InvalidMandatoryParameter = 7,

    /// <summary>
    /// Unrecognized Parameters.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         This error cause is returned to the originator of the INIT ACK chunk if the receiver does not recognize one or more Optional TLV parameters in the INIT ACK chunk.
    ///     </code>
    /// </remarks>
    UnrecognizedParameters = 8,

    /// <summary>
    /// No User Data.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         This error cause is returned to the originator of a DATA chunk if a received DATA chunk has no user data.
    ///     </code>
    ///     <code>
    ///         This cause code is normally returned in an ABORT chunk (see <a href="https://datatracker.ietf.org/doc/html/rfc9260#sec_acknowledgements_of_reception_of_data_chunks">Section 6.2</a>).
    ///     </code>
    /// </remarks>
    NoUserData = 9,

    /// <summary>
    /// Cookie Received While Shutting Down.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         A COOKIE ECHO chunk was received while the endpoint was in the SHUTDOWN-ACK-SENT state. This error is usually returned in an ERROR chunk bundled with the retransmitted SHUTDOWN ACK chunk.
    ///     </code>
    /// </remarks>
    CookieReceivedWhileShuttingDown = 10,

    /// <summary>
    /// Restart of an Association with New Addresses.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         An INIT chunk was received on an existing association. But the INIT chunk added addresses to the association that were previously not part of the association. The new addresses are listed in the error cause. This error cause is normally sent as part of an ABORT chunk refusing the INIT chunk (see <a href="https://datatracker.ietf.org/doc/html/rfc9260#sec_handle_duplicate_or_unexpected_chunks">Section 5.2</a>).
    ///     </code>
    ///     <code>
    ///         Note: Each New Address TLV is an exact copy of the TLV that was found in the INIT chunk that was new, including the Parameter Type and the Parameter Length.
    ///     </code>
    /// </remarks>
    RestartAssociationWithNewAddresses = 11,

    /// <summary>
    /// User-Initiated Abort.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         This error cause MAY be included in ABORT chunks that are sent because of an upper-layer request. The upper layer can specify an Upper Layer Abort Reason that is transported by SCTP transparently and MAY be delivered to the upper-layer protocol at the peer.
    ///     </code>
    /// </remarks>
    UserInitiatedAbort = 12,

    /// <summary>
    /// Protocol Violation.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         This error cause MAY be included in ABORT chunks that are sent because an SCTP endpoint detects a protocol violation of the peer that is not covered by the error causes described in Sections <a href="https://datatracker.ietf.org/doc/html/rfc9260#sec_invalid_stream_identifier_cause">3.3.10.1</a> - <a href="https://datatracker.ietf.org/doc/html/rfc9260#sec_user_initiated_abort_cause">3.3.10.12</a>. An implementation MAY provide additional information specifying what kind of protocol violation has been detected.
    ///     </code>
    /// </remarks>
    ProtocolViolation = 13,

    /// <summary>
    /// Request to Delete Last Remaining IP Address.
    /// </summary>
    /// <remarks>
    ///     From RFC 5061:
    ///     <code>
    ///         Request to Delete Last Remaining IP Address: The receiver of this
    ///         error sent a request to delete the last IP address from its
    ///         association with its peer.  This error indicates that the request is
    ///         rejected.
    ///     </code>
    /// </remarks>
    RequestDeleteLastRemainingIPAddress = 160,

    /// <summary>
    /// Operation Refused Due to Resource Shortage.
    /// </summary>
    /// <remarks>
    ///     From RFC 5061:
    ///     <code>
    ///         This Error Cause is used to report a failure by the receiver to
    ///         perform the requested operation due to a lack of resources.  The
    ///         entire TLV that is refused is copied from the ASCONF into the Error
    ///         Cause.
    ///     </code>
    /// </remarks>
    OperationRefusedDueToResourceShortage = 161,

    /// <summary>
    /// Request to Delete Source IP Address.
    /// </summary>
    /// <remarks>
    ///     From RFC 5061:
    ///     <code>
    ///         Request to Delete Source IP Address: The receiver of this error sent
    ///         a request to delete the source IP address of the ASCONF message.
    ///         This error indicates that the request is rejected.
    ///     </code>
    /// </remarks>
    RequestDeleteSourceIPAddress = 162,

    /// <summary>
    /// Association Aborted due to illegal ASCONF-ACK.
    /// </summary>
    /// <remarks>
    ///     From RFC 5061:
    ///     <code>
    ///         This error is to be included in an ABORT that is generated due to the
    ///         reception of an ASCONF-ACK that was not expected but is larger than
    ///         the current Sequence Number (see <a href="https://www.rfc-editor.org/rfc/rfc5061.html#section-5.3">Section 5.3</a>, Rule F0 ).  Note that a
    ///         Sequence Number is larger than the last acked Sequence Number if it
    ///         is either the next sequence or no more than 2**31-1 greater than the
    ///         current Sequence Number.  Sequence Numbers smaller than the last
    ///         acked Sequence Number are silently ignored.
    ///     </code>
    /// </remarks>
    AssocationAbortedIllegal_ASCONF_ACK = 163,

    /// <summary>
    /// Request refused - no authorization.
    /// </summary>
    /// <remarks>
    ///     From RFC 5061:
    ///     <code>
    ///         This Error Cause may be included to reject a request based on local
    ///         security policies.
    ///     </code>
    /// </remarks>
    RequestRefused_NoAuthorization = 164,

    /// <summary>
    /// Unsupported HMAC Identifier.
    /// </summary>
    /// <remarks>
    ///     From RFC 4895:
    ///     <code>
    ///         This section defines a new error cause that will be sent if an AUTH
    ///         chunk is received with an unsupported HMAC Identifier.
    ///     </code>
    /// </remarks>
    UnsupportedHMACIdentifier = 261
}
