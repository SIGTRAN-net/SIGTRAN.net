/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using System.Diagnostics.CodeAnalysis;

namespace SigtranNet.Protocols.IP.IPv4;

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
            && this.headerChecksum.Equals(other.headerChecksum)
            && this.sourceAddress.Equals(other.sourceAddress)
            && this.destinationAddress.Equals(other.destinationAddress);

        if (!equal)
            return equal;

        var optionsSpanThis = this.options.Span;
        var optionsSpanOther = other.options.Span;
        if (optionsSpanThis.Length != optionsSpanOther.Length)
            return false;

        for (var i = 0; i < optionsSpanThis.Length; i++)
        {
            if (!optionsSpanThis[i].Equals(optionsSpanOther[i]))
                return false;
        }

        return true;
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
        var hashCode =
            HashCode.Combine(
                this.internetHeaderLength,
                this.typeOfService,
                this.totalLength,
                this.identification,
                this.flags,
                this.fragmentOffset,
                this.timeToLive,
                this.protocol);
        hashCode =
            HashCode.Combine(
                hashCode,
                this.headerChecksum,
                this.sourceAddress,
                this.destinationAddress);

        var optionsSpan = this.options.Span;
        for (var i = 0; i < optionsSpan.Length; i++)
        {
            hashCode = HashCode.Combine(hashCode, optionsSpan[i]);
        }

        return hashCode;
    }
}
