using Roo.Cli.Features.Commands.Core;

namespace Roo.Cli.Tests.Helper;

public static class RepositoryDtoFactory
{
    public static RepositoryDto Create(
        string? name =  null,
        string? path =  null,
        string? url =  null,
        string? tags =  null,
        string? description =  null) 
        => new(
            name ?? "TestRepo", 
            url ?? "https://example.com/repo.git", 
            path ?? ".", 
            tags ?? "tag1,tag2", 
            description ?? "description");
}