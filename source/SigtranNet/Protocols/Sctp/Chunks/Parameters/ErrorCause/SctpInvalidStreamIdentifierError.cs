/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Sctp.Chunks.Parameters.ErrorCause;

/// <summary>
/// An Error Cause parameter for an Invalid Stream Identifier.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         Indicates that the endpoint received a DATA chunk sent using a nonexistent stream.
///     </code>
/// </remarks>
internal readonly partial struct SctpInvalidStreamIdentifierError : ISctpErrorCauseParameter<SctpInvalidStreamIdentifierError>
{
    private const SctpErrorCauseCode ErrorCauseCodeImplicit = SctpErrorCauseCode.InvalidStreamIdentifier;
    private const ushort ErrorCauseLengthImplicit = 2 * sizeof(uint);

    /// <summary>
    /// Stream Identifier.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         Contains the Stream Identifier of the DATA chunk received in error.
    ///     </code>
    /// </remarks>
    internal readonly ushort streamIdentifier;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpInvalidStreamIdentifierError" />.
    /// </summary>
    /// <param name="streamIdentifier">The Stream Identifier of the DATA chunk received in error.</param>
    internal SctpInvalidStreamIdentifierError(ushort streamIdentifier)
    {
        this.streamIdentifier = streamIdentifier;
    }

    SctpErrorCauseCode ISctpErrorCause.ErrorCauseCode => ErrorCauseCodeImplicit;
    ushort ISctpErrorCause.ErrorCauseLength => ErrorCauseLengthImplicit;
}
