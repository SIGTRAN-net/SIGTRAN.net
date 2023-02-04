/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Cli.Context;

/// <summary>
/// Execution of a Command Line Interface context.
/// </summary>
internal sealed class ContextExecution
{
    /// <summary>
    /// Initializes a new instance of <see cref="ContextExecution" />.
    /// </summary>
    /// <param name="state">The resulting context.</param>
    internal ContextExecution(ICliContext state)
    {
        this.Result = state;
    }

    /// <summary>
    /// Gets the resulting Command Line Interface context.
    /// </summary>
    internal ICliContext Result { get; }
}
