
using System.Text;
using Success = ResultType.UnitTypes.Success;

namespace Roo.Cli.Commands;

public interface IRepositoryActionLogger
{
    public void LogActionTerminalOutput(string output);
    public string GetActionTerminalOutput { get; }
}

public class RepositoryActionLogger : IRepositoryActionLogger
{
    public static string Output = string.Empty;

    public static void GetConsoleLog(string output)
    {
        Output = Output +  output + "\n";
    }
    public string GetActionTerminalOutput => _output;
    private string _output = string.Empty;
    public void LogActionTerminalOutput(string output)
    {
        _output  = _output + output + "\n";
    }
}
public interface ICommandAction<TCommand> where TCommand : ICommand
{
    Task<Result<RepositoryActionResponse, Error, Skipped>> RunCommandAsync(RepositoryDto repository, bool force  = false);
}