namespace Soenneker.Quark;

/// <summary>
/// Background color utility with fluent API aligned with shadcn/Tailwind semantic tokens.
/// </summary>
public static class BackgroundColor
{
    public static BackgroundColorBuilder Primary => new("primary");
    public static BackgroundColorBuilder Secondary => new("secondary");
    public static BackgroundColorBuilder Destructive => new("destructive");
    public static BackgroundColorBuilder Muted => new("muted");
    public static BackgroundColorBuilder Accent => new("accent");
    public static BackgroundColorBuilder Popover => new("popover");
    public static BackgroundColorBuilder Card => new("card");
    public static BackgroundColorBuilder Background => new("background");
    public static BackgroundColorBuilder White => new("white");
    public static BackgroundColorBuilder Black => new("black");
    public static BackgroundColorBuilder Transparent => new("transparent");

    /// <summary>
    /// Creates a background color builder from a Tailwind color token suffix such as <c>primary/20</c>, <c>zinc-900</c>, or <c>[var(--brand)]</c>.
    /// </summary>
    public static BackgroundColorBuilder Token(string token) => new(token);

    /// <summary>
    /// Passes through a fully-prefixed Tailwind utility such as <c>bg-primary/20</c>.
    /// </summary>
    public static BackgroundColorBuilder Utility(string utility) => new(utility, isUtility: true);
}