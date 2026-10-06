using System;
using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Explicit CSS declarations for a component selector. Values are emitted verbatim;
/// utility classes and builders belong in component properties or presets.
/// </summary>
public class ComponentOptions
{
    /// <summary>Gets or sets the CSS selector for this component (e.g., "a", "i", ":root").</summary>
    public string Selector { get; set; } = ":root";

    internal void CollectCssRules(ref ComponentCssRuleCollector buffer)
    {
        if (Selector.IsNullOrWhiteSpace())
            return;

        CollectCssRules(ref buffer, Selector);
    }

    /// <summary>
    /// Allows component option groups to append scoped child component rules.
    /// </summary>
    private protected virtual void CollectChildCssRules(ref ComponentCssRuleCollector buffer, string baseSelector)
    {
    }

    private protected static void AddChildCssRules<TOptions>(ref ComponentCssRuleCollector buffer, TOptions? options, string scopedDefaultSelector,
        string optionDefaultSelector, string baseSelector)
        where TOptions : ComponentOptions
    {
        if (options is null)
            return;

        var scopedSelector = ResolveScopedChildSelector(baseSelector, options.Selector, scopedDefaultSelector, optionDefaultSelector);

        if (scopedSelector.IsNullOrWhiteSpace())
            return;

        options.CollectCssRules(ref buffer, scopedSelector);
    }

    private static string? ResolveScopedChildSelector(string baseSelector, string? selector, string scopedDefaultSelector, string optionDefaultSelector)
    {
        if (selector.IsNullOrWhiteSpace())
            return null;

        var trimmed = selector.Trim();
        var relativeSelector = string.Equals(trimmed, optionDefaultSelector, StringComparison.Ordinal) ? scopedDefaultSelector : trimmed;

        if (relativeSelector.Contains('&', StringComparison.Ordinal))
            return relativeSelector.Replace("&", baseSelector, StringComparison.Ordinal).Trim();

        if (baseSelector.IsNullOrWhiteSpace())
            return relativeSelector;

        return $"{baseSelector} {relativeSelector}";
    }

    /// <summary>Gets or sets the literal CSS <c>display</c> value, including any required units.</summary>
    public string? Display { get; set; }

    /// <summary>Gets or sets the literal CSS <c>visibility</c> value, including any required units.</summary>
    public string? Visibility { get; set; }

    /// <summary>Gets or sets the literal CSS <c>float</c> value, including any required units.</summary>
    public string? Float { get; set; }

    /// <summary>Gets or sets the literal CSS <c>vertical-align</c> value, including any required units.</summary>
    public string? VerticalAlign { get; set; }

    /// <summary>Gets or sets the literal CSS <c>text-overflow</c> value, including any required units.</summary>
    public string? TextOverflow { get; set; }

    /// <summary>Gets or sets the literal CSS <c>box-shadow</c> value, including any required units.</summary>
    public string? Shadow { get; set; }

    /// <summary>Gets or sets the literal CSS <c>margin</c> value, including any required units.</summary>
    public string? Margin { get; set; }

    /// <summary>Gets or sets the literal CSS <c>padding</c> value, including any required units.</summary>
    public string? Padding { get; set; }

    /// <summary>Gets or sets the literal CSS <c>inset</c> value, including any required units.</summary>
    public string? Inset { get; set; }

    /// <summary>Gets or sets the literal CSS <c>top</c> value, including any required units.</summary>
    public string? Top { get; set; }

    /// <summary>Gets or sets the literal CSS <c>right</c> value, including any required units.</summary>
    public string? Right { get; set; }

    /// <summary>Gets or sets the literal CSS <c>bottom</c> value, including any required units.</summary>
    public string? Bottom { get; set; }

    /// <summary>Gets or sets the literal CSS <c>left</c> value, including any required units.</summary>
    public string? Left { get; set; }

    /// <summary>Gets or sets the literal CSS <c>position</c> value, including any required units.</summary>
    public string? Position { get; set; }

    /// <summary>Gets or sets the literal CSS <c>scroll-margin</c> value, including any required units.</summary>
    public string? ScrollMargin { get; set; }

    /// <summary>Gets or sets the literal CSS <c>scroll-padding</c> value, including any required units.</summary>
    public string? ScrollPadding { get; set; }

    /// <summary>Gets or sets the literal CSS <c>font-size</c> value, including any required units.</summary>
    public string? TextSize { get; set; }

    /// <summary>Gets or sets the literal CSS <c>width</c> value, including any required units.</summary>
    public string? Width { get; set; }

    /// <summary>Gets or sets the literal CSS <c>min-width</c> value, including any required units.</summary>
    public string? MinWidth { get; set; }

    /// <summary>Gets or sets the literal CSS <c>max-width</c> value, including any required units.</summary>
    public string? MaxWidth { get; set; }

    /// <summary>Gets or sets the literal CSS <c>height</c> value, including any required units.</summary>
    public string? Height { get; set; }

    /// <summary>Gets or sets the literal CSS <c>min-height</c> value, including any required units.</summary>
    public string? MinHeight { get; set; }

    /// <summary>Gets or sets the literal CSS <c>max-height</c> value, including any required units.</summary>
    public string? MaxHeight { get; set; }

    /// <summary>Gets or sets the literal CSS <c>overflow</c> value, including any required units.</summary>
    public string? Overflow { get; set; }

    /// <summary>Gets or sets the literal CSS <c>overflow-x</c> value, including any required units.</summary>
    public string? OverflowX { get; set; }

    /// <summary>Gets or sets the literal CSS <c>overflow-y</c> value, including any required units.</summary>
    public string? OverflowY { get; set; }

    /// <summary>Gets or sets the literal CSS <c>overscroll-behavior</c> value, including any required units.</summary>
    public string? Overscroll { get; set; }

    /// <summary>Gets or sets the literal CSS <c>object-fit</c> value, including any required units.</summary>
    public string? ObjectFit { get; set; }

    /// <summary>Gets or sets the literal CSS <c>text-align</c> value, including any required units.</summary>
    public string? TextAlign { get; set; }

    /// <summary>Gets or sets the literal CSS <c>color</c> value, including any required units.</summary>
    public string? TextColor { get; set; }

    /// <summary>Gets or sets the literal CSS <c>flex</c> value, including any required units.</summary>
    public string? Flex { get; set; }

    /// <summary>Gets or sets the literal CSS <c>flex-direction</c> value, including any required units.</summary>
    public string? FlexDirection { get; set; }

    /// <summary>Gets or sets the literal CSS <c>flex-wrap</c> value, including any required units.</summary>
    public string? FlexWrap { get; set; }

    /// <summary>Gets or sets the literal CSS <c>flex-grow</c> value, including any required units.</summary>
    public string? Grow { get; set; }

    /// <summary>Gets or sets the literal CSS <c>flex-shrink</c> value, including any required units.</summary>
    public string? Shrink { get; set; }

    /// <summary>Gets or sets the literal CSS <c>gap</c> value, including any required units.</summary>
    public string? Gap { get; set; }

    /// <summary>Gets or sets the literal CSS <c>fill</c> value, including any required units.</summary>
    public string? Fill { get; set; }

    /// <summary>Gets or sets the literal CSS <c>stroke</c> value, including any required units.</summary>
    public string? Stroke { get; set; }

    /// <summary>Gets or sets the literal CSS <c>text-decoration</c> value, including any required units.</summary>
    public string? DecorationLine { get; set; }

    /// <summary>Gets or sets the literal CSS <c>text-underline-offset</c> value, including any required units.</summary>
    public string? UnderlineOffset { get; set; }

    /// <summary>Gets or sets the literal CSS <c>letter-spacing</c> value, including any required units.</summary>
    public string? Tracking { get; set; }

    /// <summary>Gets or sets the literal CSS <c>align-content</c> value, including any required units.</summary>
    public string? ContentAlign { get; set; }

    /// <summary>Gets or sets the literal CSS <c>align-items</c> value, including any required units.</summary>
    public string? ItemsAlign { get; set; }

    /// <summary>Gets or sets the literal CSS <c>justify-content</c> value, including any required units.</summary>
    public string? Justify { get; set; }

    /// <summary>Gets or sets the literal CSS <c>align-self</c> value, including any required units.</summary>
    public string? SelfAlign { get; set; }

    /// <summary>Gets or sets the literal CSS <c>justify-items</c> value, including any required units.</summary>
    public string? JustifyItemsAlign { get; set; }

    /// <summary>Gets or sets the literal CSS <c>justify-self</c> value, including any required units.</summary>
    public string? JustifySelfAlign { get; set; }

    /// <summary>Gets or sets the literal CSS <c>grid-column-start</c> value, including any required units.</summary>
    public string? ColStart { get; set; }

    /// <summary>Gets or sets the literal CSS <c>grid-row</c> value, including any required units.</summary>
    public string? RowSpan { get; set; }

    /// <summary>Gets or sets the literal CSS <c>grid-row-start</c> value, including any required units.</summary>
    public string? RowStart { get; set; }

    /// <summary>Gets or sets the literal CSS <c>border</c> value, including any required units.</summary>
    public string? Border { get; set; }

    /// <summary>Gets or sets the literal CSS <c>border-style</c> value, including any required units.</summary>
    public string? BorderStyle { get; set; }

    /// <summary>Gets or sets the literal CSS <c>opacity</c> value, including any required units.</summary>
    public string? Opacity { get; set; }

    /// <summary>Gets or sets the literal CSS <c>z-index</c> value, including any required units.</summary>
    public string? ZIndex { get; set; }

    /// <summary>Gets or sets the literal CSS <c>pointer-events</c> value, including any required units.</summary>
    public string? PointerEvents { get; set; }

    /// <summary>Gets or sets the literal CSS <c>user-select</c> value, including any required units.</summary>
    public string? UserSelect { get; set; }

    /// <summary>Gets or sets the literal CSS <c>text-transform</c> value, including any required units.</summary>
    public string? TextTransform { get; set; }

    /// <summary>Gets or sets the literal CSS <c>font-family</c> value, including any required units.</summary>
    public string? FontFamily { get; set; }

    /// <summary>Gets or sets the literal CSS <c>font-weight</c> value, including any required units.</summary>
    public string? FontWeight { get; set; }

    /// <summary>Gets or sets the literal CSS <c>font-style</c> value, including any required units.</summary>
    public string? FontStyle { get; set; }

    /// <summary>Gets or sets the literal CSS <c>line-height</c> value, including any required units.</summary>
    public string? Leading { get; set; }

    /// <summary>Gets or sets the literal CSS <c>white-space</c> value, including any required units.</summary>
    public string? Whitespace { get; set; }

    /// <summary>Gets or sets the literal CSS <c>text-wrap</c> value, including any required units.</summary>
    public string? TextWrap { get; set; }

    /// <summary>Gets or sets the literal CSS <c>word-break</c> value, including any required units.</summary>
    public string? TextBreak { get; set; }

    /// <summary>Gets or sets the literal CSS <c>border-color</c> value, including any required units.</summary>
    public string? BorderColor { get; set; }

    /// <summary>Gets or sets the literal CSS <c>background-color</c> value, including any required units.</summary>
    public string? BackgroundColor { get; set; }

    /// <summary>Gets or sets the literal CSS <c>animation</c> value, including any required units.</summary>
    public string? Animation { get; set; }

    /// <summary>Gets or sets the literal CSS <c>transition-duration</c> value, including any required units.</summary>
    public string? Duration { get; set; }

    /// <summary>Gets or sets the literal CSS <c>aspect-ratio</c> value, including any required units.</summary>
    public string? AspectRatio { get; set; }

    /// <summary>Gets or sets the literal CSS <c>backdrop-filter</c> value, including any required units.</summary>
    public string? BackdropFilter { get; set; }

    /// <summary>Gets or sets the literal CSS <c>border-radius</c> value, including any required units.</summary>
    public string? Rounded { get; set; }

    /// <summary>Gets or sets the literal CSS <c>clip-path</c> value, including any required units.</summary>
    public string? ClipPath { get; set; }

    /// <summary>Gets or sets the literal CSS <c>cursor</c> value, including any required units.</summary>
    public string? Cursor { get; set; }

    /// <summary>Gets or sets the literal CSS <c>filter</c> value, including any required units.</summary>
    public string? Filter { get; set; }

    /// <summary>Gets or sets the literal CSS <c>object-position</c> value, including any required units.</summary>
    public string? ObjectPosition { get; set; }

    /// <summary>Gets or sets the literal CSS <c>resize</c> value, including any required units.</summary>
    public string? Resize { get; set; }

    /// <summary>Gets or sets the literal CSS <c>scroll-behavior</c> value, including any required units.</summary>
    public string? ScrollBehavior { get; set; }

    /// <summary>Gets or sets the literal CSS <c>transform</c> value, including any required units.</summary>
    public string? Transform { get; set; }

    /// <summary>Gets or sets the literal CSS <c>transition</c> value, including any required units.</summary>
    public string? Transition { get; set; }

    /// <summary>Gets or sets the literal CSS <c>line-clamp</c> value, including any required units.</summary>
    public string? LineClamp { get; set; }

    /// <summary>Gets or sets the literal CSS <c>font-variant-numeric</c> value, including any required units.</summary>
    public string? FontVariantNumeric { get; set; }

    /// <summary>Gets or sets additional literal CSS declarations, appended in order without deduplication.</summary>
    public IReadOnlyList<ThemeCssDeclaration>? Declarations { get; set; }

    /// <summary>Gets or sets nested selector rules. Use <c>&amp;</c> for the current selector; other selectors target descendants.</summary>
    public IReadOnlyList<ComponentOptions>? Rules { get; set; }

    private void CollectCssRules(ref ComponentCssRuleCollector buffer, string selector)
    {
        AddDeclaration(ref buffer, selector, "display", Display);
        AddDeclaration(ref buffer, selector, "visibility", Visibility);
        AddDeclaration(ref buffer, selector, "float", Float);
        AddDeclaration(ref buffer, selector, "vertical-align", VerticalAlign);
        AddDeclaration(ref buffer, selector, "text-overflow", TextOverflow);
        AddDeclaration(ref buffer, selector, "box-shadow", Shadow);
        AddDeclaration(ref buffer, selector, "margin", Margin);
        AddDeclaration(ref buffer, selector, "padding", Padding);
        AddDeclaration(ref buffer, selector, "inset", Inset);
        AddDeclaration(ref buffer, selector, "top", Top);
        AddDeclaration(ref buffer, selector, "right", Right);
        AddDeclaration(ref buffer, selector, "bottom", Bottom);
        AddDeclaration(ref buffer, selector, "left", Left);
        AddDeclaration(ref buffer, selector, "position", Position);
        AddDeclaration(ref buffer, selector, "scroll-margin", ScrollMargin);
        AddDeclaration(ref buffer, selector, "scroll-padding", ScrollPadding);
        AddDeclaration(ref buffer, selector, "font-size", TextSize);
        AddDeclaration(ref buffer, selector, "width", Width);
        AddDeclaration(ref buffer, selector, "min-width", MinWidth);
        AddDeclaration(ref buffer, selector, "max-width", MaxWidth);
        AddDeclaration(ref buffer, selector, "height", Height);
        AddDeclaration(ref buffer, selector, "min-height", MinHeight);
        AddDeclaration(ref buffer, selector, "max-height", MaxHeight);
        AddDeclaration(ref buffer, selector, "overflow", Overflow);
        AddDeclaration(ref buffer, selector, "overflow-x", OverflowX);
        AddDeclaration(ref buffer, selector, "overflow-y", OverflowY);
        AddDeclaration(ref buffer, selector, "overscroll-behavior", Overscroll);
        AddDeclaration(ref buffer, selector, "object-fit", ObjectFit);
        AddDeclaration(ref buffer, selector, "text-align", TextAlign);
        AddDeclaration(ref buffer, selector, "color", TextColor);
        AddDeclaration(ref buffer, selector, "flex", Flex);
        AddDeclaration(ref buffer, selector, "flex-direction", FlexDirection);
        AddDeclaration(ref buffer, selector, "flex-wrap", FlexWrap);
        AddDeclaration(ref buffer, selector, "flex-grow", Grow);
        AddDeclaration(ref buffer, selector, "flex-shrink", Shrink);
        AddDeclaration(ref buffer, selector, "gap", Gap);
        AddDeclaration(ref buffer, selector, "fill", Fill);
        AddDeclaration(ref buffer, selector, "stroke", Stroke);
        AddDeclaration(ref buffer, selector, "text-decoration", DecorationLine);
        AddDeclaration(ref buffer, selector, "text-underline-offset", UnderlineOffset);
        AddDeclaration(ref buffer, selector, "letter-spacing", Tracking);
        AddDeclaration(ref buffer, selector, "align-content", ContentAlign);
        AddDeclaration(ref buffer, selector, "align-items", ItemsAlign);
        AddDeclaration(ref buffer, selector, "justify-content", Justify);
        AddDeclaration(ref buffer, selector, "align-self", SelfAlign);
        AddDeclaration(ref buffer, selector, "justify-items", JustifyItemsAlign);
        AddDeclaration(ref buffer, selector, "justify-self", JustifySelfAlign);
        AddDeclaration(ref buffer, selector, "grid-column-start", ColStart);
        AddDeclaration(ref buffer, selector, "grid-row", RowSpan);
        AddDeclaration(ref buffer, selector, "grid-row-start", RowStart);
        AddDeclaration(ref buffer, selector, "border", Border);
        AddDeclaration(ref buffer, selector, "border-style", BorderStyle);
        AddDeclaration(ref buffer, selector, "opacity", Opacity);
        AddDeclaration(ref buffer, selector, "z-index", ZIndex);
        AddDeclaration(ref buffer, selector, "pointer-events", PointerEvents);
        AddDeclaration(ref buffer, selector, "user-select", UserSelect);
        AddDeclaration(ref buffer, selector, "text-transform", TextTransform);
        AddDeclaration(ref buffer, selector, "font-family", FontFamily);
        AddDeclaration(ref buffer, selector, "font-weight", FontWeight);
        AddDeclaration(ref buffer, selector, "font-style", FontStyle);
        AddDeclaration(ref buffer, selector, "line-height", Leading);
        AddDeclaration(ref buffer, selector, "white-space", Whitespace);
        AddDeclaration(ref buffer, selector, "text-wrap", TextWrap);
        AddDeclaration(ref buffer, selector, "word-break", TextBreak);
        AddDeclaration(ref buffer, selector, "border-color", BorderColor);
        AddDeclaration(ref buffer, selector, "background-color", BackgroundColor);
        AddDeclaration(ref buffer, selector, "animation", Animation);
        AddDeclaration(ref buffer, selector, "transition-duration", Duration);
        AddDeclaration(ref buffer, selector, "aspect-ratio", AspectRatio);
        AddDeclaration(ref buffer, selector, "backdrop-filter", BackdropFilter);
        AddDeclaration(ref buffer, selector, "border-radius", Rounded);
        AddDeclaration(ref buffer, selector, "clip-path", ClipPath);
        AddDeclaration(ref buffer, selector, "cursor", Cursor);
        AddDeclaration(ref buffer, selector, "filter", Filter);
        AddDeclaration(ref buffer, selector, "object-position", ObjectPosition);
        AddDeclaration(ref buffer, selector, "resize", Resize);
        AddDeclaration(ref buffer, selector, "scroll-behavior", ScrollBehavior);
        AddDeclaration(ref buffer, selector, "transform", Transform);
        AddDeclaration(ref buffer, selector, "transition", Transition);
        AddDeclaration(ref buffer, selector, "line-clamp", LineClamp);
        AddDeclaration(ref buffer, selector, "font-variant-numeric", FontVariantNumeric);

        if (Declarations is not null)
        {
            for (var i = 0; i < Declarations.Count; i++)
            {
                var declaration = Declarations[i];
                AddDeclaration(ref buffer, selector, declaration.Property, declaration.Value);
            }
        }

        CollectChildCssRules(ref buffer, selector);

        if (Rules is not null)
        {
            for (var i = 0; i < Rules.Count; i++)
            {
                var rule = Rules[i];
                var scopedSelector = ResolveScopedChildSelector(selector, rule.Selector, rule.Selector, string.Empty);
                if (scopedSelector is not null)
                    rule.CollectCssRules(ref buffer, scopedSelector);
            }
        }
    }

    private static void AddDeclaration(ref ComponentCssRuleCollector buffer, string selector, string property, string? value)
    {
        if (value is not null)
            buffer.Add(selector, new ThemeCssDeclaration(property, value));
    }
}
