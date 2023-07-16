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
using SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header.Options.Exceptions;

namespace SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header.Options.NoOperation;

/// <summary>
/// An IPv4 option that represents no operation, usually for alignment of 32 bit boundaries.
/// </summary>
/// <remarks>
///     From RFC 791:
///     <code>
///         This option may be used between options, for example, to align
///         the beginning of a subsequent option on a 32 bit boundary.
///
///         May be copied, introduced, or deleted on fragmentation, or for
///         any other reason.
///     </code>
/// </remarks>
internal readonly partial struct IPv4OptionNoOperation : IIPv4Option
{
    internal readonly IPv4OptionType optionType;

    /// <summary>
    /// Initializes a new instance of <see cref="IPv4OptionNoOperation" />.
    /// </summary>
    public IPv4OptionNoOperation()
        : this(IPv4OptionType.NotCopied_Control_NoOperation)
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="IPv4OptionNoOperation" />.
    /// </summary>
    /// <param name="optionType">The IPv4 header option type.</param>
    /// <exception cref="IPv4OptionInvalidTypeException">
    /// An <see cref="IPv4OptionInvalidTypeException" /> is thrown if <paramref name="optionType" /> is not a 'No Operation' option type.
    /// </exception>
    internal IPv4OptionNoOperation(IPv4OptionType optionType)
    {
        if (optionType is
                not IPv4OptionType.NotCopied_Control_NoOperation
                and not IPv4OptionType.Copied_Control_NoOperation)
            throw new IPv4OptionInvalidTypeException(optionType);

        this.optionType = optionType;
    }

    /// <inheritdoc />
    public byte Length => 1;
}
