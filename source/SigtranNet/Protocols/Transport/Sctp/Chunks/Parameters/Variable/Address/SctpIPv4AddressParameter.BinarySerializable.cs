/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Exceptions;
using System.Buffers.Binary;
using System.Net;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Variable.Address;

internal readonly partial struct SctpIPv4AddressParameter
{
    /// <inheritdoc />
    public static SctpIPv4AddressParameter FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;

        /* Parameter Header */
        var parameterType = (SctpChunkParameterType)BinaryPrimitives.ReadUInt16BigEndian(memorySpan);
        if (parameterType != ParameterTypeImplicit)
            throw new SctpChunkParameterTypeInvalidException(parameterType);
        var parameterLength = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[sizeof(ushort)..]);
        if (parameterLength != ParameterLengthImplicit)
            throw new SctpChunkParameterLengthInvalidException(parameterType, parameterLength);

        /* Parameter Value */
        var ipAddress = new IPAddress(memorySpan[4..8]);

        return new(ipAddress);
    }

    /// <inheritdoc />
    public static SctpIPv4AddressParameter Read(BinaryReader binaryReader) =>
        ISctpChunkParameterVariableLength<SctpIPv4AddressParameter>.Read(binaryReader);

    /// <inheritdoc />
    public static SctpIPv4AddressParameter Read(Stream stream) =>
        ISctpChunkParameterVariableLength<SctpIPv4AddressParameter>.Read(stream);

    /// <inheritdoc />
    public static Task<SctpIPv4AddressParameter> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        ISctpChunkParameterVariableLength<SctpIPv4AddressParameter>.ReadAsync(stream, cancellationToken);

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
        BinaryPrimitives.WriteUInt16BigEndian(span[sizeof(ushort)..], ParameterLengthImplicit);
        this.ipAddress.MapToIPv4().TryWriteBytes(span[sizeof(uint)..], out _);
    }
}
