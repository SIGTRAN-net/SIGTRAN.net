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

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.CookieEcho;

internal readonly partial struct SctpCookieEcho
{
    public static SctpCookieEcho FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;

        /* Chunk header */
        var chunkType = (SctpChunkType)memorySpan[0];
        if (chunkType != ChunkTypeImplicit)
            throw new SctpChunkTypeInvalidException(chunkType);
        // Skip Flags; COOKIE ECHO does not have Chunk Flags.
        var chunkLength = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[sizeof(ushort)..]);
        if (chunkLength < sizeof(uint) + 1)
            throw new SctpChunkLengthInvalidException(chunkLength);

        /* Cookie */
        var cookie = new Memory<byte>(new byte[chunkLength - sizeof(uint)]);
        memory[sizeof(uint)..chunkLength].CopyTo(cookie);

        return new(cookie);
    }

    public static SctpCookieEcho Read(BinaryReader binaryReader) =>
        ISctpChunk<SctpCookieEcho>.Read(binaryReader);

    public static SctpCookieEcho Read(Stream stream) =>
        ISctpChunk<SctpCookieEcho>.Read(stream);

    public static Task<SctpCookieEcho> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        ISctpChunk<SctpCookieEcho>.ReadAsync(stream, cancellationToken);

    public ReadOnlyMemory<byte> ToReadOnlyMemory()
    {
        var memory = new Memory<byte>(new byte[this.chunkLength]);
        this.Write(memory.Span);
        return memory;
    }

    public void Write(Span<byte> span)
    {
        /* Chunk header */
        span[0] = (byte)ChunkTypeImplicit;
        // Skip Flags; COOKIE ECHO does not have Chunk Flags.
        BinaryPrimitives.WriteUInt16BigEndian(span[sizeof(ushort)..], this.chunkLength);

        /* Cookie */
        this.cookie.Span.CopyTo(span[sizeof(uint)..this.chunkLength]);
    }
}
