/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Cli.Context.Diagnostics;
using SigtranNet.Cli.Context.Exit;
using SigtranNet.Cli.Context.Help;

namespace SigtranNet.Cli.Context.Main;

internal class MainContext : ICliContext<MainContext>
{
    /// <inheritdoc />
    public static string Argument => "main";

    /// <inheritdoc />
    public static string Name => ContextMetadata.Name;

    /// <inheritdoc />
    public static string Description => ContextMetadata.Description;

    /// <inheritdoc />
    public static IReadOnlyDictionary<string, IContextTransition> Next =>
        new Dictionary<string, IContextTransition>
        {
            { DiagnosticsContext.Argument, new ContextTransition<DiagnosticsContext>() },
            { ExitContext.Argument, new ContextTransition<ExitContext>() },
            { HelpContext.Argument, new ContextTransition<HelpContext>() }
        };

    /// <inheritdoc />
    /// <remarks>
    /// <see cref="MainContext" /> is the root, so <see cref="Previous" /> is <see langword="null" />.
    /// </remarks>
    public ICliContext? Previous => null;

    public static MainContext Create(ICliContext current, string[] args) =>
        new();

    static ICliContext ICliContext.Create(ICliContext current, string[] args) =>
        Create(current, args);

    /// <inheritdoc />
    public Task<ContextExecution> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(new ContextExecution(this));
    }

    /// <inheritdoc />
    public ICliContext Take(string[] args)
    {
        if (args.Length == 0)
        {
            Console.Clear();
            Console.WriteLine(ContextMetadata.Introduction);
            return this;
        }

        if (Next.TryGetValue(args[0], out var contextTransition))
            return
                args.Length == 1
                    ? contextTransition.Create(this, args[1..])
                    : contextTransition.Create(this, args[1..]).Take(args[1..]);

        Console.WriteLine(Common.InvalidCommand, args[0]);
        return this;
    }
}
