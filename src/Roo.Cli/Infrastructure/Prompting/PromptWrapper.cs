namespace Roo.Cli.Infrastructure.Prompting;

public interface IPromptWrapper
{
    public bool AskYesNo(string question);
    public string SingleSelect(string question, string[] choices);

    public List<string> MultiSelect(string question, string[] choices, int pageSize, string moreChoicesText,
        string instructionsText);
    public PromptAnswer PromptYesNo(string message, string defaultValue = "n");
}
public sealed class PromptWrapper : IPromptWrapper
{
    public string AskForString(string question) 
        => AnsiConsole.Prompt(new TextPrompt<string>(question));
    
    public int AskForInt(string question) 
        => AnsiConsole.Prompt(new TextPrompt<int>(question));

    public void AskYesNo(string question, string[] choices, string defaultAnswer) 
        => AnsiConsole.Prompt(
            new TextPrompt<string>(question)
                .AddChoices(choices)
                .DefaultValue(defaultAnswer));
    
    public void AskOptionalQuestion(string question) 
        => AnsiConsole.Prompt(
            new TextPrompt<string>(question)
                .AllowEmpty());

    public bool AskYesNo(string question)
    {
        
        var confirmation = AnsiConsole.Prompt(
            new TextPrompt<bool>("Run prompt example?")
                .AddChoice(true)
                .AddChoice(false)
                .DefaultValue(false)
                .WithConverter(choice => choice ? "yes" : "no"));
                // .WithConverter(choice => choice ? "y" : "n"));
        
        Console.WriteLine(confirmation ? "Confirmed" : "Declined");
        return confirmation;
    }

    
    private static readonly HashSet<string> YesOptions = new(StringComparer.OrdinalIgnoreCase) { "y", "ye", "yes" };
    private static readonly HashSet<string> NoOptions = new(StringComparer.OrdinalIgnoreCase) { "n", "no" };
    public PromptAnswer PromptYesNo(string message, string defaultValue = "n")
    {
        var messageLegend = YesOptions.Contains(defaultValue)
            ? "[[Y/n]]"
            : "[[y/N]]";
    
        var input = AnsiConsole.Prompt(
            new TextPrompt<string>($"{message} {messageLegend}")
                .DefaultValue(defaultValue)
                .Validate(value =>
                {
                    if (YesOptions.Contains(value) ||
                        NoOptions.Contains(value))
                    {
                        return ValidationResult.Success();
                    }
                    
                    return ValidationResult.Error("Please enter 'yes/y' or 'no/n':");
                })
        );
    
        return YesOptions.Contains(input) 
            ? PromptAnswer.Yes 
            : PromptAnswer.No;
    }


    public string SingleSelect(string question, string[] choices, int pageSize, string moreChoicesText)
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title($"{question}[/]?")
                .PageSize(10)
                .MoreChoicesText(MoreChoicesText)
                .AddChoices(choices));
    }
    
    public List<string> MultiSelect(string question, string[] choices, int pageSize = DefaultPageSize, string moreChoicesText = MoreChoicesText, string instructionsText = InstructionsText)
    {
        return AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title($"{question}[/]?")
                .PageSize(pageSize)
                .MoreChoicesText(moreChoicesText)
                .InstructionsText(instructionsText)
                .AddChoices(choices));
    }
}