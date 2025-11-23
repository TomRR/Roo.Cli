namespace Roo.Cli.Features.Commands.Npm.Run;

public sealed record RunRequest : ICommandRequest
{
    public bool Interactive { get; }
    public bool Select { get; }
    public bool MultiSelect { get; }
    
    public string CommandName { get; }
    public RepositoryDto Repository { get; }

    private RunRequest(
        string commandName, 
        RepositoryDto repository,
        bool interactive, 
        bool select, 
        bool multiSelect)
    {
        CommandName = commandName;
        Repository = repository;
        Interactive = interactive;
        Select = select;
        MultiSelect = multiSelect;
    }

    public static RunRequest Create(
        string commandName, 
        RepositoryDto repository,
        bool interactive, 
        bool select, 
        bool multiSelect
    )
        => new(commandName, repository, interactive, select, multiSelect);
}
