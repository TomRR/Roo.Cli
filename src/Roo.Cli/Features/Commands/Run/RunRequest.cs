namespace Roo.Cli.Features.Commands.Run;

public sealed record RunRequest : ICommandRequest
{
    public bool Interactive { get; }
    public bool Select { get; }
    public bool MultiSelect { get; }
    
    public string CommandName { get; }
    public RepositoryDto Repository { get; }
    public string CommandToRun { get; }

    private RunRequest(
        string commandName, 
        RepositoryDto repository,
        string commandToRun,
        bool interactive, 
        bool select, 
        bool multiSelect)
    {
        CommandName = commandName;
        Repository = repository;
        CommandToRun = commandToRun;
        Interactive = interactive;
        Select = select;
        MultiSelect = multiSelect;
    }

    public static RunRequest Create(
        string commandName, 
        RepositoryDto repository,
        string commandToRun,
        bool interactive, 
        bool select, 
        bool multiSelect
    )
        => new(commandName, repository, commandToRun, interactive, select, multiSelect);
}
