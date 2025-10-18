namespace Soenneker.Quark;

/// <summary>
/// Represents a visual media component (image or video) with layout and loading properties.
/// </summary>
public interface IVisualMediaElement : IElement
{
    /// <summary>
    /// Gets or sets the source URL of the media.
    /// </summary>
    string? Source { get; set; }

    /// <summary>
    /// Gets or sets the alternative text for accessibility and fallback display.
    /// </summary>
    string? Alt { get; set; }

    /// <summary>
    /// Gets or sets the aspect ratio of the media element.
    /// </summary>
    CssValue<AspectRatioBuilder>? AspectRatio { get; set; }

    /// <summary>
    /// Gets or sets how the media should fit within its container.
    /// </summary>
    CssValue<ObjectFitBuilder>? ObjectFit { get; set; }

    /// <summary>
    /// Gets or sets the position of the media within its container.
    /// </summary>
    CssValue<ObjectPositionBuilder>? ObjectPosition { get; set; }

    /// <summary>
    /// Gets or sets whether the media should be lazy-loaded.
    /// </summary>
    bool Lazy { get; set; }
}

