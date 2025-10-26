namespace Roo.Cli.Features.Commands.System.Init;

[Command("init")]
public sealed partial class InitCommand : ICommand
{
    private readonly IRooLogger _logger;
    private readonly IRooConfigService _rooConfigService;

    [Option("--path", "-p")]
    public bool FilePath { get; set; } 
    
    [Argument(ofOption: nameof(FilePath), 0)]
    public string? FilePathName { get; set; } = Directory.GetCurrentDirectory();

    [Option("--force", "-f", hasValue: false)]
    public bool Force { get; set; }

    // [Argument()]
    // public string? Template { get; set; }

    public InitCommand(IRooLogger logger, IRooConfigService rooConfigService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _rooConfigService = rooConfigService ?? throw new ArgumentNullException(nameof(rooConfigService));
    }
    public async Task RunAsync()
    {
        _logger.LogApplicationNameFiglet();
        _logger.Log(Components.Rules.InitCommandRule());

        await Task.CompletedTask;

        // _logger.Log($"Init with\n{PathString()}\n{ForceString()}\n{TemplateString()}", additionalLineBreaksAfter:1);

        if (FilePathName is null)
        {
            return;
        }
        var result = _rooConfigService.InitRooConfig(FilePathName, Force);

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
    // public string TemplateString()
    // {
    //     // var templateMessage = string.IsNullOrWhiteSpace(Template) 
    //     //     ? Icons.ErrorIcon 
    //     //     : $"{Icons.CheckIcon} {Template}";
    //     // return $"Template={templateMessage}";
    // }
}