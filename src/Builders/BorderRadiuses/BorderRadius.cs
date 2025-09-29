using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.BorderRadiuses;

/// <summary>
/// Simplified border radius utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class BorderRadius
{
    /// <summary>
    /// No border radius (0).
    /// </summary>
    public static BorderRadiusBuilder S0 => new(ScaleType.S0.Value);

    /// <summary>
    /// Small border radius (0.25rem).
    /// </summary>
    public static BorderRadiusBuilder Small => new("sm");

    /// <summary>
    /// Default border radius (0.375rem).
    /// </summary>
    public static BorderRadiusBuilder Default => new("");

    /// <summary>
    /// Large border radius (0.5rem).
    /// </summary>
    public static BorderRadiusBuilder Large => new("lg");

    /// <summary>
    /// Extra large border radius (1rem).
    /// </summary>
    public static BorderRadiusBuilder ExtraLarge => new("xl");

    /// <summary>
    /// 2XL border radius (2rem).
    /// </summary>
    public static BorderRadiusBuilder Xxl => new("2xl");

    /// <summary>
    /// Pill border radius (50rem).
    /// </summary>
    public static BorderRadiusBuilder Pill => new("pill");

    /// <summary>
    /// Circle border radius (50%).
    /// </summary>
    public static BorderRadiusBuilder Circle => new("circle");

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
