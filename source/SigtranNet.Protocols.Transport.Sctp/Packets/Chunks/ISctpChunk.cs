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

using SigtranNet.Binary;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Abort;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.CookieAcknowledgement;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.CookieEcho;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Exceptions;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.HeartbeatAcknowledgement;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.HeartbeatRequest;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Initiation;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.InitiationAcknowledgement;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.OperationError;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.PayloadData;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.SelectiveAcknowledgement;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.ShutdownAcknowledgement;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.ShutdownAssociation;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.ShutdownComplete;
using System.Buffers.Binary;
using System.Runtime.InteropServices;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters;

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks;

/// <summary>
/// A Chunk in an SCTP packet.
/// </summary>
internal interface ISctpChunk : IBinarySerializable
{
    private static readonly Dictionary<SctpChunkType, Func<ReadOnlyMemory<byte>, ISctpChunk>> Deserializers =
        new()
        {
            { SctpChunkType.PayloadData, memory => SctpPayloadData.FromReadOnlyMemory(memory) },
            { SctpChunkType.Initiation, memory => SctpInitiation.FromReadOnlyMemory(memory) },
            { SctpChunkType.InitiationAcknowledgement, memory => SctpInitiationAcknowledgement.FromReadOnlyMemory(memory) },
            { SctpChunkType.SelectiveAcknowledgement, memory => SctpSelectiveAcknowledgement.FromReadOnlyMemory(memory) },
            { SctpChunkType.HeartbeatRequest, memory => SctpHeartbeatRequest.FromReadOnlyMemory(memory) },
            { SctpChunkType.HeartbeatAcknowledgement, memory => SctpHeartbeatAcknowledgement.FromReadOnlyMemory(memory) },
            { SctpChunkType.Abort, memory => SctpAbort.FromReadOnlyMemory(memory) },
            { SctpChunkType.Shutdown, memory => SctpShutdownAssociation.FromReadOnlyMemory(memory) },
            { SctpChunkType.ShutdownAcknowledgement, memory => SctpShutdownAcknowledgement.FromReadOnlyMemory(memory) },
            { SctpChunkType.OperationError, memory => SctpOperationError.FromReadOnlyMemory(memory) },
            { SctpChunkType.StateCookie, memory => SctpCookieEcho.FromReadOnlyMemory(memory) },
            { SctpChunkType.CookieAcknowledgement, memory => SctpCookieAcknowledgement.FromReadOnlyMemory(memory) },
            { SctpChunkType.ShutdownComplete, memory => SctpShutdownComplete.FromReadOnlyMemory(memory) },
        };

    /// <summary>
    /// Gets the Chunk Type.
    /// </summary>
    SctpChunkType ChunkType { get; }

    /// <summary>
    /// Gets the Chunk Flags.
    /// </summary>
    byte ChunkFlags { get; }

    /// <summary>
    /// Gets the Chunk Length.
    /// </summary>
    ushort ChunkLength { get; }

    /// <summary>
    /// Gets the chunk parameters.
    /// </summary>
    ReadOnlyMemory<ISctpChunkParameter> Parameters { get; }

    /// <summary>
    /// Deserializes an SCTP chunk from <paramref name="memory" /> without knowing before this method is called which type of chunk it is.
    /// </summary>
    /// <param name="memory">The memory that contains the SCTP chunk.</param>
    /// <returns>The deserialized SCTP chunk.</returns>
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the chunk type is invalid or not supported.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if the chunk has specified an invalid chunk length.
    /// </exception>
    static ISctpChunk FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;
        var chunkType = (SctpChunkType)memorySpan[0];
        ref var deserializer = ref CollectionsMarshal.GetValueRefOrNullRef(Deserializers, chunkType);
        if (deserializer == null)
            throw new SctpChunkTypeInvalidException(chunkType);
        return deserializer(memory);
    }
}

/// <summary>
/// A Chunk in an SCTP Packet.
/// </summary>
/// <typeparam name="TChunk"></typeparam>
internal interface ISctpChunk<TChunk> : ISctpChunk, IBinarySerializable<TChunk>
    where TChunk : ISctpChunk<TChunk>
{
    /// <summary>
    /// Reads and deserializes an SCTP Chunk <typeparamref name="TChunk" /> from <paramref name="binaryReader" />.
    /// </summary>
    /// <param name="binaryReader">The binary reader that reads a serialized SCTP Chunk <typeparamref name="TChunk" />.</param>
    /// <returns>The deserialized SCTP Chunk <typeparamref name="TChunk" />.</returns>
    new static TChunk Read(BinaryReader binaryReader)
    {
        /* Chunk Header */
        var chunkHeader = new ReadOnlyMemory<byte>(binaryReader.ReadBytes(sizeof(uint)));
        var chunkHeaderSpan = chunkHeader.Span;
        var chunkLength = BinaryPrimitives.ReadUInt16BigEndian(chunkHeaderSpan[sizeof(ushort)..]);

        /* Chunk Parameters */
        var chunkParameters = new ReadOnlyMemory<byte>(binaryReader.ReadBytes(chunkLength - sizeof(uint)));

        /* Result */
        var memory = new Memory<byte>(new byte[chunkLength]);
        chunkHeader.CopyTo(memory);
        chunkParameters.CopyTo(memory[sizeof(uint)..]);
        return TChunk.FromReadOnlyMemory(memory);
    }

    /// <summary>
    /// Reads and deserializes an SCTP Chunk <typeparamref name="TChunk" /> from <paramref name="stream" />.
    /// </summary>
    /// <param name="stream">The stream that contains a serialized SCTP Chunk <typeparamref name="TChunk" />.</param>
    /// <returns>The deserialized SCTP Chunk <typeparamref name="TChunk" />.</returns>
    new static TChunk Read(Stream stream)
    {
        /* Chunk Header */
        var chunkHeader = new Memory<byte>(new byte[sizeof(uint)]);
        var chunkHeaderSpan = chunkHeader.Span;
        stream.Read(chunkHeaderSpan);
        var chunkLength = BinaryPrimitives.ReadUInt16BigEndian(chunkHeaderSpan[sizeof(ushort)..]);

        /* Result */
        var memory = new Memory<byte>(new byte[chunkLength]);
        chunkHeader.CopyTo(memory);
        stream.Read(memory.Span[sizeof(uint)..]);
        return TChunk.FromReadOnlyMemory(memory);
    }

    /// <summary>
    /// Reads and deserializes an SCTP Chunk <typeparamref name="TChunk" /> from <paramref name="stream" />.
    /// </summary>
    /// <param name="stream">The stream that contains a serialized SCTP Chunk <typeparamref name="TChunk" />.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The deserialized SCTP Chunk <typeparamref name="TChunk" />.</returns>
    new static async Task<TChunk> ReadAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        /* Chunk Header */
        var chunkHeader = new Memory<byte>(new byte[sizeof(uint)]);
        await stream.ReadAsync(chunkHeader, cancellationToken);
        var chunkLength = BinaryPrimitives.ReadUInt16BigEndian(chunkHeader.Span[sizeof(ushort)..]);

        /* Result */
        var memory = new Memory<byte>(new byte[chunkLength]);
        chunkHeader.CopyTo(memory);
        await stream.ReadAsync(memory[sizeof(uint)..], cancellationToken);
        return TChunk.FromReadOnlyMemory(memory);
    }
}