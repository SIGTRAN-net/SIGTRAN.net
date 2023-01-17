/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using System.Diagnostics.CodeAnalysis;

namespace SigtranNet.Protocols.Sctp.Chunks.Parameters.ErrorCause;

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
