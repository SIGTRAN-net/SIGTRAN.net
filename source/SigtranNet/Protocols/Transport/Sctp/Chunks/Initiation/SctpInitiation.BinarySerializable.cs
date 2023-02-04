/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks.Exceptions;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Exceptions;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Fixed;
using SigtranNet.Protocols.Transport.Sctp.Chunks;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Variable;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Initiation;

internal readonly partial struct SctpInitiation
{
    /// <inheritdoc />
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the Chunk Type is invalid.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if the Chunk Length is invalid.
    /// </exception>
    public static SctpInitiation FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;

        /* Chunk Header */
        var chunkType = (SctpChunkType)memorySpan[0];
        if (chunkType != ChunkTypeImplicit)
            throw new SctpChunkTypeInvalidException(chunkType);
        // Skip Chunk Flags - INIT does not have Chunk Flags.
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
            var variableLengthParameterType = (SctpChunkParameterType)BinaryPrimitives.ReadUInt16BigEndian(memorySpan[offset..]);
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
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the Chunk Type is invalid.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if the Chunk Length is invalid.
    /// </exception>
    public static SctpInitiation Read(BinaryReader binaryReader) =>
        ISctpChunk<SctpInitiation>.Read(binaryReader);

    /// <inheritdoc />
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the Chunk Type is invalid.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if the Chunk Length is invalid.
    /// </exception>
    public static SctpInitiation Read(Stream stream) =>
        ISctpChunk<SctpInitiation>.Read(stream);

    /// <inheritdoc />
    /// <exception cref="SctpChunkTypeInvalidException">
    /// An <see cref="SctpChunkTypeInvalidException" /> is thrown if the Chunk Type is invalid.
    /// </exception>
    /// <exception cref="SctpChunkLengthInvalidException">
    /// An <see cref="SctpChunkLengthInvalidException" /> is thrown if the Chunk Length is invalid.
    /// </exception>
    public static Task<SctpInitiation> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        ISctpChunk<SctpInitiation>.ReadAsync(stream, cancellationToken);

    /// <inheritdoc />
    public ReadOnlyMemory<byte> ToReadOnlyMemory()
    {
        // Add padding if necessary.
        var memory = new Memory<byte>(new byte[this.chunkLength + this.chunkLength % sizeof(uint)]);
        this.Write(memory.Span);
        return memory;
    }

    public void Write(Span<byte> span)
    {
        var offset = 0;

        /* Chunk Header */
        span[offset] = (byte)ChunkTypeImplicit;
        // Skip Chunk Flags - INIT does not have Chunk Flags.
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
