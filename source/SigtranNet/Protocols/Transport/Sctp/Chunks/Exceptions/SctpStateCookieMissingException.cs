/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Exceptions;

/// <summary>
/// An exception that is thrown if an SCTP Packet Chunk requires a State Cookie parameter, but it was not found in the chunk.
/// </summary>
internal sealed class SctpStateCookieMissingException : SctpChunkException
{
    /// <summary>
    /// Initializes a new instance of <see cref="SctpStateCookieMissingException" />.
    /// </summary>
    internal SctpStateCookieMissingException()
        : base(ExceptionMessages.StateCookieMissing)
    {
    }
}
