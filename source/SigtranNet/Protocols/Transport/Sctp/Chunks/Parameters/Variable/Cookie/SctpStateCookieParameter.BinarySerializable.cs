/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Exceptions;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Variable.Cookie;

internal readonly partial struct SctpStateCookieParameter
{
    /// <inheritdoc />
    public static SctpStateCookieParameter FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;

        /* Parameter Header */
        var parameterType = (SctpChunkParameterType)BinaryPrimitives.ReadUInt16BigEndian(memorySpan);
        if (parameterType != ParameterTypeImplicit)
            throw new SctpChunkParameterTypeInvalidException(parameterType);
        var parameterLength = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[2..]);
        if (parameterLength < 1)
            throw new SctpChunkParameterLengthInvalidException(parameterType, parameterLength);

        /* Parameter Value */
        var stateCookie = memory[4..parameterLength];

        return new(stateCookie);
    }

    /// <inheritdoc />
    public static SctpStateCookieParameter Read(BinaryReader binaryReader) =>
        ISctpChunkParameterVariableLength<SctpStateCookieParameter>.Read(binaryReader);

    /// <inheritdoc />
    public static SctpStateCookieParameter Read(Stream stream) =>
        ISctpChunkParameterVariableLength<SctpStateCookieParameter>.Read(stream);

    /// <inheritdoc />
    public static Task<SctpStateCookieParameter> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        ISctpChunkParameterVariableLength<SctpStateCookieParameter>.ReadAsync(stream, cancellationToken);

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
        BinaryPrimitives.WriteUInt16BigEndian(span, (ushort)ParameterTypeImplicit);
        BinaryPrimitives.WriteUInt16BigEndian(span[2..], parameterLength);
        this.stateCookie.Span.CopyTo(span[4..]);
    }
}
