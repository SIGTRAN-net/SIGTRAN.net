/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;
using SigtranNet.Protocols.Network.Icmp.Messages.DestinationUnreachable;
using SigtranNet.Protocols.Network.Icmp.Messages.Echo;
using SigtranNet.Protocols.Network.Icmp.Messages.Exceptions;
using SigtranNet.Protocols.Network.Icmp.Messages.Information;
using SigtranNet.Protocols.Network.Icmp.Messages.ParameterProblem;
using SigtranNet.Protocols.Network.Icmp.Messages.Redirect;
using SigtranNet.Protocols.Network.Icmp.Messages.SourceQuench;
using SigtranNet.Protocols.Network.Icmp.Messages.TimeExceeded;
using SigtranNet.Protocols.Network.Icmp.Messages.Timestamp;
using System.Runtime.InteropServices;

namespace SigtranNet.Protocols.Network.Icmp.Messages;

/// <summary>
/// An Internet Control Message Protocol (ICMP) message.
/// </summary>
internal interface IIcmpMessage : IBinarySerializable
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
internal interface IIcmpMessage<TMessage> : IIcmpMessage, IBinarySerializable<TMessage>
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