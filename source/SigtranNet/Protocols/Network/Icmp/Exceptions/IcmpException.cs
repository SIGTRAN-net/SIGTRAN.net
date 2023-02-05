/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Exceptions;

namespace SigtranNet.Protocols.Network.Icmp.Exceptions;

/// <summary>
/// An exception that is thrown if an error in the Internet Control Message Protocol (ICMP) occurs.
/// </summary>
internal abstract class IcmpException : ProtocolException
{
    /// <summary>
    /// Initializes a new instance of <see cref="IcmpException" />.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">An optional inner exception.</param>
    protected IcmpException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }
}
