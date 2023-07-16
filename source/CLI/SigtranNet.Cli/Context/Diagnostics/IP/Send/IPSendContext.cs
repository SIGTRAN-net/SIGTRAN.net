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

using SigtranNet.Cli.Context.Exit;
using SigtranNet.Cli.Context.Help;
using SigtranNet.Cli.Context.Main;
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
            /*
            var options = new IPv4ControllerOptions();
            IIPProtocolController<IPv4Datagram> ipController = new IPv4Controller(options, sourceIPAddress: new IPAddress(new byte[] { 0, 0, 0, 0 }), destinationAddress);
            await ipController.SendAsync(IPProtocol.ExperimentationAndTesting_253, new byte[] { 1, 2, 3, 4 }, cancellationToken);
            */
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
