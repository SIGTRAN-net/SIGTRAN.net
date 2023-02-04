/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Network.IP.IPv4.Exceptions;

namespace SigtranNet.Protocols.Network.IP.IPv4.Controllers.Exceptions;

/// <summary>
/// An exception that is thrown if an <see cref="IPv4Controller" /> encounters an error.
/// </summary>
internal abstract class IPv4ControllerException : IPv4Exception
{
    /// <summary>
    /// Initializes a new instance of <see cref="IPv4ControllerException" />.
    /// </summary>
    /// <param name="exceptionMessage">The exception message.</param>
    /// <param name="innerException">An optional inner exception.</param>
    protected IPv4ControllerException(string exceptionMessage, Exception? innerException = null)
        : base(exceptionMessage, innerException)
    {
    }
}
