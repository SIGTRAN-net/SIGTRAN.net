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

using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Exceptions;

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.ErrorCause.Exceptions;

/// <summary>
/// An exception that is thrown if an Error Cause Length is invalid.
/// </summary>
internal sealed partial class SctpErrorCauseLengthInvalidException : SctpChunkParameterException
{
    /// <summary>
    /// Initializes a new instance of <see cref="SctpErrorCauseLengthInvalidException" />.
    /// </summary>
    /// <param name="errorCauseCode">The Error Cause Code.</param>
    /// <param name="errorCauseLength">The invalid Error Cause Length.</param>
    internal SctpErrorCauseLengthInvalidException(SctpErrorCauseCode errorCauseCode, ushort errorCauseLength)
        : base(CreateExceptionMessage(errorCauseCode, errorCauseLength))
    {
        this.ErrorCauseLength = errorCauseLength;
    }

    /// <summary>
    /// Gets the Error Cause Code.
    /// </summary>
    internal SctpErrorCauseCode ErrorCauseCode { get; }

    /// <summary>
    /// Gets the invalid Error Cause Length.
    /// </summary>
    internal ushort ErrorCauseLength { get; }

    private static string CreateExceptionMessage(SctpErrorCauseCode errorCauseCode, ushort errorCauseLength) =>
        string.Format(ExceptionMessages.ErrorCauseLengthInvalid, errorCauseCode, errorCauseLength);
}
