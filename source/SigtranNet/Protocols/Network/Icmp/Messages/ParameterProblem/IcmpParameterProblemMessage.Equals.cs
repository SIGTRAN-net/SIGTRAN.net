/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using System.Diagnostics.CodeAnalysis;

namespace SigtranNet.Protocols.Network.Icmp.Messages.ParameterProblem;

internal readonly partial struct IcmpParameterProblemMessage
{
    private bool Equals(IcmpParameterProblemMessage other) =>
        this.pointer.Equals(other.pointer)
        && this.ipHeaderOriginal.Equals(other.ipHeaderOriginal)
        && this.originalDataDatagramSample.Span.SequenceEqual(other.originalDataDatagramSample.Span);

    /// <inheritdoc />
    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj switch
        {
            null => false,
            IcmpParameterProblemMessage other => this.Equals(other),
            _ => false
        };

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(this.pointer);
        hashCode.Add(this.ipHeaderOriginal);
        hashCode.AddBytes(this.originalDataDatagramSample.Span);
        return hashCode.ToHashCode();
    }
}
