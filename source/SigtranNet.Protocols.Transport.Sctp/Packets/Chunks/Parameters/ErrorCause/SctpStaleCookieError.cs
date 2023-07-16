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

using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.ErrorCause;

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.ErrorCause;

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
