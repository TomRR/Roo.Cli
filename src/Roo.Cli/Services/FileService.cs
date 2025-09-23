namespace Roo.Cli.Services;

public interface IFileService
{
    public void WriteFile(string path, string content);
    public string ReadFile(string path);
}
public class FileService : IFileService
{
    public void WriteFile(string path, string content)
    {
        File.WriteAllText(path, content);
    }

    public string ReadFile(string path)
    {
        return File.Exists(path) ? File.ReadAllText(path) : string.Empty;
    }
}