/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Exceptions;

namespace SigtranNet.Protocols.Network.IP.Exceptions;

/// <summary>
/// An exception is thrown if an Internet Protocol error is encountered.
/// </summary>
internal abstract class IPException : ProtocolException
{
    /// <summary>
    /// Initializes a nw instance of <see cref="IPException" />.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">An optional inner exception.</param>
    protected IPException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }
}
