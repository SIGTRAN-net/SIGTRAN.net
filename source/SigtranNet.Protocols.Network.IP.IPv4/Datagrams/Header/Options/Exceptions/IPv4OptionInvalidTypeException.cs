/*
 * This file is part of SIGTRAN.net.
 * 
 * SIGTRAN.net is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, 
 * either version 3 of the License, or (at your option) any later version.
 * 
 * SIGTRAN.net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. 
 * See the GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License along with SIGTRAN.net. If not, see <https://www.gnu.org/licenses/>.
 */

using SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header.Options;

namespace SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header.Options.Exceptions;

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
        OptionType = optionType;
    }

    /// <summary>
    /// Gets the invalid option type.
    /// </summary>
    internal IPv4OptionType OptionType { get; }

    private static string CreateExceptionMessage(IPv4OptionType optionType) =>
        string.Format(ExceptionMessages.InvalidType, ((byte)optionType).ToString("X2"));
}
