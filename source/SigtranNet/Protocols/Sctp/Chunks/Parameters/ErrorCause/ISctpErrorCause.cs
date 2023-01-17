/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;
using SigtranNet.Protocols.Sctp.Chunks.Parameters.ErrorCause.Exceptions;
using System.Buffers.Binary;
using System.Runtime.InteropServices;

namespace SigtranNet.Protocols.Sctp.Chunks.Parameters.ErrorCause;

/// <summary>
/// An Error Cause in an SCTP Packet Chunk.
/// </summary>
internal interface ISctpErrorCause : IBinarySerializable
{
    private static readonly Dictionary<SctpErrorCauseCode, Func<ReadOnlyMemory<byte>, ISctpErrorCause>> Deserializers =
        new()
        {
            { SctpErrorCauseCode.InvalidStreamIdentifier, memory => SctpInvalidStreamIdentifierError.FromReadOnlyMemory(memory) },
            { SctpErrorCauseCode.MissingMandatoryParameter, memory => SctpMissingMandatoryParameterError.FromReadOnlyMemory(memory) },
            { SctpErrorCauseCode.StaleCookie, memory => SctpStaleCookieError.FromReadOnlyMemory(memory) },
            { SctpErrorCauseCode.OutOfResource, memory => SctpOutOfResourceError.FromReadOnlyMemory(memory) },
            { SctpErrorCauseCode.UnresolvableAddress, memory => SctpUnresolvableAddressError.FromReadOnlyMemory(memory) },
            { SctpErrorCauseCode.UnrecognizedChunkType, memory => SctpUnrecognizedChunkTypeError.FromReadOnlyMemory(memory) },
            { SctpErrorCauseCode.InvalidMandatoryParameter, memory => SctpInvalidMandatoryParameterError.FromReadOnlyMemory(memory) },
            { SctpErrorCauseCode.UnrecognizedParameters, memory => SctpUnrecognizedParametersError.FromReadOnlyMemory(memory) },
            { SctpErrorCauseCode.NoUserData, memory => SctpNoUserDataError.FromReadOnlyMemory(memory) },
            { SctpErrorCauseCode.CookieReceivedWhileShuttingDown, memory => SctpCookieReceivedWhileShuttingDownError.FromReadOnlyMemory(memory) },
            { SctpErrorCauseCode.RestartAssociationWithNewAddresses, memory => SctpRestartAssociationWithNewAddressesError.FromReadOnlyMemory(memory) },
            { SctpErrorCauseCode.UserInitiatedAbort, memory => SctpUserInitiatedAbortError.FromReadOnlyMemory(memory) },
        };

    /// <summary>
    /// Gets the Error Cause Code.
    /// </summary>
    internal SctpErrorCauseCode ErrorCauseCode { get; }

    /// <summary>
    /// Gets the Error Cause Length.
    /// </summary>
    internal ushort ErrorCauseLength { get; }

    /// <summary>
    /// Deserializes an Error Cause parameter from <paramref name="memory" />.
    /// </summary>
    /// <param name="memory">The memory that contains a serialized Error Cause parameter.</param>
    /// <returns>The deserialized Error Cause parameter.</returns>
    /// <exception cref="SctpErrorCauseCodeInvalidException">
    /// An <see cref="SctpErrorCauseCodeInvalidException" /> is thrown if the specified Error Cause code is invalid or unsupported.
    /// </exception>
    static ISctpErrorCause FromReadOnlyMemory(ReadOnlyMemory<byte> memory)
    {
        var memorySpan = memory.Span;
        var errorCauseCode = (SctpErrorCauseCode)BinaryPrimitives.ReadUInt16BigEndian(memorySpan);
        ref var deserializer = ref CollectionsMarshal.GetValueRefOrNullRef(Deserializers, errorCauseCode);
        if (deserializer is null)
            throw new SctpErrorCauseCodeInvalidException(errorCauseCode);
        return deserializer(memory);
    }
}

internal interface ISctpErrorCauseParameter<TErrorCause> : ISctpErrorCause, IBinarySerializable<TErrorCause>
    where TErrorCause : ISctpErrorCauseParameter<TErrorCause>
{
    /// <summary>
    /// Reads an SCTP Error Cause <typeparamref name="TErrorCause" /> from <paramref name="binaryReader" />.
    /// </summary>
    /// <param name="binaryReader">The binary reader that reads a serialized <typeparamref name="TErrorCause" />.</param>
    /// <returns>The deserialized <typeparamref name="TErrorCause" />.</returns>
    new static TErrorCause Read(BinaryReader binaryReader)
    {
        /* Error Cause header */
        var errorCauseHeader = new ReadOnlyMemory<byte>(binaryReader.ReadBytes(sizeof(uint)));
        var errorCauseLength = BinaryPrimitives.ReadUInt16BigEndian(errorCauseHeader.Span[sizeof(ushort)..]);

        /* Error Cause body */
        var errorCauseBody = new ReadOnlyMemory<byte>(binaryReader.ReadBytes(errorCauseLength - sizeof(uint)));

        /* Result */
        var memory = new Memory<byte>(new byte[errorCauseLength]);
        errorCauseHeader.CopyTo(memory);
        errorCauseBody.CopyTo(memory[sizeof(ushort)..]);
        return TErrorCause.FromReadOnlyMemory(memory);
    }

    /// <summary>
    /// Reads an SCTP Error Cause <typeparamref name="TErrorCause" /> from <paramref name="stream" />.
    /// </summary>
    /// <param name="stream">The stream that contains a serialized <typeparamref name="TErrorCause" />.</param>
    /// <returns>The deserialized <typeparamref name="TErrorCause" />.</returns>
    new static TErrorCause Read(Stream stream)
    {
        /* Error Cause header */
        var errorCauseHeader = new Memory<byte>(new byte[sizeof(uint)]);
        var errorCauseHeaderSpan = errorCauseHeader.Span;
        stream.Read(errorCauseHeaderSpan);
        var errorCauseLength = BinaryPrimitives.ReadUInt16BigEndian(errorCauseHeaderSpan[sizeof(ushort)..]);

        /* Result */
        var memory = new Memory<byte>(new byte[errorCauseLength]);
        errorCauseHeader.CopyTo(memory);
        stream.Read(memory.Span[sizeof(uint)..]);
        return TErrorCause.FromReadOnlyMemory(memory);
    }

    /// <summary>
    /// Reads an SCTP Error Cause <typeparamref name="TErrorCause" /> from <paramref name="stream" />.
    /// </summary>
    /// <param name="stream">The stream that contains a serialized <typeparamref name="TErrorCause" />.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The deserialized <typeparamref name="TErrorCause" />.</returns>
    new static async Task<TErrorCause> ReadAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        /* Error Cause header */
        var errorCauseHeader = new Memory<byte>(new byte[sizeof(uint)]);
        await stream.ReadAsync(errorCauseHeader, cancellationToken);
        var errorCauseLength = BinaryPrimitives.ReadUInt16BigEndian(errorCauseHeader.Span[sizeof(ushort)..]);

        /* Result */
        var memory = new Memory<byte>(new byte[errorCauseLength]);
        errorCauseHeader.CopyTo(memory);
        await stream.ReadAsync(memory[sizeof(uint)..], cancellationToken);
        return TErrorCause.FromReadOnlyMemory(memory);
    }
}
