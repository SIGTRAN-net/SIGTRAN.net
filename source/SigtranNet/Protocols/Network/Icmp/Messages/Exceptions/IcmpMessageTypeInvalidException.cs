/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Network.Icmp.Exceptions;

namespace SigtranNet.Protocols.Network.Icmp.Messages.Exceptions;

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
