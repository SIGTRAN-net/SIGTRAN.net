/*
 * This file is part of SIGTRAN.net.
 * 
 * SIGTRAN.net is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, 
 * either version 3 of the License, or (at your option) any later version.
 * 
 * SIGTRAN.net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. 
 * See the GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License along with SIGTRAN.net. If not, see <https://www.gnu.org/licenses/>.
 */

namespace SigtranNet.Protocols.Network.IP.Exceptions;

/// <summary>
/// An exception that is thrown if an IP Protocol Controller could not be constructed.
/// </summary>
public sealed class IPProtocolControllerConstructionFailedException : IPException
{
    internal IPProtocolControllerConstructionFailedException(InternetProtocol protocol, Type protocolControllerType)
        : base(CreateExceptionMessage(protocol, protocolControllerType))
    {
        this.Protocol = protocol;
        this.ProtocolControllerType = protocolControllerType;
    }

    /// <summary>
    /// Gets the IP Protocol.
    /// </summary>
    public InternetProtocol Protocol { get; }

    /// <summary>
    /// Gets the IP Protocol Controller Type.
    /// </summary>
    public Type ProtocolControllerType { get; }

    private static string CreateExceptionMessage(InternetProtocol protocol, Type protocolControllerType) =>
        string.Format(ExceptionMessages.IPProtocolControllerConstructionFailed, protocol, protocolControllerType);
}
