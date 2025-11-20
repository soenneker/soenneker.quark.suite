using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Color utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class Color
{
    /// <summary>
    /// Gets a color builder with primary theme color.
    /// </summary>
    public static ColorBuilder Primary => new("primary");
    /// <summary>
    /// Gets a color builder with secondary theme color.
    /// </summary>
    public static ColorBuilder Secondary => new("secondary");
    /// <summary>
    /// Gets a color builder with success theme color.
    /// </summary>
    public static ColorBuilder Success => new("success");
    /// <summary>
    /// Gets a color builder with danger theme color.
    /// </summary>
    public static ColorBuilder Danger => new("danger");
    /// <summary>
    /// Gets a color builder with warning theme color.
    /// </summary>
    public static ColorBuilder Warning => new("warning");
    /// <summary>
    /// Gets a color builder with info theme color.
    /// </summary>
    public static ColorBuilder Info => new("info");
    /// <summary>
    /// Gets a color builder with light theme color.
    /// </summary>
    public static ColorBuilder Light => new("light");
    /// <summary>
    /// Gets a color builder with dark theme color.
    /// </summary>
    public static ColorBuilder Dark => new("dark");
    /// <summary>
    /// Gets a color builder with link color.
    /// </summary>
    public static ColorBuilder Link => new("link");
    /// <summary>
    /// Gets a color builder with muted color.
    /// </summary>
    public static ColorBuilder Muted => new("muted");

    /// <summary>
    /// Gets a color builder with white color.
    /// </summary>
    public static ColorBuilder White => new("white");

    /// <summary>
    /// Gets a color builder with black color.
    /// </summary>
    public static ColorBuilder Black => new("black");

    /// <summary>
    /// Gets a color builder with inherit keyword.
    /// </summary>
    public static ColorBuilder Inherit => new(GlobalKeyword.InheritValue);
    /// <summary>
    /// Gets a color builder with initial keyword.
    /// </summary>
    public static ColorBuilder Initial => new(GlobalKeyword.InitialValue);
    /// <summary>
    /// Gets a color builder with revert keyword.
    /// </summary>
    public static ColorBuilder Revert => new(GlobalKeyword.RevertValue);
    /// <summary>
    /// Gets a color builder with revert-layer keyword.
    /// </summary>
    public static ColorBuilder RevertLayer => new(GlobalKeyword.RevertLayerValue);
    /// <summary>
    /// Gets a color builder with unset keyword.
    /// </summary>
    public static ColorBuilder Unset => new(GlobalKeyword.UnsetValue);

    /// <summary>
    /// Create from an arbitrary CSS color value (e.g., "#ff0000", "rgb(10 20 30 / 0.5)").
    /// </summary>
    public static ColorBuilder FromCss(string css) => new(css);
}