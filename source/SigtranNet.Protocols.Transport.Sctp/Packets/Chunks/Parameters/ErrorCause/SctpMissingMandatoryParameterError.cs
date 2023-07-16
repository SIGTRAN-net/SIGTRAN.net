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

using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.ErrorCause;

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.ErrorCause;

/// <summary>
/// A Missing Mandatory Parameter Error Cause.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         Indicates that one or more mandatory TLV parameters are missing in a received INIT or INIT ACK chunk.
///     </code>
/// </remarks>
internal readonly partial struct SctpMissingMandatoryParameterError : ISctpErrorCauseParameter<SctpMissingMandatoryParameterError>
{
    private const SctpErrorCauseCode ErrorCauseCodeImplicit = SctpErrorCauseCode.MissingMandatoryParameter;
    private const ushort ErrorCauseLengthMinimum = 2 * sizeof(uint);

    /// <summary>
    /// The Cause Length.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         Set to the size of the parameter in bytes, including the Cause Code, Cause Length, and Cause-Specific Information fields.
    ///     </code>
    /// </remarks>
    internal readonly ushort causeLength;

    /// <summary>
    /// The types of the Missing Mandatory Parameters.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         Each field will contain the missing mandatory parameter number.
    ///     </code>
    /// </remarks>
    internal readonly ReadOnlyMemory<SctpChunkParameterType> missingParameterTypes;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpMissingMandatoryParameterError" />.
    /// </summary>
    /// <param name="missingParameterTypes">The types of the Missing Mandatory Parameters.</param>
    internal SctpMissingMandatoryParameterError(ReadOnlyMemory<SctpChunkParameterType> missingParameterTypes)
    {
        this.causeLength = (ushort)(ErrorCauseLengthMinimum + 2 * missingParameterTypes.Length);
        this.missingParameterTypes = missingParameterTypes;
    }

    SctpErrorCauseCode ISctpErrorCause.ErrorCauseCode => ErrorCauseCodeImplicit;
    ushort ISctpErrorCause.ErrorCauseLength => this.causeLength;
}
