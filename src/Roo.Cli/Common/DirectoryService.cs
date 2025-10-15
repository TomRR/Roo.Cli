namespace Roo.Cli.Common;

public interface IDirectoryService
{
    string GetCurrentDirectory();
    bool DirectoryExists(string path);
}

public class DirectoryService : IDirectoryService
{
    public string GetCurrentDirectory()
    {
        return Directory.GetCurrentDirectory();
    }

    public bool DirectoryExists(string path)
    {
        throw new NotImplementedException();
    }
}