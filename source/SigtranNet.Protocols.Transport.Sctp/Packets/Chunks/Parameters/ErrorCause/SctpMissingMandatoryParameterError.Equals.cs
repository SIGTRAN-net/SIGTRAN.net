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

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.ErrorCause;

internal readonly partial struct SctpMissingMandatoryParameterError
{
    private bool Equals(SctpMissingMandatoryParameterError other)
    {
        var equal = this.causeLength.Equals(other.causeLength);
        if (!equal) return equal;

        var missingParameterTypesSpanThis = this.missingParameterTypes.Span;
        var missingParameterTypesSpanOther = other.missingParameterTypes.Span;
        return missingParameterTypesSpanThis.SequenceEqual(missingParameterTypesSpanOther);
    }

    /// <inheritdoc />
    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj switch
        {
            null => false,
            SctpMissingMandatoryParameterError other => this.Equals(other),
            _ => false
        };

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(this.causeLength);

        var missingParameterTypesSpan = this.missingParameterTypes.Span;
        for (var i = 0; i < this.missingParameterTypes.Length; i++)
        {
            hashCode.Add(missingParameterTypesSpan[i]);
        }

        return hashCode.ToHashCode();
    }
}
