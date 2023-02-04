/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using System.Diagnostics.CodeAnalysis;

namespace SigtranNet.Protocols.Network.IP.IPv4.Options.LooseSourceRouting;

internal readonly partial struct IPv4OptionLooseSourceRecordRoute
{
    private bool Equals(IPv4OptionLooseSourceRecordRoute other)
    {
        var equal =
            this.optionType.Equals(other.optionType)
            && this.length.Equals(other.length)
            && this.pointer.Equals(other.pointer);

        if (!equal) return equal;

        var routeSpanThis = this.route.Span;
        var routeSpanOther = other.route.Span;
        equal &= routeSpanThis.Length.Equals(routeSpanOther.Length);
        if (!equal) return equal;
        return routeSpanThis.SequenceEqual(routeSpanOther);
    }

    /// <inheritdoc />
    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj switch
        {
            null => false,
            IPv4OptionLooseSourceRecordRoute other => this.Equals(other),
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

    /// <summary>
    /// Determines whether <paramref name="left" /> and <paramref name="right" /> are equal.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>A value that indicates whether <paramref name="left" /> and <paramref name="right" /> are equal.</returns>
    public static bool operator ==(IPv4OptionLooseSourceRecordRoute left, object? right) =>
        left.Equals(right);

    /// <summary>
    /// Determines whether <paramref name="left" /> and <paramref name="right" /> are not equal.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>A value that indicates whether <paramref name="left" /> and <paramref name="right" /> are not equal.</returns>
    public static bool operator !=(IPv4OptionLooseSourceRecordRoute left, object? right) =>
        !left.Equals(right);

    /// <summary>
    /// Determines whether <paramref name="left" /> and <paramref name="right" /> are equal.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>A value that indicates whether <paramref name="left" /> and <paramref name="right" /> are equal.</returns>
    public static bool operator ==(object? left, IPv4OptionLooseSourceRecordRoute right) =>
        right.Equals(left);

    /// <summary>
    /// Determines whether <paramref name="left" /> and <paramref name="right" /> are not equal.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>A value that indicates whether <paramref name="left" /> and <paramref name="right" /> are not equal.</returns>
    public static bool operator !=(object? left, IPv4OptionLooseSourceRecordRoute right) =>
        !right.Equals(left);
}
