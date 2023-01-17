/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Sctp.Chunks.Exceptions;

internal sealed partial class SctpChunkTypeInvalidException
{
    private bool Equals(SctpChunkTypeInvalidException other) =>
        this.ChunkType.Equals(other.ChunkType);

    /// <inheritdoc />
    public override bool Equals(object? obj) =>
        obj switch
        {
            null => false,
            SctpChunkTypeInvalidException other => this.Equals(other),
            _ => false
        };

    /// <inheritdoc />
    public override int GetHashCode() =>
        this.ChunkType.GetHashCode();
}
