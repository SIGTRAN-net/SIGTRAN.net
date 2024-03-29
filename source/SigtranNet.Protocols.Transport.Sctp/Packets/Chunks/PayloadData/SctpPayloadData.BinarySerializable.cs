﻿/*
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

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.PayloadData;

internal readonly partial struct SctpPayloadData
{
    /// <inheritdoc />
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if an invalid chunk type is specified.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if an invalid chunk length is specified.
    /// </exception>
    public static SctpPayloadData FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;
        if (memorySpan[0] != (byte)ChunkTypeImplicit)
            throw new SctpChunkTypeInvalidException((SctpChunkType)memorySpan[0]);
        var flags = (SctpPayloadDataFlags)memorySpan[1];
        var length = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[2..]);
        if (length < 1)
            throw new SctpChunkLengthInvalidException(length);
        var transmissionSequenceNumber = BinaryPrimitives.ReadUInt32BigEndian(memorySpan[4..]);
        var streamIdentifier = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[8..]);
        var streamSequenceNumber = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[10..]);
        var payloadProtocolIdentifier = (SctpPayloadProtocolIdentifier)BinaryPrimitives.ReadUInt32BigEndian(memorySpan[12..]);
        var userData = memory[16..length];
        return new(
            flags,
            transmissionSequenceNumber,
            streamIdentifier,
            streamSequenceNumber,
            payloadProtocolIdentifier,
            userData);
    }

    /// <inheritdoc />
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if an invalid chunk type is specified.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if an invalid chunk length is specified.
    /// </exception>
    public static SctpPayloadData Read(BinaryReader binaryReader) =>
        ISctpChunk<SctpPayloadData>.Read(binaryReader);

    /// <inheritdoc />
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if an invalid chunk type is specified.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if an invalid chunk length is specified.
    /// </exception>
    public static SctpPayloadData Read(Stream stream) =>
        ISctpChunk<SctpPayloadData>.Read(stream);

    /// <inheritdoc />
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if an invalid chunk type is specified.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if an invalid chunk length is specified.
    /// </exception>
    public static Task<SctpPayloadData> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        ISctpChunk<SctpPayloadData>.ReadAsync(stream, cancellationToken);

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
        span[0] = (byte)ChunkTypeImplicit;
        span[1] = (byte)chunkFlags;
        BinaryPrimitives.WriteUInt16BigEndian(span[2..], this.chunkLength);
        BinaryPrimitives.WriteUInt32BigEndian(span[4..], this.transmissionSequenceNumber);
        BinaryPrimitives.WriteUInt16BigEndian(span[8..], this.streamIdentifier);
        BinaryPrimitives.WriteUInt16BigEndian(span[10..], this.streamSequenceNumber);
        BinaryPrimitives.WriteUInt32BigEndian(span[12..], (uint)this.payloadProtocolIdentifier);
        this.userData.Span.CopyTo(span[16..]);
    }
}
