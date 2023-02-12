/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using System.Diagnostics.CodeAnalysis;

namespace SigtranNet.Protocols.Network.Icmp.Messages.SourceQuench;

internal readonly partial struct IcmpSourceQuenchMessage
{
    private bool Equals(IcmpSourceQuenchMessage other) =>
        this.ipHeaderOriginal.Equals(other.ipHeaderOriginal)
        && this.originalDataDatagramSample.Span.SequenceEqual(other.originalDataDatagramSample.Span);

    /// <inheritdoc />
    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj switch
        {
            null => false,
            IcmpSourceQuenchMessage other => this.Equals(other),
            _ => false
        };

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(this.ipHeaderOriginal);
        hashCode.AddBytes(this.originalDataDatagramSample.Span);
        return hashCode.ToHashCode();
    }
}
