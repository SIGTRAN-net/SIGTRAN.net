/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using System.Diagnostics.CodeAnalysis;

namespace SigtranNet.Protocols.Network.Icmp.Messages.Information;

internal readonly partial struct IcmpInformationMessage
{
    private bool Equals(IcmpInformationMessage other) =>
        this.isReply.Equals(other.isReply)
        && this.code.Equals(other.code)
        && this.identifier.Equals(other.identifier)
        && this.sequenceNumber.Equals(other.sequenceNumber);

    /// <inheritdoc />
    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj switch
        {
            null => false,
            IcmpInformationMessage other => this.Equals(other),
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
        return hashCode.ToHashCode();
    }
}
