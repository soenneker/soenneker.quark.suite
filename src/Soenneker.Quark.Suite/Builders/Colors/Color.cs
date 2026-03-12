using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Generic color utility aligned with shadcn/Tailwind semantic tokens.
/// </summary>
public static class Color
{
    public static ColorBuilder Primary => new("primary");
    public static ColorBuilder PrimaryForeground => new("primary-foreground");
    public static ColorBuilder Secondary => new("secondary");
    public static ColorBuilder SecondaryForeground => new("secondary-foreground");
    public static ColorBuilder Destructive => new("destructive");
    public static ColorBuilder DestructiveForeground => new("destructive-foreground");
    public static ColorBuilder Muted => new("muted");
    public static ColorBuilder MutedForeground => new("muted-foreground");
    public static ColorBuilder Accent => new("accent");
    public static ColorBuilder AccentForeground => new("accent-foreground");
    public static ColorBuilder Popover => new("popover");
    public static ColorBuilder PopoverForeground => new("popover-foreground");
    public static ColorBuilder Card => new("card");
    public static ColorBuilder CardForeground => new("card-foreground");
    public static ColorBuilder Background => new("background");
    public static ColorBuilder Foreground => new("foreground");
    public static ColorBuilder Border => new("border");
    public static ColorBuilder Input => new("input");
    public static ColorBuilder Ring => new("ring");

    public static ColorBuilder Success => new("success");
    public static ColorBuilder Danger => new("danger");
    public static ColorBuilder Warning => new("warning");
    public static ColorBuilder Info => new("info");
    public static ColorBuilder Light => new("light");
    public static ColorBuilder Dark => new("dark");
    public static ColorBuilder Link => new("link");
    public static ColorBuilder White => new("white");
    public static ColorBuilder Black => new("black");

    public static ColorBuilder Inherit => new(GlobalKeyword.InheritValue);
    public static ColorBuilder Initial => new(GlobalKeyword.InitialValue);
    public static ColorBuilder Revert => new(GlobalKeyword.RevertValue);
    public static ColorBuilder RevertLayer => new(GlobalKeyword.RevertLayerValue);
    public static ColorBuilder Unset => new(GlobalKeyword.UnsetValue);

    public static ColorBuilder FromCss(string css) => new(css);
}
