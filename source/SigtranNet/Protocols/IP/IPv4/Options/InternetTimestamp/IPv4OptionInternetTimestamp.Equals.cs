/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using System.Diagnostics.CodeAnalysis;

namespace SigtranNet.Protocols.IP.IPv4.Options.InternetTimestamp;

internal readonly partial struct IPv4OptionInternetTimestamp
{
    private bool Equals(IPv4OptionInternetTimestamp other)
    {
        var equal =
            this.optionType.Equals(other.optionType)
            && this.length.Equals(other.length)
            && this.pointer.Equals(other.pointer)
            && this.overflow.Equals(other.overflow)
            && this.flag.Equals(other.flag);

        if (!equal) return equal;

        var timestampsSpanThis = this.timestamps.Span;
        var timestampsSpanOther = other.timestamps.Span;
        if (timestampsSpanThis.Length != timestampsSpanOther.Length) return false;
        return timestampsSpanThis.SequenceEqual(timestampsSpanOther);
    }

    /// <inheritdoc />
    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj switch
        {
            null => false,
            IPv4OptionInternetTimestamp other => this.Equals(other),
            _ => false
        };

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hashCode =
            HashCode.Combine(
                this.optionType,
                this.length,
                this.pointer,
                this.overflow,
                this.flag);

        var timestampsSpan = this.timestamps.Span;
        for (var i = 0; i < timestampsSpan.Length; i++)
        {
            hashCode = HashCode.Combine(hashCode, timestampsSpan[i]);
        }

        return hashCode;
    }
}
