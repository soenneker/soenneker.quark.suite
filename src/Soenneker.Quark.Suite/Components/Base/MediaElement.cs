using System;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Focused base for image, video, audio, iframe, and SVG-like media concerns.
/// </summary>
public abstract class MediaElement : Element
{
    /// <summary>
    /// Gets or sets source.
    /// </summary>
    [Parameter]
    public string? Source { get; set; }

    /// <summary>
    /// Gets or sets the meaningful text alternative for content images. Leave null or empty only when the image is decorative.
    /// </summary>
    [Parameter]
    public string? Alt { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether lazy.
    /// </summary>
    [Parameter]
    public bool Lazy { get; set; }

    protected override void ComputeRenderKeyCore(ref HashCode hc)
    {
        base.ComputeRenderKeyCore(ref hc);

        hc.Add(Source);
        hc.Add(Alt);
        hc.Add(Lazy);
    }
}
