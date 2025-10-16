using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Background color utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class BackgroundColor
{
    public static BackgroundColorBuilder Primary => new("primary");
    public static BackgroundColorBuilder Secondary => new("secondary");
    public static BackgroundColorBuilder Success => new("success");
    public static BackgroundColorBuilder Danger => new("danger");
    public static BackgroundColorBuilder Warning => new("warning");
    public static BackgroundColorBuilder Info => new("info");
    public static BackgroundColorBuilder Light => new("light");
    public static BackgroundColorBuilder Dark => new("dark");
    public static BackgroundColorBuilder Link => new("link");
    public static BackgroundColorBuilder Muted => new("muted");

    public static BackgroundColorBuilder White => new("white");

    public static BackgroundColorBuilder Black => new("black");

    public static BackgroundColorBuilder Transparent => new("transparent");

    public static BackgroundColorBuilder Inherit => new(GlobalKeyword.InheritValue);
    public static BackgroundColorBuilder Initial => new(GlobalKeyword.InitialValue);
    public static BackgroundColorBuilder Revert => new(GlobalKeyword.RevertValue);
    public static BackgroundColorBuilder RevertLayer => new(GlobalKeyword.RevertLayerValue);
    public static BackgroundColorBuilder Unset => new(GlobalKeyword.UnsetValue);

    /// <summary>
    /// Create from an arbitrary CSS color value (e.g., "#ff0000", "rgb(10 20 30 / 0.5)").
    /// </summary>
    public static BackgroundColorBuilder FromCss(string css) => new(css);
}
