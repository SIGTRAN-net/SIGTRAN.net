/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using System.Diagnostics.CodeAnalysis;

namespace SigtranNet.Protocols.Network.Icmp.Messages.Timestamp;

internal readonly partial struct IcmpTimestampMessage
{
    private bool Equals(IcmpTimestampMessage other) =>
        this.isReply.Equals(other.isReply)
        && this.code.Equals(other.code)
        && this.identifier.Equals(other.identifier)
        && this.sequenceNumber.Equals(other.sequenceNumber)
        && this.originateTimestamp.Equals(other.originateTimestamp)
        && this.receiveTimestamp.Equals(other.receiveTimestamp)
        && this.transmitTimestamp.Equals(other.transmitTimestamp);

    /// <inheritdoc />
    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj switch
        {
            null => false,
            IcmpTimestampMessage other => this.Equals(other),
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
        hashCode.Add(this.originateTimestamp);
        hashCode.Add(this.receiveTimestamp);
        hashCode.Add(this.transmitTimestamp);
        return hashCode.ToHashCode();
    }
}
