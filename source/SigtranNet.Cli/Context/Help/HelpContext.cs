/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Cli.Context.Exceptions;
using System.Reflection;

namespace SigtranNet.Cli.Context.Help;

/// <summary>
/// A Command Line Interface that displays helpful information about the program.
/// </summary>
internal sealed class HelpContext : ICliContext<HelpContext>
{
    /// <summary>
    /// Initializes a new instance of <see cref="HelpContext" />.
    /// </summary>
    /// <param name="current">
    /// The current context.
    /// </param>
    private HelpContext(ICliContext current)
    {
        this.Previous = current;
    }

    /// <inheritdoc />
    public static string Argument => "help";

    /// <inheritdoc />
    public static string Name => ContextMetadata.Name;

    /// <inheritdoc />
    public static string Description => ContextMetadata.Description;

    /// <inheritdoc />
    public static IReadOnlyDictionary<string, IContextTransition> Next =>
        new Dictionary<string, IContextTransition>();

    /// <inheritdoc />
    public ICliContext Previous { get; }

    public static HelpContext Create(ICliContext current, string[] args) =>
        new(current);

    static ICliContext ICliContext.Create(ICliContext current, string[] args) =>
        Create(current, args);

    /// <inheritdoc />
    public Task<ContextExecution> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        Console.Clear();
        Console.WriteLine(ContextMetadata.Introduction);
        var previousType = this.Previous.GetType();
        var nextProperty = previousType.GetProperty(nameof(ICliContext.Next), BindingFlags.Public | BindingFlags.Static)!;
        var next = (IReadOnlyDictionary<string, IContextTransition>)nextProperty.GetValue(null)!;
        foreach (var command in next)
        {
            var contextType = command.Value.GetType().GetGenericArguments()[0];
            var contextArgumentProperty = contextType.GetProperty(nameof(ICliContext.Argument), BindingFlags.Public | BindingFlags.Static)!;
            var contextArgument = (string)contextArgumentProperty.GetValue(null)!;
            var contextNameProperty = contextType.GetProperty(nameof(ICliContext.Name), BindingFlags.Public | BindingFlags.Static)!;
            var contextName = (string)contextNameProperty.GetValue(null)!;
            var contextDescriptionProperty = contextType.GetProperty(nameof(ICliContext.Description), BindingFlags.Public | BindingFlags.Static)!;
            var contextDescription = (string)contextDescriptionProperty.GetValue(null)!;
            Console.WriteLine($"{contextArgument} ('{contextName}'): {contextDescription}");
        }
        return Task.FromResult(new ContextExecution(this.Previous));
    }

    /// <inheritdoc />
    public ICliContext Take(string[] args)
    {
        if (args.Length > 0)
            throw new CliContextArgumentsNotAllowedException<HelpContext>(args);
        return this.Previous;
    }
}
