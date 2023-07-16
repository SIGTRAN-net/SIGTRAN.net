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

using SigtranNet.Cli.Context.Diagnostics.IP;
using SigtranNet.Cli.Context.Exit;
using SigtranNet.Cli.Context.Help;
using SigtranNet.Cli.Context.Main;

namespace SigtranNet.Cli.Context.Diagnostics;

/// <summary>
/// A Command Line Interface context for diagnostics.
/// </summary>
internal sealed class DiagnosticsContext : ICliContext<DiagnosticsContext>
{
    private DiagnosticsContext(ICliContext current)
    {
        this.Previous = current;
    }

    /// <inheritdoc />
    public static string Argument => "diag";

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
            { IPDiagnosticsContext.Argument, new ContextTransition<IPDiagnosticsContext>() },
            { MainContext.Argument, new ContextTransition<MainContext>() }
        };

    /// <inheritdoc />
    public ICliContext? Previous { get; }

    /// <inheritdoc />
    public static DiagnosticsContext Create(ICliContext current, string[] args) =>
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
