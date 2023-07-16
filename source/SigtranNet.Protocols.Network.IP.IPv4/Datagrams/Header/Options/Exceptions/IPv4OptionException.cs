﻿/*
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

namespace SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header.Options.Exceptions;

/// <summary>
/// An exception that is thrown if an error occurs during the processing of an IPv4 Option.
/// </summary>
internal abstract class IPv4OptionException : IPv4Exception
{
    /// <summary>
    /// Initializes a new instance of <see cref="IPv4OptionException" />.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">An optional inner exception.</param>
    protected IPv4OptionException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }
}
