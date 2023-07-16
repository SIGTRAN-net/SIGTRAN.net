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

namespace SigtranNet.Protocols.Network.IP.Exceptions;

/// <summary>
/// An exception is thrown if an Internet Protocol error is encountered.
/// </summary>
public abstract class IPException : ProtocolException
{
    /// <summary>
    /// Initializes a nw instance of <see cref="IPException" />.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">An optional inner exception.</param>
    protected IPException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }
}
