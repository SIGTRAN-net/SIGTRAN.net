/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Cli.Exceptions;

namespace SigtranNet.Cli.Context.Exceptions;

/// <summary>
/// An exception that is thrown if a Command Line Interface context encounters an error.
/// </summary>
internal abstract class CliContextException : CliException
{
    /// <summary>
    /// Initializes a new instance of <see cref="CliContextException" />.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">An optional inner exception.</param>
    protected CliContextException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }
}
