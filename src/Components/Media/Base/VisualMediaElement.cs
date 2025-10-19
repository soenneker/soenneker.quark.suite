using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Common base for visual media components (images, videos).
/// Provides properties for src, alt, aspect ratio, object-fit, and object-position.
/// </summary>
public abstract class VisualMediaElement : Element, IVisualMediaElement
{
    /// <summary>
    /// Gets or sets the source URL of the media.
    /// </summary>
    [Parameter]
    public string? Source { get; set; }

    /// <summary>
    /// Gets or sets the alternative text for accessibility and fallback display.
    /// </summary>
    [Parameter]
    public string? Alt { get; set; }

    /// <summary>
    /// Gets or sets the aspect ratio of the media element.
    /// </summary>
    [Parameter]
    public CssValue<AspectRatioBuilder>? AspectRatio { get; set; }

    /// <summary>
    /// Gets or sets how the media should fit within its container.
    /// </summary>
    [Parameter]
    public CssValue<ObjectFitBuilder>? ObjectFit { get; set; }

    /// <summary>
    /// Gets or sets the position of the media within its container.
    /// </summary>
    [Parameter]
    public CssValue<ObjectPositionBuilder>? ObjectPosition { get; set; }

    /// <summary>
    /// Gets or sets whether the media should be lazy-loaded.
    /// </summary>
    [Parameter]
    public bool Lazy { get; set; }
}
