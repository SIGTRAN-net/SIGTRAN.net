/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Exceptions;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Variable;

internal readonly partial struct SctpUnrecognizedParameter
{
    /// <inheritdoc />
    public static SctpUnrecognizedParameter FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
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
        var unrecognizedParameter = ISctpChunkParameterVariableLength.FromReadOnlyMemory(memory[sizeof(uint)..]);

        return new(unrecognizedParameter);
    }

    /// <inheritdoc />
    public static SctpUnrecognizedParameter Read(BinaryReader binaryReader) =>
        ISctpChunkParameterVariableLength<SctpUnrecognizedParameter>.Read(binaryReader);

    /// <inheritdoc />
    public static SctpUnrecognizedParameter Read(Stream stream) =>
        ISctpChunkParameterVariableLength<SctpUnrecognizedParameter>.Read(stream);

    /// <inheritdoc />
    public static Task<SctpUnrecognizedParameter> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        ISctpChunkParameterVariableLength<SctpUnrecognizedParameter>.ReadAsync(stream, cancellationToken);

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
        this.unrecognizedParameter.Write(span[sizeof(uint)..]);
    }
}
