using Roo.Cli.Infrastructure.Logging;

namespace Roo.Cli.Tests;

public class TestLogger : IRooLogger
{
    public void LogError(Error error, string? additionalMessage = null, Style? style = null, int additionalLineBreaksAfter = 0)
    { }

    public void Log(string text, Style? style = null, int additionalLineBreaksAfter = 0)
    { }

    public void LogWithNoNewLine(string text)
    { }

    public void Log(IRenderable renderable, int additionalLineBreaksAfter = 0)
    { }

    public void AddLineBreak(int count = 1)
    { }

    public void LogApplicationNameFiglet()
    { }

    public void LogTaskCompleted()
    { }
}