/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Sctp.Chunks.Parameters.Exceptions;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Sctp.Chunks.Parameters.Variable;

internal readonly partial struct SctpHeartbeatInfoParameter
{
    /// <inheritdoc />
    public static SctpHeartbeatInfoParameter FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;

        /* Parameter Header */
        var parameterType = (SctpChunkParameterType)BinaryPrimitives.ReadUInt16BigEndian(memorySpan);
        if (parameterType != ParameterTypeImplicit)
            throw new SctpChunkParameterTypeInvalidException(parameterType);
        var parameterLength = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[sizeof(ushort)..]);
        if (parameterLength < 1)
            throw new SctpChunkParameterLengthInvalidException(parameterType, parameterLength);

        /* Parameter Value */
        var heartbeatInformation = memory[sizeof(uint)..parameterLength];

        return new(heartbeatInformation);
    }

    /// <inheritdoc />
    public static SctpHeartbeatInfoParameter Read(BinaryReader binaryReader) =>
        ISctpChunkParameterVariableLength<SctpHeartbeatInfoParameter>.Read(binaryReader);

    /// <inheritdoc />
    public static SctpHeartbeatInfoParameter Read(Stream stream) =>
        ISctpChunkParameterVariableLength<SctpHeartbeatInfoParameter>.Read(stream);

    /// <inheritdoc />
    public static Task<SctpHeartbeatInfoParameter> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        ISctpChunkParameterVariableLength<SctpHeartbeatInfoParameter>.ReadAsync(stream, cancellationToken);

    /// <inheritdoc />
    public ReadOnlyMemory<byte> ToReadOnlyMemory()
    {
        var memory = new Memory<byte>(new byte[this.parameterLength]);
        this.Write(memory.Span);
        return memory;
    }

    /// <inheritdoc />
    public void Write(Span<byte> span)
    {
        /* Parameter Header */
        BinaryPrimitives.WriteUInt16BigEndian(span, (ushort)ParameterTypeImplicit);
        BinaryPrimitives.WriteUInt16BigEndian(span[sizeof(ushort)..], this.parameterLength);

        /* Parameter Value */
        this.heartbeatInformation.Span.CopyTo(span[sizeof(uint)..]);
    }
}
