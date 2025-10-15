namespace Roo.Cli.Common;

public interface IRooConfigRetriever
{
    public Result<RooConfig, Error> RetrieveRooConfig();
}

public class RooConfigRetriever : IRooConfigRetriever
{
    private readonly IFileService _fileService;
    private readonly IDirectoryService _directoryService;

    public RooConfigRetriever(IDirectoryService directoryService, IFileService fileService)
    {
        _directoryService = directoryService ?? throw new ArgumentNullException(nameof(directoryService));
        _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
    }
    public Result<RooConfig, Error> RetrieveRooConfig()
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
}