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

using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.ErrorCause;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Variable;

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.ErrorCause;

/// <summary>
/// An Unrecognized Parameters Error Cause parameter in an SCTP Packet Chunk.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         This error cause is returned to the originator of the INIT ACK chunk if the receiver does not recognize one or more Optional TLV parameters in the INIT ACK chunk.
///     </code>
/// </remarks>
internal readonly partial struct SctpUnrecognizedParametersError : ISctpErrorCauseParameter<SctpUnrecognizedParametersError>
{
    private const SctpErrorCauseCode ErrorCauseCodeImplicit = SctpErrorCauseCode.UnrecognizedParameters;

    /// <summary>
    /// The Cause Length.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         Set to the size of the parameter in bytes, including the Cause Code, Cause Length, and Cause-Specific Information fields.
    ///     </code>
    /// </remarks>
    internal readonly ushort errorCauseLength;

    /// <summary>
    /// The Unrecognized Parameters.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         The Unrecognized Parameters field contains the unrecognized parameters copied from the INIT ACK chunk complete with TLV. This error cause is normally contained in an ERROR chunk bundled with the COOKIE ECHO chunk when responding to the INIT ACK chunk, when the sender of the COOKIE ECHO chunk wishes to report unrecognized parameters.
    ///     </code>
    /// </remarks>
    internal readonly ReadOnlyMemory<ISctpChunkParameterVariableLength> unrecognizedParameters;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpUnrecognizedParametersError" />.
    /// </summary>
    /// <param name="unrecognizedParameters">
    /// The unrecognized parameters.
    /// </param>
    internal SctpUnrecognizedParametersError(ReadOnlyMemory<ISctpChunkParameterVariableLength> unrecognizedParameters)
    {
        this.errorCauseLength = sizeof(uint);
        var unrecognizedParametersSpan = unrecognizedParameters.Span;
        for (var i = 0; i < unrecognizedParametersSpan.Length; i++)
        {
            this.errorCauseLength += unrecognizedParametersSpan[i].ParameterLength;
        }

        this.unrecognizedParameters = unrecognizedParameters;
    }

    SctpErrorCauseCode ISctpErrorCause.ErrorCauseCode => ErrorCauseCodeImplicit;
    ushort ISctpErrorCause.ErrorCauseLength => this.errorCauseLength;
}
