/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Exceptions;

/// <summary>
/// An exception that is thrown if a Protocol error occurs.
/// </summary>
internal abstract class ProtocolException : Exception
{
    /// <summary>
    /// Initializes a new instance of <see cref="ProtocolException" />.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">An optional inner exception.</param>
    protected ProtocolException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }
}
