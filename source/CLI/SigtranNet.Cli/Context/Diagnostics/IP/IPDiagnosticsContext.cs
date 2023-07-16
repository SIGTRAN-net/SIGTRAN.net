/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Cli.Context.Diagnostics.IP.Send;
using SigtranNet.Cli.Context.Help;
using SigtranNet.Cli.Context.Main;

namespace SigtranNet.Cli.Context.Diagnostics.IP;

/// <summary>
/// A Command Line Interface context for Internet Protocol diagnostics.
/// </summary>
internal sealed class IPDiagnosticsContext : ICliContext<IPDiagnosticsContext>
{
    private IPDiagnosticsContext(ICliContext current)
    {
        this.Previous = current;
    }

    /// <inheritdoc />
    public static string Argument => "ip";

    /// <inheritdoc />
    public static string Name => ContextMetadata.Name;

    /// <inheritdoc />
    public static string Description => ContextMetadata.Description;

    public static IReadOnlyDictionary<string, IContextTransition> Next =>
        new Dictionary<string, IContextTransition>
        {
            { HelpContext.Argument, new ContextTransition<HelpContext>() },
            { MainContext.Argument, new ContextTransition<MainContext>() },
            { IPSendContext.Argument, new ContextTransition<IPSendContext>() }
        };

    /// <inheritdoc />
    public ICliContext Previous { get; }

    /// <inheritdoc />
    public static IPDiagnosticsContext Create(ICliContext current, string[] args) =>
        new(current);

    static ICliContext ICliContext.Create(ICliContext current, string[] args) =>
        Create(current, args);

    /// <inheritdoc />
    public Task<ContextExecution> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        Console.Clear();
        Console.WriteLine(ContextMetadata.Introduction);
        return Task.FromResult(new ContextExecution(this));
    }

    /// <inheritdoc />
    public ICliContext Take(string[] args)
    {
        if (args.Length == 0)
        {
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
