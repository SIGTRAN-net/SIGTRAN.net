/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Cli.Context.Exceptions;

/// <summary>
/// An exception that is thrown if a Command Line Interface context receives arguments when the context does not expect any more arguments.
/// </summary>
internal sealed class CliContextArgumentsNotAllowedException<TContext> : CliContextException
    where TContext : ICliContext<TContext>
{
    /// <summary>
    /// Initializes a new instance of <see cref="CliContextArgumentsNotAllowedException" />.
    /// </summary>
    /// <param name="args">The arguments that were received by the context.</param>
    internal CliContextArgumentsNotAllowedException(string[] args)
        : base(CreateExceptionMessage(TContext.Name, args))
    {
    }

    private static string CreateExceptionMessage(string contextName, string[] args) =>
        string.Format(ExceptionMessages.ArgumentsNotAllowed, contextName, string.Join(",", args.Select(a => $"\"{a}\"")));
}
