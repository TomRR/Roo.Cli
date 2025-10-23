using Spectre.Console;

namespace Roo.Cli.Commands.Status;

[Command("status")]
public class StatusCommand : RooCommandBase
{
    private readonly IRooLogger _logger;
    private readonly IRepositoryActionLogger _repositoryActionLogger;
    private readonly IGitStatusParser _gitStatusParser;
    private readonly ICommandAction<StatusCommand> _action;
    private Table _table;


    public StatusCommand(ICommandAction<StatusCommand> action, IRooLogger logger, IRooConfigService configService, IRepositoryActionLogger  repositoryActionLogger,IDirectoryService directoryService, IGitStatusParser gitStatusParser)        
        : base(logger, configService, directoryService)
    {
        _action = action ?? throw new ArgumentNullException(nameof(action));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repositoryActionLogger = repositoryActionLogger ?? throw new ArgumentNullException(nameof(repositoryActionLogger));
        _gitStatusParser = gitStatusParser ?? throw new ArgumentNullException(nameof(gitStatusParser));
    }
    
    [Option("--interactive", "-i", hasValue: false)]
    public bool Interactive { get; set; }

    public override async Task RunAsync()
    {
        _table = new Table()
            .RoundedBorder()
            .Title("[bold cyan]📦 Roo Status Summary[/]")
            .AddColumn("[bold]Repository[/]")
            .AddColumn("[bold]Status[/]");
        
        _logger.Log(LoggingComponents.GetCloneCommandRule());
        await WithRooConfigAsync(RunStatusCommandAsync);
        
        _logger.Log(_table);
        
        
        _logger.Log(InternalLogOutput.Output);



    }
    
    private async Task RunStatusCommandAsync(RepositoryDto repository)
    {
        _logger.Log(LoggingComponents.GetRepoName(repository.Name));

        var commandResult = await _action.RunCommandAsync(repository);

        if (commandResult.HasError)
        {
            _logger.LogError(commandResult.Error);
            return;
        }

        var parsed = _gitStatusParser.Parse(commandResult.Value.StandardOutput);

        // Add to summary table
        _table.AddRow(
            $"[grey]{repository.Name}[/]",
            $"{parsed.Status.StateToEmoji()} ({parsed.BranchHeadName})"
        );

        AnsiConsole.MarkupLine($"[yellow]Branch:[/] Head: {parsed.BranchHeadName} / Upstream: {parsed.BranchUpstreamName}");

        if (parsed.Status is RepoStatus.Clean)
        {
            _logger.Log($"{parsed.Status.StateToEmoji()}");
            _logger.Log(LoggingComponents.EmptyRule());
            return;
        }
        if (parsed.ModifiedFiles.Any())
        {
            AnsiConsole.MarkupLine("[orange1]Modified files:[/]");
            foreach (var file in parsed.ModifiedFiles)
            {
                AnsiConsole.MarkupLine($"  [grey]- {file}[/]");
            }        }

        if (parsed.UntrackedFiles.Any())
        {
            AnsiConsole.MarkupLine("[yellow]Untracked files:[/]");
            foreach (var file in parsed.UntrackedFiles)
            {
                AnsiConsole.MarkupLine($"  [grey]- {file}[/]");
            }        }

        if (parsed.Ahead > 0 || parsed.Behind > 0)
        {
            AnsiConsole.MarkupLine($"[blue]Ahead {parsed.Ahead}, Behind {parsed.Behind}[/]");
        }
        _logger.Log(LoggingComponents.EmptyRule());
    }
}

