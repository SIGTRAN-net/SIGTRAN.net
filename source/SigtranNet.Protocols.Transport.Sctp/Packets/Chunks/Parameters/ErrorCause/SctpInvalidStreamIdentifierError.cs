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
