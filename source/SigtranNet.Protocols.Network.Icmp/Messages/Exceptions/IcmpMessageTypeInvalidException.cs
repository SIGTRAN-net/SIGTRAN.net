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

using SigtranNet.Protocols.Network.IP.IPv4.Icmp.Exceptions;

namespace SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.Exceptions;

/// <summary>
/// An exception that is thrown if a specified Internet Control Message Protocol (ICMP) message type is invalid.
/// </summary>
internal sealed class IcmpMessageTypeInvalidException : IcmpException
{
    /// <summary>
    /// Initializes a new instance of <see cref="IcmpMessageTypeInvalidException" />.
    /// </summary>
    /// <param name="messageType">The invalid message type.</param>
    internal IcmpMessageTypeInvalidException(IcmpMessageType messageType)
        : base(CreateExceptionMessage(messageType))
    {
        this.MessageType = messageType;
    }

    /// <summary>
    /// Gets the invalid Internet Control Message Protocol (ICMP) message type.
    /// </summary>
    internal IcmpMessageType MessageType { get; }

    private static string CreateExceptionMessage(IcmpMessageType messageType) =>
        string.Format(ExceptionMessages.MessageTypeInvalid, messageType);
}
