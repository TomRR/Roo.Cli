namespace Roo.Cli.Common;
public static class RooTomlReader
{
    public static RooConfig ReadRooConfig(string path)
    {
        var repositories = new List<Repository>();
        Repository? current = null;
        string? title = null;
        Owner? owner = null;

        var lines = File.ReadAllLines(path);
        foreach (var line in lines)
        {
            var trimmed = line.Trim();

            // Skip empty or comment lines
            if (string.IsNullOrEmpty(trimmed) || trimmed.StartsWith("#"))
            {
                continue;
            }
            
            if (trimmed.StartsWith("[[repositories]]", StringComparison.OrdinalIgnoreCase))
            {
                if (current != null)
                    repositories.Add(current);
                current = new Repository();
                {
                    continue;
                }            
            }

            // Parse key/value pairs
            var parts = trimmed.Split('=', 2);
            if (parts.Length != 2)
            {
                continue;
            }
            var key = parts[0].Trim();
            var value = parts[1].Trim().Trim('"');

            if (current == null)
            {
                // We're at top level (e.g., title)
                if (key.Equals("title", StringComparison.OrdinalIgnoreCase))
                {
                    title = value;
                }
                
            }
            else
            {
                current = key switch
                {
                    "name" => current with { Name = value },
                    "url" => current with { Url = value },
                    "path" => current with { Path = value },
                    "tags" => current with { Tags = value },
                    "description" => current with { Description = value },
                    _ => current
                };
            }
        }

        if (current != null)
        {
            repositories.Add(current);
        }
        return new RooConfig { Title = title, Repositories = repositories };
    }

    public static List<string> Validate(RooConfig config)
    {
        var errors = new List<string>();

        if (config.Repositories.Count == 0)
        {
            errors.Add("No repositories defined.");
        }

        for (var i = 0; i < config.Repositories.Count; i++)
        {
            var r = config.Repositories[i];
            var name = string.IsNullOrWhiteSpace(r.Name) ? $"{i + 1}" : r.Name;
            if (string.IsNullOrWhiteSpace(r.Name))
                errors.Add($"Repository '{i + 1}' is missing a name.");
            if (string.IsNullOrWhiteSpace(r.Url))
                errors.Add($"Repository '{name}' is missing a URL.");
            if (string.IsNullOrWhiteSpace(r.Path))
                errors.Add($"Repository '{name}' is missing a path.");
        }

        return errors;
    }
}