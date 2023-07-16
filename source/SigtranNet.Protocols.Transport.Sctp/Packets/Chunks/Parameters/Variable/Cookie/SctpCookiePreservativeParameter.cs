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

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Variable.Cookie;

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
