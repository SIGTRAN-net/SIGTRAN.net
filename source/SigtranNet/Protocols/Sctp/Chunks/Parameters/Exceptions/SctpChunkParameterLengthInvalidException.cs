/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Sctp.Chunks.Parameters.Exceptions;

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
