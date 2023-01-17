/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using System.Diagnostics.CodeAnalysis;

namespace SigtranNet.Protocols.Sctp.Chunks.OperationError;

internal readonly partial struct SctpOperationError
{
    private bool Equals(SctpOperationError other)
    {
        var equal = this.chunkLength.Equals(other.chunkLength);
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
            SctpOperationError other => this.Equals(other),
            _ => false
        };

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(this.chunkLength);

        var errorCausesSpan = this.errorCauses.Span;
        for (var i = 0; i < this.errorCauses.Length; i++)
        {
            hashCode.Add(errorCausesSpan[i]);
        }

        return hashCode.ToHashCode();
    }
}
