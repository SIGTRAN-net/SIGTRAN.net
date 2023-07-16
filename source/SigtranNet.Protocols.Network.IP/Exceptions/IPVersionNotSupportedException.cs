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
/// An exception that is thrown if the Internet Protocol version is not supported.
/// </summary>
public sealed class IPVersionNotSupportedException : IPException
{
    /// <summary>
    /// Initializes a new instance of <see cref="IPVersionNotSupportedException" />.
    /// </summary>
    /// <param name="version">
    /// The Internet Protocol (IP) version.
    /// </param>
    internal IPVersionNotSupportedException(InternetProtocolVersion version)
        : base(CreateExceptionMessage(version))
    {
        this.Version = version;
    }

    /// <summary>
    /// Gets the version that is not supported.
    /// </summary>
    internal InternetProtocolVersion Version { get; }

    private static string CreateExceptionMessage(InternetProtocolVersion version) =>
        string.Format(ExceptionMessages.IPVersionNotSupported, version);
}
