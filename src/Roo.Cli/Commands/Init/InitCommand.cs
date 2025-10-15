namespace Roo.Cli.Commands.Init;

[Command("init")]
public sealed class InitCommand : ICommand
{
    private readonly IRooLogger _logger;
    private readonly IFileService _fileService;

    [Option("--path", "-p")]
    public string FilePath { get; set; } = Directory.GetCurrentDirectory();

    [Option("--force", "-f", hasValue: false)]
    public bool Force { get; set; }

    [Argument(0)]
    public string? Template { get; set; }

    public InitCommand(IRooLogger logger, IFileService fileService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
    }
    public Task RunAsync()
    {
        _logger.Log($"Init with Path={FilePath}, Force={Force}, Template={Template}");
        _logger.Log("[bold yellow]Repository[/] [red]initialized![/]\n");
        
         var filePath = Path.Combine(FilePath, InitConfigFileName);

         if (_fileService.FileExists(filePath))
         {
             _logger.Log("roo.toml already exists!");
             return Task.CompletedTask;

         }

         _fileService.WriteAllText(filePath, InitConfigFileContent);
         _logger.Log($"roo.toml created at Path={FilePath}");
         
        return Task.CompletedTask;
    }
    
    private const string InitConfigFileName = "roo.toml";
    private const string InitConfigFileContent = $@"# Roo CLI configuration
# This is the configuration file for Roo.
# It defines all the repositories that make up this project workspace.

# The [[repositories]] syntax defines an item in an array of tables.
# Each block represents one repository.

[[repositories]]
# A friendly name for the repository, used in status outputs.
#name = ""aspire-app-host""
# The SSH or HTTPS URL for cloning.
#url = ""git@github.com:our-org/aspire-app-host.git""
# The local path relative to this roo.toml file.
#path = ""src/AspireHost""

title = ""Roo Example""

[[repositories]]
name = ""XXX""
url = ""XXX""
path = ""XXX""                                           
";
}