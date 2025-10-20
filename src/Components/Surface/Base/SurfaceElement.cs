using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Shared visual layer base for surfaces like cards, panels, alerts, and callouts.
/// Provides background, border, radius, shadow, padding, and opacity properties.
/// Note: Child components must provide their own markup.
/// </summary>
public abstract class SurfaceElement : Element, ISurfaceElement
{
    /// <summary>
    /// Gets or sets the HTML tag to render (e.g., "div", "section", "article").
    /// Note: This is kept for interface compatibility but child components should provide their own markup.
    /// </summary>
    [Parameter]
    public string Tag { get; set; } = "div";

    /// <summary>
    /// Gets or sets the box shadow for depth and elevation effects.
    /// </summary>
    [Parameter]
    public CssValue<BoxShadowBuilder>? BoxShadow { get; set; }

    /// <summary>
    /// Gets or sets the background opacity.
    /// </summary>
    [Parameter]
    public CssValue<BackgroundOpacityBuilder>? BackgroundOpacity { get; set; }

    /// <summary>
    /// Gets or sets the border opacity.
    /// </summary>
    [Parameter]
    public CssValue<BorderOpacityBuilder>? BorderOpacity { get; set; }

    protected override Dictionary<string, object> BuildAttributes()
    {
        var attributes = base.BuildAttributes();

        BuildClassAndStyleAttributes(attributes, (ref PooledStringBuilder cls, ref PooledStringBuilder sty) =>
        {
            // Apply surface-specific properties
            AddCss(ref sty, ref cls, BoxShadow);
            AddCss(ref sty, ref cls, BackgroundOpacity);
            AddCss(ref sty, ref cls, BorderOpacity);
        });

        return attributes;
    }
}

