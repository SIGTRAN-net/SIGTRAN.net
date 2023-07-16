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

using SigtranNet.Protocols.Network.IP.IPv4.Exceptions;

namespace SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header.Exceptions;

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
        InternetHeaderLength = internetHeaderLength;
    }

    /// <summary>
    /// Gets the expected IPv4 Internet Header Length.
    /// </summary>
    internal byte InternetHeaderLength { get; }

    private static string CreateExceptionMessage(byte internetHeaderLength) =>
        string.Format(ExceptionMessages.HeaderIncomplete, internetHeaderLength);
}
