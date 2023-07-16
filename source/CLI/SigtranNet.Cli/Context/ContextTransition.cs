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