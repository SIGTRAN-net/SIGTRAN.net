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
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.ShutdownAcknowledgement;

internal readonly partial struct SctpShutdownAcknowledgement
{
    /// <inheritdoc />
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the specified Chunk Type is not equal to <see cref="SctpChunkType.ShutdownAcknowledgement" />.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if the specified Chunk Length is not equal to an unsigned 32-bit integer.
    /// </exception>
    public static SctpShutdownAcknowledgement FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;

        /* Chunk header */
        var chunkType = (SctpChunkType)memorySpan[0];
        if (chunkType != ChunkTypeImplicit)
            throw new SctpChunkTypeInvalidException(chunkType);
        // Skip Flags; SHUTDOWN ACK does not have Chunk Flags.
        var chunkLength = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[sizeof(ushort)..]);
        if (chunkLength != ChunkLengthImplicit)
            throw new SctpChunkLengthInvalidException(chunkLength);

        return new SctpShutdownAcknowledgement();
    }

    /// <inheritdoc />
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the specified Chunk Type is not equal to <see cref="SctpChunkType.ShutdownAcknowledgement" />.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if the specified Chunk Length is not equal to an unsigned 32-bit integer.
    /// </exception>
    public static SctpShutdownAcknowledgement Read(BinaryReader binaryReader) =>
        ISctpChunk<SctpShutdownAcknowledgement>.Read(binaryReader);

    /// <inheritdoc />
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the specified Chunk Type is not equal to <see cref="SctpChunkType.ShutdownAcknowledgement" />.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if the specified Chunk Length is not equal to an unsigned 32-bit integer.
    /// </exception>
    public static SctpShutdownAcknowledgement Read(Stream stream) =>
        ISctpChunk<SctpShutdownAcknowledgement>.Read(stream);

    /// <inheritdoc />
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the specified Chunk Type is not equal to <see cref="SctpChunkType.ShutdownAcknowledgement" />.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if the specified Chunk Length is not equal to an unsigned 32-bit integer.
    /// </exception>
    public static Task<SctpShutdownAcknowledgement> ReadAsync(Stream stream, CancellationToken cancellationToken) =>
        ISctpChunk<SctpShutdownAcknowledgement>.ReadAsync(stream, cancellationToken);

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
        span[0] = (byte)ChunkTypeImplicit;
        // Skip Flags; SHUTDOWN ACK does not have Chunk Flags.
        BinaryPrimitives.WriteUInt16BigEndian(span[sizeof(ushort)..], ChunkLengthImplicit);
    }
}
