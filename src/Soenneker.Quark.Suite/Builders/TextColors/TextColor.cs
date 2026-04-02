namespace Soenneker.Quark;

/// <summary>
/// Text color utility with fluent API aligned with shadcn/Tailwind semantic tokens.
/// </summary>
public static class TextColor
{
    public static TextColorBuilder Primary => new("primary");
    public static TextColorBuilder PrimaryForeground => new("primary-foreground");
    public static TextColorBuilder Secondary => new("secondary");
    public static TextColorBuilder SecondaryForeground => new("secondary-foreground");
    public static TextColorBuilder Destructive => new("destructive");
    public static TextColorBuilder DestructiveForeground => new("destructive-foreground");
    public static TextColorBuilder MutedForeground => new("muted-foreground");
    public static TextColorBuilder Accent => new("accent");
    public static TextColorBuilder AccentForeground => new("accent-foreground");
    public static TextColorBuilder PopoverForeground => new("popover-foreground");
    public static TextColorBuilder CardForeground => new("card-foreground");
    public static TextColorBuilder Foreground => new("foreground");
    public static TextColorBuilder White => new("white");
    public static TextColorBuilder Black => new("black");

    /// <summary>
    /// Creates a text color builder from a Tailwind color token suffix such as <c>primary/80</c>, <c>zinc-700</c>, or <c>[var(--brand)]</c>.
    /// </summary>
    public static TextColorBuilder Token(string token) => new(token);

    /// <summary>
    /// Passes through a fully-prefixed Tailwind utility such as <c>text-primary/80</c>.
    /// </summary>
    public static TextColorBuilder Utility(string utility) => new(utility, isUtility: true);
}
