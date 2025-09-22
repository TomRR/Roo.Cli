namespace Roo.Cli.Services;

public static class CliParser
{
    public static CliContext Parse(string[] args)
    {
        var ctx = new CliContext();
        if (args.Length == 0) return ctx;

        ctx.Command = args[0];

        for (int i = 1; i < args.Length; i++)
        {
            var arg = args[i];
            if (arg.StartsWith("--"))
            {
                var parts = arg.Substring(2).Split('=', 2);
                ctx.Options[parts[0]] = parts.Length > 1 ? parts[1] : "true";
            }
            else if (arg.StartsWith("-"))
            {
                ctx.Options[arg.Substring(1)] = "true";
            }
            else
            {
                ctx.Arguments.Add(arg);
            }
        }

        return ctx;
    }
}