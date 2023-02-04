/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Exceptions;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause.Exceptions;

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
