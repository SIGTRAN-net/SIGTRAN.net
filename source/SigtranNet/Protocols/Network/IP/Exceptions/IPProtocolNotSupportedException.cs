/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Network.IP.Exceptions;

/// <summary>
/// An exception that is thrown if a protocol that is carried by the Internet Protocol (IP) is not supported. 
/// </summary>
internal sealed class IPProtocolNotSupportedException : IPException
{
    /// <summary>
    /// Initializes a new instance of <see cref="IPProtocolNotSupportedException" />.
    /// </summary>
    /// <param name="protocol">
    /// The protocol that is not supported.
    /// </param>
    internal IPProtocolNotSupportedException(IPProtocol protocol)
        : base(CreateExceptionMessage(protocol))
    {
        this.Protocol = protocol;
    }

    /// <summary>
    /// Gets the protocol that is not supported.
    /// </summary>
    internal IPProtocol Protocol { get; }

    private static string CreateExceptionMessage(IPProtocol protocol) =>
        string.Format(ExceptionMessages.IPProtocolNotSupported, protocol);
}
