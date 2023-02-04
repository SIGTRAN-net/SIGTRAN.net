/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Network.IP.IPv4.Controllers.Exceptions;

/// <summary>
/// An exception that is thrown if an <see cref="IPv4Controller" /> can not resolve a network interface for the IP connection.
/// </summary>
internal sealed class IPv4ControllerCannotResolveNetworkInterfaceException : IPv4ControllerException
{
    /// <summary>
    /// Initializes a new instance of <see cref="IPv4ControllerCannotResolveNetworkInterfaceException" />.
    /// </summary>
    internal IPv4ControllerCannotResolveNetworkInterfaceException()
        : base(ExceptionMessages.CannotResolveNetworkInterface)
    {
    }
}
