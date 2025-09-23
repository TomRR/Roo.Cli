using Roo.Cli.Cli.Nuget.Commands;
using Roo.Cli.Commands.Status;

namespace TomRR.Cli.Tooling;

// Auto-generated at compile time
internal static class CommandDispatcher_Binders
{
    public static async Task BindAndRun(ICommand cmd, string[] args)
    {
        switch (cmd)
        {
            case InitCommand init:
                InitCommand_Binder.Bind(init, args);
                await init.RunAsync();
                break;

            case StatusCommand status:
                StatusCommand_Binder.Bind(status, args);
                await status.RunAsync();
                break;
            
            // case VersionCommand version:
            //     VersionCommand_Binder.Bind(version, args);
            //     await version.RunAsync();
            //     break;
            //
            // case HelpCommand help:
            //     HelpCommand_Binder.Bind(help, args);
            //     await help.RunAsync();
            //     break;

            default:
                throw new InvalidOperationException(
                    $"No binder generated for command type {cmd.GetType().FullName}");
        }
    }
}
