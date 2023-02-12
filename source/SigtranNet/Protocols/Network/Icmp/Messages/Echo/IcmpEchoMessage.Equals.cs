/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using System.Diagnostics.CodeAnalysis;

namespace SigtranNet.Protocols.Network.Icmp.Messages.Echo;

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
