/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using System.Diagnostics.CodeAnalysis;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.InitiationAcknowledgement;

internal readonly partial struct SctpInitiationAcknowledgement
{
    private bool Equals(SctpInitiationAcknowledgement other)
    {
        var equal =
            this.chunkLength.Equals(other.chunkLength)
            && this.initiateTag.Equals(other.initiateTag)
            && this.advertisedReceiverWindowCredit.Equals(other.advertisedReceiverWindowCredit)
            && this.numberOutboundStreams.Equals(other.numberOutboundStreams)
            && this.numberInboundStreams.Equals(other.numberInboundStreams)
            && this.initialTransmissionSequenceNumber.Equals(other.initialTransmissionSequenceNumber);
        if (!equal) return equal;

        if (this.variableLengthParameters.Length != other.variableLengthParameters.Length) return false;
        var variableLengthParametersSpanThis = this.variableLengthParameters.Span;
        var variableLengthParametersSpanOther = other.variableLengthParameters.Span;
        for (var i = 0; i < this.variableLengthParameters.Length; i++)
        {
            equal &= variableLengthParametersSpanThis[i].Equals(variableLengthParametersSpanOther[i]);
            if (!equal) return equal;
        }

        return equal;
    }

    /// <inheritdoc />
    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj switch
        {
            null => false,
            SctpInitiationAcknowledgement other => this.Equals(other),
            _ => false
        };

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hashCode = new HashCode();

        hashCode.Add(this.chunkLength);
        hashCode.Add(this.initiateTag);
        hashCode.Add(this.advertisedReceiverWindowCredit);
        hashCode.Add(this.numberOutboundStreams);
        hashCode.Add(this.numberInboundStreams);
        hashCode.Add(this.initialTransmissionSequenceNumber);

        var variableLengthParametersSpan = this.variableLengthParameters.Span;
        for (var i = 0; i < this.variableLengthParameters.Length; i++)
        {
            hashCode.Add(variableLengthParametersSpan[i]);
        }

        return hashCode.ToHashCode();
    }
}
