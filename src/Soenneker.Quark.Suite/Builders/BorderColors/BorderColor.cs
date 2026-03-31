namespace Soenneker.Quark;

/// <summary>
/// Border color utility with fluent API aligned with shadcn/Tailwind semantic tokens.
/// </summary>
public static class BorderColor
{
    public static BorderColorBuilder Primary => new("primary");
    public static BorderColorBuilder PrimaryForeground => new("primary-foreground");
    public static BorderColorBuilder Secondary => new("secondary");
    public static BorderColorBuilder SecondaryForeground => new("secondary-foreground");
    public static BorderColorBuilder Destructive => new("destructive");
    public static BorderColorBuilder DestructiveForeground => new("destructive-foreground");
    public static BorderColorBuilder Muted => new("muted");
    public static BorderColorBuilder MutedForeground => new("muted-foreground");
    public static BorderColorBuilder Accent => new("accent");
    public static BorderColorBuilder AccentForeground => new("accent-foreground");
    public static BorderColorBuilder Popover => new("popover");
    public static BorderColorBuilder PopoverForeground => new("popover-foreground");
    public static BorderColorBuilder Card => new("card");
    public static BorderColorBuilder CardForeground => new("card-foreground");
    public static BorderColorBuilder Background => new("background");
    public static BorderColorBuilder Foreground => new("foreground");
    public static BorderColorBuilder Border => new("border");
    public static BorderColorBuilder Input => new("input");
    public static BorderColorBuilder Ring => new("ring");
    public static BorderColorBuilder White => new("white");
    public static BorderColorBuilder Black => new("black");
}
