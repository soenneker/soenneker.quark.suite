using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

///<inheritdoc cref="IDecoratedTypographicElement"/>
public abstract class DecoratedTypographicElement : TypographicElement, IDecoratedTypographicElement
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

    protected override Dictionary<string, object> BuildAttributes()
    {
        var attributes = base.BuildAttributes();

        BuildClassAndStyleAttributes(attributes, (cls, sty) =>
        {
            // Apply decorative properties
            AddCss(ref sty, ref cls, Border);
            AddCss(ref sty, ref cls, BorderRadius);
            AddCss(ref sty, ref cls, BoxShadow);
            AddCss(ref sty, ref cls, BackgroundOpacity);
            AddCss(ref sty, ref cls, BorderOpacity);
        });

        return attributes;
    }
}
