/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;

namespace SigtranNet.Protocols.Network.Icmp.Messages;

/// <summary>
/// An Internet Control Message Protocol (ICMP) message.
/// </summary>
internal interface IIcmpMessage : IBinarySerializable
{
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