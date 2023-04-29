/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Network.IP.IPv4.Controllers.Exceptions;

/// <summary>
/// An exception that is thrown if an IPv4 Controller is asked to listen but is already listening.
/// </summary>
internal sealed class IPv4ControllerAlreadyListeningException : IPv4ControllerException
{
    /// <summary>
    /// Initializes a new instance of <see cref="IPv4ControllerAlreadyListeningException" />.
    /// </summary>
    internal IPv4ControllerAlreadyListeningException()
        : base(ExceptionMessages.AlreadyListening)
    {
    }
}
