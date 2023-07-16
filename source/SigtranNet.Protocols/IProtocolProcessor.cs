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

using SigtranNet.Protocols.Messages;

namespace SigtranNet.Protocols;

/// <summary>
/// Processes an OSI protocol.
/// </summary>
public interface IProtocolProcessor
{
}

/// <summary>
/// Processes an OSI protocol.
/// </summary>
/// <typeparam name="TMessage">The type of data message.</typeparam>
public interface IProtocolProcessor<TMessage> : IProtocolProcessor
    where TMessage : IProtocolMessage<TMessage>
{
    /// <summary>
    /// Processes the data message.
    /// </summary>
    /// <param name="data">The binary data.</param>
    /// <returns>The processed message.</returns>
    internal virtual TMessage Process(ReadOnlyMemory<byte> data)
    {
        return TMessage.FromReadOnlyMemory(data);
    }
}