using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Simplified border radius utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class BorderRadius
{
    /// <summary>
    /// No border radius (0).
    /// </summary>
    public static BorderRadiusBuilder Is0 => new(ScaleType.Is0.Value);

    /// <summary>
    /// Default border radius. Generates 'rounded' class.
    /// </summary>
    public static BorderRadiusBuilder Default => new("");

    /// <summary>
    /// Pill border radius (50rem). Generates 'rounded-pill' class.
    /// </summary>
    public static BorderRadiusBuilder Pill => new("pill");

    /// <summary>
    /// Circle border radius (50%). Generates 'rounded-circle' class.
    /// </summary>
    public static BorderRadiusBuilder Circle => new("circle");

    // ----- Numbered scale (Bootstrap 5.3+) -----
    /// <summary>
    /// Border radius 1. Generates 'rounded-1' class.
    /// </summary>
    public static BorderRadiusBuilder Is1 => new("1");

    /// <summary>
    /// Border radius 2. Generates 'rounded-2' class.
    /// </summary>
    public static BorderRadiusBuilder Is2 => new("2");

    /// <summary>
    /// Border radius 3. Generates 'rounded-3' class.
    /// </summary>
    public static BorderRadiusBuilder Is3 => new("3");

    /// <summary>
    /// Border radius 4. Generates 'rounded-4' class.
    /// </summary>
    public static BorderRadiusBuilder Is4 => new("4");

    /// <summary>
    /// Border radius 5. Generates 'rounded-5' class.
    /// </summary>
    public static BorderRadiusBuilder Is5 => new("5");

    // ----- Corner-specific builders -----
    /// <summary>
    /// Top-left corner border radius builder.
    /// </summary>
    public static BorderRadiusBuilder TopLeft => new("", null, "tl");

    /// <summary>
    /// Top-right corner border radius builder.
    /// </summary>
    public static BorderRadiusBuilder TopRight => new("", null, "tr");

    /// <summary>
    /// Bottom-left corner border radius builder.
    /// </summary>
    public static BorderRadiusBuilder BottomLeft => new("", null, "bl");

    /// <summary>
    /// Bottom-right corner border radius builder.
    /// </summary>
    public static BorderRadiusBuilder BottomRight => new("", null, "br");
}
