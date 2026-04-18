using System;
using Microsoft.AspNetCore.Components;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Focused text/typography base for components that expose additional text-only utilities.
/// </summary>
public abstract class TypographyElement : Element
{
    /// <summary>
    /// Typography-first alias for Tailwind text-decoration utilities. Applied after <see cref="Component.TextDecoration"/> so it wins when both are set.
    /// </summary>
    [Parameter]
    public CssValue<TextDecorationBuilder>? Decoration { get; set; }

    /// <summary>
    /// Typography-first alias for Tailwind leading utilities. Applied after <see cref="Component.LineHeight"/> so it wins when both are set.
    /// </summary>
    [Parameter]
    public CssValue<LeadingBuilder>? Leading { get; set; }

    /// <summary>
    /// Typography-first alias for Tailwind tracking utilities. Applied after <see cref="Component.LetterSpacing"/> so it wins when both are set.
    /// </summary>
    [Parameter]
    public CssValue<TrackingBuilder>? Tracking { get; set; }

    /// <summary>
    /// Typography-first alias for Tailwind whitespace utilities. Applied after <see cref="Component.Whitespace"/> so it wins when both are set.
    /// </summary>
    [Parameter]
    public CssValue<WhitespaceBuilder>? WhiteSpace { get; set; }

    /// <summary>
    /// Typography-first alias for Tailwind text-wrap utilities. Applied after <see cref="Component.TextWrap"/> so it wins when both are set.
    /// </summary>
    [Parameter]
    public CssValue<TextWrapBuilder>? Wrap { get; set; }

    /// <summary>
    /// Typography-first alias for Tailwind word-break utilities. Applied after <see cref="Component.TextBreak"/> so it wins when both are set.
    /// </summary>
    [Parameter]
    public CssValue<TextBreakBuilder>? Break { get; set; }

    /// <summary>
    /// Typography-first alias for Tailwind font-variant-numeric utilities. Applied after <see cref="Component.FontVariantNumeric"/> so it wins when both are set.
    /// </summary>
    [Parameter]
    public CssValue<FontVariantNumericBuilder>? Numeric { get; set; }

    protected override void BuildOwnedClassAndStyle(ref PooledStringBuilder sty, ref PooledStringBuilder cls)
    {
        base.BuildOwnedClassAndStyle(ref sty, ref cls);

        AddCss(ref sty, ref cls, Decoration);
        AddCss(ref sty, ref cls, Leading);
        AddCss(ref sty, ref cls, Tracking);
        AddCss(ref sty, ref cls, WhiteSpace);
        AddCss(ref sty, ref cls, Wrap);
        AddCss(ref sty, ref cls, Break);
        AddCss(ref sty, ref cls, Numeric);
    }

    protected override void ComputeRenderKeyCore(ref HashCode hc)
    {
        base.ComputeRenderKeyCore(ref hc);

        AddIf(ref hc, Decoration);
        AddIf(ref hc, Leading);
        AddIf(ref hc, Tracking);
        AddIf(ref hc, WhiteSpace);
        AddIf(ref hc, Wrap);
        AddIf(ref hc, Break);
        AddIf(ref hc, Numeric);
    }
}
