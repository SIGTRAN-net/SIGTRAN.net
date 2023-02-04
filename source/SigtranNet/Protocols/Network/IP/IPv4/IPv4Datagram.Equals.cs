/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using System.Diagnostics.CodeAnalysis;

namespace SigtranNet.Protocols.Network.IP.IPv4;

internal readonly partial struct IPv4Datagram
{
    private bool Equals(IPv4Datagram other)
    {
        var equal = this.header.Equals(other.header);
        if (!equal) return equal;

        if (this.payload.Length != other.payload.Length) return false;
        var payloadThis = this.payload.Span;
        var payloadOther = other.payload.Span;
        return payloadThis.SequenceEqual(payloadOther);
    }

    /// <inheritdoc />
    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj switch
        {
            null => false,
            IPv4Datagram other => this.Equals(other),
            _ => false
        };

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(this.header);
        hashCode.AddBytes(this.payload.Span);
        return hashCode.ToHashCode();
    }
}
