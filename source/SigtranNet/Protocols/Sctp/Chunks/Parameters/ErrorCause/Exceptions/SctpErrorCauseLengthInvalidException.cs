/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Sctp.Chunks.Parameters.Exceptions;

namespace SigtranNet.Protocols.Sctp.Chunks.Parameters.ErrorCause.Exceptions;

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
