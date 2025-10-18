using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Shared visual layer base for surfaces like cards, panels, alerts, and callouts.
/// Provides background, border, radius, shadow, padding, and opacity properties.
/// Note: Child components must provide their own markup.
/// </summary>
public abstract class Surface : Component, ISurface
{
    /// <summary>
    /// Gets or sets the HTML tag to render (e.g., "div", "section", "article").
    /// Note: This is kept for interface compatibility but child components should provide their own markup.
    /// </summary>
    [Parameter]
    public string Tag { get; set; } = "div";

    /// <summary>
    /// Gets or sets the child content to be rendered within the surface.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Gets or sets the border style and width.
    /// </summary>
    [Parameter]
    public CssValue<BorderBuilder>? Border { get; set; }

    /// <summary>
    /// Gets or sets the border radius for rounded corners.
    /// </summary>
    [Parameter]
    public CssValue<BorderRadiusBuilder>? BorderRadius { get; set; }

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

    protected virtual Dictionary<string, object> BuildSurfaceAttributes()
    {
        var attributes = BuildAttributes();

        var sty = new PooledStringBuilder(64);
        var cls = new PooledStringBuilder(64);

        try
        {
            // Get existing class and style
            if (attributes.TryGetValue("style", out var existingStyle))
                sty.Append(existingStyle.ToString());
            if (attributes.TryGetValue("class", out var existingClass))
                cls.Append(existingClass.ToString());

            // Apply surface-specific properties
            AddCss(ref sty, ref cls, Border);
            AddCss(ref sty, ref cls, BorderRadius);
            AddCss(ref sty, ref cls, BoxShadow);
            AddCss(ref sty, ref cls, BackgroundOpacity);
            AddCss(ref sty, ref cls, BorderOpacity);

            // Update attributes with surface styles
            if (cls.Length > 0)
                attributes["class"] = cls.ToString();
            if (sty.Length > 0)
                attributes["style"] = sty.ToString();

            return attributes;
        }
        finally
        {
            sty.Dispose();
            cls.Dispose();
        }
    }
}

