/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.IP.IPv4.Exceptions;

namespace SigtranNet.Protocols.IP.IPv4.Options.Exceptions;

/// <summary>
/// An exception that is thrown if an error occurs during the processing of an IPv4 Option.
/// </summary>
internal abstract class IPv4OptionException : IPv4Exception
{
    /// <summary>
    /// Initializes a new instance of <see cref="IPv4OptionException" />.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">An optional inner exception.</param>
    protected IPv4OptionException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }
}
