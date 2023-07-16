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
/// A State Cookie parameter in an SCTP Packet Chunk.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         This parameter value MUST contain all the necessary state and parameter information required for the sender of this INIT ACK chunk to create the association, along with a Message Authentication Code (MAC). See <a href="https://datatracker.ietf.org/doc/html/rfc9260#sec_generating_state_cookie">Section 5.1.3</a> for details on State Cookie definition.
///     </code>
/// </remarks>
internal readonly partial struct SctpStateCookieParameter : ISctpChunkParameterVariableLength<SctpStateCookieParameter>
{
    private const SctpChunkParameterType ParameterTypeImplicit = SctpChunkParameterType.StateCookie;

    /// <summary>
    /// The Parameter Length.
    /// </summary>
    internal readonly ushort parameterLength;

    /// <summary>
    /// An SCTP State Cookie.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         This parameter value MUST contain all the necessary state and parameter information required for the sender of this INIT ACK chunk to create the association, along with a Message Authentication Code (MAC). See <a href="https://datatracker.ietf.org/doc/html/rfc9260#sec_generating_state_cookie">Section 5.1.3</a> for details on State Cookie definition.
    ///     </code>
    /// </remarks>
    internal readonly ReadOnlyMemory<byte> stateCookie;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpStateCookieParameter" />.
    /// </summary>
    /// <param name="stateCookie">The SCTP State Cookie.</param>
    internal SctpStateCookieParameter(ReadOnlyMemory<byte> stateCookie)
    {
        this.parameterLength = (ushort)(sizeof(uint) + stateCookie.Length);
        this.stateCookie = stateCookie;
    }

    public SctpChunkParameterType ParameterType => ParameterTypeImplicit;
    public ushort ParameterLength => this.parameterLength;
}
