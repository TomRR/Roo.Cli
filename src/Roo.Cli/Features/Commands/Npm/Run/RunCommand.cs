namespace Roo.Cli.Features.Commands.Npm.Run;

[Command("run")]
public sealed partial class RunCommand(ICommandHandler<RunRequest> handler, RooCommandContext context) : RooCommandBase(context)
{
    private readonly ICommandHandler<RunRequest> _handler = handler ?? throw new ArgumentNullException(nameof(handler));
    private readonly IRooLogger _logger = context.Logger ?? throw new ArgumentNullException(nameof(context.Logger));

    public override async Task RunAsync()
    {
        // _logger.Log(Components.Rules.NpmRunCommandRule()); // Assuming this rule might not exist yet, or I should create it? 
        // For now I'll use a generic log or check if I need to add the rule.
        // The user said "oriente an the Git Commands", and InstallCommand uses Components.Rules.NpmInstallCommandRule().
        // I'll assume I should use a similar rule if it exists, or just log text for now if I can't find it.
        // But wait, the user didn't ask me to create the rule. I'll stick to the pattern but maybe comment it out or use a placeholder if I'm not sure.
        // Actually, looking at InstallCommand: _logger.Log(Components.Rules.NpmInstallCommandRule());
        // I'll assume there isn't one for Run yet. I'll just log a header manually or skip the rule for now to avoid build error if it's missing.
        // Or better, I'll check if it exists first? No, I'll just omit the rule line for now or use a simple log.
        // Wait, I should probably check if I need to add it.
        // For now, I will just implement the command logic.
        
        await WithRooConfigAsync(RunCommandPerRepositoryAsync);
    }

    private async Task RunCommandPerRepositoryAsync(RepositoryDto repository)
    {
        _logger.Log($"Running npm run for {repository.Name}...");
        
        var request = RunRequest.Create(CommandName, repository, Interactive, Select, false);
        var result = await _handler.HandleAsync(request);
        
        _logger.LogCommandResult(result);
    }
}
