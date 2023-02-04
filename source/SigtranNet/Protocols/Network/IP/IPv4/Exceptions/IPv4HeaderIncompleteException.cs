/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Network.IP.IPv4.Exceptions;

/// <summary>
/// An exception that is thrown if the memory that has been read does not have the expected IPv4 Internet Header length.
/// </summary>
internal sealed class IPv4HeaderIncompleteException : IPv4Exception
{
    /// <summary>
    /// Initializes a new instance of <see cref="IPv4HeaderIncompleteException" />.
    /// </summary>
    /// <param name="internetHeaderLength">The expected IPv4 Internet Header length.</param>
    internal IPv4HeaderIncompleteException(byte internetHeaderLength)
        : base(CreateExceptionMessage(internetHeaderLength))
    {
        this.InternetHeaderLength = internetHeaderLength;
    }

    /// <summary>
    /// Gets the expected IPv4 Internet Header Length.
    /// </summary>
    internal byte InternetHeaderLength { get; }

    private static string CreateExceptionMessage(byte internetHeaderLength) =>
        string.Format(ExceptionMessages.HeaderIncomplete, internetHeaderLength);
}
