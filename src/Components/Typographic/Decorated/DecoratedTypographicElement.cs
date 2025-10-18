using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Base class for typographic components with decorative visual elements.
/// Extends TypographicElement by adding background, border, radius, shadow, padding, and opacity.
/// Used for badges, pills, labels, and similar decorated text elements.
/// </summary>
public abstract class DecoratedTypographicElement : TypographicElement
{
    [Parameter]
    public CssValue<BorderBuilder>? Border { get; set; }

    [Parameter]
    public CssValue<BorderRadiusBuilder>? BorderRadius { get; set; }

    [Parameter]
    public CssValue<BoxShadowBuilder>? BoxShadow { get; set; }

    [Parameter]
    public CssValue<BackgroundOpacityBuilder>? BackgroundOpacity { get; set; }

    [Parameter]
    public CssValue<BorderOpacityBuilder>? BorderOpacity { get; set; }

    protected override Dictionary<string, object> BuildTypographicAttributes()
    {
        var attributes = base.BuildTypographicAttributes();

        var sty = new PooledStringBuilder(64);
        var cls = new PooledStringBuilder(64);

        try
        {
            // Get existing class and style
            if (attributes.TryGetValue("style", out var existingStyle))
                sty.Append(existingStyle.ToString());
            if (attributes.TryGetValue("class", out var existingClass))
                cls.Append(existingClass.ToString());

            // Apply decorative properties
            AddCss(ref sty, ref cls, Border);
            AddCss(ref sty, ref cls, BorderRadius);
            AddCss(ref sty, ref cls, BoxShadow);
            AddCss(ref sty, ref cls, BackgroundOpacity);
            AddCss(ref sty, ref cls, BorderOpacity);

            // Update attributes
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
