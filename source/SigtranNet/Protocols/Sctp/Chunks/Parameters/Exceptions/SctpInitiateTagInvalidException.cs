/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Sctp.Chunks.Parameters.Exceptions;

/// <summary>
/// An exception that is thrown if an SCTP Initiate Tag is invalid.
/// </summary>
internal class SctpInitiateTagInvalidException : SctpChunkParameterException
{
    /// <summary>
    /// Initializes a new instance of <see cref="SctpInitiateTagInvalidException" />.
    /// </summary>
    internal SctpInitiateTagInvalidException()
        : base(ExceptionMessages.InitiateTagInvalid)
    {
    }
}
