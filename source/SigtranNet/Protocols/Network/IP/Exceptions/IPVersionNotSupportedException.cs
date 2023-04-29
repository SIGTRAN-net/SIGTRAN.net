/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Network.IP.Exceptions;

/// <summary>
/// An exception that is thrown if the Internet Protocol version is not supported.
/// </summary>
internal sealed class IPVersionNotSupportedException : IPException
{
    /// <summary>
    /// Initializes a new instance of <see cref="IPVersionNotSupportedException" />.
    /// </summary>
    /// <param name="version">
    /// The Internet Protocol (IP) version.
    /// </param>
    internal IPVersionNotSupportedException(IPVersion version)
        : base(CreateExceptionMessage(version))
    {
        this.Version = version;
    }

    /// <summary>
    /// Gets the version that is not supported.
    /// </summary>
    internal IPVersion Version { get; }

    private static string CreateExceptionMessage(IPVersion version) =>
        string.Format(ExceptionMessages.IPVersionNotSupported, version);
}
