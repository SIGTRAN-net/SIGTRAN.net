/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Sctp.Chunks.Parameters.Exceptions;

/// <summary>
/// An exception that is thrown if an SCTP Chunk Parameter Type is invalid or not supported.
/// </summary>
internal sealed class SctpChunkParameterTypeInvalidException : SctpChunkParameterException
{
    /// <summary>
    /// Initializes a new instance of <see cref="SctpChunkParameterTypeInvalidException" />.
    /// </summary>
    /// <param name="chunkParameterType">The invalid or unsupported Chunk Parameter Type.</param>
    internal SctpChunkParameterTypeInvalidException(SctpChunkParameterType chunkParameterType)
        : base(CreateExceptionMessage(chunkParameterType))
    {
        this.ChunkParameterType = chunkParameterType;
    }

    /// <summary>
    /// Gets the invalid or unsupported Chunk Parameter Type.
    /// </summary>
    internal SctpChunkParameterType ChunkParameterType { get; }

    private static string CreateExceptionMessage(SctpChunkParameterType chunkParameterType) =>
        string.Format(ExceptionMessages.ChunkParameterTypeInvalid, chunkParameterType);
}
