/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.IP.IPv4.Options.Exceptions;

namespace SigtranNet.Protocols.IP.IPv4.Options.NoOperation;

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
