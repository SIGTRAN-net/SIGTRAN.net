/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Cli.Context.Exceptions;

namespace SigtranNet.Cli.Context.Exit;

/// <summary>
/// A Command Line Interface context for exiting the application.
/// </summary>
internal sealed class ExitContext : ICliContext<ExitContext>
{
    /// <summary>
    /// Initializes a new instance of <see cref="ExitContext" />.
    /// </summary>
    /// <param name="current">The current Command Line Interface context.</param>
    private ExitContext(ICliContext current)
    {
        this.Previous = current;
    }

    /// <inheritdoc />
    public static string Argument => "exit";

    /// <inheritdoc />
    public static string Name => ContextMetadata.Name;

    /// <inheritdoc />
    public static string Description => ContextMetadata.Description;

    /// <inheritdoc />
    public static IReadOnlyDictionary<string, IContextTransition> Next =>
        new Dictionary<string, IContextTransition>();

    /// <inheritdoc />
    public ICliContext Previous { get; }

    public static ExitContext Create(ICliContext current, string[] args) =>
        new(current);

    static ICliContext ICliContext.Create(ICliContext current, string[] args) =>
        Create(current, args);

    /// <inheritdoc />
    public Task<ContextExecution> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        Environment.Exit(0);
        return Task.FromResult(new ContextExecution(this));
    }

    /// <inheritdoc />
    public ICliContext Take(string[] args)
    {
        if (args.Length > 0)
            throw new CliContextArgumentsNotAllowedException<ExitContext>(args);
        return this.Previous;
    }
}
