/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Sctp.Chunks.Parameters.ErrorCause.Exceptions;

internal sealed partial class SctpErrorCauseCodeInvalidException
{
    private bool Equals(SctpErrorCauseCodeInvalidException other) =>
        this.ErrorCauseCode.Equals(other.ErrorCauseCode);

    /// <inheritdoc />
    public override bool Equals(object? obj) =>
        obj switch
        {
            null => false,
            _ when ReferenceEquals(this, obj) => true,
            SctpErrorCauseCodeInvalidException other => this.Equals(other),
            _ => false
        };

    /// <inheritdoc />
    public override int GetHashCode() =>
        this.ErrorCauseCode.GetHashCode();
}
