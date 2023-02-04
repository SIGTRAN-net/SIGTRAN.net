/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Cli.Exceptions;

/// <summary>
/// An exception that is thrown if the Command Line Interface (CLI) encounters an error.
/// </summary>
internal abstract class CliException : Exception
{
    /// <summary>
    /// Initializes a new instance of <see cref="CliException" />.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">An optional inner exception.</param>
    protected CliException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }
}
