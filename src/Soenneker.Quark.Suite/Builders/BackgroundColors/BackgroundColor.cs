using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Background color utility with fluent API aligned with shadcn/Tailwind semantic tokens.
/// </summary>
public static class BackgroundColor
{
    public static BackgroundColorBuilder Primary => new("primary");
    public static BackgroundColorBuilder PrimaryForeground => new("primary-foreground");
    public static BackgroundColorBuilder Secondary => new("secondary");
    public static BackgroundColorBuilder SecondaryForeground => new("secondary-foreground");
    public static BackgroundColorBuilder Destructive => new("destructive");
    public static BackgroundColorBuilder DestructiveForeground => new("destructive-foreground");
    public static BackgroundColorBuilder Muted => new("muted");
    public static BackgroundColorBuilder MutedForeground => new("muted-foreground");
    public static BackgroundColorBuilder Accent => new("accent");
    public static BackgroundColorBuilder AccentForeground => new("accent-foreground");
    public static BackgroundColorBuilder Popover => new("popover");
    public static BackgroundColorBuilder PopoverForeground => new("popover-foreground");
    public static BackgroundColorBuilder Card => new("card");
    public static BackgroundColorBuilder CardForeground => new("card-foreground");
    public static BackgroundColorBuilder Background => new("background");
    public static BackgroundColorBuilder Foreground => new("foreground");
    public static BackgroundColorBuilder Border => new("border");
    public static BackgroundColorBuilder Input => new("input");
    public static BackgroundColorBuilder Ring => new("ring");
    public static BackgroundColorBuilder White => new("white");
    public static BackgroundColorBuilder Black => new("black");
    public static BackgroundColorBuilder Transparent => new("transparent");

    public static BackgroundColorBuilder Inherit => new(GlobalKeyword.Inherit);
    public static BackgroundColorBuilder Initial => new(GlobalKeyword.Initial);
    public static BackgroundColorBuilder Revert => new(GlobalKeyword.Revert);
    public static BackgroundColorBuilder RevertLayer => new(GlobalKeyword.RevertLayer);
    public static BackgroundColorBuilder Unset => new(GlobalKeyword.Unset);

    public static BackgroundColorBuilder FromCss(string css) => new(css);
}
