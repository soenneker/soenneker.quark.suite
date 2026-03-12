namespace Soenneker.Quark;

/// <summary>
/// Static utility class for creating link underline builders with predefined values.
/// </summary>
public static class LinkUnderline
{
    /// <summary>
    /// Gets a link underline builder with default styling.
    /// </summary>
    public static LinkUnderlineBuilder Default => new("", "base");
    
    // Opacity
    /// <summary>
    /// Gets a link underline builder with opacity 0 (fully transparent).
    /// </summary>
    public static LinkUnderlineBuilder Opacity0 => new("0", "opacity");
    /// <summary>
    /// Gets a link underline builder with opacity 10 (10% opacity).
    /// </summary>
    public static LinkUnderlineBuilder Opacity10 => new("10", "opacity");
    /// <summary>
    /// Gets a link underline builder with opacity 25 (25% opacity).
    /// </summary>
    public static LinkUnderlineBuilder Opacity25 => new("25", "opacity");
    /// <summary>
    /// Gets a link underline builder with opacity 50 (50% opacity).
    /// </summary>
    public static LinkUnderlineBuilder Opacity50 => new("50", "opacity");
    /// <summary>
    /// Gets a link underline builder with opacity 75 (75% opacity).
    /// </summary>
    public static LinkUnderlineBuilder Opacity75 => new("75", "opacity");
    /// <summary>
    /// Gets a link underline builder with opacity 100 (fully opaque).
    /// </summary>
    public static LinkUnderlineBuilder Opacity100 => new("100", "opacity");
    
    // Colors
    /// <summary>
    /// Gets a link underline builder with primary theme color.
    /// </summary>
    public static LinkUnderlineBuilder Primary => new("primary", "color");
    /// <summary>
    /// Gets a link underline builder with secondary theme color.
    /// </summary>
    public static LinkUnderlineBuilder Secondary => new("secondary", "color");
    /// <summary>
    /// Gets a link underline builder with success theme color.
    /// </summary>
    public static LinkUnderlineBuilder Success => new("success", "color");
    /// <summary>
    /// Gets a link underline builder with info theme color.
    /// </summary>
    public static LinkUnderlineBuilder Info => new("info", "color");
    /// <summary>
    /// Gets a link underline builder with warning theme color.
    /// </summary>
    public static LinkUnderlineBuilder Warning => new("warning", "color");
    /// <summary>
    /// Gets a link underline builder with danger theme color.
    /// </summary>
    public static LinkUnderlineBuilder Danger => new("danger", "color");
    /// <summary>
    /// Gets a link underline builder with light theme color.
    /// </summary>
    public static LinkUnderlineBuilder Light => new("light", "color");
    /// <summary>
    /// Gets a link underline builder with dark theme color.
    /// </summary>
    public static LinkUnderlineBuilder Dark => new("dark", "color");
}







