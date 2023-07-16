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

using System.Buffers.Binary;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Variable;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Exceptions;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Exceptions;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Fixed;

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.InitiationAcknowledgement;

internal readonly partial struct SctpInitiationAcknowledgement
{
    /// <inheritdoc />
    public static SctpInitiationAcknowledgement FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;

        /* Chunk Header */
        var chunkType = (SctpChunkType)memorySpan[0];
        if (chunkType != ChunkTypeImplicit)
            throw new SctpChunkTypeInvalidException(chunkType);
        // Skip Chunk Flags - INIT ACK does not have Chunk Flags.
        var chunkLength = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[2..]);
        if (chunkLength < ChunkLengthMinimum)
            throw new SctpChunkLengthInvalidException(chunkLength);

        /* Fixed Length Parameters */
        var initiateTag = SctpInitiateTag.FromReadOnlyMemory(memory[4..]);
        var advertisedReceiverWindowCredit = SctpAdvertisedReceiverWindowCredit.FromReadOnlyMemory(memory[8..]);
        var numberOutboundStreams = SctpNumberOutboundStreams.FromReadOnlyMemory(memory[12..]);
        var numberInboundStreams = SctpNumberInboundStreams.FromReadOnlyMemory(memory[14..]);
        var initialTransmissionSequenceNumber = SctpTransmissionSequenceNumber.FromReadOnlyMemory(memory[16..]);

        /* Optional/Variable-Length Parameters */
        var offset = 20;
        var variableLengthParameters = new List<ISctpChunkParameterVariableLength>();
        while (offset < chunkLength)
        {
            var variableLengthParameterType = (SctpChunkParameterType)BinaryPrimitives.ReadInt16BigEndian(memorySpan[offset..]);
            if (!SupportedParameterTypes.Contains(variableLengthParameterType))
                throw new SctpChunkParameterTypeInvalidException(variableLengthParameterType);
            var variableLengthParameter = ISctpChunkParameterVariableLength.FromReadOnlyMemory(memory[offset..]);
            variableLengthParameters.Add(variableLengthParameter);
            offset += variableLengthParameter.ParameterLength;
        }

        return new(
            initiateTag,
            advertisedReceiverWindowCredit,
            numberOutboundStreams,
            numberInboundStreams,
            initialTransmissionSequenceNumber,
            variableLengthParameters.ToArray());
    }

    /// <inheritdoc />
    public static SctpInitiationAcknowledgement Read(BinaryReader binaryReader) =>
        ISctpChunk<SctpInitiationAcknowledgement>.Read(binaryReader);

    /// <inheritdoc />
    public static SctpInitiationAcknowledgement Read(Stream stream) =>
        ISctpChunk<SctpInitiationAcknowledgement>.Read(stream);

    /// <inheritdoc />
    public static Task<SctpInitiationAcknowledgement> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        ISctpChunk<SctpInitiationAcknowledgement>.ReadAsync(stream, cancellationToken);

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
        var offset = 0;

        /* Chunk Header */
        span[offset] = (byte)ChunkTypeImplicit;
        // Skip Chunk Flags - INIT ACK does not have Chunk Flags.
        offset += 2 * sizeof(byte);
        BinaryPrimitives.WriteUInt16BigEndian(span[offset..], this.chunkLength);
        offset += sizeof(ushort);

        /* Chunk Value */
        this.initiateTag.Write(span[offset..]);
        offset += sizeof(uint);
        this.advertisedReceiverWindowCredit.Write(span[offset..]);
        offset += sizeof(uint);
        this.numberOutboundStreams.Write(span[offset..]);
        offset += sizeof(ushort);
        this.numberInboundStreams.Write(span[offset..]);
        offset += sizeof(ushort);
        this.initialTransmissionSequenceNumber.Write(span[offset..]);
        offset += sizeof(uint);

        var variableLengthParametersSpan = this.variableLengthParameters.Span;
        for (var i = 0; i < variableLengthParametersSpan.Length; i++)
        {
            variableLengthParametersSpan[i].Write(span[offset..]);
            offset += variableLengthParametersSpan[i].ParameterLength;
        }
    }
}
