using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Background color utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class BackgroundColor
{
    /// <summary>
    /// Gets a background color builder with primary theme color.
    /// </summary>
    public static BackgroundColorBuilder Primary => new("primary");
    /// <summary>
    /// Gets a background color builder with secondary theme color.
    /// </summary>
    public static BackgroundColorBuilder Secondary => new("secondary");
    /// <summary>
    /// Gets a background color builder with success theme color.
    /// </summary>
    public static BackgroundColorBuilder Success => new("success");
    /// <summary>
    /// Gets a background color builder with danger theme color.
    /// </summary>
    public static BackgroundColorBuilder Danger => new("danger");
    /// <summary>
    /// Gets a background color builder with warning theme color.
    /// </summary>
    public static BackgroundColorBuilder Warning => new("warning");
    /// <summary>
    /// Gets a background color builder with info theme color.
    /// </summary>
    public static BackgroundColorBuilder Info => new("info");
    /// <summary>
    /// Gets a background color builder with light theme color.
    /// </summary>
    public static BackgroundColorBuilder Light => new("light");
    /// <summary>
    /// Gets a background color builder with dark theme color.
    /// </summary>
    public static BackgroundColorBuilder Dark => new("dark");
    /// <summary>
    /// Gets a background color builder with link color.
    /// </summary>
    public static BackgroundColorBuilder Link => new("link");
    /// <summary>
    /// Gets a background color builder with muted color.
    /// </summary>
    public static BackgroundColorBuilder Muted => new("muted");

    /// <summary>
    /// Gets a background color builder with white color.
    /// </summary>
    public static BackgroundColorBuilder White => new("white");

    /// <summary>
    /// Gets a background color builder with black color.
    /// </summary>
    public static BackgroundColorBuilder Black => new("black");

    /// <summary>
    /// Gets a background color builder with transparent color.
    /// </summary>
    public static BackgroundColorBuilder Transparent => new("transparent");

    /// <summary>
    /// Gets a background color builder with inherit keyword.
    /// </summary>
    public static BackgroundColorBuilder Inherit => new(GlobalKeyword.InheritValue);
    /// <summary>
    /// Gets a background color builder with initial keyword.
    /// </summary>
    public static BackgroundColorBuilder Initial => new(GlobalKeyword.InitialValue);
    /// <summary>
    /// Gets a background color builder with revert keyword.
    /// </summary>
    public static BackgroundColorBuilder Revert => new(GlobalKeyword.RevertValue);
    /// <summary>
    /// Gets a background color builder with revert-layer keyword.
    /// </summary>
    public static BackgroundColorBuilder RevertLayer => new(GlobalKeyword.RevertLayerValue);
    /// <summary>
    /// Gets a background color builder with unset keyword.
    /// </summary>
    public static BackgroundColorBuilder Unset => new(GlobalKeyword.UnsetValue);

    /// <summary>
    /// Create from an arbitrary CSS color value (e.g., "#ff0000", "rgb(10 20 30 / 0.5)").
    /// </summary>
    public static BackgroundColorBuilder FromCss(string css) => new(css);
}
