# **Guide: Adding a New CLI Command (`{NEWCOMMAND}`) in Roo.Cli**

This guide explains how to create a new CLI command in the Roo project, including command definition, action logic, options/arguments, and dependency injection.

---

## **Step 1: Create Command Directory**

Create a new folder for your command:

```
src/Roo.Cli/Commands/{NEWCOMMAND}
```

---

## **Step 2: Create `{NEWCOMMAND}Command.cs`**

File: `src/Roo.Cli/Commands/{NEWCOMMAND}/{NEWCOMMAND}Command.cs`

```csharp
namespace Roo.Cli.Commands.{NEWCOMMAND};

[Command(nameof(CommandName))]
public class {NEWCOMMAND}Command : RooCommandBase
{
    private const string CommandName = "{NEWCOMMAND}";
    private readonly ICommandAction<{NEWCOMMAND}Command> _action;
    private readonly IRooLogger _logger;

    public {NEWCOMMAND}Command(RooCommandContext context, ICommandAction<{NEWCOMMAND}Command> action)
        : base(context)
    {
        _logger = context.Logger ?? throw new ArgumentNullException(nameof(context.Logger));
        _action = action ?? throw new ArgumentNullException(nameof(action));
    }

    // Define options or arguments here
    // Example:
    // [Option("--set-upstream", hasValue: false)]
    // public bool SetUpstream { get; set; }
    //
    // [Argument(0, ofOption: nameof(SetUpstream))]
    // public string? UpstreamRemote { get; set; }

    public override async Task RunAsync()
    {
        _logger.Log(LoggingComponents.AddCommandRule());
        await WithRooConfigAsync(RunCommandPerRepositoryAsync);
    }

    private async Task RunCommandPerRepositoryAsync(RepositoryDto repository)
    {
        _logger.Log(LoggingComponents.Get{NEWCOMMAND}ingWithRepoName(repository.Name));

        var args = BuildCommand();
        var request = RooCommandRequest.Create(repository, args);
        var commandResult = await _action.RunCommandAsync(request);

        if (commandResult.HasError)
        {
            _logger.LogError(commandResult.Error);
            _logger.Log(LoggingComponents.GreyDimRule());
            return;
        }

        _logger.Log($"{Icons.GreenDotIcon} {commandResult.Value.StandardOutput}");
        _logger.Log(LoggingComponents.GreyDimRule());
    }

    private IReadOnlyList<string> BuildCommand()
    {
        var builder = CliCommandBuilder.Create()
            .Add(CommandName);

        return builder.Build();
    }
}
```

---

## **Step 3: Define Options and Arguments**

Use attributes for type-safe options and arguments:

```csharp
[Option("--set-upstream", hasValue: false)]
public bool SetUpstream { get; set; }

[Argument(0, ofOption: nameof(SetUpstream))]
public string? UpstreamRemote { get; set; }
```

**Attributes reference:**

```csharp
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class ArgumentAttribute : Attribute
{
    public int Position { get; }
    public string? OfOption { get; }

    public ArgumentAttribute(int position, string? ofOption = null)
    {
        Position = position;
        OfOption = ofOption;
    }
}

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class OptionAttribute : Attribute
{
    public string LongName { get; }
    public string? ShortName { get; }
    public bool HasValue { get; }
    public string? Description { get; }

    public OptionAttribute(string longName, string? shortName = null, bool hasValue = true, string? description = null)
    {
        LongName = longName;
        ShortName = shortName;
        HasValue = hasValue;
        Description = description;
    }
}
```

---

## **Step 4: Create `{NEWCOMMAND}CommandAction.cs`**

File: `src/Roo.Cli/Commands/{NEWCOMMAND}/{NEWCOMMAND}CommandAction.cs`

```csharp
namespace Roo.Cli.Commands.{NEWCOMMAND};

public class {NEWCOMMAND}CommandAction : ICommandAction<{NEWCOMMAND}Command>
{
    public async Task<Result<RooCommandResponse, Error, Skipped>> RunCommandAsync(RooCommandRequest request)
    {
        try
        {
            var result = await CliWrapFactory.GitCliCommand()
                .WithArguments(request.Arguments)
                .WithWorkingDirectory(request.Repository.GetLocalRepoPath())
                .ExecuteBufferedAsync();

            var response = RooCommandResponse.Create(
                request.Repository,
                result.StandardOutput,
                result.StandardError);

            return response;
        }
        catch (Exception e)
        {
            return Error.Conflict(e.Message);
        }
    }
}
```

---

## **Step 5: Adjust Dependency Injection**

### 1. Register Command Action

In `AddRooDependencies`:

```csharp
services.AddSingleton<ICommandAction<{NEWCOMMAND}Command>, {NEWCOMMAND}CommandAction>();
```

Place **before** the `return services;` line.

### 2. Register Command

In `AddRooCommands`:

```csharp
builder.AddCommand<{NEWCOMMAND}Command>("{NEWCOMMAND}");
```

Place **before** the `return builder;` line.