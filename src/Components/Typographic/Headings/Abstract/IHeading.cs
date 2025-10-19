namespace Soenneker.Quark;

/// <summary>
/// Represents a dynamic heading component that can render as h1 through h6 based on scale.
/// </summary>
public interface IHeading : IElement
{
    /// <summary>
    /// Gets or sets the scale/level of the heading (1-6), determining which HTML heading tag to render.
    /// </summary>
    CssValue<ScaleBuilder>? Scale { get; set; }

    /// <summary>
    /// Gets or sets the display size for Bootstrap display headings (display-1 through display-6).
    /// </summary>
    CssValue<DisplaySizeBuilder>? DisplaySize { get; set; }
}