namespace Roo.Cli.Features.Commands.Core;

public interface IRooCommandValidator
{
    public Result<Success, Error> IsRepositoryUrlValid(RepositoryDto repository);
}
public class RooCommandValidator : IRooCommandValidator
{
    public Result<Success, Error> IsRepositoryUrlValid(RepositoryDto repository)
    {
        if (string.IsNullOrWhiteSpace(repository.Url))
        {
            return Error.Validation(Components.Messages.InvalidRepoUrl(repository.Name));
        }
        return Result.Success;
    }
}