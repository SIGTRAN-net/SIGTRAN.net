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

using SigtranNet.Protocols.Network.Datagrams;
using SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.DestinationUnreachable;
using SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.Echo;
using SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.Exceptions;
using SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.Information;
using SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.ParameterProblem;
using SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.Redirect;
using SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.SourceQuench;
using SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.TimeExceeded;
using SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.Timestamp;
using System.Runtime.InteropServices;

namespace SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages;

/// <summary>
/// An Internet Control Message Protocol (ICMP) message.
/// </summary>
public interface IIcmpMessage : INetworkDatagram
{
    private static readonly Dictionary<IcmpMessageType, Func<ReadOnlyMemory<byte>, IIcmpMessage>> Deserializers =
        new()
        {
            { IcmpMessageType.EchoReply, memory => IcmpEchoMessage.FromReadOnlyMemory(memory) },
            { IcmpMessageType.DestinationUnreachable, memory => IcmpDestinationUnreachableMessage.FromReadOnlyMemory(memory) },
            { IcmpMessageType.SourceQuench, memory => IcmpSourceQuenchMessage.FromReadOnlyMemory(memory) },
            { IcmpMessageType.Redirect, memory => IcmpRedirectMessage.FromReadOnlyMemory(memory) },
            { IcmpMessageType.Echo, memory => IcmpEchoMessage.FromReadOnlyMemory(memory) },
            { IcmpMessageType.TimeExceeded, memory => IcmpTimeExceededMessage.FromReadOnlyMemory(memory) },
            { IcmpMessageType.ParameterProblem, memory => IcmpParameterProblemMessage.FromReadOnlyMemory(memory) },
            { IcmpMessageType.Timestamp, memory => IcmpTimestampMessage.FromReadOnlyMemory(memory) },
            { IcmpMessageType.TimestampReply, memory => IcmpTimestampMessage.FromReadOnlyMemory(memory) },
            { IcmpMessageType.InformationRequest, memory => IcmpInformationMessage.FromReadOnlyMemory(memory) },
            { IcmpMessageType.InformationReply, memory => IcmpInformationMessage.FromReadOnlyMemory(memory) },
        };

    /// <summary>
    /// Deserializes an Internet Control Message Protocol (ICMP) message from <paramref name="memory" />.
    /// </summary>
    /// <param name="memory">The memory that contains the ICMP message.</param>
    /// <returns>The deserialized ICMP message.</returns>
    /// <exception cref="IcmpMessageTypeInvalidException">
    /// An <see cref="IcmpMessageTypeInvalidException" /> is thrown if the specified message type is invalid.
    /// </exception>
    /// <exception cref="IcmpMessageChecksumInvalidException">
    /// An <see cref="IcmpMessageChecksumInvalidException" /> is thrown if the message's checksum is invalid or the message is corrupted.
    /// </exception>
    static IIcmpMessage FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var span = memory.Span;
        var type = (IcmpMessageType)span[0];
        ref var deserializer = ref CollectionsMarshal.GetValueRefOrNullRef(Deserializers, type);
        if (deserializer == null)
            throw new IcmpMessageTypeInvalidException(type);
        return deserializer(memory);
    }
}

/// <summary>
/// An Internet Control Message Protocol (ICMP) message.
/// </summary>
/// <typeparam name="TMessage">The type of ICMP message.</typeparam>
internal interface IIcmpMessage<TMessage> : IIcmpMessage, INetworkDatagram<TMessage>
    where TMessage : IIcmpMessage<TMessage>
{
    /// <summary>
    /// Reads the <typeparamref name="TMessage" /> from the stream of <paramref name="binaryReader" />.
    /// </summary>
    /// <param name="binaryReader">The binary reader.</param>
    /// <returns>The deserialized <typeparamref name="TMessage" />.</returns>
    new static TMessage Read(BinaryReader binaryReader)
    {
        var memoryMainLength = 7 * sizeof(uint);
        var memoryMain = new Memory<byte>(new byte[memoryMainLength]);
        var memoryMainSpan = memoryMain.Span;
        binaryReader.Read(memoryMainSpan);

        var ipHeaderOriginalLength = memoryMainSpan[9] & 0x0F;
        var memoryLength = 2 * sizeof(uint) + ipHeaderOriginalLength * sizeof(uint) + sizeof(ulong);
        var memory = new Memory<byte>(new byte[memoryLength]);
        memoryMain.CopyTo(memory);
        binaryReader.Read(memory.Span[memoryMainLength..memoryLength]);

        return TMessage.FromReadOnlyMemory(memory);
    }

    /// <summary>
    /// Reads the <typeparamref name="TMessage" /> from <paramref name="stream" />.
    /// </summary>
    /// <param name="stream">The stream to read from.</param>
    /// <returns>The deserialized <typeparamref name="TMessage" />.</returns>
    new static TMessage Read(Stream stream)
    {
        var memoryMainLength = 7 * sizeof(uint);
        var memoryMain = new Memory<byte>(new byte[memoryMainLength]);
        var memoryMainSpan = memoryMain.Span;
        stream.Read(memoryMainSpan);

        var ipHeaderOriginalLength = memoryMainSpan[9] & 0x0F;
        var memoryLength = 2 * sizeof(uint) + ipHeaderOriginalLength * sizeof(uint) + sizeof(ulong);
        var memory = new Memory<byte>(new byte[memoryLength]);
        memoryMain.CopyTo(memory);
        stream.Read(memory.Span[memoryMainLength..memoryLength]);

        return TMessage.FromReadOnlyMemory(memory);
    }

    /// <summary>
    /// Reads the <typeparamref name="TMessage" /> from <paramref name="stream" />.
    /// </summary>
    /// <param name="stream">The stream to read from.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The deserialized <typeparamref name="TMessage" />.</returns>
    new static async Task<TMessage> ReadAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        var memoryMainLength = 7 * sizeof(uint);
        var memoryMain = new Memory<byte>(new byte[memoryMainLength]);
        await stream.ReadAsync(memoryMain, cancellationToken);

        var ipHeaderOriginalLength = memoryMain.Span[9] & 0x0F;
        var memoryLength = 2 * sizeof(uint) + ipHeaderOriginalLength * sizeof(uint) + sizeof(ulong);
        var memory = new Memory<byte>(new byte[memoryLength]);
        memoryMain.CopyTo(memory);
        await stream.ReadAsync(memory[memoryMainLength..memoryLength], cancellationToken);

        return TMessage.FromReadOnlyMemory(memory);
    }
}