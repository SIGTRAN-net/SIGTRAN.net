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

namespace SigtranNet.Protocols.Network.IP.IPv4.Datagrams;

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
