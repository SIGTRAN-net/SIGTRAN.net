/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Network.Icmp.Exceptions;

namespace SigtranNet.Protocols.Network.Icmp.Messages.Exceptions;

/// <summary>
/// An exception that is thrown if an Internet Control Message Protocol (ICMP) message checksum is invalid.
/// </summary>
internal sealed class IcmpMessageChecksumInvalidException : IcmpException
{
    /// <summary>
    /// Initializes a new instance of <see cref="IcmpMessageTypeInvalidException" />.
    /// </summary>
    /// <param name="checksum">The invalid message checksum.</param>
    internal IcmpMessageChecksumInvalidException(ushort checksum)
        : base(CreateExceptionMessage(checksum))
    {
        this.Checksum = checksum;
    }

    /// <summary>
    /// Gets the invalid message checksum.
    /// </summary>
    internal ushort Checksum { get; }

    private static string CreateExceptionMessage(ushort checksum) =>
        string.Format(ExceptionMessages.MessageChecksumInvalid, checksum);
}
