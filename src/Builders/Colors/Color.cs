using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Color utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class Color
{
    public static ColorBuilder Primary => new("primary");
    public static ColorBuilder Secondary => new("secondary");
    public static ColorBuilder Success => new("success");
    public static ColorBuilder Danger => new("danger");
    public static ColorBuilder Warning => new("warning");
    public static ColorBuilder Info => new("info");
    public static ColorBuilder Light => new("light");
    public static ColorBuilder Dark => new("dark");
    public static ColorBuilder Link => new("link");
    public static ColorBuilder Muted => new("muted");

    public static ColorBuilder Inherit => new(GlobalKeyword.InheritValue);
    public static ColorBuilder Initial => new(GlobalKeyword.InitialValue);
    public static ColorBuilder Revert => new(GlobalKeyword.RevertValue);
    public static ColorBuilder RevertLayer => new(GlobalKeyword.RevertLayerValue);
    public static ColorBuilder Unset => new(GlobalKeyword.UnsetValue);

    /// <summary>
    /// Create from an arbitrary CSS color value (e.g., "#ff0000", "rgb(10 20 30 / 0.5)").
    /// </summary>
    public static ColorBuilder FromCss(string css) => new(css);
}


