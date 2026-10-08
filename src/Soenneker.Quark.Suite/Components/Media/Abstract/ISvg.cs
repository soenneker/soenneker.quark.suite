namespace Soenneker.Quark;

/// <summary>
/// Interface for the Svg component.
/// </summary>
public interface ISvg : IElement
{
    /// <summary>Gets or sets the stroke width utilities.</summary>
    CssValue<StrokeWidthBuilder>? StrokeWidth { get; set; }

    /// <summary>Gets or sets the Fill utilities, including responsive and state variants.</summary>
    CssValue<FillBuilder>? Fill { get; set; }

    /// <summary>Gets or sets the FillRule utilities, including responsive and state variants.</summary>
    CssValue<FillRuleBuilder>? FillRule { get; set; }

    /// <summary>Gets or sets the Stroke utilities, including responsive and state variants.</summary>
    CssValue<StrokeBuilder>? Stroke { get; set; }

    /// <summary>Gets or sets the StrokeLineCap utilities, including responsive and state variants.</summary>
    CssValue<StrokeLineCapBuilder>? StrokeLineCap { get; set; }

    /// <summary>Gets or sets the StrokeLineJoin utilities, including responsive and state variants.</summary>
    CssValue<StrokeLineJoinBuilder>? StrokeLineJoin { get; set; }

    /// <summary>Gets or sets the SvgStrokeWidth utilities, including responsive and state variants.</summary>
    CssValue<StrokeWidthBuilder>? SvgStrokeWidth { get; set; }

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
