/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using System.Diagnostics.CodeAnalysis;

namespace SigtranNet.Protocols.Sctp.Chunks.Parameters.ErrorCause;

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
