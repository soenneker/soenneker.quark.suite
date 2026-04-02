namespace Soenneker.Quark;

/// <summary>
/// Border color utility with fluent API aligned with shadcn/Tailwind semantic tokens.
/// </summary>
public static class BorderColor
{
    public static BorderColorBuilder Primary => new("primary");
    public static BorderColorBuilder Secondary => new("secondary");
    public static BorderColorBuilder Destructive => new("destructive");
    public static BorderColorBuilder Muted => new("muted");
    public static BorderColorBuilder Accent => new("accent");
    public static BorderColorBuilder Popover => new("popover");
    public static BorderColorBuilder Card => new("card");
    public static BorderColorBuilder Background => new("background");
    public static BorderColorBuilder Border => new("border");
    public static BorderColorBuilder Input => new("input");
    public static BorderColorBuilder Ring => new("ring");
    public static BorderColorBuilder White => new("white");
    public static BorderColorBuilder Black => new("black");
    public static BorderColorBuilder Transparent => new("transparent");

    /// <summary>
    /// Creates a border color builder from a Tailwind color token suffix such as <c>primary/30</c>, <c>zinc-300</c>, or <c>[var(--brand)]</c>.
    /// </summary>
    public static BorderColorBuilder Token(string token) => new(token);

    /// <summary>
    /// Passes through a fully-prefixed Tailwind utility such as <c>border-primary/30</c>.
    /// </summary>
    public static BorderColorBuilder Utility(string utility) => new(utility, isUtility: true);
}
