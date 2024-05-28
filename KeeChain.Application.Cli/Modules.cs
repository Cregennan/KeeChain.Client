namespace KeeChain.Application.Cli
{
    using Salavat;
    using Spectre.Console;
    using Warlin.Exceptions;

    public static class Modules
    {
        public static Task CliProcessUnlock(this Salavat salavat)
        {
            var message = salavat.EntriesCount == 0 ? "Придумайте пароль" : "Введите ваш мастер пароль";
            AnsiConsole.Prompt(
                new TextPrompt<string>($"{message}: ")
                    .PromptStyle("green")
                    .ValidationErrorMessage("[red]Неверный пароль[/]")
                    .Validate(password =>
                    {
                        try
                        {
                            salavat.Unlock(password).Wait();
                            return ValidationResult.Success();
                        }
                        catch (WarlinException e)
                        {
                            return ValidationResult.Error(e.Message);
                        }
                    })
            );

            return Task.CompletedTask;
        }

        public static Task DisplayCodesUntilInterrupted(Salavat salavat)
        {
            using var source = new CancellationTokenSource();
            var interruptedTask = Task.Run(() =>
            {
                while (Console.ReadKey(true).Key != ConsoleKey.Escape)
                {
                    
                }

                return Task.CompletedTask;
            }, cancellationToken: source.Token);

            if (salavat.EntriesCount == 0)
            {
                AnsiConsole.MarkupLine();
            }
            
            
            
            
        }
        
        
        
    }
}