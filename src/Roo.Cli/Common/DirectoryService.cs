namespace Roo.Cli.Common;

public interface IDirectoryService
{
    string GetCurrentDirectory();
    bool DirectoryExists(string path);
    public Result<Success, Error> DeleteDirectory(string path,  bool recursive = false);
}

public class DirectoryService : IDirectoryService
{
    public string GetCurrentDirectory()
    {
        return Directory.GetCurrentDirectory();
    }

    public bool DirectoryExists(string path)
    {
        return Directory.Exists(path);
    }
    public Result<Success, Error> DeleteDirectory(string path,  bool recursive = false)
    {
        Directory.Delete(path, true);

        if (Directory.Exists(path))
        {
            
        }
        return Result.Success;
    }
}