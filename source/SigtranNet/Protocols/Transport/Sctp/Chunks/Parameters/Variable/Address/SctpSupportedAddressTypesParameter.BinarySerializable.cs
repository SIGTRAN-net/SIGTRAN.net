/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Exceptions;
using System.Buffers.Binary;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Variable.Address;

internal readonly partial struct SctpSupportedAddressTypesParameter
{
    /// <inheritdoc />
    public static SctpSupportedAddressTypesParameter FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;

        /* Parameter Header */
        var parameterType = (SctpChunkParameterType)BinaryPrimitives.ReadUInt16BigEndian(memorySpan);
        if (parameterType != ParameterTypeImplicit)
            throw new SctpChunkParameterTypeInvalidException(parameterType);
        var parameterLength = BinaryPrimitives.ReadUInt16BigEndian(memorySpan[2..]);

        /* Parameter Value */
        var offset = 4;
        var supportedAddressTypes = new List<SctpChunkParameterType>();
        while (offset < parameterLength)
        {
            var supportedAddressType = (SctpChunkParameterType)BinaryPrimitives.ReadUInt16BigEndian(memorySpan[offset..]);
            supportedAddressTypes.Add(supportedAddressType);
            offset += sizeof(ushort);
        }

        return new(supportedAddressTypes.ToArray());
    }

    /// <inheritdoc />
    public static SctpSupportedAddressTypesParameter Read(BinaryReader binaryReader) =>
        ISctpChunkParameterVariableLength<SctpSupportedAddressTypesParameter>.Read(binaryReader);

    /// <inheritdoc />
    public static SctpSupportedAddressTypesParameter Read(Stream stream) =>
        ISctpChunkParameterVariableLength<SctpSupportedAddressTypesParameter>.Read(stream);

    /// <inheritdoc />
    public static Task<SctpSupportedAddressTypesParameter> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        ISctpChunkParameterVariableLength<SctpSupportedAddressTypesParameter>.ReadAsync(stream, cancellationToken);

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
        var offset = sizeof(uint);
        var supportedAddressTypesSpan = this.supportedAddressTypes.Span;
        for (var i = 0; i < this.supportedAddressTypes.Length; i++)
        {
            BinaryPrimitives.WriteUInt16BigEndian(span[offset..], (ushort)supportedAddressTypesSpan[i]);
            offset += sizeof(ushort);
        }
    }
}
