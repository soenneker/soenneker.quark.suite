namespace Soenneker.Quark;

/// <summary>
/// Interface for the Svg component.
/// </summary>
public interface ISvg : IElement
{
    /// <summary>
    /// Gets or sets the SVG namespace attribute.
    /// </summary>
    string? Xmlns { get; set; }

    /// <summary>
    /// Gets or sets the SVG viewBox attribute.
    /// </summary>
    string? ViewBox { get; set; }

    /// <summary>
    /// Gets or sets the native SVG width attribute.
    /// </summary>
    string? NativeWidth { get; set; }

    /// <summary>
    /// Gets or sets the native SVG height attribute.
    /// </summary>
    string? NativeHeight { get; set; }

    /// <summary>
    /// Gets or sets the preserveAspectRatio attribute.
    /// </summary>
    string? PreserveAspectRatio { get; set; }

    /// <summary>
    /// Gets or sets the focusable attribute.
    /// </summary>
    bool? Focusable { get; set; }
}
