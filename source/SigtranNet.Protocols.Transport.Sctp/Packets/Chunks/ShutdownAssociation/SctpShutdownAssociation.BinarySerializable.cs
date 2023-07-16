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
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Fixed;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.ShutdownAssociation;

internal readonly partial struct SctpShutdownAssociation
{
    /// <inheritdoc />
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the specified Chunk Type is not equal to <see cref="SctpChunkType.Shutdown" />.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if the specified Chunk Length is not equal to the length of two unsigned 32-bit integers.
    /// </exception>
    public static SctpShutdownAssociation FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;

        /* Chunk header */
        var chunkType = (SctpChunkType)memorySpan[0];
        if (chunkType != ChunkTypeImplicit)
            throw new SctpChunkTypeInvalidException(chunkType);
        // Skip Flags; SHUTDOWN does not have Chunk Flags.
        var chunkLength = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[sizeof(ushort)..]);
        if (chunkLength != ChunkLengthImplicit)
            throw new SctpChunkLengthInvalidException(chunkLength);

        /* Cumulative TSN Ack */
        var cumulativeTransmissionSequenceNumberAcknowledgement = SctpTransmissionSequenceNumber.FromReadOnlyMemory(memory[sizeof(uint)..chunkLength]);

        return new(cumulativeTransmissionSequenceNumberAcknowledgement);
    }

    /// <inheritdoc />
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the specified Chunk Type is not equal to <see cref="SctpChunkType.Shutdown" />.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if the specified Chunk Length is not equal to the length of two unsigned 32-bit integers.
    /// </exception>
    public static SctpShutdownAssociation Read(BinaryReader binaryReader) =>
        ISctpChunk<SctpShutdownAssociation>.Read(binaryReader);

    /// <inheritdoc />
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the specified Chunk Type is not equal to <see cref="SctpChunkType.Shutdown" />.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if the specified Chunk Length is not equal to the length of two unsigned 32-bit integers.
    /// </exception>
    public static SctpShutdownAssociation Read(Stream stream) =>
        ISctpChunk<SctpShutdownAssociation>.Read(stream);

    /// <inheritdoc />
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the specified Chunk Type is not equal to <see cref="SctpChunkType.Shutdown" />.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if the specified Chunk Length is not equal to the length of two unsigned 32-bit integers.
    /// </exception>
    public static Task<SctpShutdownAssociation> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        ISctpChunk<SctpShutdownAssociation>.ReadAsync(stream, cancellationToken);

    /// <inheritdoc />
    public ReadOnlyMemory<byte> ToReadOnlyMemory()
    {
        var memory = new Memory<byte>(new byte[ChunkLengthImplicit]);
        this.Write(memory.Span);
        return memory;
    }

    /// <inheritdoc />
    public void Write(Span<byte> span)
    {
        /* Chunk header */
        span[0] = (byte)ChunkTypeImplicit;
        // Skip Flags; SHUTDOWN does not have Chunk Flags.
        BinaryPrimitives.WriteUInt16BigEndian(span[sizeof(ushort)..], ChunkLengthImplicit);

        /* Cumulative TSN Ack */
        this.cumulativeTransmissionSequenceNumberAcknowledgement.Write(span[sizeof(uint)..]);
    }
}
