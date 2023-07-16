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
/// An exception that is thrown if an invalid or unsupported Error Cause Code is provided.
/// </summary>
internal sealed partial class SctpErrorCauseCodeInvalidException : SctpChunkParameterException
{
    /// <summary>
    /// Initializes a new instance of <see cref="SctpErrorCauseCodeInvalidException" />.
    /// </summary>
    /// <param name="errorCauseCode">The invalid Error Cause Code.</param>
    internal SctpErrorCauseCodeInvalidException(SctpErrorCauseCode errorCauseCode)
        : base(CreateExceptionMessage(errorCauseCode))
    {
        this.ErrorCauseCode = errorCauseCode;
    }

    /// <summary>
    /// Gets the invalid Error Cause Code.
    /// </summary>
    internal SctpErrorCauseCode ErrorCauseCode { get; }

    private static string CreateExceptionMessage(SctpErrorCauseCode errorCauseCode) =>
        string.Format(ExceptionMessages.ErrorCauseCodeInvalid, errorCauseCode);
}
