/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Sctp.Chunks.Exceptions;
using SigtranNet.Protocols.Sctp.Chunks.Parameters.Variable;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Sctp.Chunks.HeartbeatRequest;

internal readonly partial struct SctpHeartbeatRequest
{
    /// <inheritdoc />
    public static SctpHeartbeatRequest FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;

        /* Chunk Header */
        var chunkType = (SctpChunkType)memorySpan[0];
        if (chunkType != ChunkTypeImplicit)
            throw new SctpChunkTypeInvalidException(chunkType);
        // Skip Chunk Flags; HEARTBEAT chunks do not have Chunk Flags.
        var chunkLength = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[sizeof(ushort)..]);

        /* Chunk Parameters */
        var heartbeatInformation = SctpHeartbeatInfoParameter.FromReadOnlyMemory(memory[sizeof(uint)..chunkLength]);

        return new(heartbeatInformation);
    }

    /// <inheritdoc />
    public static SctpHeartbeatRequest Read(BinaryReader binaryReader) =>
        ISctpChunk<SctpHeartbeatRequest>.Read(binaryReader);

    /// <inheritdoc />
    public static SctpHeartbeatRequest Read(Stream stream) =>
        ISctpChunk<SctpHeartbeatRequest>.Read(stream);

    /// <inheritdoc />
    public static Task<SctpHeartbeatRequest> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        ISctpChunk<SctpHeartbeatRequest>.ReadAsync(stream, cancellationToken);

    /// <inheritdoc />
    public ReadOnlyMemory<byte> ToReadOnlyMemory()
    {
        // Add padding if necessary.
        var memory = new Memory<byte>(new byte[this.chunkLength + this.chunkLength % sizeof(uint)]);
        this.Write(memory.Span);
        return memory;
    }

    /// <inheritdoc />
    public void Write(Span<byte> span)
    {
        /* Chunk Header */
        span[0] = (byte)ChunkTypeImplicit;
        // Skip Chunk Flags; the HEARTBEAT chunk does not have Chunk Flags.
        BinaryPrimitives.WriteUInt16BigEndian(span[sizeof(ushort)..], this.chunkLength);

        /* Heartbeat Information */
        this.heartbeatInformation.Write(span[sizeof(uint)..]);
    }
}
