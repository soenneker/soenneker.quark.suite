using System;
using Microsoft.AspNetCore.Components;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Focused text/typography base for components that intentionally expose typographic controls.
/// </summary>
public abstract class TypographyElement : Element
{
    [Parameter]
    public CssValue<TextAlignmentBuilder>? TextAlignment { get; set; }

    [Parameter]
    public CssValue<TextColorBuilder>? TextColor { get; set; }

    [Parameter]
    public CssValue<TextSizeBuilder>? TextSize { get; set; }

    [Parameter]
    public CssValue<TextDecorationBuilder>? TextDecoration { get; set; }

    [Parameter]
    public CssValue<TextTransformBuilder>? TextTransform { get; set; }

    [Parameter]
    public CssValue<FontWeightBuilder>? FontWeight { get; set; }

    [Parameter]
    public CssValue<FontStyleBuilder>? FontStyle { get; set; }

    [Parameter]
    public CssValue<LineHeightBuilder>? LineHeight { get; set; }

    [Parameter]
    public CssValue<TextWrapBuilder>? TextWrap { get; set; }

    [Parameter]
    public CssValue<TextBreakBuilder>? TextBreak { get; set; }

    [Parameter]
    public CssValue<TextOverflowBuilder>? TextOverflow { get; set; }

    [Parameter]
    public CssValue<TruncateBuilder>? Truncate { get; set; }

    [Parameter]
    public CssValue<TextStyleBuilder>? TextStyle { get; set; }

    [Parameter]
    public CssValue<TextBackgroundBuilder>? TextBackground { get; set; }

    [Parameter]
    public CssValue<TextOpacityBuilder>? TextOpacity { get; set; }

    [Parameter]
    public CssValue<LetterSpacingBuilder>? LetterSpacing { get; set; }

    [Parameter]
    public CssValue<LinkOpacityBuilder>? LinkOpacity { get; set; }

    [Parameter]
    public CssValue<LinkOffsetBuilder>? LinkOffset { get; set; }

    [Parameter]
    public CssValue<LinkUnderlineBuilder>? LinkUnderline { get; set; }

    protected override void BuildOwnedClassAndStyle(ref PooledStringBuilder sty, ref PooledStringBuilder cls)
    {
        base.BuildOwnedClassAndStyle(ref sty, ref cls);

        AddCss(ref sty, ref cls, TextAlignment);
        ApplyTextColor(ref sty, ref cls, TextColor);
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
        AddCss(ref sty, ref cls, LetterSpacing);
        AddCss(ref sty, ref cls, LinkOpacity);
        AddCss(ref sty, ref cls, LinkOffset);
        AddCss(ref sty, ref cls, LinkUnderline);
    }

    protected override void ComputeRenderKeyCore(ref HashCode hc)
    {
        base.ComputeRenderKeyCore(ref hc);

        AddIf(ref hc, TextAlignment);
        AddIf(ref hc, TextColor);
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
        AddIf(ref hc, LetterSpacing);
        AddIf(ref hc, LinkOpacity);
        AddIf(ref hc, LinkOffset);
        AddIf(ref hc, LinkUnderline);
    }
}
