/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using System.Diagnostics.CodeAnalysis;

namespace SigtranNet.Protocols.Sctp.Chunks.Parameters.ErrorCause;

internal readonly partial struct SctpUnrecognizedParametersError
{
    private bool Equals(SctpUnrecognizedParametersError other) =>
        this.errorCauseLength.Equals(other.errorCauseLength)
        && this.unrecognizedParameters.Span.SequenceEqual(other.unrecognizedParameters.Span);

    /// <inheritdoc />
    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj switch
        {
            null => false,
            SctpUnrecognizedParametersError other => this.Equals(other),
            _ => false
        };

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var unrecognizedParametersSpan = this.unrecognizedParameters.Span;
        var hashCode = new HashCode();
        for (var i = 0; i < unrecognizedParameters.Length; i++)
        {
            hashCode.Add(unrecognizedParametersSpan[i]);
        }
        return hashCode.ToHashCode();
    }
}
