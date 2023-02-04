/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using System.Diagnostics.CodeAnalysis;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause;

internal readonly partial struct SctpUserInitiatedAbortError
{
    private bool Equals(SctpUserInitiatedAbortError other) =>
        this.errorCauseLength.Equals(other.errorCauseLength)
        && this.upperLayerAbortReason.Span.SequenceEqual(other.upperLayerAbortReason.Span);

    /// <inheritdoc />
    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj switch
        {
            null => false,
            SctpUserInitiatedAbortError other => this.Equals(other),
            _ => false
        };

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(this.errorCauseLength);
        hashCode.AddBytes(this.upperLayerAbortReason.Span);
        return hashCode.ToHashCode();
    }
}
