using System;
using Microsoft.AspNetCore.Components;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Focused text/typography base for components that expose additional text-only utilities.
/// </summary>
public abstract class TypographyElement : Element
{
    [Parameter]
    public CssValue<TextOpacityBuilder>? TextOpacity { get; set; }

    protected override void BuildOwnedClassAndStyle(ref PooledStringBuilder sty, ref PooledStringBuilder cls)
    {
        base.BuildOwnedClassAndStyle(ref sty, ref cls);

        AddCss(ref sty, ref cls, TextOpacity);
    }

    protected override void ComputeRenderKeyCore(ref HashCode hc)
    {
        base.ComputeRenderKeyCore(ref hc);

        AddIf(ref hc, TextOpacity);
    }
}
