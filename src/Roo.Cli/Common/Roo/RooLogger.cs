using Spectre.Console;
using Spectre.Console.Rendering;

namespace Roo.Cli.Common.Roo;

public interface IRooLogger
{
    public void LogError(Error error, string? additionalMessage = null, Style? style = null, int additionalLineBreaksAfter = 0);
    public void Log(string text, Style? style = null, int additionalLineBreaksAfter = 0);
    public void LogWithNoNewLine(string text);
    public void Log(IRenderable renderable, int additionalLineBreaksAfter = 0);
    public void AddLineBreak(int count = 1);
    public void LogApplicationNameFiglet();
    public void LogTaskCompleted();
}

public class RooLogger : IRooLogger
{
    public void LogError(Error error, string? additionalMessage = null, Style? style = null, int additionalLineBreaksAfter = 0)
    {
        var errorType = error.ErrorType;
        var errorDescription = error.Description;
        var errorMessage = $"{Icons.RedDotIcon} {errorType}: \n{errorDescription}\n";
        if (additionalMessage is not null)
        {
            AnsiConsole.Write($"{additionalMessage}\n");
        }
        AnsiConsole.Write(new Markup(errorMessage, style));
        
        if (additionalLineBreaksAfter > 0)
        {
            AddLineBreak(additionalLineBreaksAfter);
        }
    }
    
    public void  Log(string text, Style? style = null, int additionalLineBreaksAfter = 0)
    {
        AnsiConsole.Write(new Markup($"{text}\n", style));
        if (additionalLineBreaksAfter > 0)
        {
            AddLineBreak(additionalLineBreaksAfter);
        }
    }

    public void LogWithNoNewLine(string text)
    {
        Console.Write(text); // no newline
    }

    public void  Log(IRenderable renderable, int additionalLineBreaksAfter = 0)
    {
        AnsiConsole.Write(renderable);
        AddLineBreak(additionalLineBreaksAfter);
        if (additionalLineBreaksAfter > 0)
        {
            AddLineBreak(additionalLineBreaksAfter);
        }
    }

    public void AddLineBreak(int amount = 1)
    {
        for (var i = 0; i < amount; i++)
        {
            AnsiConsole.WriteLine();
        }
    }

    public void LogApplicationNameFiglet()
    {
        AnsiConsole.Write(new FigletText("Roo CLI").Color(Color.Yellow));
    }

    public void LogTaskCompleted()
    {
        AnsiConsole.Write($"{Icons.FinishFlagIcon} Completed\n");
    }
}