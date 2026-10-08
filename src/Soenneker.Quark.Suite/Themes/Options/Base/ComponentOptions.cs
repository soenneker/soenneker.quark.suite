using System;
using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// CSS declarations for a component selector, authored with typed builders or literal CSS values.
/// Builder utilities are resolved only when theme CSS is generated.
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
            return ThemeSelector.Resolve(baseSelector, relativeSelector);

        if (baseSelector.IsNullOrWhiteSpace())
            return relativeSelector;

        return ThemeSelector.Resolve(baseSelector, relativeSelector, descendant: true);
    }

    /// <summary>Gets or sets the builder or literal CSS <c>display</c> value, including any required units.</summary>
    public ThemeValue<DisplayBuilder>? Display { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>visibility</c> value, including any required units.</summary>
    public ThemeValue<VisibilityBuilder>? Visibility { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>float</c> value, including any required units.</summary>
    public ThemeValue<FloatBuilder>? Float { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>vertical-align</c> value, including any required units.</summary>
    public ThemeValue<VerticalAlignBuilder>? VerticalAlign { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>text-overflow</c> value, including any required units.</summary>
    public ThemeValue<TextOverflowBuilder>? TextOverflow { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>box-shadow</c> value, including any required units.</summary>
    public ThemeValue<ShadowBuilder>? Shadow { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>margin</c> value, including any required units.</summary>
    public ThemeValue<MarginBuilder>? Margin { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>padding</c> value, including any required units.</summary>
    public ThemeValue<PaddingBuilder>? Padding { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>inset</c> value, including any required units.</summary>
    public ThemeValue<InsetBuilder>? Inset { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>top</c> value, including any required units.</summary>
    public ThemeValue<TopBuilder>? Top { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>right</c> value, including any required units.</summary>
    public ThemeValue<RightBuilder>? Right { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>bottom</c> value, including any required units.</summary>
    public ThemeValue<BottomBuilder>? Bottom { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>left</c> value, including any required units.</summary>
    public ThemeValue<LeftBuilder>? Left { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>position</c> value, including any required units.</summary>
    public ThemeValue<PositionBuilder>? Position { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>scroll-margin</c> value, including any required units.</summary>
    public ThemeValue<ScrollMarginBuilder>? ScrollMargin { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>scroll-padding</c> value, including any required units.</summary>
    public ThemeValue<ScrollPaddingBuilder>? ScrollPadding { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>font-size</c> value, including any required units.</summary>
    public ThemeValue<TextSizeBuilder>? TextSize { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>width</c> value, including any required units.</summary>
    public ThemeValue<WidthBuilder>? Width { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>min-width</c> value, including any required units.</summary>
    public ThemeValue<WidthBuilder>? MinWidth { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>max-width</c> value, including any required units.</summary>
    public ThemeValue<WidthBuilder>? MaxWidth { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>height</c> value, including any required units.</summary>
    public ThemeValue<HeightBuilder>? Height { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>min-height</c> value, including any required units.</summary>
    public ThemeValue<MinHeightBuilder>? MinHeight { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>max-height</c> value, including any required units.</summary>
    public ThemeValue<MaxHeightBuilder>? MaxHeight { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>overflow</c> value, including any required units.</summary>
    public ThemeValue<OverflowBuilder>? Overflow { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>overflow-x</c> value, including any required units.</summary>
    public ThemeValue<OverflowBuilder>? OverflowX { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>overflow-y</c> value, including any required units.</summary>
    public ThemeValue<OverflowBuilder>? OverflowY { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>overscroll-behavior</c> value, including any required units.</summary>
    public ThemeValue<OverscrollBuilder>? Overscroll { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>object-fit</c> value, including any required units.</summary>
    public ThemeValue<ObjectFitBuilder>? ObjectFit { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>text-align</c> value, including any required units.</summary>
    public ThemeValue<TextAlignBuilder>? TextAlign { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>color</c> value, including any required units.</summary>
    public ThemeValue<TextColorBuilder>? TextColor { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>flex</c> value, including any required units.</summary>
    public ThemeValue<FlexBuilder>? Flex { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>flex-direction</c> value, including any required units.</summary>
    public ThemeValue<FlexDirectionBuilder>? FlexDirection { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>flex-wrap</c> value, including any required units.</summary>
    public ThemeValue<FlexWrapBuilder>? FlexWrap { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>flex-grow</c> value, including any required units.</summary>
    public ThemeValue<GrowBuilder>? Grow { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>flex-shrink</c> value, including any required units.</summary>
    public ThemeValue<ShrinkBuilder>? Shrink { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>gap</c> value, including any required units.</summary>
    public ThemeValue<GapBuilder>? Gap { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>fill</c> value, including any required units.</summary>
    public ThemeValue<FillBuilder>? Fill { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>stroke</c> value, including any required units.</summary>
    public ThemeValue<StrokeBuilder>? Stroke { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>text-decoration</c> value, including any required units.</summary>
    public ThemeValue<DecorationLineBuilder>? DecorationLine { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>text-underline-offset</c> value, including any required units.</summary>
    public ThemeValue<UnderlineOffsetBuilder>? UnderlineOffset { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>letter-spacing</c> value, including any required units.</summary>
    public ThemeValue<TrackingBuilder>? Tracking { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>align-content</c> value, including any required units.</summary>
    public ThemeValue<ContentAlignBuilder>? ContentAlign { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>align-items</c> value, including any required units.</summary>
    public ThemeValue<ItemsBuilder>? ItemsAlign { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>justify-content</c> value, including any required units.</summary>
    public ThemeValue<JustifyBuilder>? Justify { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>align-self</c> value, including any required units.</summary>
    public ThemeValue<SelfBuilder>? SelfAlign { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>justify-items</c> value, including any required units.</summary>
    public ThemeValue<JustifyItemsAlignBuilder>? JustifyItemsAlign { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>justify-self</c> value, including any required units.</summary>
    public ThemeValue<JustifySelfAlignBuilder>? JustifySelfAlign { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>grid-column-start</c> value, including any required units.</summary>
    public ThemeValue<ColStartBuilder>? ColStart { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>grid-row</c> value, including any required units.</summary>
    public ThemeValue<RowSpanBuilder>? RowSpan { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>grid-row-start</c> value, including any required units.</summary>
    public ThemeValue<RowStartBuilder>? RowStart { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>border</c> value, including any required units.</summary>
    public ThemeValue<BorderBuilder>? Border { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>border-style</c> value, including any required units.</summary>
    public ThemeValue<BorderStyleBuilder>? BorderStyle { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>opacity</c> value, including any required units.</summary>
    public ThemeValue<OpacityBuilder>? Opacity { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>z-index</c> value, including any required units.</summary>
    public ThemeValue<ZIndexBuilder>? ZIndex { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>pointer-events</c> value, including any required units.</summary>
    public ThemeValue<PointerEventsBuilder>? PointerEvents { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>user-select</c> value, including any required units.</summary>
    public ThemeValue<UserSelectBuilder>? UserSelect { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>text-transform</c> value, including any required units.</summary>
    public ThemeValue<TextTransformBuilder>? TextTransform { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>font-family</c> value, including any required units.</summary>
    public ThemeValue<FontFamilyBuilder>? FontFamily { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>font-weight</c> value, including any required units.</summary>
    public ThemeValue<FontWeightBuilder>? FontWeight { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>font-style</c> value, including any required units.</summary>
    public ThemeValue<FontStyleBuilder>? FontStyle { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>line-height</c> value, including any required units.</summary>
    public ThemeValue<LeadingBuilder>? Leading { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>white-space</c> value, including any required units.</summary>
    public ThemeValue<WhitespaceBuilder>? Whitespace { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>text-wrap</c> value, including any required units.</summary>
    public ThemeValue<TextWrapBuilder>? TextWrap { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>word-break</c> value, including any required units.</summary>
    public ThemeValue<TextBreakBuilder>? TextBreak { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>border-color</c> value, including any required units.</summary>
    public ThemeValue<BorderColorBuilder>? BorderColor { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>background-color</c> value, including any required units.</summary>
    public ThemeValue<BackgroundColorBuilder>? BackgroundColor { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>animation</c> value, including any required units.</summary>
    public ThemeValue<AnimationBuilder>? Animation { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>transition-duration</c> value, including any required units.</summary>
    public ThemeValue<DurationBuilder>? Duration { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>aspect-ratio</c> value, including any required units.</summary>
    public ThemeValue<AspectRatioBuilder>? AspectRatio { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>backdrop-filter</c> value, including any required units.</summary>
    public ThemeValue<BackdropFilterBuilder>? BackdropFilter { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>border-radius</c> value, including any required units.</summary>
    public ThemeValue<RoundedBuilder>? Rounded { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>clip-path</c> value, including any required units.</summary>
    public ThemeValue<ClipPathBuilder>? ClipPath { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>cursor</c> value, including any required units.</summary>
    public ThemeValue<CursorBuilder>? Cursor { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>filter</c> value, including any required units.</summary>
    public ThemeValue<FilterBuilder>? Filter { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>object-position</c> value, including any required units.</summary>
    public ThemeValue<ObjectPositionBuilder>? ObjectPosition { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>resize</c> value, including any required units.</summary>
    public ThemeValue<ResizeBuilder>? Resize { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>scroll-behavior</c> value, including any required units.</summary>
    public ThemeValue<ScrollBehaviorBuilder>? ScrollBehavior { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>transform</c> value, including any required units.</summary>
    public ThemeValue<TransformBuilder>? Transform { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>transition</c> value, including any required units.</summary>
    public ThemeValue<TransitionBuilder>? Transition { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>line-clamp</c> value, including any required units.</summary>
    public ThemeValue<LineClampBuilder>? LineClamp { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>font-variant-numeric</c> value, including any required units.</summary>
    public ThemeValue<FontVariantNumericBuilder>? FontVariantNumeric { get; set; }

    /// <summary>Gets or sets the ring utilities for this component.</summary>
    public ThemeValue<RingBuilder>? Ring { get; set; }

    /// <summary>Gets or sets additional literal CSS declarations, appended in order without deduplication.</summary>
    public IReadOnlyList<ThemeCssDeclaration>? Declarations { get; set; }

    /// <summary>Gets or sets nested selector rules. Use <c>&amp;</c> for the current selector; other selectors target descendants.</summary>
    public IReadOnlyList<ComponentOptions>? Rules { get; set; }

    private void CollectCssRules(ref ComponentCssRuleCollector buffer, string selector)
    {
        ThemeUtilityConverter.AddRules(ref buffer, selector, "display", Display);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "visibility", Visibility);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "float", Float);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "vertical-align", VerticalAlign);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "text-overflow", TextOverflow);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "box-shadow", Shadow);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "margin", Margin);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "padding", Padding);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "inset", Inset);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "top", Top);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "right", Right);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "bottom", Bottom);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "left", Left);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "position", Position);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "scroll-margin", ScrollMargin);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "scroll-padding", ScrollPadding);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "font-size", TextSize);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "width", Width);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "min-width", MinWidth);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "max-width", MaxWidth);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "height", Height);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "min-height", MinHeight);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "max-height", MaxHeight);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "overflow", Overflow);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "overflow-x", OverflowX);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "overflow-y", OverflowY);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "overscroll-behavior", Overscroll);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "object-fit", ObjectFit);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "text-align", TextAlign);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "color", TextColor);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "flex", Flex);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "flex-direction", FlexDirection);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "flex-wrap", FlexWrap);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "flex-grow", Grow);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "flex-shrink", Shrink);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "gap", Gap);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "fill", Fill);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "stroke", Stroke);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "text-decoration", DecorationLine);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "text-underline-offset", UnderlineOffset);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "letter-spacing", Tracking);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "align-content", ContentAlign);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "align-items", ItemsAlign);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "justify-content", Justify);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "align-self", SelfAlign);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "justify-items", JustifyItemsAlign);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "justify-self", JustifySelfAlign);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "grid-column-start", ColStart);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "grid-row", RowSpan);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "grid-row-start", RowStart);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "border", Border);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "border-style", BorderStyle);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "opacity", Opacity);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "z-index", ZIndex);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "pointer-events", PointerEvents);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "user-select", UserSelect);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "text-transform", TextTransform);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "font-family", FontFamily);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "font-weight", FontWeight);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "font-style", FontStyle);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "line-height", Leading);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "white-space", Whitespace);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "text-wrap", TextWrap);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "word-break", TextBreak);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "border-color", BorderColor);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "background-color", BackgroundColor);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "animation", Animation);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "transition-duration", Duration);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "aspect-ratio", AspectRatio);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "backdrop-filter", BackdropFilter);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "border-radius", Rounded);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "clip-path", ClipPath);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "cursor", Cursor);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "filter", Filter);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "object-position", ObjectPosition);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "resize", Resize);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "scroll-behavior", ScrollBehavior);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "transform", Transform);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "transition", Transition);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "line-clamp", LineClamp);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "font-variant-numeric", FontVariantNumeric);

        ThemeUtilityConverter.AddRules(ref buffer, selector, "box-shadow", Ring);

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
