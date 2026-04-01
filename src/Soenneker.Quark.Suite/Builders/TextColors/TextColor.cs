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
    public static TextColorBuilder Success => new("success");
    public static TextColorBuilder Warning => new("warning");
    public static TextColorBuilder Info => new("info");
    public static TextColorBuilder PopoverForeground => new("popover-foreground");
    public static TextColorBuilder CardForeground => new("card-foreground");
    public static TextColorBuilder Foreground => new("foreground");
    public static TextColorBuilder White => new("white");
    public static TextColorBuilder Black => new("black");
}
