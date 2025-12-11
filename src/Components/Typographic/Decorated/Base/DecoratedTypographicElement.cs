using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

///<inheritdoc cref="IDecoratedTypographicElement"/>
public abstract class DecoratedTypographicElement : TypographicElement, IDecoratedTypographicElement
{
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

    /// <summary>
    /// Gets or sets the display size (Bootstrap display heading classes).
    /// </summary>
    [Parameter]
    public CssValue<DisplaySizeBuilder>? DisplaySize { get; set; }

    protected override Dictionary<string, object> BuildAttributes()
    {
        var attributes = base.BuildAttributes();

        BuildClassAndStyleAttributes(attributes, (ref cls, ref sty) =>
        {
            // Apply decorative properties
            AddCss(ref sty, ref cls, BoxShadow);
            AddCss(ref sty, ref cls, BackgroundOpacity);
            AddCss(ref sty, ref cls, BorderOpacity);
            AddCss(ref sty, ref cls, DisplaySize);
        });

        return attributes;
    }
}
