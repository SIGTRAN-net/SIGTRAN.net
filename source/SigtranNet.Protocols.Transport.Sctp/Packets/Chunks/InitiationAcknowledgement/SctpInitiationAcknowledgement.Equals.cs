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

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.InitiationAcknowledgement;

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
