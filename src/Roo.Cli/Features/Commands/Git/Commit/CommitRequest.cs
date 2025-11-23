namespace Roo.Cli.Features.Commands.Git.Commit;

public sealed record CommitRequest : ICommandRequest
{
    // ICommandRequest Member
    public bool Interactive { get; }
    public bool Select { get; }
    public bool MultiSelect { get; }
    
    //
    public string CommandName { get; }
    public RepositoryDto Repository { get; }
    
    private CommitRequest(
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

    public static CommitRequest Create(
        string commandName, 
        RepositoryDto repository,
        bool interactive, 
        bool select, 
        bool multiSelect)
        => new(commandName, repository, interactive, select, multiSelect);
}