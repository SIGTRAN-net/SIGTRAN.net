/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Sctp.Chunks.Exceptions;

/// <summary>
/// An exception that is thrown if an SCTP chunk has an invalid type.
/// </summary>
internal sealed partial class SctpChunkTypeInvalidException : SctpChunkException
{
    /// <summary>
    /// Initializes a new instance of <see cref="SctpChunkTypeInvalidException" />.
    /// </summary>
    /// <param name="chunkType">The invalid SCTP chunk type.</param>
    internal SctpChunkTypeInvalidException(SctpChunkType chunkType)
        : base(CreateExceptionMessage(chunkType))
    {
        this.ChunkType = chunkType;
    }

    /// <summary>
    /// Gets the invalid chunk type.
    /// </summary>
    internal SctpChunkType ChunkType { get; }

    private static string CreateExceptionMessage(SctpChunkType chunkType) =>
        string.Format(ExceptionMessages.TypeInvalid, chunkType);
}
