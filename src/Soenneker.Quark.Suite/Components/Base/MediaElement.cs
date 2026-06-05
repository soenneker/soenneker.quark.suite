using System;
using Microsoft.AspNetCore.Components;
using Soenneker.Utils.PooledStringBuilders;

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
    /// Gets or sets alt.
    /// </summary>
    [Parameter]
    public string? Alt { get; set; }

    /// <summary>
    /// Gets or sets aspect ratio.
    /// </summary>
    [Parameter]
    public CssValue<AspectRatioBuilder>? AspectRatio { get; set; }

    /// <summary>
    /// Gets or sets object fit.
    /// </summary>
    [Parameter]
    public CssValue<ObjectFitBuilder>? ObjectFit { get; set; }

    /// <summary>
    /// Gets or sets object position.
    /// </summary>
    [Parameter]
    public CssValue<ObjectPositionBuilder>? ObjectPosition { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether lazy.
    /// </summary>
    [Parameter]
    public bool Lazy { get; set; }

    /// <summary>
    /// Gets or sets fill.
    /// </summary>
    [Parameter]
    public CssValue<FillBuilder>? Fill { get; set; }

    /// <summary>
    /// Gets or sets stroke.
    /// </summary>
    [Parameter]
    public CssValue<StrokeBuilder>? Stroke { get; set; }

    /// <summary>
    /// Gets or sets stroke width.
    /// </summary>
    [Parameter]
    public CssValue<StrokeWidthBuilder>? StrokeWidth { get; set; }

    /// <summary>
    /// Gets or sets stroke line cap.
    /// </summary>
    [Parameter]
    public CssValue<StrokeLineCapBuilder>? StrokeLineCap { get; set; }

    /// <summary>
    /// Gets or sets stroke line join.
    /// </summary>
    [Parameter]
    public CssValue<StrokeLineJoinBuilder>? StrokeLineJoin { get; set; }

    /// <summary>
    /// Gets or sets fill rule.
    /// </summary>
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
