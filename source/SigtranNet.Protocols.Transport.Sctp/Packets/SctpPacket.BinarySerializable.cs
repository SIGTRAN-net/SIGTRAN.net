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
using SigtranNet.Protocols.Transport.Sctp.Exceptions;
using SigtranNet.Protocols.Transport.Sctp.Packets.Header;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks;

namespace SigtranNet.Protocols.Transport.Sctp.Packets;

internal readonly partial struct SctpPacket
{
    public static SctpPacket FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var commonHeader = SctpCommonHeader.FromReadOnlyMemory(memory);
        if (!CRC32cChecksum.Validate(memory, 8))
            throw new SctpPacketChecksumInvalidException(commonHeader.checksum);

        var chunks = new List<ISctpChunk>();

        var offset = 3 * sizeof(uint);
        while (offset < memory.Length)
        {
            // TODO exception handling per chunk, aggregated into one exception, if applicable
            var chunk = ISctpChunk.FromReadOnlyMemory(memory[offset..]);
            chunks.Add(chunk);
            offset += chunk.ChunkLength;
        }

        return new(commonHeader, chunks.ToArray());
    }

    public static SctpPacket Read(BinaryReader binaryReader)
    {
        // Presuming that the packet fills the entire base stream.
        var memory = new ReadOnlyMemory<byte>(binaryReader.ReadBytes((int)binaryReader.BaseStream.Length));
        return FromReadOnlyMemory(memory);
    }

    public static SctpPacket Read(Stream stream)
    {
        // Presuming that the packet fills the entire stream.
        var memory = new Memory<byte>(new byte[stream.Length]);
        stream.Read(memory.Span);
        return FromReadOnlyMemory(memory);
    }

    public static async Task<SctpPacket> ReadAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        // Presuming that the packet fills the entire stream.
        var memory = new Memory<byte>(new byte[stream.Length]);
        await stream.ReadAsync(memory, cancellationToken);
        return FromReadOnlyMemory(memory);
    }

    public ReadOnlyMemory<byte> ToReadOnlyMemory()
    {
        // Determine packet length.
        var packetLength = 3 * sizeof(uint);
        var chunksSpan = this.chunks.Span;
        for (var i = 0; i < this.chunks.Length; i++)
        {
            packetLength += chunksSpan[i].ChunkLength;
        }

        // Write result.
        var memory = new Memory<byte>(new byte[packetLength]);
        this.Write(memory.Span);
        return memory;
    }

    public void Write(Span<byte> span)
    {
        this.commonHeader.Write(span);

        var offset = 3 * sizeof(uint);
        var chunksSpan = this.chunks.Span;
        for (var i = 0; i < this.chunks.Length; i++)
        {
            chunksSpan[i].Write(span[offset..]);
            offset += chunksSpan[i].ChunkLength;
        }
    }
}
