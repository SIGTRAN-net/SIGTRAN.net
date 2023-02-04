/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Cli.Context.Exit;
using SigtranNet.Cli.Context.Help;
using SigtranNet.Cli.Context.Main;
using SigtranNet.Protocols.Network.IP;
using SigtranNet.Protocols.Network.IP.IPv4.Controllers;
using System.Net;

namespace SigtranNet.Cli.Context.Diagnostics.IP.Send;

internal sealed class IPSendContext : ICliContext<IPSendContext>
{
    private readonly string[] args;

    private IPSendContext(ICliContext current, string[] args)
    {
        Previous = current;
        this.args = args;
    }

    /// <inheritdoc />
    public static string Argument => "send";

    /// <inheritdoc />
    public static string Name => ContextMetadata.Name;

    /// <inheritdoc />
    public static string Description => ContextMetadata.Description;

    /// <inheritdoc />
    public static IReadOnlyDictionary<string, IContextTransition> Next =>
        new Dictionary<string, IContextTransition>
        {
            { ExitContext.Argument, new ContextTransition<ExitContext>() },
            { HelpContext.Argument, new ContextTransition<HelpContext>() },
            { MainContext.Argument, new ContextTransition<MainContext>() }
        };

    /// <inheritdoc />
    public ICliContext Previous { get; }

    /// <inheritdoc />
    public static IPSendContext Create(ICliContext current, string[] args) =>
        new(current, args);

    static ICliContext ICliContext.Create(ICliContext current, string[] args) =>
        Create(current, args);

    /// <inheritdoc />
    public async Task<ContextExecution> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        if (this.args.Length == 0)
        {
            Console.Clear();
            Console.WriteLine(ContextMetadata.Introduction);
            return new ContextExecution(this);
        }

        if (IPAddress.TryParse(this.args[0], out var destinationAddress))
        {
            var ipController = new IPv4ControllerFactory(new IPv4ControllerOptions()).Create(destinationAddress);
            await ipController.SendAsync(IPProtocol.ExperimentationAndTesting_253, new byte[] { 1, 2, 3, 4 }, cancellationToken);
            return new ContextExecution(this);
        }

        return new ContextExecution(this);
    }

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

        if (args.Length >= 1)
            return new IPSendContext(this.Previous, args);

        return this;
    }
}
