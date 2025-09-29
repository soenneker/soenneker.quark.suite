namespace Soenneker.Quark.Builders.Filters;

/// <summary>
/// Simplified filter utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class Filter
{
    /// <summary>
    /// No filter (none).
    /// </summary>
    public static FilterBuilder None => new("none");

    /// <summary>
    /// Blur filter (blur).
    /// </summary>
    public static FilterBuilder Blur => new("blur");

    /// <summary>
    /// Brightness filter (brightness).
    /// </summary>
    public static FilterBuilder Brightness => new("brightness");

    /// <summary>
    /// Contrast filter (contrast).
    /// </summary>
    public static FilterBuilder Contrast => new("contrast");

    /// <summary>
    /// Drop shadow filter (drop-shadow).
    /// </summary>
    public static FilterBuilder DropShadow => new("drop-shadow");

    /// <summary>
    /// Grayscale filter (grayscale).
    /// </summary>
    public static FilterBuilder Grayscale => new("grayscale");

    /// <summary>
    /// Hue rotate filter (hue-rotate).
    /// </summary>
    public static FilterBuilder HueRotate => new("hue-rotate");

    /// <summary>
    /// Invert filter (invert).
    /// </summary>
    public static FilterBuilder Invert => new("invert");

    /// <summary>
    /// Opacity filter (opacity).
    /// </summary>
    public static FilterBuilder Opacity => new("opacity");

    /// <summary>
    /// Saturate filter (saturate).
    /// </summary>
    public static FilterBuilder Saturate => new("saturate");

    /// <summary>
    /// Sepia filter (sepia).
    /// </summary>
    public static FilterBuilder Sepia => new("sepia");
}
