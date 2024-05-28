using KeeChain.Application.Cli;
using KeeChain.Salavat;
using KeeChain.Warlin.Exceptions;
using Spectre.Console;

var salavat = new Salavat();

try
{
    await salavat.Initialize();
    AnsiConsole.MarkupLine("[b][green]Подключение установлено![/][/]");

    await salavat.CliProcessUnlock();
    AnsiConsole.MarkupLine("[b][green]Устройство разблокировано![/][/]");

    while (true)
    {
           
    }
}
catch (WarlinException e)
{
    AnsiConsole.MarkupLine($"[b][red]{e.Message}[/][/]");
    return;
}
