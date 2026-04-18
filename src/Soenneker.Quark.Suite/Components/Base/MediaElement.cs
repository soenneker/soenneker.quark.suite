using System;
using Microsoft.AspNetCore.Components;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Focused base for image, video, audio, iframe, and SVG-like media concerns.
/// </summary>
public abstract class MediaElement : Element
{
    [Parameter]
    public string? Source { get; set; }

    [Parameter]
    public string? Alt { get; set; }

    [Parameter]
    public CssValue<AspectRatioBuilder>? AspectRatio { get; set; }

    [Parameter]
    public CssValue<ObjectFitBuilder>? ObjectFit { get; set; }

    [Parameter]
    public CssValue<ObjectPositionBuilder>? ObjectPosition { get; set; }

    [Parameter]
    public bool Lazy { get; set; }

    [Parameter]
    public CssValue<FillBuilder>? Fill { get; set; }

    [Parameter]
    public CssValue<StrokeBuilder>? Stroke { get; set; }

    [Parameter]
    public CssValue<StrokeWidthBuilder>? StrokeWidth { get; set; }

    [Parameter]
    public CssValue<StrokeLineCapBuilder>? StrokeLineCap { get; set; }

    [Parameter]
    public CssValue<StrokeLineJoinBuilder>? StrokeLineJoin { get; set; }

    [Parameter]
    public CssValue<FillRuleBuilder>? FillRule { get; set; }

    protected override void BuildOwnedClassAndStyle(ref PooledStringBuilder sty, ref PooledStringBuilder cls)
    {
        base.BuildOwnedClassAndStyle(ref sty, ref cls);

        AddCss(ref sty, ref cls, AspectRatio);
        AddCss(ref sty, ref cls, ObjectFit);
        AddCss(ref sty, ref cls, ObjectPosition);
        AddCss(ref sty, ref cls, Fill);
        AddCss(ref sty, ref cls, Stroke);
        AddCss(ref sty, ref cls, StrokeWidth);
        AddCss(ref sty, ref cls, StrokeLineCap);
        AddCss(ref sty, ref cls, StrokeLineJoin);
        AddCss(ref sty, ref cls, FillRule);
    }

    protected override void ComputeRenderKeyCore(ref HashCode hc)
    {
        base.ComputeRenderKeyCore(ref hc);

        hc.Add(Source);
        hc.Add(Alt);
        hc.Add(Lazy);
        AddIf(ref hc, AspectRatio);
        AddIf(ref hc, ObjectFit);
        AddIf(ref hc, ObjectPosition);
        AddIf(ref hc, Fill);
        AddIf(ref hc, Stroke);
        AddIf(ref hc, StrokeWidth);
        AddIf(ref hc, StrokeLineCap);
        AddIf(ref hc, StrokeLineJoin);
        AddIf(ref hc, FillRule);
    }
}
