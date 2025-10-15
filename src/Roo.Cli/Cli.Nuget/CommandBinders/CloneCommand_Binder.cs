namespace Roo.Cli.Cli.Nuget.CommandBinders;

public static class CloneCommand_Binder
{
    public static void Bind(CloneCommand cmd, string[] args)
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
