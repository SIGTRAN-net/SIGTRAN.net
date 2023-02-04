/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Exceptions;
using System.Buffers.Binary;
using System.Net;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Variable.Address;

internal readonly partial struct SctpIPv6AddressParameter
{
    /// <inheritdoc />
    public static SctpIPv6AddressParameter FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
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
        var ipAddress = new IPAddress(memorySpan[4..parameterLength]);

        return new(ipAddress);
    }

    /// <inheritdoc />
    public static SctpIPv6AddressParameter Read(BinaryReader binaryReader) =>
        ISctpChunkParameterVariableLength<SctpIPv6AddressParameter>.Read(binaryReader);

    /// <inheritdoc />
    public static SctpIPv6AddressParameter Read(Stream stream) =>
        ISctpChunkParameterVariableLength<SctpIPv6AddressParameter>.Read(stream);

    /// <inheritdoc />
    public static Task<SctpIPv6AddressParameter> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        ISctpChunkParameterVariableLength<SctpIPv6AddressParameter>.ReadAsync(stream, cancellationToken);

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
        this.ipAddress.MapToIPv6().TryWriteBytes(span[4..], out _);
    }
}
