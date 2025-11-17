namespace Roo.Cli.Infrastructure.Prompting;

public interface IRooPromptHandler
{
    public string? ReadInput();
}

public class RooPromptHandler : IRooPromptHandler
{
    public string? ReadInput()
    {
        return Console.ReadLine()?.Trim();
    }
}