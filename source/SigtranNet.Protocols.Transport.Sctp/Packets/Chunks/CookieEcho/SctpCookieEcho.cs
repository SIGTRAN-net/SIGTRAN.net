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

using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks;
using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters;

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.CookieEcho;

/// <summary>
/// A Cookie Echo (COOKIE ECHO) chunk.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         This chunk is used only during the initialization of an association. It is sent by the initiator of an association to its peer to complete the initialization process. This chunk MUST precede any DATA chunk sent within the association but MAY be bundled with one or more DATA chunks in the same packet.
///     </code>
/// </remarks>
internal readonly partial struct SctpCookieEcho : ISctpChunk<SctpCookieEcho>
{
    private const SctpChunkType ChunkTypeImplicit = SctpChunkType.StateCookie;

    /// <summary>
    /// The Chunk Length.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         Set to the size of the chunk in bytes, including the 4 bytes of the chunk header and the size of the cookie.
    ///     </code>
    /// </remarks>
    internal readonly ushort chunkLength;

    /// <summary>
    /// The Cookie.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         This field MUST contain the exact cookie received in the State Cookie parameter from the previous INIT ACK chunk.
    ///
    ///             An implementation SHOULD make the cookie as small as possible to ensure interoperability.
    /// 
    ///             Note: A Cookie Echo does not contain a State Cookie parameter; instead, the data within the State Cookie's Parameter Value becomes the data within the Cookie Echo's Chunk Value.This allows an implementation to change only the first 2 bytes of the State Cookie parameter to become a COOKIE ECHO chunk.
    ///     </code>
    /// </remarks>
    internal readonly ReadOnlyMemory<byte> cookie;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpCookieEcho" />.
    /// </summary>
    /// <param name="cookie">The cookie.</param>
    internal SctpCookieEcho(ReadOnlyMemory<byte> cookie)
    {
        this.chunkLength = (ushort)(sizeof(uint) + cookie.Length);
        this.cookie = cookie;
    }

    /// <inheritdoc />
    SctpChunkType ISctpChunk.ChunkType => ChunkTypeImplicit;

    /// <inheritdoc />
    /// <remarks>
    ///     <para>
    ///         The COOKIE ECHO chunk does not have Chunk Flags.
    ///     </para>
    ///     <para>
    ///         From RFC 9260:
    ///         <code>
    ///             Set to 0 on transmit and ignored on receipt.
    ///         </code>
    ///     </para>
    /// </remarks>
    byte ISctpChunk.ChunkFlags => 0;

    /// <inheritdoc />
    ushort ISctpChunk.ChunkLength => this.chunkLength;

    /// <inheritdoc />
    ReadOnlyMemory<ISctpChunkParameter> ISctpChunk.Parameters => new();
}
