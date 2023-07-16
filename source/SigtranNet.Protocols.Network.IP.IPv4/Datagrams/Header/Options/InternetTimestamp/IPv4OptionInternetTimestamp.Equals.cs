/*
 * This file is part of SIGTRAN.net.
 * 
 * SIGTRAN.net is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, 
 * either version 3 of the License, or (at your option) any later version.
 * 
 * SIGTRAN.net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. 
 * See the GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License along with SIGTRAN.net. If not, see <https://www.gnu.org/licenses/>.
 */

using System.Diagnostics.CodeAnalysis;

namespace SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header.Options.InternetTimestamp;

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
