namespace Soenneker.Quark;

/// <summary>
/// Represents a surface component with visual styling properties.
/// </summary>
public interface ISurfaceElement : IElement
{
    /// <summary>
    /// Gets or sets the HTML tag to render (e.g., "div", "section", "article").
    /// </summary>
    string Tag { get; set; }

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
}