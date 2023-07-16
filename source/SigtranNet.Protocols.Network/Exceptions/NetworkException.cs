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

using SigtranNet.Protocols.Exceptions;

namespace SigtranNet.Protocols.Network.Exceptions;

/// <summary>
/// An exception that is thrown when an error occurs in a Network protocol.
/// </summary>
public abstract class NetworkException : ProtocolException
{
    /// <summary>
    /// Initializes a new instance of <see cref="NetworkException" />.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">An optional inner exception.</param>
    protected NetworkException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }
}
