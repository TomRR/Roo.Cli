using Spectre.Console;

namespace Roo.Cli.Common;

public interface IRooLogger
{
    public void LogError(Error error, Style? style = null);
    public void Log(string text, Style? style = null);
}
public class RooLogger : IRooLogger
{
    public void LogError(Error error, Style? style = null)
    {
        var errorType = error.ErrorType;
        var errorDescription = error.Description;
        var errorMessage = $"{errorType}:\n{errorDescription}";
        
        AnsiConsole.Write(new Markup(errorMessage, style));
    }

    public void  Log(string text, Style? style = null)
    {
        AnsiConsole.Write(new Markup(text, style));

    }
}