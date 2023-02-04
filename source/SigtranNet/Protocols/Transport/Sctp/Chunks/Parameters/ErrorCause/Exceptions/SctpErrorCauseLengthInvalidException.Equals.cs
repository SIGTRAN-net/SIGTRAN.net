/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause.Exceptions;

internal sealed partial class SctpErrorCauseLengthInvalidException
{
    private bool Equals(SctpErrorCauseLengthInvalidException other) =>
        this.ErrorCauseLength.Equals(other.ErrorCauseLength);

    /// <inheritdoc />
    public override bool Equals(object? obj) =>
        obj switch
        {
            null => false,
            _ when ReferenceEquals(this, obj) => true,
            SctpErrorCauseLengthInvalidException other => this.Equals(other),
            _ => false
        };

    /// <inheritdoc />
    public override int GetHashCode() =>
        this.ErrorCauseLength.GetHashCode();
}
