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
    public static TextColorBuilder Muted => new("muted");
    public static TextColorBuilder MutedForeground => new("muted-foreground");
    public static TextColorBuilder Accent => new("accent");
    public static TextColorBuilder AccentForeground => new("accent-foreground");
    public static TextColorBuilder Popover => new("popover");
    public static TextColorBuilder PopoverForeground => new("popover-foreground");
    public static TextColorBuilder Card => new("card");
    public static TextColorBuilder CardForeground => new("card-foreground");
    public static TextColorBuilder Background => new("background");
    public static TextColorBuilder Foreground => new("foreground");
    public static TextColorBuilder Border => new("border");
    public static TextColorBuilder Input => new("input");
    public static TextColorBuilder Ring => new("ring");

    public static TextColorBuilder Success => new("success");
    public static TextColorBuilder Danger => new("danger");
    public static TextColorBuilder Warning => new("warning");
    public static TextColorBuilder Info => new("info");
    public static TextColorBuilder Light => new("light");
    public static TextColorBuilder Dark => new("dark");
    public static TextColorBuilder White => new("white");
    public static TextColorBuilder Black => new("black");

    public static TextColorBuilder Body => new("body");
    public static TextColorBuilder BodySecondary => new("body-secondary");
    public static TextColorBuilder BodyTertiary => new("body-tertiary");
    public static TextColorBuilder BodyEmphasis => new("body-emphasis");
    public static TextColorBuilder BodyHighlight => new("body-highlight");
    public static TextColorBuilder BodyMuted => new("body-muted");
    public static TextColorBuilder BodyReset => new("body-reset");
    public static TextColorBuilder BodyInverse => new("body-inverse");
    public static TextColorBuilder BodyInverseSecondary => new("body-inverse-secondary");
    public static TextColorBuilder BodyInverseTertiary => new("body-inverse-tertiary");
    public static TextColorBuilder BodyInverseEmphasis => new("body-inverse-emphasis");
    public static TextColorBuilder BodyInverseHighlight => new("body-inverse-highlight");
    public static TextColorBuilder BodyInverseMuted => new("body-inverse-muted");
    public static TextColorBuilder BodyInverseReset => new("body-inverse-reset");
}
