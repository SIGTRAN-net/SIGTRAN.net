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

namespace SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header;

internal readonly partial struct IPv4Header
{
    private bool Equals(IPv4Header other)
    {
        var equal =
            this.internetHeaderLength.Equals(other.internetHeaderLength)
            && this.typeOfService.Equals(other.typeOfService)
            && this.totalLength.Equals(other.totalLength)
            && this.identification.Equals(other.identification)
            && this.flags.Equals(other.flags)
            && this.fragmentOffset.Equals(other.fragmentOffset)
            && this.timeToLive.Equals(other.timeToLive)
            && this.protocol.Equals(other.protocol)
            && this.sourceAddress.Equals(other.sourceAddress)
            && this.destinationAddress.Equals(other.destinationAddress);

        if (!equal) return equal;

        var optionsSpanThis = this.options.Span;
        var optionsSpanOther = other.options.Span;
        if (optionsSpanThis.Length != optionsSpanOther.Length) return false;
        return optionsSpanThis.SequenceEqual(optionsSpanOther);
    }

    /// <inheritdoc />
    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj switch
        {
            null => false,
            IPv4Header other => this.Equals(other),
            _ => false
        };

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(this.internetHeaderLength);
        hashCode.Add(this.typeOfService);
        hashCode.Add(this.totalLength);
        hashCode.Add(this.identification);
        hashCode.Add(this.flags);
        hashCode.Add(this.fragmentOffset);
        hashCode.Add(this.timeToLive);
        hashCode.Add(this.protocol);
        hashCode.Add(this.sourceAddress);
        hashCode.Add(this.destinationAddress);

        var optionsSpan = this.options.Span;
        for (var i = 0; i < optionsSpan.Length; i++)
        {
            hashCode.Add(optionsSpan[i]);
        }

        return hashCode.ToHashCode();
    }
}
