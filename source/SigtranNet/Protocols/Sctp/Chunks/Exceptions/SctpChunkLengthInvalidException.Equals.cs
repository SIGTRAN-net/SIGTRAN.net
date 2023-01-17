/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Sctp.Chunks.Exceptions;

internal sealed partial class SctpChunkLengthInvalidException
{
    private bool Equals(SctpChunkLengthInvalidException other) =>
        this.ChunkLength.Equals(other.ChunkLength);

    /// <inheritdoc />
    public override bool Equals(object? obj) =>
        obj switch
        {
            null => false,
            SctpChunkLengthInvalidException other => this.Equals(other),
            _ => false
        };

    /// <inheritdoc />
    public override int GetHashCode() =>
        this.ChunkLength.GetHashCode();
}
