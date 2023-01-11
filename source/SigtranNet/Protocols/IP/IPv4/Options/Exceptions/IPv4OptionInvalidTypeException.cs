/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.IP.IPv4.Options.Exceptions;

/// <summary>
/// An exception that is thrown if an IPv4 option type is invalid or unexpected.
/// </summary>
internal sealed class IPv4OptionInvalidTypeException : IPv4OptionException
{
    /// <summary>
    /// Initializes a new instance of <see cref="IPv4OptionInvalidTypeException" />.
    /// </summary>
    /// <param name="optionType">The invalid option type.</param>
    internal IPv4OptionInvalidTypeException(IPv4OptionType optionType)
        : base(CreateExceptionMessage(optionType))
    {
        this.OptionType = optionType;
    }

    /// <summary>
    /// Gets the invalid option type.
    /// </summary>
    internal IPv4OptionType OptionType { get; }

    private static string CreateExceptionMessage(IPv4OptionType optionType) =>
        string.Format(ExceptionMessages.InvalidType, ((byte)optionType).ToString("X2"));
}
