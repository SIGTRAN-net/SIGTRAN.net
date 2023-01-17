/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Sctp.Exceptions;

namespace SigtranNet.Protocols.Sctp.Chunks.Exceptions;

/// <summary>
/// An exception that is thrown if an error occurs during the processing of an SCTP chunk.
/// </summary>
internal abstract class SctpChunkException : SctpException
{
    /// <summary>
    /// Initializes a new instance of <see cref="SctpChunkException" />.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">An optional inner exception.</param>
    protected SctpChunkException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }
}
