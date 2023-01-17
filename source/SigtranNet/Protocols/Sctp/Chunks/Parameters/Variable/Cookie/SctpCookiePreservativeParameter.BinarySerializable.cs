/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;
using SigtranNet.Protocols.Sctp.Chunks.Parameters.Exceptions;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Sctp.Chunks.Parameters.Variable.Cookie;

internal readonly partial struct SctpCookiePreservativeParameter
{
    /// <inheritdoc />
    public static SctpCookiePreservativeParameter FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;

        /* Parameter Header */
        var parameterType = (SctpChunkParameterType)BinaryPrimitives.ReadUInt16BigEndian(memorySpan);
        if (parameterType != ParameterTypeImplicit)
            throw new SctpChunkParameterTypeInvalidException(parameterType);
        var parameterLength = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[2..]);
        if (parameterLength != ParameterLengthImplicit)
            throw new SctpChunkParameterLengthInvalidException(parameterType, parameterLength);

        /* Parameter Value */
        var suggestedCookieLifeSpanIncrement = BinaryPrimitives.ReadUInt32BigEndian(memorySpan[4..]);

        return new(suggestedCookieLifeSpanIncrement);
    }

    /// <inheritdoc />
    public static SctpCookiePreservativeParameter Read(BinaryReader binaryReader) =>
        ISctpChunkParameterVariableLength<SctpCookiePreservativeParameter>.Read(binaryReader);

    /// <inheritdoc />
    public static SctpCookiePreservativeParameter Read(Stream stream) =>
        ISctpChunkParameterVariableLength<SctpCookiePreservativeParameter>.Read(stream);

    /// <inheritdoc />
    public static Task<SctpCookiePreservativeParameter> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        ISctpChunkParameterVariableLength<SctpCookiePreservativeParameter>.ReadAsync(stream, cancellationToken);

    /// <inheritdoc />
    public ReadOnlyMemory<byte> ToReadOnlyMemory()
    {
        var memory = new Memory<byte>(new byte[ParameterLengthImplicit]);
        this.Write(memory.Span);
        return memory;
    }

    /// <inheritdoc />
    public void Write(Span<byte> span)
    {
        BinaryPrimitives.WriteUInt16BigEndian(span, (ushort)ParameterTypeImplicit);
        BinaryPrimitives.WriteUInt16BigEndian(span[2..], ParameterLengthImplicit);
        BinaryPrimitives.WriteUInt32BigEndian(span[4..], this.suggestedCookieLifeSpanIncrement);
    }
}
