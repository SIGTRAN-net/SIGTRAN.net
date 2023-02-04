/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks.Exceptions;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Exceptions;

/// <summary>
/// An exception that is thrown if an error occurs during the processing of an SCTP chunk parameter.
/// </summary>
internal abstract class SctpChunkParameterException : SctpChunkException
{
    /// <summary>
    /// Initializes a new instance of <see cref="SctpChunkParameterException" />.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">An optional inner exception.</param>
    internal SctpChunkParameterException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }
}
