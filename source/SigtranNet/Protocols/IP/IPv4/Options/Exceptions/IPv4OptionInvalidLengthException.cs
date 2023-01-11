/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.IP.IPv4.Options.Exceptions;

/// <summary>
/// An exception that is thrown if the specified length for an IPv4 option is invalid.
/// </summary>
internal sealed class IPv4OptionInvalidLengthException : IPv4OptionException
{
    /// <summary>
    /// Initializes a new instance of <see cref="IPv4OptionInvalidLengthException" />.
    /// </summary>
    /// <param name="length">The invalid length.</param>
    internal IPv4OptionInvalidLengthException(byte length)
        : base(CreateExceptionMessage(length))
    {
        this.Length = length;
    }

    /// <summary>
    /// Gets the invalid length.
    /// </summary>
    internal byte Length { get; }

    private static string CreateExceptionMessage(ushort length) =>
        string.Format(ExceptionMessages.InvalidLength, length);
}
