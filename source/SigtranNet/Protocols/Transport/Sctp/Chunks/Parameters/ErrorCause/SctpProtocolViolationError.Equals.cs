/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using System.Diagnostics.CodeAnalysis;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause;

internal readonly partial struct SctpProtocolViolationError
{
    private bool Equals(SctpProtocolViolationError other) =>
        this.errorCauseLength.Equals(other.errorCauseLength)
        && this.additionalInformation.Span.SequenceEqual(other.additionalInformation.Span);

    /// <inheritdoc />
    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj switch
        {
            null => false,
            SctpProtocolViolationError other => this.Equals(other),
            _ => false
        };

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(this.errorCauseLength);
        hashCode.AddBytes(this.additionalInformation.Span);
        return hashCode.ToHashCode();
    }
}
