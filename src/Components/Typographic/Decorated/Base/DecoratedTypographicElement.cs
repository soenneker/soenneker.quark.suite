using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

///<inheritdoc cref="IDecoratedTypographicElement"/>
public abstract class DecoratedTypographicElement : TypographicElement, IDecoratedTypographicElement
{
    [Parameter]
    public CssValue<BorderBuilder>? Border { get; set; }

    [Parameter]
    public CssValue<BoxShadowBuilder>? BoxShadow { get; set; }

    [Parameter]
    public CssValue<BackgroundOpacityBuilder>? BackgroundOpacity { get; set; }

    [Parameter]
    public CssValue<BorderOpacityBuilder>? BorderOpacity { get; set; }

    [Parameter]
    public CssValue<DisplaySizeBuilder>? DisplaySize { get; set; }

    protected override Dictionary<string, object> BuildAttributes()
    {
        var attributes = base.BuildAttributes();

        BuildClassAndStyleAttributes(attributes, (ref PooledStringBuilder cls, ref PooledStringBuilder sty) =>
        {
            // Apply decorative properties
            AddCss(ref sty, ref cls, Border);
            AddCss(ref sty, ref cls, BoxShadow);
            AddCss(ref sty, ref cls, BackgroundOpacity);
            AddCss(ref sty, ref cls, BorderOpacity);
            AddCss(ref sty, ref cls, DisplaySize);
        });

        return attributes;
    }
}
