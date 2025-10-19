using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Pure text element base with inheritable text properties.
/// Provides properties for text color, size, weight, alignment, wrapping, and breaking.
/// Note: Child components must provide their own markup.
/// </summary>
public abstract class TypographicElement : Element
{
    // Typography-specific properties
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

    protected override Dictionary<string, object> BuildAttributes()
    {
        var attributes = base.BuildAttributes();

        BuildClassAndStyleAttributes(attributes, (cls, sty) =>
        {
            // Apply typography-specific properties
            ApplyTextColor(ref sty, ref cls);
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

        return attributes;
    }

    /// <summary>
    /// Applies text color styling. Override this method in derived components to customize text color application.
    /// </summary>
    protected virtual void ApplyTextColor(ref PooledStringBuilder sty, ref PooledStringBuilder cls)
    {
        AddCss(ref sty, ref cls, TextColor);
    }
}
