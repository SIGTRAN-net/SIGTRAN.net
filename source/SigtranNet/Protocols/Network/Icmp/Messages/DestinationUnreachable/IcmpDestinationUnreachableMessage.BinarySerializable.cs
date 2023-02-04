/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Network.Icmp.Messages.DestinationUnreachable;

internal readonly partial struct IcmpDestinationUnreachableMessage
{
    public ReadOnlyMemory<byte> ToReadOnlyMemory()
    {
        var messageLength =
            2 * sizeof(uint)
            + this.header.InternetHeaderLength * sizeof(uint)
            + sizeof(ulong);
        var memory = new Memory<byte>(new byte[messageLength]);
        this.Write(memory.Span);
        return memory;
    }

    public void Write(Span<byte> span)
    {
        span[0] = (byte)IcmpMessageType.DestinationUnreachable;
        span[1] = (byte)this.code;
        // Skip checksum for now in order to calculate it.
        // 2nd unsigned 32-bit word is unused.
        var offset = 2 * sizeof(uint);
        this.headerOriginal.Write(span[offset..]);
        offset += this.headerOriginal.InternetHeaderLength * sizeof(uint);
        this.originalDataDatagramSample.Span.CopyTo(span[offset..]);
        offset += this.originalDataDatagramSample.Length;

        // Calculate the checksum and insert it.
        var checksum = OnesComplementChecksum16Bit.Generate(span);
        BinaryPrimitives.WriteUInt16BigEndian(span[2..], checksum);
    }
}
