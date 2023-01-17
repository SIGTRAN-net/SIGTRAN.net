/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;
using SigtranNet.Protocols.Sctp.Chunks.Parameters.Exceptions;
using SigtranNet.Protocols.Sctp.Chunks.Parameters.Variable.Address;
using SigtranNet.Protocols.Sctp.Chunks.Parameters.Variable.Cookie;
using System.Buffers.Binary;
using System.Runtime.InteropServices;

namespace SigtranNet.Protocols.Sctp.Chunks.Parameters.Variable;

/// <summary>
/// An SCTP Chunk Parameter which is optional or has a variable length.
/// </summary>
internal interface ISctpChunkParameterVariableLength : ISctpChunkParameter
{
    private static readonly Dictionary<SctpChunkParameterType, Func<ReadOnlyMemory<byte>, ISctpChunkParameterVariableLength>> Deserializers =
        new()
        {
            { SctpChunkParameterType.IPv4Address, memory => SctpIPv4AddressParameter.FromReadOnlyMemory(memory) },
            { SctpChunkParameterType.IPv6Address, memory => SctpIPv6AddressParameter.FromReadOnlyMemory(memory) },
            { SctpChunkParameterType.CookiePreservative, memory => SctpCookiePreservativeParameter.FromReadOnlyMemory(memory) },
            { SctpChunkParameterType.HostNameAddress, memory => SctpHostNameAddressParameter.FromReadOnlyMemory(memory) },
            { SctpChunkParameterType.SupportedAddressTypes, memory => SctpSupportedAddressTypesParameter.FromReadOnlyMemory(memory) },
            { SctpChunkParameterType.StateCookie, memory => SctpStateCookieParameter.FromReadOnlyMemory(memory) },
            { SctpChunkParameterType.UnrecognizedParameter, memory => SctpUnrecognizedParameter.FromReadOnlyMemory(memory) },
            { SctpChunkParameterType.HeartbeatInfo, memory => SctpHeartbeatInfoParameter.FromReadOnlyMemory(memory) },
        };

    /// <summary>
    /// Gets the type of the SCTP chunk parameter.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         The Type field is a 16-bit identifier of the type of parameter. It takes a value of 0 to 65534.
    ///
    ///             The value of 65535 is reserved for IETF-defined extensions. Values other than those defined in specific SCTP chunk descriptions are reserved for use by IETF.
    ///     </code>
    /// </remarks>
    SctpChunkParameterType ParameterType { get; }

    /// <summary>
    /// Deserializes a variable-length SCTP Chunk Parameter from <paramref name="memory" />.
    /// </summary>
    /// <param name="memory">The memory to deserialize a variable-length SCTP Chunk Parameter from.</param>
    /// <returns>The deserialized variable-length SCTP Chunk Parameter.</returns>
    /// <exception cref="SctpChunkParameterTypeInvalidException">
    /// An <see cref="SctpChunkParameterTypeInvalidException" /> is thrown if the Chunk Parameter Type is invalid or unsupported.
    /// </exception>
    /// <exception cref="SctpChunkParameterLengthInvalidException">
    /// An <see cref="SctpChunkParameterLengthInvalidException" /> is thrown if the Chunk Parameter Length is invalid for the specified Chunk Parameter Type.
    /// </exception>
    static ISctpChunkParameterVariableLength FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;
        var chunkParameterType = (SctpChunkParameterType)BinaryPrimitives.ReadUInt16BigEndian(memorySpan);
        ref var deserializer = ref CollectionsMarshal.GetValueRefOrNullRef(Deserializers, chunkParameterType);
        if (deserializer is null)
            throw new SctpChunkParameterTypeInvalidException(chunkParameterType);
        return deserializer(memory);
    }
}

/// <summary>
/// An SCTP Chunk Parameter which is optional or has a variable length.
/// </summary>
/// <typeparam name="TParameter">
/// The type of Chunk Parameter.
/// </typeparam>
internal interface ISctpChunkParameterVariableLength<TParameter> : ISctpChunkParameterVariableLength, IBinarySerializable<TParameter>
    where TParameter : ISctpChunkParameterVariableLength<TParameter>
{
    /// <summary>
    /// Reads an SCTP Chunk Parameter <typeparamref name="TParameter" /> from <paramref name="binaryReader" />.
    /// </summary>
    /// <param name="binaryReader">The binary reader that reads the serialized <typeparamref name="TParameter" />.</param>
    /// <returns>The deserialized <typeparamref name="TParameter" />.</returns>
    new static TParameter Read(BinaryReader binaryReader)
    {
        /* Parameter header */
        var parameterHeader = new ReadOnlyMemory<byte>(binaryReader.ReadBytes(sizeof(uint)));
        var parameterHeaderSpan = parameterHeader.Span;
        var parameterLength = BinaryPrimitives.ReadUInt16BigEndian(parameterHeaderSpan[sizeof(ushort)..]);

        /* Parameter value */
        var parameterValue = new ReadOnlyMemory<byte>(binaryReader.ReadBytes(parameterLength - sizeof(uint)));

        /* Result */
        var memory = new Memory<byte>(new byte[parameterLength]);
        parameterHeader.CopyTo(memory);
        parameterValue.CopyTo(memory[sizeof(uint)..]);
        return TParameter.FromReadOnlyMemory(memory);
    }

    /// <summary>
    /// Reads an SCTP Chunk Parameter <typeparamref name="TParameter" /> from <paramref name="stream" />.
    /// </summary>
    /// <param name="stream">The stream that contains the serialized <typeparamref name="TParameter" />.</param>
    /// <returns>The deserialized <typeparamref name="TParameter" />.</returns>
    new static TParameter Read(Stream stream)
    {
        /* Parameter header */
        var parameterHeader = new Memory<byte>(new byte[sizeof(uint)]);
        var parameterHeaderSpan = parameterHeader.Span;
        stream.Read(parameterHeaderSpan);
        var parameterLength = BinaryPrimitives.ReadUInt16BigEndian(parameterHeaderSpan[sizeof(ushort)..]);

        /* Result */
        var memory = new Memory<byte>(new byte[parameterLength]);
        parameterHeader.CopyTo(memory);
        stream.Read(memory.Span[sizeof(uint)..parameterLength]);
        return TParameter.FromReadOnlyMemory(memory);
    }

    /// <summary>
    /// Reads an SCTP Chunk Parameter <typeparamref name="TParameter" /> from <paramref name="stream" />.
    /// </summary>
    /// <param name="stream">The stream that contains the serialized <typeparamref name="TParameter" />.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The deserialized <typeparamref name="TParameter" />.</returns>
    new static async Task<TParameter> ReadAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        /* Parameter header */
        var parameterHeader = new Memory<byte>(new byte[sizeof(uint)]);
        await stream.ReadAsync(parameterHeader, cancellationToken);
        var parameterLength = BinaryPrimitives.ReadUInt16BigEndian(parameterHeader.Span[sizeof(ushort)..]);

        /* Result */
        var memory = new Memory<byte>(new byte[parameterLength]);
        parameterHeader.CopyTo(memory);
        await stream.ReadAsync(memory[sizeof(uint)..parameterLength], cancellationToken);
        return TParameter.FromReadOnlyMemory(memory);
    }
}