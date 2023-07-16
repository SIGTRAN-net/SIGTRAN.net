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

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Abort;

internal readonly partial struct SctpAbort
{
    private bool Equals(SctpAbort other)
    {
        var equal =
            this.chunkFlags.Equals(other.chunkFlags)
            && this.chunkLength.Equals(other.chunkLength);
        if (!equal) return equal;

        var errorCausesSpanThis = this.errorCauses.Span;
        var errorCausesSpanOther = other.errorCauses.Span;
        for (var i = 0; i < this.errorCauses.Length; i++)
        {
            equal &= errorCausesSpanThis[i].Equals(errorCausesSpanOther[i]);
            if (!equal) return equal;
        }

        return equal;
    }

    /// <inheritdoc />
    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj switch
        {
            null => false,
            SctpAbort other => this.Equals(other),
            _ => false
        };

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(this.chunkFlags);
        hashCode.Add(this.chunkLength);

        var errorCausesSpan = this.errorCauses.Span;
        for (var i = 0; i < this.errorCauses.Length; i++)
        {
            hashCode.Add(errorCausesSpan[i]);
        }

        return hashCode.ToHashCode();
    }
}
