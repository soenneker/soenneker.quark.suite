namespace Soenneker.Quark.Builders.BackdropFilters;

/// <summary>
/// Simplified backdrop filter utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class BackdropFilter
{
    /// <summary>
    /// No backdrop filter (none).
    /// </summary>
    public static BackdropFilterBuilder None => new("none");

    /// <summary>
    /// Backdrop blur filter (blur).
    /// </summary>
    public static BackdropFilterBuilder Blur => new("blur");

    /// <summary>
    /// Backdrop brightness filter (brightness).
    /// </summary>
    public static BackdropFilterBuilder Brightness => new("brightness");

    /// <summary>
    /// Backdrop contrast filter (contrast).
    /// </summary>
    public static BackdropFilterBuilder Contrast => new("contrast");

    /// <summary>
    /// Backdrop grayscale filter (grayscale).
    /// </summary>
    public static BackdropFilterBuilder Grayscale => new("grayscale");

    /// <summary>
    /// Backdrop hue rotate filter (hue-rotate).
    /// </summary>
    public static BackdropFilterBuilder HueRotate => new("hue-rotate");

    /// <summary>
    /// Backdrop invert filter (invert).
    /// </summary>
    public static BackdropFilterBuilder Invert => new("invert");

    /// <summary>
    /// Backdrop opacity filter (opacity).
    /// </summary>
    public static BackdropFilterBuilder Opacity => new("opacity");

    /// <summary>
    /// Backdrop saturate filter (saturate).
    /// </summary>
    public static BackdropFilterBuilder Saturate => new("saturate");

    /// <summary>
    /// Backdrop sepia filter (sepia).
    /// </summary>
    public static BackdropFilterBuilder Sepia => new("sepia");
}
