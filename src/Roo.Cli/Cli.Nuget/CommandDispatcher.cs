namespace TomRR.Cli.Tooling;
public class CommandDispatcher
{
    private readonly IServiceProvider _provider;
    private readonly Dictionary<string, Type> _commands = new();

    public CommandDispatcher(IServiceProvider provider)
    {
        _provider = provider;
    }

    public void Register<TCommand>(string name, string? shortName = null)
        where TCommand : class, ICommand
    {
        _commands[name] = typeof(TCommand);

        if (!string.IsNullOrWhiteSpace(shortName))
            _commands[shortName!] = typeof(TCommand);
    }

    public async Task DispatchAsync(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("No command specified.");
            Console.WriteLine("Available commands: " + string.Join(", ", _commands.Keys));
            return;
        }
        

        var commandName = args[0];
        if (_commands.TryGetValue(commandName, out var type))
        {
            var cmd = (ICommand)_provider.GetRequiredService(type);

            // var remaining = args.Skip(1).ToArray();
            // await CommandParser.ParseAndRunAsync(cmd, remaining);
            var remaining = args.Skip(1).ToArray();
            CommandDispatcher_Binders.BindAndRun(cmd, remaining);
        }
        else
        {
            Console.WriteLine($"Unknown command: {commandName}");
        }
    }

}
