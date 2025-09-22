namespace Roo.Cli.Infrastructure.Prompting;

public interface IRooPrompt
{
    public PromptAnswersType AskYesNo(string message, string defaultValue = "n");
}
public class RooPrompt(IPromptWrapper promptWrapper) : IRooPrompt
{
    private readonly IPromptWrapper _promptWrapper = promptWrapper ?? throw new ArgumentNullException(nameof(promptWrapper));

    private const string MoreChoicesText = "[grey](Move up and down to reveal more options)[/]";
    private const int DefaultPageSize = 10;
    private const string InstructionsText = "[grey](Press [blue]<space>[/] to toggle a selection, [green]<enter>[/] to accept)[/]";
    
    private static readonly HashSet<string> YesOptions = new(StringComparer.OrdinalIgnoreCase) { "y", "ye", "yes" };
    private static readonly HashSet<string> NoOptions = new(StringComparer.OrdinalIgnoreCase) { "n", "no" };
    private const string YesNoValidationErrorMessage = "Please enter 'yes/y' or 'no/n':";
    public PromptAnswersType AskYesNo(string message, string defaultValue = "n")
    {
        var messageLegend = YesOptions.Contains(defaultValue)
            ? "[[Y/n]]"
            : "[[y/N]]";
        var input = _promptWrapper.AskWithValidation(
            message + messageLegend,
            defaultValue,
            value => YesOptions.Contains(value) || NoOptions.Contains(value),
            YesNoValidationErrorMessage
        );
        return YesOptions.Contains(input) 
            ? PromptAnswersType.Yes 
            : PromptAnswersType.No;
    }
    
    public List<string> MultiSelect(string question, string[] choices, int pageSize, string moreChoicesText, string instructionsText)
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