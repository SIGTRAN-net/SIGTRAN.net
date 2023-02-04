/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using System.Diagnostics.CodeAnalysis;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.PayloadData;

internal readonly partial struct SctpPayloadData
{
    private bool Equals(SctpPayloadData other)
    {
        var equal =
            this.chunkFlags.Equals(other.chunkFlags)
            && this.chunkLength.Equals(other.chunkLength)
            && this.transmissionSequenceNumber.Equals(other.transmissionSequenceNumber)
            && this.streamIdentifier.Equals(other.streamIdentifier)
            && this.streamSequenceNumber.Equals(other.streamSequenceNumber)
            && this.payloadProtocolIdentifier.Equals(other.payloadProtocolIdentifier);

        if (!equal) return equal;

        var userDataSpanThis = this.userData.Span;
        var userDataSpanOther = other.userData.Span;
        if (userDataSpanThis.Length != userDataSpanOther.Length) return false;
        for (var i = 0; i < userDataSpanThis.Length; i++)
        {
            equal &= userDataSpanThis[i].Equals(userDataSpanOther[i]);
        }

        return equal;
    }

    /// <inheritdoc />
    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj switch
        {
            null => false,
            SctpPayloadData other => this.Equals(other),
            _ => false
        };

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hashCode =
            HashCode.Combine(
                this.chunkFlags,
                this.chunkLength,
                this.transmissionSequenceNumber,
                this.streamIdentifier,
                this.streamSequenceNumber,
                this.payloadProtocolIdentifier);
        var userDataSpan = this.userData.Span;
        for (var i = 0; i < userDataSpan.Length; i++)
        {
            hashCode = HashCode.Combine(hashCode, userDataSpan[i]);
        }
        return hashCode;
    }
}
