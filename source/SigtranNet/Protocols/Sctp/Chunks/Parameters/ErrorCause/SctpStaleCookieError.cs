/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Sctp.Chunks.Parameters.ErrorCause;

/// <summary>
/// A Stale Cookie error, an SCTP Packet Chunk parameter that indicates an error.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         Indicates the receipt of a valid State Cookie that has expired.
///     </code>
/// </remarks>
internal readonly partial struct SctpStaleCookieError : ISctpErrorCauseParameter<SctpStaleCookieError>
{
    private const SctpErrorCauseCode ErrorCauseCodeImplicit = SctpErrorCauseCode.StaleCookie;
    private const ushort ErrorCauseLengthImplicit = 2 * sizeof(uint);

    /// <summary>
    /// The Measure of Staleness.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         This field contains the difference, rounded up in microseconds, between the current time and the time the State Cookie expired.
    ///         
    ///             The sender of this error cause MAY choose to report how long past expiration the State Cookie is by including a non-zero value in the Measure of Staleness field. If the sender does not wish to provide the Measure of Staleness, it <b>SHOULD</b> set this field to the value of zero.
    ///     </code>
    /// </remarks>
    internal readonly uint measureOfStaleness;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpStaleCookieError" />.
    /// </summary>
    /// <param name="measureOfStaleness">The Measure of Staleness.</param>
    internal SctpStaleCookieError(uint measureOfStaleness)
    {
        this.measureOfStaleness = measureOfStaleness;
    }

    SctpErrorCauseCode ISctpErrorCause.ErrorCauseCode => ErrorCauseCodeImplicit;
    ushort ISctpErrorCause.ErrorCauseLength => ErrorCauseLengthImplicit;
}
