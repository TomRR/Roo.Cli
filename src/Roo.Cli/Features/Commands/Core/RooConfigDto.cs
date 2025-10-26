namespace Roo.Cli.Features.Commands.Core;

public record RooConfigDto(string? Title, OwnerDto? Owner, List<RepositoryDto> Repositories);

public record OwnerDto(string? Name);

public record RepositoryDto(string Name, string Url, string Path, string? Tags, string? Description);