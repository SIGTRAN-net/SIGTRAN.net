/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.IP.Exceptions;

/// <summary>
/// An exception that is thrown if the Internet Protocol version is not supported.
/// </summary>
internal sealed class IPVersionNotSupportedException : IPException
{
    /// <summary>
    /// Initializes a new instance of <see cref="IPVersionNotSupportedException" />.
    /// </summary>
    internal IPVersionNotSupportedException()
        : base("IP version not supported.")
    {
    }
}
