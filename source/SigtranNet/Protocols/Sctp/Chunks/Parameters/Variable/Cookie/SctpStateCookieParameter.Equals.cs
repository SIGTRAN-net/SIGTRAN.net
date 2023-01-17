/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using System.Diagnostics.CodeAnalysis;

namespace SigtranNet.Protocols.Sctp.Chunks.Parameters.Variable.Cookie;

internal readonly partial struct SctpStateCookieParameter
{
    private bool Equals(SctpStateCookieParameter other) =>
        this.parameterLength.Equals(other.parameterLength)
        && this.stateCookie.Span.SequenceEqual(other.stateCookie.Span);

    /// <inheritdoc />
    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj switch
        {
            null => false,
            SctpStateCookieParameter other => this.Equals(other),
            _ => false
        };

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(this.parameterLength);
        hashCode.AddBytes(this.stateCookie.Span);
        return hashCode.ToHashCode();
    }
}
