using ResultType.UnitTypes;
using Roo.Cli.Features.Commands.Core;
using Roo.Cli.Infrastructure.Logging;

namespace Roo.Cli.Tests.Helper;

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

    public void LogCommandResult(Result<IRooCommandResponse, Error, Skipped> result)
    { }

    public Result<NoErrors, Errors> LogCommandResultErrors(Result<IRooCommandResponse, Error, Skipped> result)
    {
        throw new NotImplementedException();
    }
}