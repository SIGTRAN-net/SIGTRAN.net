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

internal readonly partial struct SctpRestartAssociationWithNewAddressesError
{
    private bool Equals(SctpRestartAssociationWithNewAddressesError other)
    {
        var equal = this.errorCauseLength.Equals(other.errorCauseLength);
        if (!equal) return equal;

        var newAddressParametersSpanThis = this.newAddressParameters.Span;
        var newAddressParametersSpanOther = other.newAddressParameters.Span;
        for (var i = 0; i < this.newAddressParameters.Length; i++)
        {
            equal &= newAddressParametersSpanThis[i].Equals(newAddressParametersSpanOther[i]);
        }

        return equal;
    }

    /// <inheritdoc />
    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj switch
        {
            null => false,
            SctpRestartAssociationWithNewAddressesError other => this.Equals(other),
            _ => false
        };

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(this.errorCauseLength);

        var newAddressParametersSpan = this.newAddressParameters.Span;
        for (var i = 0; i < this.newAddressParameters.Length; i++)
        {
            hashCode.Add(newAddressParametersSpan[i]);
        }

        return hashCode.ToHashCode();
    }
}
