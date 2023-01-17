/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using System.Diagnostics.CodeAnalysis;

namespace SigtranNet.Protocols.Sctp.Chunks.Parameters.Variable.Address;

internal readonly partial struct SctpSupportedAddressTypesParameter
{
    private bool Equals(SctpSupportedAddressTypesParameter other) =>
        this.supportedAddressTypes.Span.SequenceEqual(other.supportedAddressTypes.Span);

    /// <inheritdoc />
    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj switch
        {
            null => false,
            SctpSupportedAddressTypesParameter other => this.Equals(other),
            _ => false
        };

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var supportedAddressTypesSpan = this.supportedAddressTypes.Span;
        var hashCode = new HashCode();
        for (var i = 0; i < supportedAddressTypes.Length; i++)
        {
            hashCode.Add(supportedAddressTypesSpan[i]);
        }
        return hashCode.ToHashCode();
    }
}
