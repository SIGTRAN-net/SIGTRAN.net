/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Exceptions;
using System.Buffers.Binary;
using System.Text;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Variable.Address;

internal readonly partial struct SctpHostNameAddressParameter
{
    /// <inheritdoc />
    public static SctpHostNameAddressParameter FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
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
        var hostName = Encoding.ASCII.GetString(memorySpan[4..parameterLength]);
        if (hostName[^1] != '\0')
            throw new SctpHostNameAddressInvalidException();
        hostName = hostName.TrimEnd('\0');
        if (hostName.Contains('\0'))
            throw new SctpHostNameAddressInvalidException();

        return new(hostName);
    }

    /// <inheritdoc />
    public static SctpHostNameAddressParameter Read(BinaryReader binaryReader) =>
        ISctpChunkParameterVariableLength<SctpHostNameAddressParameter>.Read(binaryReader);

    /// <inheritdoc />
    public static SctpHostNameAddressParameter Read(Stream stream) =>
        ISctpChunkParameterVariableLength<SctpHostNameAddressParameter>.Read(stream);

    /// <inheritdoc />
    public static Task<SctpHostNameAddressParameter> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
        ISctpChunkParameterVariableLength<SctpHostNameAddressParameter>.ReadAsync(stream, cancellationToken);

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
        BinaryPrimitives.WriteUInt16BigEndian(span[sizeof(ushort)..], this.parameterLength);
        new ReadOnlyMemory<byte>(Encoding.ASCII.GetBytes(this.hostName + '\0')).Span.CopyTo(span[sizeof(uint)..]);
    }
}
