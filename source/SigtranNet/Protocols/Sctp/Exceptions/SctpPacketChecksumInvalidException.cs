/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Sctp.Exceptions;

/// <summary>
/// An exception that is thrown if an SCTP Packet's checksum is not valid (or the data that it checks is not valid).
/// </summary>
internal sealed class SctpPacketChecksumInvalidException : SctpException
{
    /// <summary>
    /// Initializes a new instance of <see cref="SctpPacketChecksumInvalidException" />.
    /// </summary>
    /// <param name="checksum">The invalid checksum.</param>
    internal SctpPacketChecksumInvalidException(uint checksum)
        : base(CreateExceptionMessage(checksum))
    {
        this.Checksum = checksum;
    }

    /// <summary>
    /// Gets the invalid checksum.
    /// </summary>
    internal uint Checksum { get; }

    private static string CreateExceptionMessage(uint checksum) =>
        string.Format(ExceptionMessages.PacketInvalidChecksum, checksum);
}
