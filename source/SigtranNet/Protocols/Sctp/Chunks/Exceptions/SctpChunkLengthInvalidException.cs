/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Sctp.Chunks.Exceptions;

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
