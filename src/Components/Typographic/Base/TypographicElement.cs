using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

public abstract class TypographicElement : Element
{
    [Parameter] public CssValue<TextSizeBuilder>? TextSize { get; set; }
    [Parameter] public CssValue<TextDecorationBuilder>? TextDecoration { get; set; }
    [Parameter] public CssValue<TextTransformBuilder>? TextTransform { get; set; }
    [Parameter] public CssValue<FontWeightBuilder>? FontWeight { get; set; }
    [Parameter] public CssValue<FontStyleBuilder>? FontStyle { get; set; }
    [Parameter] public CssValue<LineHeightBuilder>? LineHeight { get; set; }
    [Parameter] public CssValue<TextWrapBuilder>? TextWrap { get; set; }
    [Parameter] public CssValue<TextBreakBuilder>? TextBreak { get; set; }
    [Parameter] public CssValue<TextOverflowBuilder>? TextOverflow { get; set; }
    [Parameter] public CssValue<TruncateBuilder>? Truncate { get; set; }
    [Parameter] public CssValue<TextStyleBuilder>? TextStyle { get; set; }
    [Parameter] public CssValue<TextBackgroundBuilder>? TextBackground { get; set; }
    [Parameter] public CssValue<TextOpacityBuilder>? TextOpacity { get; set; }

    protected override void BuildAttributesCore(Dictionary<string, object> attributes)
    {
        base.BuildAttributesCore(attributes);

        BuildClassAndStyleAttributes(attributes, (ref cls, ref sty) =>
        {
            AddCss(ref sty, ref cls, TextSize);
            AddCss(ref sty, ref cls, TextDecoration);
            AddCss(ref sty, ref cls, TextTransform);
            AddCss(ref sty, ref cls, FontWeight);
            AddCss(ref sty, ref cls, FontStyle);
            AddCss(ref sty, ref cls, LineHeight);
            AddCss(ref sty, ref cls, TextWrap);
            AddCss(ref sty, ref cls, TextBreak);
            AddCss(ref sty, ref cls, TextOverflow);
            AddCss(ref sty, ref cls, Truncate);
            AddCss(ref sty, ref cls, TextStyle);
            AddCss(ref sty, ref cls, TextBackground);
            AddCss(ref sty, ref cls, TextOpacity);
        });
    }

    protected override void ComputeRenderKeyCore(ref HashCode hc)
    {
        AddIf(ref hc, TextSize);
        AddIf(ref hc, TextDecoration);
        AddIf(ref hc, TextTransform);
        AddIf(ref hc, FontWeight);
        AddIf(ref hc, FontStyle);
        AddIf(ref hc, LineHeight);
        AddIf(ref hc, TextWrap);
        AddIf(ref hc, TextBreak);
        AddIf(ref hc, TextOverflow);
        AddIf(ref hc, Truncate);
        AddIf(ref hc, TextStyle);
        AddIf(ref hc, TextBackground);
        AddIf(ref hc, TextOpacity);
    }
}
