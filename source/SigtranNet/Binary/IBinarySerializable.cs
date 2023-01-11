/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Binary;

/// <summary>
/// A data structure that may be serialized to binary data.
/// </summary>
public interface IBinarySerializable
{
    /// <summary>
    /// Returns the data as a <see cref="ReadOnlyMemory{T}" />.
    /// </summary>
    /// <returns>The data as a <see cref="ReadOnlyMemory{T}" />.</returns>
    ReadOnlyMemory<byte> ToReadOnlyMemory();

    /// <summary>
    /// Writes the data to the stream of <paramref name="writer" />.
    /// </summary>
    /// <param name="writer">The binary writer.</param>
    void Write(BinaryWriter writer);

    /// <summary>
    /// Writes the data to <paramref name="stream" />.
    /// </summary>
    /// <param name="stream">The stream.</param>
    void Write(Stream stream);

    /// <summary>
    /// Writes the data to <paramref name="stream" />.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An awaitable task.</returns>
    ValueTask WriteAsync(Stream stream, CancellationToken cancellationToken = default);
}

/// <summary>
/// A data structure that may be serialized and deserialized to and from binary data.
/// </summary>
/// <typeparam name="TData">The type of the data structure.</typeparam>
public interface IBinarySerializable<TData> : IBinarySerializable
    where TData : IBinarySerializable<TData>
{
    /// <summary>
    /// Deserializes <typeparamref name="TData" /> from <paramref name="memory" />.
    /// </summary>
    /// <param name="memory">The memory that contains a serialization of <typeparamref name="TData" />.</param>
    /// <returns>The deserialized <typeparamref name="TData" />.</returns>
    static abstract TData FromReadOnlyMemory(ReadOnlyMemory<byte> memory);

    /// <summary>
    /// Reads the <typeparamref name="TData" /> from the stream of <paramref name="reader" />.
    /// </summary>
    /// <param name="reader">The binary reader.</param>
    /// <returns>The deserialized <typeparamref name="TData" />.</returns>
    static abstract TData Read(BinaryReader reader);

    /// <summary>
    /// Reads the <typeparamref name="TData" /> from <paramref name="stream" />.
    /// </summary>
    /// <param name="stream">The stream to read from.</param>
    /// <returns>The deserialized <typeparamref name="TData" />.</returns>
    static abstract TData Read(Stream stream);

    /// <summary>
    /// Reads the <typeparamref name="TData" /> from <paramref name="stream" />.
    /// </summary>
    /// <param name="stream">The stream to read from.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The deserialized <typeparamref name="TData" />.</returns>
    static abstract Task<TData> ReadAsync(Stream stream, CancellationToken cancellationToken = default);
}