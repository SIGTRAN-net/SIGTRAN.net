/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.IP.IPv4.Options.Exceptions;

/// <summary>
/// An exception that is thrown if an invalid pointer is specified in an IPv4 option.
/// </summary>
internal sealed class IPv4OptionInvalidPointerException : IPv4OptionException
{
    /// <summary>
    /// Initializes a new instance of <see cref="IPv4OptionInvalidPointerException" />.
    /// </summary>
    /// <param name="pointer">The invalid pointer value.</param>
    internal IPv4OptionInvalidPointerException(byte pointer)
        : base(CreateExceptionMessage(pointer))
    {
        this.Pointer = pointer;
    }

    /// <summary>
    /// Gets the invalid pointer.
    /// </summary>
    internal byte Pointer { get; }

    private static string CreateExceptionMessage(byte pointer) =>
        string.Format(ExceptionMessages.InvalidPointer, pointer);
}
