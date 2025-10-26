namespace Roo.Cli.Infrastructure.Prompting;

public interface IRooUserInput
{
    public string? ReadInput();
}

public class RooUserInput : IRooUserInput
{
    public string? ReadInput()
    {
        return Console.ReadLine()?.Trim();
    }
}