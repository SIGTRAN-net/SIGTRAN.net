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

namespace SigtranNet.Cli.Context.Exceptions;

/// <summary>
/// An exception that is thrown if a Command Line Interface context receives arguments when the context does not expect any more arguments.
/// </summary>
internal sealed class CliContextArgumentsNotAllowedException<TContext> : CliContextException
    where TContext : ICliContext<TContext>
{
    /// <summary>
    /// Initializes a new instance of <see cref="CliContextArgumentsNotAllowedException{TContext}" />.
    /// </summary>
    /// <param name="args">The arguments that were received by the context.</param>
    internal CliContextArgumentsNotAllowedException(string[] args)
        : base(CreateExceptionMessage(TContext.Name, args))
    {
    }

    private static string CreateExceptionMessage(string contextName, string[] args) =>
        string.Format(ExceptionMessages.ArgumentsNotAllowed, contextName, string.Join(",", args.Select(a => $"\"{a}\"")));
}
