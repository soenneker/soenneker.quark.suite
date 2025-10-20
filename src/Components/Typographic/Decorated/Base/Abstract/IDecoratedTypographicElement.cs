namespace Soenneker.Quark;

/// <summary>
/// Interface for decorated typographic elements that add visual styling to text.
/// Extends ITypographicElement with background, border, radius, shadow, padding, and opacity.
/// Used for badges, pills, labels, and similar decorated text elements.
/// </summary>
public interface IDecoratedTypographicElement : ITypographicElement
{
    /// <summary>
    /// Gets or sets the border radius for rounded corners.
    /// </summary>
    CssValue<BorderRadiusBuilder>? BorderRadius { get; set; }

    /// <summary>
    /// Gets or sets the box shadow for depth and elevation effects.
    /// </summary>
    CssValue<BoxShadowBuilder>? BoxShadow { get; set; }

    /// <summary>
    /// Gets or sets the background opacity.
    /// </summary>
    CssValue<BackgroundOpacityBuilder>? BackgroundOpacity { get; set; }

    /// <summary>
    /// Gets or sets the border opacity.
    /// </summary>
    CssValue<BorderOpacityBuilder>? BorderOpacity { get; set; }

    /// <summary>
    /// Gets or sets the display size (Bootstrap display heading classes).
    /// </summary>
    CssValue<DisplaySizeBuilder>? DisplaySize { get; set; }
}