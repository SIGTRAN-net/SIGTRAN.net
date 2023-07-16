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

using SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Exceptions;

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Exceptions;

/// <summary>
/// An exception that is thrown if an SCTP chunk has an invalid length.
/// </summary>
internal sealed partial class SctpChunkLengthInvalidException : SctpChunkException
{
    /// <summary>
    /// Initializes a new instance of <see cref="SctpChunkLengthInvalidException" />.
    /// </summary>
    /// <param name="chunkLength">The invalid Chunkl Length.</param>
    internal SctpChunkLengthInvalidException(ushort chunkLength)
        : base(CreateExceptionMessage(chunkLength))
    {
        this.ChunkLength = chunkLength;
    }

    /// <summary>
    /// Gets the invalid length.
    /// </summary>
    internal ushort ChunkLength { get; }

    private static string CreateExceptionMessage(ushort length) =>
        string.Format(ExceptionMessages.LengthInvalid, length);
}
