namespace Roo.Cli.Cli.Nuget.CommandBinders;

public static class StatusCommand_Binder
{
    public static void Bind(StatusCommand cmd, string[] args)
    {
        for (var i = 0; i < args.Length; i++)
        {
            var token = args[i];

            switch (token)
            {
                case "--interactive":
                case "-i":
                    cmd.Interactive = true;
                    break;
            }
        }
    }
}
