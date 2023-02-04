/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Exceptions;

internal sealed partial class SctpChunkParameterLengthInvalidException
{
    private bool Equals(SctpChunkParameterLengthInvalidException other) =>
        this.ChunkParameterType.Equals(other.ChunkParameterType)
        && this.ChunkParameterLength.Equals(other.ChunkParameterLength);

    /// <inheritdoc />
    public override bool Equals(object? obj) =>
        obj switch
        {
            null => false,
            _ when ReferenceEquals(this, obj) => true,
            SctpChunkParameterLengthInvalidException other => this.Equals(other),
            _ => false
        };

    /// <inheritdoc />
    public override int GetHashCode() =>
        HashCode.Combine(this.ChunkParameterType, this.ChunkParameterLength);
}
