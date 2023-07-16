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

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Exceptions;

/// <summary>
/// An exception that is thrown if an SCTP Chunk Parameter has an invalid length for a particular Chunk Parameter Type.
/// </summary>
internal sealed partial class SctpChunkParameterLengthInvalidException : SctpChunkParameterException
{
    /// <summary>
    /// Initializes a new instance of <see cref="SctpChunkParameterLengthInvalidException" />.
    /// </summary>
    /// <param name="chunkParameterType">The Chunk Parameter Type.</param>
    /// <param name="chunkParameterLength">The invalid Chunk Parameter Length.</param>
    internal SctpChunkParameterLengthInvalidException(SctpChunkParameterType chunkParameterType, ushort chunkParameterLength)
        : base(CreateExceptionMessage(chunkParameterType, chunkParameterLength))
    {
        this.ChunkParameterType = chunkParameterType;
        this.ChunkParameterLength = chunkParameterLength;
    }

    /// <summary>
    /// Gets the Chunk Parameter Type.
    /// </summary>
    internal SctpChunkParameterType ChunkParameterType { get; }

    /// <summary>
    /// Gets the invalid Chunk Parameter Length.
    /// </summary>
    internal ushort ChunkParameterLength { get; }

    private static string CreateExceptionMessage(SctpChunkParameterType chunkParameterType, ushort chunkParameterLength) =>
        string.Format(ExceptionMessages.ChunkParameterLengthInvalid, chunkParameterType, chunkParameterLength);
}
