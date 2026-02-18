using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <inheritdoc cref="IDecoratedTypographicElement"/>
public abstract class DecoratedTypographicElement : TypographicElement, IDecoratedTypographicElement
{
    [Parameter] public CssValue<BoxShadowBuilder>? BoxShadow { get; set; }
    [Parameter] public CssValue<BackgroundOpacityBuilder>? BackgroundOpacity { get; set; }
    [Parameter] public CssValue<BorderOpacityBuilder>? BorderOpacity { get; set; }

    protected override void BuildAttributesCore(Dictionary<string, object> attributes)
    {
        base.BuildAttributesCore(attributes);

        BuildClassAndStyleAttributes(attributes, (ref cls, ref sty) =>
        {
            AddCss(ref sty, ref cls, BoxShadow);
            AddCss(ref sty, ref cls, BackgroundOpacity);
            AddCss(ref sty, ref cls, BorderOpacity);
        });
    }

    protected override void ComputeRenderKeyCore(ref HashCode hc)
    {
        AddIf(ref hc, BoxShadow);
        AddIf(ref hc, BackgroundOpacity);
        AddIf(ref hc, BorderOpacity);
    }
}