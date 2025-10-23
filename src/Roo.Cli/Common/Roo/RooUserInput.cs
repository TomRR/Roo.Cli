namespace Roo.Cli.Common.Roo;

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