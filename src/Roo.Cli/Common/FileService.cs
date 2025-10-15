namespace Roo.Cli.Common;

public interface IFileService
{
    public void WriteAllText(string path, string content);
    public bool FileExists(string path);
    public string ReadFile(string path);
    public string? FindSpecFile(string dir);
}
public class FileService : IFileService
{
    public void WriteAllText(string path, string content)
    {
        File.WriteAllText(path, content);
    }

    public bool FileExists(string path)
    {
        return File.Exists(path);
    }
    public string ReadFile(string path)
    {
        return File.Exists(path) ? File.ReadAllText(path) : string.Empty;
    }
    
    public string? FindSpecFile(string path)
    {
        // Prefer "roo.toml"
        var roo = Path.Combine(path, "roo.toml");
        if (FileExists(roo))
        {
            return roo;
        }
        
        // Fallback: any *.toml file
        return Directory
            .EnumerateFiles(path, "*.toml", SearchOption.TopDirectoryOnly)
            .FirstOrDefault();
    }
}