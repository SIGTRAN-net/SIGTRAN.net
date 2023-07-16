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

using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Exceptions;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Variable;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.HeartbeatAcknowledgement;

internal readonly partial struct SctpHeartbeatAcknowledgement
{
    /// <inheritdoc />
    public static SctpHeartbeatAcknowledgement FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;

        /* Chunk Header */
        var chunkType = (SctpChunkType)memorySpan[0];
        if (chunkType != ChunkTypeImplicit)
            throw new SctpChunkTypeInvalidException(chunkType);
        // Skip Chunk Flags; HEARTBEAT ACK chunks do not have Chunk Flags.
        var chunkLength = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[sizeof(ushort)..]);

        /* Chunk Parameters */
        var heartbeatInformation = SctpHeartbeatInfoParameter.FromReadOnlyMemory(memory[sizeof(uint)..chunkLength]);

        return new(heartbeatInformation);
    }

    /// <inheritdoc />
    public static SctpHeartbeatAcknowledgement Read(BinaryReader binaryReader) =>
        ISctpChunk<SctpHeartbeatAcknowledgement>.Read(binaryReader);

    /// <inheritdoc />
    public static SctpHeartbeatAcknowledgement Read(Stream stream) =>
        ISctpChunk<SctpHeartbeatAcknowledgement>.Read(stream);

    /// <inheritdoc />
    public static Task<SctpHeartbeatAcknowledgement> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        ISctpChunk<SctpHeartbeatAcknowledgement>.ReadAsync(stream, cancellationToken);

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
        // Skip Chunk Flags; the HEARTBEAT ACK chunk does not have Chunk Flags.
        BinaryPrimitives.WriteUInt16BigEndian(span[sizeof(ushort)..], this.chunkLength);

        /* Heartbeat Information */
        this.heartbeatInformation.Write(span[sizeof(uint)..]);
    }
}
