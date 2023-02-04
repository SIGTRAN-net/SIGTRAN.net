/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using System.Diagnostics.CodeAnalysis;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.CookieEcho;

internal readonly partial struct SctpCookieEcho
{
    private bool Equals(SctpCookieEcho other) =>
        this.chunkLength.Equals(other.chunkLength)
        && this.cookie.Span.SequenceEqual(other.cookie.Span);

    /// <inheritdoc />
    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj switch
        {
            null => false,
            SctpCookieEcho other => this.Equals(other),
            _ => false
        };

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(this.chunkLength);
        hashCode.AddBytes(this.cookie.Span);
        return hashCode.ToHashCode();
    }
}
