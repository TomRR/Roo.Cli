namespace Roo.Cli.Features.Commands.Core;

public class RooCommandContext(
    IRooLogger logger,
    IRooConfigService config,
    IDirectoryService directory,
    IRooCommandValidator validator)
{
    public IRooLogger Logger { get; } = logger;
    public IRooConfigService Config { get; } = config;
    public IDirectoryService DirectoryService { get; } = directory;
    public IRooCommandValidator Validator { get; } = validator;
}