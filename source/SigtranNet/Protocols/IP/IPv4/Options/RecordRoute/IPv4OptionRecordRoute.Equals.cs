/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using System.Diagnostics.CodeAnalysis;

namespace SigtranNet.Protocols.IP.IPv4.Options.RecordRoute;

internal readonly partial struct IPv4OptionRecordRoute
{
    private bool Equals(IPv4OptionRecordRoute other)
    {
        var equal =
            this.optionType.Equals(other.optionType)
            && this.length.Equals(other.length)
            && this.pointer.Equals(other.pointer);

        if (!equal) return equal;

        var routeSpanThis = this.route.Span;
        var routeSpanOther = other.route.Span;
        if (routeSpanThis.Length != routeSpanOther.Length) return false;
        for (var i = 0; i < routeSpanThis.Length; i++)
        {
            equal &= routeSpanThis[i].Equals(routeSpanOther[i]);
        }

        return equal;
    }

    /// <inheritdoc />
    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj switch
        {
            null => false,
            IPv4OptionRecordRoute other => this.Equals(other),
            _ => false
        };

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(this.optionType);
        hashCode.Add(this.length);
        hashCode.Add(this.pointer);

        var routeSpan = this.route.Span;
        for (var i = 0; i < routeSpan.Length; i++)
        {
            hashCode.Add(routeSpan[i]);
        }

        return hashCode.ToHashCode();
    }
}
