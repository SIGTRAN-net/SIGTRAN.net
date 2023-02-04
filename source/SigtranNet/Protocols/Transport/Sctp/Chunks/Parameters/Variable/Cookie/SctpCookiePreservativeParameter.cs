/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Variable.Cookie;

/// <summary>
/// A Cookie Preservative Parameter in an SCTP Packet Chunk.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         The sender of the INIT chunk uses this parameter to suggest to the receiver of the INIT chunk a longer life span for the State Cookie.
///     </code>
/// </remarks>
internal readonly partial struct SctpCookiePreservativeParameter : ISctpChunkParameterVariableLength<SctpCookiePreservativeParameter>
{
    private const SctpChunkParameterType ParameterTypeImplicit = SctpChunkParameterType.CookiePreservative;
    private const ushort ParameterLengthImplicit = 2 * sizeof(uint);

    /// <summary>
    /// The Suggested Cookie Life-Span Increment.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         This parameter indicates to the receiver how much increment in milliseconds the sender wishes the receiver to add to its default cookie life span.
    ///
    ///             This optional parameter MAY be added to the INIT chunk by the sender when it reattempts establishing an association with a peer to which its previous attempt of establishing the association failed due to a stale cookie operation error.The receiver MAY choose to ignore the suggested cookie life span increase for its own security reasons.
    ///     </code>
    /// </remarks>
    internal readonly uint suggestedCookieLifeSpanIncrement;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpCookiePreservativeParameter" />.
    /// </summary>
    /// <param name="suggestedCookieLifeSpanIncrement">The Suggested Cookie Life-Span Increment (msec.)</param>
    internal SctpCookiePreservativeParameter(uint suggestedCookieLifeSpanIncrement)
    {
        this.suggestedCookieLifeSpanIncrement = suggestedCookieLifeSpanIncrement;
    }

    SctpChunkParameterType ISctpChunkParameterVariableLength.ParameterType => throw new NotImplementedException();

    ushort ISctpChunkParameter.ParameterLength => throw new NotImplementedException();
}
