namespace Roo.Cli.Infrastructure.Config;

public interface IRooConfigService
{
    public Result<RooConfig, Error> GetRooConfig();
    public Result<string, Error> InitRooConfig(string filePath, bool? force);

}

public class RooConfigService : IRooConfigService
{
    private readonly IFileService _fileService;
    private readonly IDirectoryService _directoryService;

    public RooConfigService(IDirectoryService directoryService, IFileService fileService)
    {
        _directoryService = directoryService ?? throw new ArgumentNullException(nameof(directoryService));
        _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
    }

    public Result<RooConfig, Error> GetRooConfig()
    {
        var currentDirectory = _directoryService.GetCurrentDirectory();
        var configFile = _fileService.FindSpecFile(currentDirectory);

        if (configFile == null)
        {
            return Error.NotFound("[bold yellow]Status[/] [red]Config not found.[/]");
        }
        
        var s = _fileService.ReadFile(configFile);
        var rooConfig = RooTomlReader.ReadRooConfig(configFile);
        var validate = RooTomlReader.Validate(rooConfig);
        
        if (validate.Count is 0)
        {
            return rooConfig;
        }
        
        var message = string.Join(Environment.NewLine, validate.Select(r => $"[red]{r}[/]"));
        return Error.Validation(message);

    }
    
    public Result<string, Error> InitRooConfig(string filePath, bool? force)
    {
        var configFilePath = Path.Combine(filePath, InitConfigFileName);

        if (_fileService.FileExists(configFilePath))
        {
            if (force is false)
            {
                return Error.Conflict("roo.toml already exists!");
            }
            _fileService.Delete(configFilePath);
        }

        _fileService.WriteAllText(configFilePath, InitConfigFileContent);
        return $"roo.toml created at Path={configFilePath}";
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