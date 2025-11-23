using System.Diagnostics;
using System.Runtime.InteropServices;
using Spectre.Console.Rendering;

namespace Roo.Cli.Infrastructure.Logging;

public interface IRooLogger
{
    public void LogError(Error error, string? additionalMessage = null, Style? style = null, int additionalLineBreaksAfter = 0);
    public void Log(string text, Style? style = null, int additionalLineBreaksAfter = 0);
    public void LogWithNoNewLine(string text);
    public void Log(IRenderable renderable, int additionalLineBreaksAfter = 0);
    public void AddLineBreak(int count = 1);
    public void LogApplicationNameFiglet();
    public void LogTaskCompleted();
    public void LogCommandResult(Result<IRooCommandResponse, Error, Skipped> result);
    public Result<NoErrors, Errors> LogCommandResultErrors(Result<IRooCommandResponse, Error, Skipped> result);

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
        AddLineBreak();
        
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
    
    public void LogCommandResult(Result<IRooCommandResponse, Error, Skipped> result)
    {
        if (result.HasError)
        {
            LogError(result.Error);
            Log(Components.Rules.GreyDimRule());
            
            return;
        }
        
        var stdErr = result.Value?.StandardError ?? string.Empty;
        var stdout = result.Value?.StandardOutput ?? string.Empty;
        
        if (!string.IsNullOrWhiteSpace(stdErr))
        {
            Log($"{Icons.RedDotIcon} {result.Value?.StandardError}");
            return;
        }

        Log(!string.IsNullOrWhiteSpace(stdout) 
            ? $"{Icons.GreenDotIcon} {stdout}" 
            : $"{Icons.CheckSimpleIcon}");

        Log(Components.Rules.GreyDimRule());
    }
    
    public Result<NoErrors, Errors> LogCommandResultErrors(Result<IRooCommandResponse, Error, Skipped> result)
    {
        if (result.HasError)
        {
            LogError(result.Error);
            Log(Components.Rules.GreyDimRule());
            
            return new Errors();
        }
        
        var stdErr = result.Value?.StandardError ?? string.Empty;
        
        if (!string.IsNullOrWhiteSpace(stdErr))
        {
            Log($"{Icons.RedDotIcon} {result.Value?.StandardError}");
            return new Errors();
        }

        return new NoErrors();

    }
}