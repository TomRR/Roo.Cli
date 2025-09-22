namespace Roo.Cli.Infrastructure.Models;

public static class MapperExtensions
{
    public static RooConfig MapTo(this RooConfigDto source)
    {
        return new RooConfig()
        {
            Title = source.Title,
            Owner = source.Owner?.MapTo(),
            Repositories = source.Repositories?.MapTo() ?? new()
        };
    }

    public static RooConfigDto MapTo(this RooConfig source)
    {
        return new RooConfigDto(source.Title, source.Owner?.MapTo(), source.Repositories?.MapTo() ?? new());
    }

    public static Owner MapTo(this OwnerDto source)
    {
        return new Owner()
        {
            Name = source.Name,
        };
    }

    public static OwnerDto MapTo(this Owner source)
    {
        return new OwnerDto(source.Name);
    }

    public static Repository MapTo(this RepositoryDto source)
    {
        return new Repository
        {
            Name = source.Name,
            Path = source.Path,
            Url = source.Url,
            Tags = source.Tags,
            Description = source.Description
        };
    }
    public static RepositoryDto MapTo(this Repository source)
    {
        return new RepositoryDto(
            source.Name,
            source.Url,
            source.Path,
            source.Tags,
            source.Description
        );
    }
    
    public static List<Repository> MapTo(this IEnumerable<RepositoryDto> source)
    {
        return source.Select(x => x.MapTo()).ToList();
    }
    public static List<RepositoryDto> MapTo(this IEnumerable<Repository> source)
    {
        return source.Select(x => x.MapTo()).ToList();
    }
}