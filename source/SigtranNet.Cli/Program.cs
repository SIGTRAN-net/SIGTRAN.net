/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Cli.Context;
using SigtranNet.Cli.Context.Main;

ICliContext contextCurrent = new MainContext();
contextCurrent = contextCurrent.Take(args);
while (Console.ReadLine() is { } line)
{
    var argsNext = line.Split(' ');
    contextCurrent = contextCurrent.Take(argsNext);
    contextCurrent = (await contextCurrent.ExecuteAsync()).Result;
}