/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using System.Diagnostics.CodeAnalysis;

namespace SigtranNet.Protocols.Transport.Sctp;

internal readonly partial struct SctpPacket
{
    private bool Equals(SctpPacket other)
    {
        var equal = this.commonHeader.Equals(other.commonHeader);
        if (!equal) return equal;

        if (this.chunks.Length != other.chunks.Length) return false;
        var chunksSpanThis = this.chunks.Span;
        var chunksSpanOther = other.chunks.Span;
        for (var i = 0; i < chunksSpanThis.Length; i++)
        {
            equal &= chunksSpanThis[i].Equals(chunksSpanOther[i]);
            if (!equal) return equal;
        }

        return equal;
    }

    /// <inheritdoc />
    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj switch
        {
            null => false,
            SctpPacket other => this.Equals(other),
            _ => false
        };

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(this.commonHeader);

        var chunksSpan = this.chunks.Span;
        for (var i = 0; i < this.chunks.Length; i++)
        {
            hashCode.Add(chunksSpan[i]);
        }

        return hashCode.ToHashCode();
    }
}
