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
    public CssValue<TextAlignmentBuilder>? TextAlignment { get; set; }

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

    protected virtual Dictionary<string, object> BuildTypographicAttributes()
    {
        var attributes = BuildAttributes();

        var sty = new PooledStringBuilder(64);
        var cls = new PooledStringBuilder(64);

        try
        {
            // Get existing class and style
            if (attributes.TryGetValue("style", out var existingStyle))
                sty.Append(existingStyle.ToString());
            if (attributes.TryGetValue("class", out var existingClass))
                cls.Append(existingClass.ToString());

            // Apply typography-specific properties
            ApplyTextColor(ref sty, ref cls);
            AddCss(ref sty, ref cls, TextSize);
            AddCss(ref sty, ref cls, TextAlignment);
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

            // Update attributes with typography styles
            if (cls.Length > 0)
                attributes["class"] = cls.ToString();
            if (sty.Length > 0)
                attributes["style"] = sty.ToString();

            return attributes;
        }
        finally
        {
            sty.Dispose();
            cls.Dispose();
        }
    }

    /// <summary>
    /// Applies text color styling. Override this method in derived components to customize text color application.
    /// </summary>
    protected virtual void ApplyTextColor(ref PooledStringBuilder sty, ref PooledStringBuilder cls)
    {
        AddCss(ref sty, ref cls, TextColor);
    }
}
