/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Network.IP.IPv4.Exceptions;

/// <summary>
/// An exception that is thrown if an IPv4 header is corrupted or has an invalid checksum.
/// </summary>
internal sealed class IPv4HeaderChecksumInvalidException
    : IPv4Exception
{
    /// <summary>
    /// Initializes a new instance of <see cref="IPv4HeaderChecksumInvalidException" />.
    /// </summary>
    /// <param name="checksum">The invalid checksum.</param>
    internal IPv4HeaderChecksumInvalidException(ushort checksum)
        : base(CreateExceptionMessage(checksum))
    {
        this.Checksum = checksum;
    }

    /// <summary>
    /// Gets the invalid IPv4 header checksum.
    /// </summary>
    internal ushort Checksum { get; }

    private static string CreateExceptionMessage(ushort checksum) =>
        string.Format(ExceptionMessages.HeaderChecksumInvalid, checksum);
}
