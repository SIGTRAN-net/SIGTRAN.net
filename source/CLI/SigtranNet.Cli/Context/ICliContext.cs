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

namespace SigtranNet.Cli.Context;

/// <summary>
/// A Command Line Interface context.
/// </summary>
internal interface ICliContext
{
    /// <summary>
    /// Gets the argument that activates the Command Line Interface context.
    /// </summary>
    abstract static string Argument { get; }

    /// <summary>
    /// Gets the name of the Command Line Interface context.
    /// </summary>
    abstract static string Name { get; }

    /// <summary>
    /// Gets the description of the Command Line Interface context.
    /// </summary>
    abstract static string Description { get; }

    /// <summary>
    /// Gets the next contexts based on the following argument.
    /// </summary>
    abstract static IReadOnlyDictionary<string, IContextTransition> Next { get; }

    /// <summary>
    /// Gets the previous context.
    /// </summary>
    ICliContext? Previous { get; }

    /// <summary>
    /// Creates the Command Line Interface context.
    /// </summary>
    /// <param name="current">The current Command Line Interface context.</param>
    /// <param name="args">The next Command Line Arguments.</param>
    /// <returns>The next Command Line Interface context.</returns>
    abstract static ICliContext Create(ICliContext current, string[] args);

    /// <summary>
    /// Take command line arguments.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    /// <returns>Returns the new context after having taken the command line arguments.</returns>
    ICliContext Take(string[] args);

    /// <summary>
    /// Executes the context.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task<ContextExecution> ExecuteAsync(CancellationToken cancellationToken = default);
}

/// <summary>
/// A Command Line Interface context.
/// </summary>
/// <typeparam name="TContext">The type of context.</typeparam>
internal interface ICliContext<TContext> : ICliContext
    where TContext : ICliContext<TContext>
{
    static string ICliContext.Argument => TContext.Argument;

    static string ICliContext.Name => TContext.Name;

    static string ICliContext.Description => TContext.Description;

    new abstract static TContext Create(ICliContext current, string[] args);
}