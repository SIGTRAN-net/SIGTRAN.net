/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Cli.Context;

/// <summary>
/// A transition from one Command Line Interface context to another.
/// </summary>
internal interface IContextTransition
{
    /// <summary>
    /// Creates the next Command Line Interface context.
    /// </summary>
    /// <param name="current">The current context.</param>
    /// <param name="args">The next command line arguments.</param>
    /// <returns>The next context.</returns>
    ICliContext Create(ICliContext current, string[] args);
}

/// <summary>
/// A transition from one Command Line Interface context to another.
/// </summary>
/// <typeparam name="TContext">The type of Command Line Interface context.</typeparam>
internal sealed class ContextTransition<TContext> : IContextTransition
    where TContext : ICliContext<TContext>
{
    ICliContext IContextTransition.Create(ICliContext previous, string[] args) =>
        TContext.Create(previous, args);
}