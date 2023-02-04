/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Variable;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Variable.Cookie;

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
