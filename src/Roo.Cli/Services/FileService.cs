namespace Roo.Cli.Services;

public static class FileService
{
    public static void WriteFile(string path, string content)
    {
        File.WriteAllText(path, content);
    }

    public static string ReadFile(string path)
    {
        return File.Exists(path) ? File.ReadAllText(path) : string.Empty;
    }
}