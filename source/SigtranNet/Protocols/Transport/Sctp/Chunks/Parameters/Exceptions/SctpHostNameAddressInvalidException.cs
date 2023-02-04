/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Exceptions;

/// <summary>
/// An exception that is thrown if a Host Name Address in an SCTP Packet Chunk Header Parameter is invalid.
/// </summary>
internal sealed class SctpHostNameAddressInvalidException : SctpChunkParameterException
{
    /// <summary>
    /// Initializes a new instance of <see cref="SctpHostNameAddressInvalidException" />.
    /// </summary>
    internal SctpHostNameAddressInvalidException()
        : base(ExceptionMessages.HostNameAddressInvalid)
    {
    }
}
