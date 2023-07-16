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

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.ErrorCause;

internal readonly partial struct SctpUnrecognizedParametersError
{
    private bool Equals(SctpUnrecognizedParametersError other) =>
        this.errorCauseLength.Equals(other.errorCauseLength)
        && this.unrecognizedParameters.Span.SequenceEqual(other.unrecognizedParameters.Span);

    /// <inheritdoc />
    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj switch
        {
            null => false,
            SctpUnrecognizedParametersError other => this.Equals(other),
            _ => false
        };

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var unrecognizedParametersSpan = this.unrecognizedParameters.Span;
        var hashCode = new HashCode();
        for (var i = 0; i < unrecognizedParameters.Length; i++)
        {
            hashCode.Add(unrecognizedParametersSpan[i]);
        }
        return hashCode.ToHashCode();
    }
}
