namespace Soenneker.Quark;

/// <summary>
/// Ring color utility with fluent API aligned with shadcn/Tailwind semantic tokens.
/// </summary>
public static class RingColor
{
    public static RingColorBuilder Primary => new("primary");
    public static RingColorBuilder PrimaryForeground => new("primary-foreground");
    public static RingColorBuilder Secondary => new("secondary");
    public static RingColorBuilder SecondaryForeground => new("secondary-foreground");
    public static RingColorBuilder Destructive => new("destructive");
    public static RingColorBuilder DestructiveForeground => new("destructive-foreground");
    public static RingColorBuilder Muted => new("muted");
    public static RingColorBuilder MutedForeground => new("muted-foreground");
    public static RingColorBuilder Accent => new("accent");
    public static RingColorBuilder AccentForeground => new("accent-foreground");
    public static RingColorBuilder Popover => new("popover");
    public static RingColorBuilder PopoverForeground => new("popover-foreground");
    public static RingColorBuilder Card => new("card");
    public static RingColorBuilder CardForeground => new("card-foreground");
    public static RingColorBuilder Background => new("background");
    public static RingColorBuilder Foreground => new("foreground");
    public static RingColorBuilder Border => new("border");
    public static RingColorBuilder Input => new("input");
    public static RingColorBuilder Ring => new("ring");
    public static RingColorBuilder Success => new("success");
    public static RingColorBuilder Warning => new("warning");
    public static RingColorBuilder Info => new("info");
    public static RingColorBuilder White => new("white");
    public static RingColorBuilder Black => new("black");
}
