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
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.ErrorCause;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.OperationError;

internal readonly partial struct SctpOperationError
{
    /// <inheritdoc />
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the specified Chunk Type is not equal to <see cref="SctpChunkType.OperationError" />.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if there is not at least one Error Cause in the ERROR chunk.
    /// </exception>
    public static SctpOperationError FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;

        /* Chunk header */
        var chunkType = (SctpChunkType)memorySpan[0];
        if (chunkType != ChunkTypeImplicit)
            throw new SctpChunkTypeInvalidException(chunkType);
        // Skip Flags; ERROR does not have Chunk Flags.
        var chunkLength = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[sizeof(ushort)..]);

        /* Error Causes */
        var errorCauses = new List<ISctpErrorCause>();
        var offset = sizeof(uint);
        while (offset < chunkLength)
        {
            var errorCause = ISctpErrorCause.FromReadOnlyMemory(memory[offset..]);
            offset += errorCause.ErrorCauseLength;
            errorCauses.Add(errorCause);
        }

        return new SctpOperationError(errorCauses.ToArray());
    }

    /// <inheritdoc />
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the specified Chunk Type is not equal to <see cref="SctpChunkType.OperationError" />.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if there is not at least one Error Cause in the ERROR chunk.
    /// </exception>
    public static SctpOperationError Read(BinaryReader binaryReader) =>
        ISctpChunk<SctpOperationError>.Read(binaryReader);

    /// <inheritdoc />
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the specified Chunk Type is not equal to <see cref="SctpChunkType.OperationError" />.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if there is not at least one Error Cause in the ERROR chunk.
    /// </exception>
    public static SctpOperationError Read(Stream stream) =>
        ISctpChunk<SctpOperationError>.Read(stream);

    /// <inheritdoc />
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the specified Chunk Type is not equal to <see cref="SctpChunkType.OperationError" />.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if there is not at least one Error Cause in the ERROR chunk.
    /// </exception>
    public static Task<SctpOperationError> ReadAsync(Stream stream, CancellationToken cancellationToken) =>
        ISctpChunk<SctpOperationError>.ReadAsync(stream, cancellationToken);

    /// <inheritdoc />
    public ReadOnlyMemory<byte> ToReadOnlyMemory()
    {
        var memory = new Memory<byte>(new byte[this.chunkLength]);
        this.Write(memory.Span);
        return memory;
    }

    /// <inheritdoc />
    public void Write(Span<byte> span)
    {
        /* Chunk header */
        span[0] = (byte)ChunkTypeImplicit;
        // Skip Flags; ERROR does not have Chunk Flags.
        BinaryPrimitives.WriteUInt16BigEndian(span[sizeof(ushort)..], this.chunkLength);

        /* Error Causes */
        var offset = sizeof(uint);
        var errorCausesSpan = this.errorCauses.Span;
        for (var i = 0; i < errorCauses.Length; i++)
        {
            errorCausesSpan[i].Write(span[offset..]);
            offset += errorCausesSpan[i].ErrorCauseLength;
        }
    }
}
