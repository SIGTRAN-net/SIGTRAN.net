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

using System.Diagnostics.CodeAnalysis;

namespace SigtranNet.Protocols.Transport.Sctp.Packets;

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
