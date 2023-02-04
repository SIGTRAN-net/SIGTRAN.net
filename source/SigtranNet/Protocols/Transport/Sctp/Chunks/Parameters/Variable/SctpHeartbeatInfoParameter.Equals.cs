/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using System.Diagnostics.CodeAnalysis;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Variable;

internal readonly partial struct SctpHeartbeatInfoParameter
{
    private bool Equals(SctpHeartbeatInfoParameter other)
    {
        if (this.parameterLength != other.parameterLength)
            return false;
        return this.heartbeatInformation.Span.SequenceEqual(other.heartbeatInformation.Span);
    }

    /// <inheritdoc />
    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj switch
        {
            null => false,
            SctpHeartbeatInfoParameter other => this.Equals(other),
            _ => false
        };

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(this.parameterLength);
        hashCode.AddBytes(this.heartbeatInformation.Span);
        return hashCode.ToHashCode();
    }
}
