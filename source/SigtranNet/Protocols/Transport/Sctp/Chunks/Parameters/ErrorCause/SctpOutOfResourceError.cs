/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause;

/// <summary>
/// An Out of Resource error parameter in an SCTP Packet Chunk.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         Indicates that the sender is out of resource. This is usually sent in combination with or within an ABORT chunk.
///     </code>
/// </remarks>
internal readonly partial struct SctpOutOfResourceError : ISctpErrorCauseParameter<SctpOutOfResourceError>
{
    private const SctpErrorCauseCode ErrorCauseCodeImplicit = SctpErrorCauseCode.OutOfResource;
    private const ushort ErrorCauseLengthImplicit = sizeof(uint);

    /// <summary>
    /// Initializes a new instance of <see cref="SctpOutOfResourceError" />.
    /// </summary>
    public SctpOutOfResourceError()
    {
    }

    SctpErrorCauseCode ISctpErrorCause.ErrorCauseCode => ErrorCauseCodeImplicit;
    ushort ISctpErrorCause.ErrorCauseLength => ErrorCauseLengthImplicit;
}
