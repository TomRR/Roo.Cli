namespace Roo.Cli.Commands.Init;

[Command("init")]
public sealed class InitCommand : ICommand
{
    private readonly IRooLogger _logger;
    private readonly IRooConfigService _rooConfigService;

    [Option("--path", "-p")]
    public string FilePath { get; set; } = Directory.GetCurrentDirectory();

    [Option("--force", "-f", hasValue: false)]
    public bool Force { get; set; }

    [Argument(0)]
    public string? Template { get; set; }

    public InitCommand(IRooLogger logger, IRooConfigService rooConfigService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _rooConfigService = rooConfigService ?? throw new ArgumentNullException(nameof(rooConfigService));
    }
    public async Task RunAsync()
    {
        _logger.LogApplicationNameFiglet();
        _logger.Log(LoggingComponents.InitCommandRule());

        await Task.CompletedTask;

        _logger.Log($"Init with\n{PathString()}\n{ForceString()}\n{TemplateString()}", additionalLineBreaksAfter:1);
        
        var result = _rooConfigService.InitRooConfig(FilePath, Force);

        if (result.HasFailed)
        {
            _logger.LogError(result.Error);
            return;
        }

        _logger.Log(result.Value);
        _logger.LogTaskCompleted();
    }

    public string PathString()
    {
        return $"[bold yellow]Path='{FilePath}'[/]";
    }
    public string ForceString()
    {
        var forceMessage = Force 
            ? $"{Icons.CheckIcon}" 
            : $"{Icons.ErrorIcon}";
        return $"Force={forceMessage}";
    }
    public string TemplateString()
    {
        var templateMessage = string.IsNullOrWhiteSpace(Template) 
            ? Icons.ErrorIcon 
            : $"{Icons.CheckIcon} {Template}";
        return $"Template={templateMessage}";
    }
}