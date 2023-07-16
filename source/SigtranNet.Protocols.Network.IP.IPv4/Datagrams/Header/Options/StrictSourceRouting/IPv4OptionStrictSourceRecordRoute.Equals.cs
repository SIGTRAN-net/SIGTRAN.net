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

namespace SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header.Options.StrictSourceRouting;

internal readonly partial struct IPv4OptionStrictSourceRecordRoute
{
    private bool Equals(IPv4OptionStrictSourceRecordRoute other)
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
            IPv4OptionStrictSourceRecordRoute other => this.Equals(other),
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
