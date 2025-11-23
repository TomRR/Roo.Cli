namespace Roo.Cli.Features.Commands.Git.Add;

[Command("add")]
public sealed partial class AddCommand(ICommandHandler<AddRequest> handler, RooCommandContext context)
    : RooCommandBase(context)
{
    private readonly ICommandHandler<AddRequest> _handler = handler ?? throw new ArgumentNullException(nameof(handler));
    private readonly IRooLogger _logger = context.Logger ?? throw new ArgumentNullException(nameof(context.Logger));
    
    [Option("--force", "-f", hasValue: false)]
    public bool Force { get; set; }
    
    [Option("--path", "-p", hasValue: false)]
    public bool Path { get; set; }
    
    [Argument(ofOption: nameof(Path), 0)]
    public string PathArg { get; set; } = "";
    
    public override async Task RunAsync()
    {
        _logger.Log(Components.Rules.AddCommandRule());
        await WithRooConfigAsync(RunCommandPerRepositoryAsync);
    }
    
    private async Task RunCommandPerRepositoryAsync(RepositoryDto repository)
    {
        _logger.Log(Components.Messages.GetAddingWithRepoName(repository.Name));
        
        var request = AddRequest.Create(CommandName, repository, Interactive, Select, false, Force, new OptionArgumentPair(Path, PathArg));
        var result = await _handler.HandleAsync(request);
        
        _logger.LogCommandResult(result);
    }
}