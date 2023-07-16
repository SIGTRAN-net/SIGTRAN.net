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
/// An exception that is thrown if a protocol that is carried by the Internet Protocol (IP) is not supported. 
/// </summary>
public sealed class IPProtocolNotSupportedException : IPException
{
    /// <summary>
    /// Initializes a new instance of <see cref="IPProtocolNotSupportedException" />.
    /// </summary>
    /// <param name="protocol">
    /// The protocol that is not supported.
    /// </param>
    internal IPProtocolNotSupportedException(InternetProtocol protocol)
        : base(CreateExceptionMessage(protocol))
    {
        this.Protocol = protocol;
    }

    /// <summary>
    /// Gets the protocol that is not supported.
    /// </summary>
    internal InternetProtocol Protocol { get; }

    private static string CreateExceptionMessage(InternetProtocol protocol) =>
        string.Format(ExceptionMessages.IPProtocolNotSupported, protocol);
}
