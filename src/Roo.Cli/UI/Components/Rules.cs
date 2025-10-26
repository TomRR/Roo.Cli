namespace Roo.Cli.UI.Components;

public static partial class Components
{
    public static class Rules
    {
        public static Rule InitCommandRule()
            => new($"[yellow]{Icons.RocketIcon} Init Roo[/]");
        public static Rule CloneCommandRule()
            => new Rule($"[yellow]{Icons.RocketIcon} Clone[/]");
        public static Rule StatusCommandRule()
            => new Rule($"[yellow]{Icons.StatusIcon} Status[/]");
        public static Rule CommitCommandRule()
            => new Rule($"[yellow]{Icons.CommitIcon} Status[/]");
        public static Rule FetchCommandRule()
            => new Rule($"[yellow]{Icons.FetchIcon} Fetch[/]");
        public static Rule PullCommandRule()
            => new Rule($"[yellow]{Icons.PullIcon} Pull[/]");
        public static Rule PushCommandRule()
            => new Rule($"[yellow]{Icons.PushIcon} Push[/]");
        public static Rule AddCommandRule()
            => new Rule($"[yellow]{Icons.AddIcon} Add[/]");
        public static Rule CheckoutCommandRule()
            => new Rule($"[yellow]{Icons.BranchIcon} Switch Branch[/]");
        public static Rule StashCommandRule()
            => new Rule($"[yellow]{Icons.StashIcon} Stash[/]");
    
    
        public static Rule EmptyRule()
            => new Rule();
        public static Rule GreyDimRule()
            => new Rule().RuleStyle("grey dim");

        public static Rule GetCloneStatisticRule()
            => new Rule($"{Icons.StatisticIcon} Clone Summary").LeftJustified();
    }
}