/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.IP.Exceptions;

namespace SigtranNet.Protocols.IP.IPv4.Exceptions;

/// <summary>
/// An exception that is thrown if an error occurs in the IPv4 protocol.
/// </summary>
internal abstract class IPv4Exception : IPException
{
    /// <summary>
    /// Initializes a new instance of <see cref="IPv4Exception" />.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">An optional inner exception.</param>
    protected IPv4Exception(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }
}
