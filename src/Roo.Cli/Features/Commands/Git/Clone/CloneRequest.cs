namespace Roo.Cli.Features.Commands.Git.Clone;

public class CloneRequest : ICommandRequest
{
    public bool Interactive { get; }
    public bool Select { get; }
    public bool MultiSelect { get; }
    
    public string CommandName { get; }
    public RepositoryDto Repository { get; }

    public bool Force { get; }
    public OptionArgumentPair Path { get; }

    private CloneRequest(
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

    public static CloneRequest Create(
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