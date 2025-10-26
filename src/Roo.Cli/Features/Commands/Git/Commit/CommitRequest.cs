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

    public bool Force { get; }
    public OptionArgumentPair Path { get; }

    private CommitRequest(
        string commandName, 
        RepositoryDto repository,
        bool interactive, 
        bool select, 
        bool multiSelect,
        bool force,
        OptionArgumentPair path)
    {
        CommandName = commandName;
        Repository = repository;
        Interactive = interactive;
        Select = select;
        MultiSelect = multiSelect;
        Force = force;
        Path = path;
    }

    public static CommitRequest Create(
        string commandName, 
        RepositoryDto repository,
        bool interactive, 
        bool select, 
        bool multiSelect,
        bool force,
        OptionArgumentPair path
    )
        => new(commandName, repository, interactive, select, multiSelect, force, path);
}