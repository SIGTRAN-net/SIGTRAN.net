/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Exceptions;

namespace SigtranNet.Protocols.Transport.Sctp.Exceptions;

/// <summary>
/// An exception that is thrown if an error occurs during the processing of the SCTP protocol.
/// </summary>
internal abstract class SctpException : ProtocolException
{
    /// <summary>
    /// Initializes a new instance of <see cref="SctpException" />.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">An optional inner exception.</param>
    protected SctpException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }
}
