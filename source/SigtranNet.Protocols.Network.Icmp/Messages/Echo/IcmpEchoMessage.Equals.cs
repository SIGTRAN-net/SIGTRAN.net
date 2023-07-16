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

namespace SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.Echo;

internal readonly partial struct IcmpEchoMessage
{
    private bool Equals(IcmpEchoMessage other) =>
        this.isReply.Equals(other.isReply)
        && this.code.Equals(other.code)
        && this.identifier.Equals(other.identifier)
        && this.sequenceNumber.Equals(other.sequenceNumber)
        && this.data.Span.SequenceEqual(other.data.Span);

    /// <inheritdoc />
    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj switch
        {
            null => false,
            IcmpEchoMessage other => this.Equals(other),
            _ => false
        };

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(this.isReply);
        hashCode.Add(this.code);
        hashCode.Add(this.identifier);
        hashCode.Add(this.sequenceNumber);
        hashCode.AddBytes(this.data.Span);
        return hashCode.ToHashCode();
    }
}
