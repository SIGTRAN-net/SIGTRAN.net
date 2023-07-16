/*
 * This file is part of SIGTRAN.net.
 * 
 * SIGTRAN.net is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, 
 * either version 3 of the License, or (at your option) any later version.
 * 
 * SIGTRAN.net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. 
 * See the GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License along with SIGTRAN.net. If not, see <https://www.gnu.org/licenses/>.
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
    /// <param name="writer">The binary writer that will write the data.</param>
    virtual void Write(BinaryWriter writer) =>
        writer.Write(this.ToReadOnlyMemory().Span);

    /// <summary>
    /// Writes the data to <paramref name="stream" />.
    /// </summary>
    /// <param name="stream">The stream that will contain the data.</param>
    virtual void Write(Stream stream) =>
        stream.Write(this.ToReadOnlyMemory().Span);

    /// <summary>
    /// Writes the data to <paramref name="span" />.
    /// </summary>
    /// <param name="span">The span that will contain the data.</param>
    void Write(Span<byte> span);

    /// <summary>
    /// Writes the data to <paramref name="stream" />.
    /// </summary>
    /// <param name="stream">The stream that will contain the data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An awaitable task.</returns>
    virtual ValueTask WriteAsync(Stream stream, CancellationToken cancellationToken = default) =>
        stream.WriteAsync(this.ToReadOnlyMemory(), cancellationToken);

    /// <summary>
    /// Writes the data to <paramref name="memory"/>
    /// </summary>
    /// <param name="memory">The memory that will contain the data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An awaitable task.</returns>
    virtual Task WriteAsync(Memory<byte> memory, CancellationToken cancellationToken = default)
    {
        var me = this;
        return Task.Run(() => me.Write(memory.Span), cancellationToken);
    }
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
    /// Reads the <typeparamref name="TData" /> from the stream of <paramref name="binaryReader" />.
    /// </summary>
    /// <param name="binaryReader">The binary reader.</param>
    /// <returns>The deserialized <typeparamref name="TData" />.</returns>
    static abstract TData Read(BinaryReader binaryReader);

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