using System.Buffers;
using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Represents CSS styling options for a component, allowing configuration of various CSS properties
/// that can be applied to the component's selector in theme generation.
/// </summary>
public class ComponentOptions
{
    private static readonly SearchValues<char> _cssSelectorPrefixChars = SearchValues.Create(":.#[");

    /// <summary>Gets or sets the CSS selector for this component (e.g., "a", "i", ":root").</summary>
    public string Selector { get; set; } = ":root";

    /// <summary>
    /// Gets all CSS rules for this component based on the configured properties.
    /// </summary>
    internal virtual IEnumerable<ComponentCssRule> GetCssRules()
    {
        return GetCssRules(Selector);
    }

    /// <summary>
    /// Gets all CSS rules for this component under the provided selector.
    /// </summary>
    internal virtual IEnumerable<ComponentCssRule> GetCssRules(string baseSelector)
    {
        if (baseSelector.IsNullOrWhiteSpace())
            return [];

        var buffer = new List<ComponentCssRule>(32);
        CollectCssRules(buffer, baseSelector);
        CollectChildCssRules(buffer, baseSelector);
        return buffer;
    }

    /// <summary>
    /// Allows component option groups to append scoped child component rules.
    /// </summary>
    private protected virtual void CollectChildCssRules(List<ComponentCssRule> buffer, string baseSelector)
    {
    }

    private protected static void AddChildCssRules<TOptions>(List<ComponentCssRule> buffer, TOptions? options, string scopedDefaultSelector,
        string optionDefaultSelector, string baseSelector)
        where TOptions : ComponentOptions
    {
        if (options is null)
            return;

        string? scopedSelector = ResolveScopedChildSelector(baseSelector, options.Selector, scopedDefaultSelector, optionDefaultSelector);

        if (scopedSelector.IsNullOrWhiteSpace())
            return;

        foreach (ComponentCssRule rule in options.GetCssRules(scopedSelector))
        {
            buffer.Add(rule);
        }
    }

    private static string? ResolveScopedChildSelector(string baseSelector, string? selector, string scopedDefaultSelector, string optionDefaultSelector)
    {
        if (selector.IsNullOrWhiteSpace())
            return null;

        string trimmed = selector.Trim();
        string relativeSelector = string.Equals(trimmed, optionDefaultSelector, System.StringComparison.Ordinal) ? scopedDefaultSelector : trimmed;

        if (relativeSelector.Contains('&', System.StringComparison.Ordinal))
            return relativeSelector.Replace("&", baseSelector, System.StringComparison.Ordinal).Trim();

        if (baseSelector.IsNullOrWhiteSpace())
            return relativeSelector;

        return $"{baseSelector} {relativeSelector}";
    }

    /// <summary>
    /// Gets or sets the CSS display configuration.
    /// </summary>
    public CssValue<DisplayBuilder>? Display { get; set; }

    /// <summary>
    /// Gets or sets the CSS visibility configuration.
    /// </summary>
    public CssValue<VisibilityBuilder>? Visibility { get; set; }

    /// <summary>
    /// Gets or sets the CSS float configuration.
    /// </summary>
    public CssValue<FloatBuilder>? Float { get; set; }

    /// <summary>
    /// Gets or sets the CSS vertical-align configuration.
    /// </summary>
    public CssValue<VerticalAlignBuilder>? VerticalAlign { get; set; }

    /// <summary>
    /// Gets or sets the CSS text-overflow configuration.
    /// </summary>
    public CssValue<TextOverflowBuilder>? TextOverflow { get; set; }

    /// <summary>
    /// Gets or sets the CSS shadow configuration.
    /// </summary>
    public CssValue<ShadowBuilder>? Shadow { get; set; }

    /// <summary>
    /// Gets or sets the CSS margin configuration.
    /// </summary>
    public CssValue<MarginBuilder>? Margin { get; set; }

    /// <summary>
    /// Gets or sets the CSS padding configuration.
    /// </summary>
    public CssValue<PaddingBuilder>? Padding { get; set; }

    /// <summary>
    /// Gets or sets inset utility classes.
    /// </summary>
    public CssValue<InsetBuilder>? Inset { get; set; }

    /// <summary>
    /// Gets or sets top offset utility classes.
    /// </summary>
    public CssValue<TopBuilder>? Top { get; set; }

    /// <summary>
    /// Gets or sets right offset utility classes.
    /// </summary>
    public CssValue<RightBuilder>? Right { get; set; }

    /// <summary>
    /// Gets or sets bottom offset utility classes.
    /// </summary>
    public CssValue<BottomBuilder>? Bottom { get; set; }

    /// <summary>
    /// Gets or sets left offset utility classes.
    /// </summary>
    public CssValue<LeftBuilder>? Left { get; set; }

    /// <summary>
    /// Gets or sets the CSS position configuration.
    /// </summary>
    public CssValue<PositionBuilder>? Position { get; set; }

    /// <summary>
    /// Gets or sets the CSS scroll-margin configuration.
    /// </summary>
    public CssValue<ScrollMarginBuilder>? ScrollMargin { get; set; }

    /// <summary>
    /// Gets or sets the CSS scroll-padding configuration.
    /// </summary>
    public CssValue<ScrollPaddingBuilder>? ScrollPadding { get; set; }

    /// <summary>
    /// Gets or sets the CSS TextColor size (font-size) configuration.
    /// </summary>
    public CssValue<TextSizeBuilder>? TextSize { get; set; }

    /// <summary>
    /// Gets or sets the CSS width configuration.
    /// </summary>
    public CssValue<SizeBuilder>? Size { get; set; }

    /// <summary>
    /// Gets or sets the CSS width configuration.
    /// </summary>
    public CssValue<WidthBuilder>? Width { get; set; }

    /// <summary>
    /// Gets or sets the CSS minimum width configuration.
    /// </summary>
    public CssValue<WidthBuilder>? MinWidth { get; set; }

    /// <summary>
    /// Gets or sets the CSS maximum width configuration.
    /// </summary>
    public CssValue<WidthBuilder>? MaxWidth { get; set; }

    /// <summary>
    /// Gets or sets the CSS height configuration.
    /// </summary>
    public CssValue<HeightBuilder>? Height { get; set; }

    /// <summary>
    /// Gets or sets the CSS minimum height configuration.
    /// </summary>
    public CssValue<HeightBuilder>? MinHeight { get; set; }

    /// <summary>
    /// Gets or sets the CSS maximum height configuration.
    /// </summary>
    public CssValue<HeightBuilder>? MaxHeight { get; set; }

    /// <summary>
    /// Gets or sets the CSS overflow configuration.
    /// </summary>
    public CssValue<OverflowBuilder>? Overflow { get; set; }

    /// <summary>
    /// Gets or sets the CSS horizontal overflow configuration.
    /// </summary>
    public CssValue<OverflowBuilder>? OverflowX { get; set; }

    /// <summary>
    /// Gets or sets the CSS vertical overflow configuration.
    /// </summary>
    public CssValue<OverflowBuilder>? OverflowY { get; set; }

    /// <summary>
    /// Gets or sets the CSS overscroll-behavior configuration.
    /// </summary>
    public CssValue<OverscrollBuilder>? Overscroll { get; set; }

    /// <summary>
    /// Gets or sets the CSS object-fit configuration.
    /// </summary>
    public CssValue<ObjectFitBuilder>? ObjectFit { get; set; }

    /// <summary>
    /// Gets or sets the CSS TextColor alignment configuration.
    /// </summary>
    public CssValue<TextAlignBuilder>? TextAlign { get; set; }

    /// <summary>
    /// Gets or sets the CSS TextColor color configuration.
    /// </summary>
    public CssValue<TextColorBuilder>? TextColor { get; set; }

    /// <summary>
    /// Gets or sets the CSS flex configuration.
    /// </summary>
    public CssValue<FlexBuilder>? Flex { get; set; }

    /// <summary>
    /// Gets or sets the CSS flex-direction configuration.
    /// </summary>
    public CssValue<FlexDirectionBuilder>? FlexDirection { get; set; }

    /// <summary>
    /// Gets or sets the CSS flex-wrap configuration.
    /// </summary>
    public CssValue<FlexWrapBuilder>? FlexWrap { get; set; }

    /// <summary>
    /// Gets or sets the CSS flex-grow configuration.
    /// </summary>
    public CssValue<GrowBuilder>? Grow { get; set; }

    /// <summary>
    /// Gets or sets the CSS flex-shrink configuration.
    /// </summary>
    public CssValue<ShrinkBuilder>? Shrink { get; set; }

    /// <summary>
    /// Gets or sets the CSS gap configuration.
    /// </summary>
    public CssValue<GapBuilder>? Gap { get; set; }

    /// <summary>
    /// Gets or sets spacing-between-children utility classes (space-x/space-y and reverse variants).
    /// </summary>
    public CssValue<SpaceBuilder>? Space { get; set; }

    /// <summary>
    /// Gets or sets divide utility classes (divide-x/y, color, opacity, style, reverse).
    /// </summary>
    public CssValue<DivideBuilder>? Divide { get; set; }

    /// <summary>
    /// Gets or sets ring-offset utility classes.
    /// </summary>
    public CssValue<RingOffsetBuilder>? RingOffset { get; set; }

    /// <summary>
    /// Gets or sets SVG fill utility classes.
    /// </summary>
    public CssValue<FillBuilder>? Fill { get; set; }

    /// <summary>
    /// Gets or sets SVG stroke utility classes.
    /// </summary>
    public CssValue<StrokeBuilder>? Stroke { get; set; }

    /// <summary>
    /// Gets or sets gradient utility classes (BackgroundColor-gradient-to/from/via/to).
    /// </summary>
    public CssValue<GradientBuilder>? Gradient { get; set; }

    /// <summary>
    /// Gets or sets TextColor decoration line utility classes (underline, overline, line-through, no-underline).
    /// </summary>
    public CssValue<DecorationLineBuilder>? DecorationLine { get; set; }

    /// <summary>
    /// Gets or sets letter-spacing utility classes (tracking-*).
    /// </summary>
    public CssValue<TrackingBuilder>? Tracking { get; set; }

    /// <summary>
    /// Gets or sets content alignment utility classes (content-*).
    /// </summary>
    public CssValue<ContentAlignBuilder>? ContentAlign { get; set; }

    /// <summary>
    /// Gets or sets items alignment utility classes (items-*).
    /// </summary>
    public CssValue<ItemsBuilder>? ItemsAlign { get; set; }

    /// <summary>
    /// Gets or sets justify alignment utility classes (justify-*).
    /// </summary>
    public CssValue<JustifyBuilder>? Justify { get; set; }

    /// <summary>
    /// Gets or sets self alignment utility classes (self-*).
    /// </summary>
    public CssValue<SelfBuilder>? SelfAlign { get; set; }

    /// <summary>
    /// Gets or sets justify-items alignment utility classes (justify-items-*).
    /// </summary>
    public CssValue<JustifyItemsAlignBuilder>? JustifyItemsAlign { get; set; }

    /// <summary>
    /// Gets or sets justify-self alignment utility classes (justify-self-*).
    /// </summary>
    public CssValue<JustifySelfAlignBuilder>? JustifySelfAlign { get; set; }

    /// <summary>
    /// Gets or sets column-start utility classes (col-start-*).
    /// </summary>
    public CssValue<ColStartBuilder>? ColStart { get; set; }

    /// <summary>
    /// Gets or sets row-span utility classes (row-span-*).
    /// </summary>
    public CssValue<RowSpanBuilder>? RowSpan { get; set; }

    /// <summary>
    /// Gets or sets row-start utility classes (row-start-*).
    /// </summary>
    public CssValue<RowStartBuilder>? RowStart { get; set; }

    /// <summary>
    /// Gets or sets the CSS border configuration.
    /// </summary>
    public CssValue<BorderBuilder>? Border { get; set; }

    /// <summary>
    /// Gets or sets the CSS opacity configuration.
    /// </summary>
    public CssValue<OpacityBuilder>? Opacity { get; set; }

    /// <summary>
    /// Gets or sets the CSS z-index configuration.
    /// </summary>
    public CssValue<ZIndexBuilder>? ZIndex { get; set; }

    /// <summary>
    /// Gets or sets the CSS pointer-events configuration.
    /// </summary>
    public CssValue<PointerEventsBuilder>? PointerEvents { get; set; }

    /// <summary>
    /// Gets or sets the CSS user-select configuration.
    /// </summary>
    public CssValue<UserSelectBuilder>? UserSelect { get; set; }

    /// <summary>
    /// Gets or sets the CSS text-transform configuration.
    /// </summary>
    public CssValue<TextTransformBuilder>? TextTransform { get; set; }

    /// <summary>
    /// Gets or sets the CSS font-family configuration.
    /// </summary>
    public CssValue<FontFamilyBuilder>? FontFamily { get; set; }

    /// <summary>
    /// Gets or sets the CSS font-weight configuration.
    /// </summary>
    public CssValue<FontWeightBuilder>? FontWeight { get; set; }

    /// <summary>
    /// Gets or sets the CSS font-style configuration.
    /// </summary>
    public CssValue<FontStyleBuilder>? FontStyle { get; set; }

    /// <summary>
    /// Gets or sets line-height utility classes (leading-*).
    /// </summary>
    public CssValue<LeadingBuilder>? Leading { get; set; }

    /// <summary>
    /// Gets or sets the CSS whitespace configuration.
    /// </summary>
    public CssValue<WhitespaceBuilder>? Whitespace { get; set; }

    /// <summary>
    /// Gets or sets the CSS text-wrap configuration.
    /// </summary>
    public CssValue<TextWrapBuilder>? TextWrap { get; set; }

    /// <summary>
    /// Gets or sets the CSS text-break (word-break) configuration.
    /// </summary>
    public CssValue<TextBreakBuilder>? TextBreak { get; set; }

    /// <summary>
    /// Gets or sets the CSS border-color configuration.
    /// </summary>
    public CssValue<BorderColorBuilder>? BorderColor { get; set; }

    /// <summary>
    /// Gets or sets the CSS background-color configuration.
    /// </summary>
    public CssValue<BackgroundColorBuilder>? BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the CSS animation configuration.
    /// </summary>
    public CssValue<AnimationBuilder>? Animation { get; set; }

    /// <summary>
    /// Gets or sets transition-duration utility classes.
    /// </summary>
    public CssValue<DurationBuilder>? Duration { get; set; }

    /// <summary>
    /// Gets or sets the CSS aspect-ratio configuration.
    /// </summary>
    public CssValue<AspectRatioBuilder>? AspectRatio { get; set; }

    /// <summary>
    /// Gets or sets the CSS backdrop-filter configuration.
    /// </summary>
    public CssValue<BackdropFilterBuilder>? BackdropFilter { get; set; }

    /// <summary>
    /// Gets or sets the CSS border-radius configuration.
    /// </summary>
    public CssValue<RoundedBuilder>? Rounded { get; set; }

    /// <summary>
    /// Gets or sets the CSS ring configuration.
    /// </summary>
    public CssValue<RingBuilder>? Ring { get; set; }

    /// <summary>
    /// Gets or sets the CSS ring-color configuration.
    /// </summary>
    public CssValue<RingColorBuilder>? RingColor { get; set; }

    /// <summary>
    /// Gets or sets the CSS clip-path configuration.
    /// </summary>
    public CssValue<ClipPathBuilder>? ClipPath { get; set; }

    /// <summary>
    /// Gets or sets the CSS cursor configuration.
    /// </summary>
    public CssValue<CursorBuilder>? Cursor { get; set; }

    /// <summary>
    /// Gets or sets the CSS filter configuration.
    /// </summary>
    public CssValue<FilterBuilder>? Filter { get; set; }

    /// <summary>
    /// Gets or sets the CSS object-position configuration.
    /// </summary>
    public CssValue<ObjectPositionBuilder>? ObjectPosition { get; set; }

    /// <summary>
    /// Gets or sets the CSS resize configuration.
    /// </summary>
    public CssValue<ResizeBuilder>? Resize { get; set; }

    /// <summary>
    /// Gets or sets the CSS screen-reader configuration.
    /// </summary>
    public CssValue<ScreenReaderBuilder>? ScreenReader { get; set; }

    /// <summary>
    /// Gets or sets the CSS scroll-behavior configuration.
    /// </summary>
    public CssValue<ScrollBehaviorBuilder>? ScrollBehavior { get; set; }

    /// <summary>
    /// Gets or sets the CSS transform configuration.
    /// </summary>
    public CssValue<TransformBuilder>? Transform { get; set; }

    /// <summary>
    /// Gets or sets the CSS transition configuration.
    /// </summary>
    public CssValue<TransitionBuilder>? Transition { get; set; }

    /// <summary>
    /// Gets or sets the CSS truncate configuration.
    /// </summary>
    public CssValue<TruncateBuilder>? Truncate { get; set; }

    /// <summary>
    /// Gets or sets the CSS line clamp configuration.
    /// </summary>
    public CssValue<LineClampBuilder>? LineClamp { get; set; }

    /// <summary>
    /// Gets or sets the CSS font-variant-numeric configuration.
    /// </summary>
    public CssValue<FontVariantNumericBuilder>? FontVariantNumeric { get; set; }

    private void CollectCssRules(List<ComponentCssRule> buffer, string baseSelector)
    {
        AddRules(buffer, baseSelector, Display, "display");
        AddRules(buffer, baseSelector, Visibility, "visibility");
        AddRules(buffer, baseSelector, Float, "float");
        AddRules(buffer, baseSelector, VerticalAlign, "vertical-align");
        AddRules(buffer, baseSelector, TextOverflow, "text-overflow");
        AddRules(buffer, baseSelector, Shadow, "box-shadow");
        AddRules(buffer, baseSelector, Margin, "margin");
        AddRules(buffer, baseSelector, Padding, "padding");
        AddRules(buffer, baseSelector, Inset, null);
        AddRules(buffer, baseSelector, Top, null);
        AddRules(buffer, baseSelector, Right, null);
        AddRules(buffer, baseSelector, Bottom, null);
        AddRules(buffer, baseSelector, Left, null);
        AddRules(buffer, baseSelector, Position, "position");
        AddRules(buffer, baseSelector, ScrollMargin, null);
        AddRules(buffer, baseSelector, ScrollPadding, null);
        AddRules(buffer, baseSelector, Size, null);
        AddRules(buffer, baseSelector, TextSize, "font-size");
        AddRules(buffer, baseSelector, Width, "width");
        AddRules(buffer, baseSelector, MinWidth, "min-width");
        AddRules(buffer, baseSelector, MaxWidth, "max-width");
        AddRules(buffer, baseSelector, Height, "height");
        AddRules(buffer, baseSelector, MinHeight, "min-height");
        AddRules(buffer, baseSelector, MaxHeight, "max-height");
        AddRules(buffer, baseSelector, Overflow, "overflow");
        AddRules(buffer, baseSelector, OverflowX, "overflow-x");
        AddRules(buffer, baseSelector, OverflowY, "overflow-y");
        AddRules(buffer, baseSelector, Overscroll, null);
        AddRules(buffer, baseSelector, ObjectFit, "object-fit");
        AddRules(buffer, baseSelector, TextAlign, "text-align");
        AddRules(buffer, baseSelector, TextColor, "color");
        AddRules(buffer, baseSelector, Flex, "flex");
        AddRules(buffer, baseSelector, FlexDirection, "flex-direction");
        AddRules(buffer, baseSelector, FlexWrap, "flex-wrap");
        AddRules(buffer, baseSelector, Grow, "flex-grow");
        AddRules(buffer, baseSelector, Shrink, "flex-shrink");
        AddRules(buffer, baseSelector, Gap, "gap");
        AddRules(buffer, baseSelector, Space, null);
        AddRules(buffer, baseSelector, Divide, null);
        AddRules(buffer, baseSelector, RingOffset, null);
        AddRules(buffer, baseSelector, Fill, null);
        AddRules(buffer, baseSelector, Stroke, null);
        AddRules(buffer, baseSelector, Gradient, null);
        AddRules(buffer, baseSelector, DecorationLine, "text-decoration");
        AddRules(buffer, baseSelector, Tracking, null);
        AddRules(buffer, baseSelector, ContentAlign, null);
        AddRules(buffer, baseSelector, ItemsAlign, "align-items");
        AddRules(buffer, baseSelector, Justify, null);
        AddRules(buffer, baseSelector, SelfAlign, null);
        AddRules(buffer, baseSelector, JustifyItemsAlign, null);
        AddRules(buffer, baseSelector, JustifySelfAlign, null);
        AddRules(buffer, baseSelector, ColStart, null);
        AddRules(buffer, baseSelector, RowSpan, null);
        AddRules(buffer, baseSelector, RowStart, null);
        AddRules(buffer, baseSelector, Border, "border");
        AddRules(buffer, baseSelector, Opacity, "opacity");
        AddRules(buffer, baseSelector, ZIndex, "z-index");
        AddRules(buffer, baseSelector, PointerEvents, "pointer-events");
        AddRules(buffer, baseSelector, UserSelect, "user-select");
        AddRules(buffer, baseSelector, TextTransform, "text-transform");
        AddRules(buffer, baseSelector, FontFamily, "font-family");
        AddRules(buffer, baseSelector, FontWeight, "font-weight");
        AddRules(buffer, baseSelector, FontStyle, "font-style");
        AddRules(buffer, baseSelector, Leading, "line-height");
        AddRules(buffer, baseSelector, Whitespace, "white-space");
        AddRules(buffer, baseSelector, TextWrap, "text-wrap");
        AddRules(buffer, baseSelector, TextBreak, "word-break");
        AddRules(buffer, baseSelector, BorderColor, "border-color");
        AddRules(buffer, baseSelector, BackgroundColor, "background-color");
        AddRules(buffer, baseSelector, Animation, "animation");
        AddRules(buffer, baseSelector, Duration, "transition-duration");
        AddRules(buffer, baseSelector, AspectRatio, "aspect-ratio");
        AddRules(buffer, baseSelector, BackdropFilter, "backdrop-filter");
        AddRules(buffer, baseSelector, Rounded, "border-radius");
        AddRules(buffer, baseSelector, Ring, null);
        AddRules(buffer, baseSelector, RingColor, null);
        AddRules(buffer, baseSelector, ClipPath, "clip-path");
        AddRules(buffer, baseSelector, Cursor, "cursor");
        AddRules(buffer, baseSelector, Filter, "filter");
        AddRules(buffer, baseSelector, ObjectPosition, "object-position");
        AddRules(buffer, baseSelector, Resize, "resize");
        AddRules(buffer, baseSelector, ScreenReader, null);
        AddRules(buffer, baseSelector, ScrollBehavior, "scroll-behavior");
        AddRules(buffer, baseSelector, Transform, "transform");
        AddRules(buffer, baseSelector, Transition, "transition");
        AddRules(buffer, baseSelector, Truncate, null);
        AddRules(buffer, baseSelector, LineClamp, null);
        AddRules(buffer, baseSelector, FontVariantNumeric, "font-variant-numeric");
    }

    private static void AddRules<TBuilder>(List<ComponentCssRule> buffer, string baseSelector, CssValue<TBuilder>? value, string? fallbackProperty = null)
        where TBuilder : class, ICssBuilder
    {
        if (value is not { IsEmpty: false })
            return;

        var declarations = ConvertToDeclarations(value.Value, fallbackProperty);

        if (declarations is null)
            return;

        var resolvedSelector = ResolveSelector(baseSelector, value.Value);

        foreach (var declaration in declarations)
        {
            if (!declaration.HasContent())
                continue;

            buffer.Add(new ComponentCssRule(resolvedSelector, declaration));
        }
    }

    private static IEnumerable<string>? ConvertToDeclarations<TBuilder>(CssValue<TBuilder> value, string? fallbackProperty)
        where TBuilder : class, ICssBuilder
    {
        var styleValue = value.StyleValue;

        if (styleValue.HasContent())
            return SplitDeclarations(styleValue);

        if (fallbackProperty.IsNullOrEmpty())
            return null;

        var rawValue = value.ToString();
        if (!rawValue.HasContent())
            return null;

        if (!value.IsCssStyle)
        {
            IEnumerable<string>? declarations = TryConvertClassOnlyDeclarations<TBuilder>(rawValue, fallbackProperty);
            return declarations;
        }

        return [$"{fallbackProperty}: {rawValue.Trim()}"];
    }

    private static IEnumerable<string>? TryConvertClassOnlyDeclarations<TBuilder>(string rawValue, string fallbackProperty)
        where TBuilder : class, ICssBuilder
    {
        var resolved = rawValue.Trim();

        if (typeof(TBuilder) == typeof(DecorationLineBuilder) &&
            fallbackProperty.Equals("text-decoration", System.StringComparison.Ordinal))
        {
            var decoration = resolved switch
            {
                "no-underline" => "none",
                "underline" => "underline",
                "line-through" => "line-through",
                "overline" => "overline",
                _ => null
            };

            return decoration is null ? null : [$"{fallbackProperty}: {decoration}"];
        }

        if (typeof(TBuilder) == typeof(DisplayBuilder) &&
            fallbackProperty.Equals("display", System.StringComparison.Ordinal))
        {
            var display = ConvertSingleTokenUtility(resolved, static token => token switch
            {
                "hidden" => "none",
                "block" => "block",
                "inline" => "inline",
                "inline-block" => "inline-block",
                "flow-root" => "flow-root",
                "flex" => "flex",
                "inline-flex" => "inline-flex",
                "grid" => "grid",
                "inline-grid" => "inline-grid",
                "table" => "table",
                "table-caption" => "table-caption",
                "table-cell" => "table-cell",
                "table-column" => "table-column",
                "table-column-group" => "table-column-group",
                "table-footer-group" => "table-footer-group",
                "table-header-group" => "table-header-group",
                "table-row" => "table-row",
                "table-row-group" => "table-row-group",
                "contents" => "contents",
                "list-item" => "list-item",
                _ => null
            });

            return display is null ? null : [$"{fallbackProperty}: {display}"];
        }

        if (typeof(TBuilder) == typeof(TextColorBuilder) &&
            fallbackProperty.Equals("color", System.StringComparison.Ordinal))
        {
            var color = ConvertColorUtility(resolved, "text-");
            return color is null ? null : [$"{fallbackProperty}: {color}"];
        }

        if (typeof(TBuilder) == typeof(BackgroundColorBuilder) &&
            fallbackProperty.Equals("background-color", System.StringComparison.Ordinal))
        {
            var color = ConvertColorUtility(resolved, "bg-");
            return color is null ? null : [$"{fallbackProperty}: {color}"];
        }

        if (typeof(TBuilder) == typeof(BorderColorBuilder) &&
            fallbackProperty.Equals("border-color", System.StringComparison.Ordinal))
        {
            var color = ConvertColorUtility(resolved, "border-");
            return color is null ? null : [$"{fallbackProperty}: {color}"];
        }

        if (typeof(TBuilder) == typeof(PaddingBuilder) &&
            fallbackProperty.Equals("padding", System.StringComparison.Ordinal))
        {
            return ConvertPaddingUtilities(resolved);
        }

        if (typeof(TBuilder) == typeof(WidthBuilder) &&
            (fallbackProperty.Equals("width", System.StringComparison.Ordinal) ||
             fallbackProperty.Equals("min-width", System.StringComparison.Ordinal) ||
             fallbackProperty.Equals("max-width", System.StringComparison.Ordinal)))
        {
            var width = ConvertWidthUtility(resolved);
            return width is null ? null : [$"{fallbackProperty}: {width}"];
        }

        if (typeof(TBuilder) == typeof(OverflowBuilder) &&
            (fallbackProperty.Equals("overflow", System.StringComparison.Ordinal) ||
             fallbackProperty.Equals("overflow-x", System.StringComparison.Ordinal) ||
             fallbackProperty.Equals("overflow-y", System.StringComparison.Ordinal)))
        {
            var overflow = ConvertSingleTokenUtility(resolved, static token => token switch
            {
                "overflow-auto" => "auto",
                "overflow-hidden" => "hidden",
                "overflow-clip" => "clip",
                "overflow-visible" => "visible",
                "overflow-scroll" => "scroll",
                _ => null
            });

            return overflow is null ? null : [$"{fallbackProperty}: {overflow}"];
        }

        if (typeof(TBuilder) == typeof(TextSizeBuilder) &&
            fallbackProperty.Equals("font-size", System.StringComparison.Ordinal))
        {
            var textSize = ConvertTextSizeUtility(resolved);
            return textSize is null ? null : [$"{fallbackProperty}: {textSize}"];
        }

        if (typeof(TBuilder) == typeof(ItemsBuilder) &&
            fallbackProperty.Equals("align-items", System.StringComparison.Ordinal))
        {
            var alignItems = ConvertSingleTokenUtility(resolved, static token => token switch
            {
                "items-start" => "flex-start",
                "items-end" => "flex-end",
                "items-center" => "center",
                "items-baseline" => "baseline",
                "items-stretch" => "stretch",
                _ => null
            });

            return alignItems is null ? null : [$"{fallbackProperty}: {alignItems}"];
        }

        if (typeof(TBuilder) == typeof(GapBuilder) &&
            fallbackProperty.Equals("gap", System.StringComparison.Ordinal))
        {
            var gap = ConvertSpacingUtility(resolved, "gap-");
            return gap is null ? null : [$"{fallbackProperty}: {gap}"];
        }

        if (typeof(TBuilder) == typeof(BorderBuilder) &&
            fallbackProperty.Equals("border", System.StringComparison.Ordinal))
        {
            return ConvertBorderUtilities(resolved);
        }

        if (typeof(TBuilder) == typeof(VerticalAlignBuilder) &&
            fallbackProperty.Equals("vertical-align", System.StringComparison.Ordinal))
        {
            var verticalAlign = ConvertSingleTokenUtility(resolved, static token => token switch
            {
                "align-baseline" => "baseline",
                "align-top" => "top",
                "align-middle" => "middle",
                "align-bottom" => "bottom",
                "align-text-top" => "text-top",
                "align-text-bottom" => "text-bottom",
                "align-sub" => "sub",
                "align-super" => "super",
                _ => null
            });

            return verticalAlign is null ? null : [$"{fallbackProperty}: {verticalAlign}"];
        }

        if (typeof(TBuilder) == typeof(TextOverflowBuilder) &&
            fallbackProperty.Equals("text-overflow", System.StringComparison.Ordinal))
        {
            var textOverflow = ConvertSingleTokenUtility(resolved, static token => token switch
            {
                "text-ellipsis" => "ellipsis",
                "text-clip" => "clip",
                _ => null
            });

            return textOverflow is null ? null : [$"{fallbackProperty}: {textOverflow}"];
        }

        if (typeof(TBuilder) == typeof(RoundedBuilder) &&
            fallbackProperty.Equals("border-radius", System.StringComparison.Ordinal))
        {
            var radius = ConvertRoundedUtility(resolved);
            return radius is null ? null : [$"{fallbackProperty}: {radius}"];
        }

        if (typeof(TBuilder) == typeof(ShadowBuilder) &&
            fallbackProperty.Equals("box-shadow", System.StringComparison.Ordinal))
        {
            var shadow = ConvertShadowUtility(resolved);
            return shadow is null ? null : [$"{fallbackProperty}: {shadow}"];
        }

        if (typeof(TBuilder) == typeof(FontWeightBuilder) &&
            fallbackProperty.Equals("font-weight", System.StringComparison.Ordinal))
        {
            var fontWeight = ConvertSingleTokenUtility(resolved, static token => token switch
            {
                "font-thin" => "100",
                "font-extralight" => "200",
                "font-light" => "300",
                "font-normal" => "400",
                "font-medium" => "500",
                "font-semibold" => "600",
                "font-bold" => "700",
                "font-extrabold" => "800",
                "font-black" => "900",
                _ => null
            });

            return fontWeight is null ? null : [$"{fallbackProperty}: {fontWeight}"];
        }

        if (typeof(TBuilder) == typeof(WhitespaceBuilder) &&
            fallbackProperty.Equals("white-space", System.StringComparison.Ordinal))
        {
            var whitespace = ConvertSingleTokenUtility(resolved, static token => token switch
            {
                "whitespace-normal" => "normal",
                "whitespace-nowrap" => "nowrap",
                "whitespace-pre" => "pre",
                "whitespace-pre-line" => "pre-line",
                "whitespace-pre-wrap" => "pre-wrap",
                "whitespace-break-spaces" => "break-spaces",
                _ => null
            });

            return whitespace is null ? null : [$"{fallbackProperty}: {whitespace}"];
        }

        if (typeof(TBuilder) == typeof(FlexDirectionBuilder) &&
            fallbackProperty.Equals("flex-direction", System.StringComparison.Ordinal))
        {
            return TryConvertFlexComposite(resolved, fallbackProperty, static token => token switch
            {
                "flex-row" => "row",
                "flex-row-reverse" => "row-reverse",
                "flex-col" => "column",
                "flex-col-reverse" => "column-reverse",
                _ => null
            });
        }

        if (typeof(TBuilder) == typeof(FlexWrapBuilder) &&
            fallbackProperty.Equals("flex-wrap", System.StringComparison.Ordinal))
        {
            return TryConvertFlexComposite(resolved, fallbackProperty, static token => token switch
            {
                "flex-wrap" => "wrap",
                "flex-wrap-reverse" => "wrap-reverse",
                "flex-nowrap" => "nowrap",
                _ => null
            });
        }

        if (typeof(TBuilder) == typeof(GrowBuilder) &&
            fallbackProperty.Equals("flex-grow", System.StringComparison.Ordinal))
        {
            var grow = resolved switch
            {
                "grow" => "1",
                "grow-0" => "0",
                _ => null
            };

            return grow is null ? null : [$"{fallbackProperty}: {grow}"];
        }

        if (typeof(TBuilder) == typeof(ShrinkBuilder) &&
            fallbackProperty.Equals("flex-shrink", System.StringComparison.Ordinal))
        {
            var shrink = resolved switch
            {
                "shrink" => "1",
                "shrink-0" => "0",
                _ => null
            };

            return shrink is null ? null : [$"{fallbackProperty}: {shrink}"];
        }

        if (typeof(TBuilder) == typeof(DurationBuilder) &&
            fallbackProperty.Equals("transition-duration", System.StringComparison.Ordinal))
        {
            var duration = ConvertDurationUtility(resolved);
            return duration is null ? null : [$"{fallbackProperty}: {duration}"];
        }

        return null;
    }

    private static string? ConvertSingleTokenUtility(string value, System.Func<string, string?> converter)
    {
        if (value.Contains(':') || value.Contains(' '))
            return null;

        return converter(value);
    }

    private static string? ConvertColorUtility(string value, string prefix)
    {
        if (!value.StartsWith(prefix, System.StringComparison.Ordinal) || value.Contains(':') || value.Contains('/'))
            return null;

        string token = value.Substring(prefix.Length);
        string? arbitrary = ConvertArbitraryToken(token);

        if (arbitrary is not null)
            return arbitrary;

        return token switch
        {
            "black" => "#000",
            "white" => "#fff",
            "transparent" => "transparent",
            "inherit" => "inherit",
            "current" => "currentColor",
            "background" => "var(--background)",
            "foreground" => "var(--foreground)",
            "card" => "var(--card)",
            "card-foreground" => "var(--card-foreground)",
            "popover" => "var(--popover)",
            "popover-foreground" => "var(--popover-foreground)",
            "primary" => "var(--primary)",
            "primary-foreground" => "var(--primary-foreground)",
            "secondary" => "var(--secondary)",
            "secondary-foreground" => "var(--secondary-foreground)",
            "muted" => "var(--muted)",
            "muted-foreground" => "var(--muted-foreground)",
            "accent" => "var(--accent)",
            "accent-foreground" => "var(--accent-foreground)",
            "destructive" => "var(--destructive)",
            "destructive-foreground" => "var(--destructive-foreground)",
            "border" => "var(--border)",
            _ => IsPaletteColorToken(token) ? $"var(--color-{token})" : $"var(--{token})"
        };
    }

    private static string? ConvertWidthUtility(string value)
    {
        if (value.Contains(':') || value.Contains(' '))
            return null;

        string token = value;

        if (token.StartsWith("min-w-", System.StringComparison.Ordinal))
            token = token.Substring("min-w-".Length);
        else if (token.StartsWith("max-w-", System.StringComparison.Ordinal))
            token = token.Substring("max-w-".Length);
        else if (token.StartsWith("w-", System.StringComparison.Ordinal))
            token = token.Substring("w-".Length);
        else
            return null;

        string? arbitrary = ConvertArbitraryToken(token);
        if (arbitrary is not null)
            return arbitrary;

        string? spacing = ConvertSpacingScaleToken(token);
        if (spacing is not null)
            return spacing;

        string? fraction = ConvertFractionToken(token);
        if (fraction is not null)
            return fraction;

        return token switch
        {
            "auto" => "auto",
            "full" => "100%",
            "screen" => "100vw",
            "svw" => "100svw",
            "lvw" => "100lvw",
            "dvw" => "100dvw",
            "min" => "min-content",
            "max" => "max-content",
            "fit" => "fit-content",
            _ => null
        };
    }

    private static string? ConvertTextSizeUtility(string value)
    {
        if (!value.StartsWith("text-", System.StringComparison.Ordinal) || value.Contains(':') || value.Contains(' '))
            return null;

        string token = value.Substring("text-".Length);

        string? arbitrary = ConvertArbitraryToken(token);
        if (arbitrary is not null)
            return arbitrary;

        return token switch
        {
            "xs" => "var(--text-xs)",
            "sm" => "var(--text-sm)",
            "base" => "var(--text-base)",
            "lg" => "var(--text-lg)",
            "xl" => "var(--text-xl)",
            "2xl" => "var(--text-2xl)",
            "3xl" => "var(--text-3xl)",
            "4xl" => "var(--text-4xl)",
            "5xl" => "var(--text-5xl)",
            "6xl" => "var(--text-6xl)",
            "7xl" => "var(--text-7xl)",
            "8xl" => "var(--text-8xl)",
            "9xl" => "var(--text-9xl)",
            _ => null
        };
    }

    private static string? ConvertSpacingUtility(string value, string prefix)
    {
        if (!value.StartsWith(prefix, System.StringComparison.Ordinal) || value.Contains(':') || value.Contains(' '))
            return null;

        string token = value.Substring(prefix.Length);
        return ConvertArbitraryToken(token) ?? ConvertSpacingScaleToken(token);
    }

    private static IEnumerable<string>? ConvertBorderUtilities(string value)
    {
        if (value.Contains(':') || value.Contains(' '))
            return null;

        string? property = value switch
        {
            "border" => "border-width",
            "border-x" => "border-inline-width",
            "border-y" => "border-block-width",
            "border-s" => "border-inline-start-width",
            "border-e" => "border-inline-end-width",
            "border-t" => "border-top-width",
            "border-r" => "border-right-width",
            "border-b" => "border-bottom-width",
            "border-l" => "border-left-width",
            _ => null
        };

        string token;

        if (property is not null)
            token = "1";
        else if (value.StartsWith("border-", System.StringComparison.Ordinal))
        {
            string suffix = value.Substring("border-".Length);
            var dash = suffix.IndexOf('-');

            if (dash > 0)
            {
                string side = suffix.Substring(0, dash);
                token = suffix.Substring(dash + 1);
                property = side switch
                {
                    "x" => "border-inline-width",
                    "y" => "border-block-width",
                    "s" => "border-inline-start-width",
                    "e" => "border-inline-end-width",
                    "t" => "border-top-width",
                    "r" => "border-right-width",
                    "b" => "border-bottom-width",
                    "l" => "border-left-width",
                    _ => null
                };
            }
            else
            {
                token = suffix;
                property = "border-width";
            }
        }
        else
            return null;

        if (property is null)
            return null;

        string? width = ConvertBorderWidthToken(token);
        return width is null ? null : [$"{property}: {width}", property.Replace("-width", "-style") + ": solid"];
    }

    private static string? ConvertBorderWidthToken(string token)
    {
        string? arbitrary = ConvertArbitraryToken(token);
        if (arbitrary is not null)
            return arbitrary;

        return token switch
        {
            "0" => "0",
            "1" => "1px",
            "2" => "2px",
            "4" => "4px",
            "8" => "8px",
            _ => null
        };
    }

    private static string? ConvertFractionToken(string token)
    {
        return token switch
        {
            "1/2" => "50%",
            "1/3" => "33.333333%",
            "2/3" => "66.666667%",
            "1/4" => "25%",
            "2/4" => "50%",
            "3/4" => "75%",
            "1/5" => "20%",
            "2/5" => "40%",
            "3/5" => "60%",
            "4/5" => "80%",
            "1/6" => "16.666667%",
            "5/6" => "83.333333%",
            "1/12" => "8.333333%",
            "2/12" => "16.666667%",
            "3/12" => "25%",
            "4/12" => "33.333333%",
            "5/12" => "41.666667%",
            "6/12" => "50%",
            "7/12" => "58.333333%",
            "8/12" => "66.666667%",
            "9/12" => "75%",
            "10/12" => "83.333333%",
            "11/12" => "91.666667%",
            _ => null
        };
    }

    private static string? ConvertArbitraryToken(string token)
    {
        if (token.Length < 2)
            return null;

        if (token[0] == '[' && token[^1] == ']')
            return token[1..^1].Replace('_', ' ');

        if (token[0] == '(' && token[^1] == ')')
            return $"var({token[1..^1]})";

        return null;
    }

    private static bool IsPaletteColorToken(string token)
    {
        int dash = token.LastIndexOf('-');
        if (dash <= 0 || dash == token.Length - 1)
            return false;

        string family = token.Substring(0, dash);
        string shade = token.Substring(dash + 1);

        return IsPaletteColorFamily(family) && IsPaletteColorShade(shade);
    }

    private static bool IsPaletteColorFamily(string family)
    {
        return family is "slate" or "gray" or "zinc" or "neutral" or "stone" or "red" or "orange" or "amber"
            or "yellow" or "lime" or "green" or "emerald" or "teal" or "cyan" or "sky" or "blue"
            or "indigo" or "violet" or "purple" or "fuchsia" or "pink" or "rose";
    }

    private static bool IsPaletteColorShade(string shade)
    {
        return shade is "50" or "100" or "200" or "300" or "400" or "500" or "600" or "700" or "800" or "900" or "950";
    }

    private static IEnumerable<string>? ConvertPaddingUtilities(string value)
    {
        string[] tokens = value.Split(' ');
        var declarations = new List<string>(tokens.Length * 2);

        for (var i = 0; i < tokens.Length; i++)
        {
            string token = tokens[i].Trim();
            if (token.Length == 0)
                continue;

            if (token.Contains(':'))
                return null;

            string? spacing = ConvertSpacingToken(token);
            if (spacing is null)
                return null;

            if (token.StartsWith("px-", System.StringComparison.Ordinal))
            {
                declarations.Add($"padding-left: {spacing}");
                declarations.Add($"padding-right: {spacing}");
            }
            else if (token.StartsWith("py-", System.StringComparison.Ordinal))
            {
                declarations.Add($"padding-top: {spacing}");
                declarations.Add($"padding-bottom: {spacing}");
            }
            else if (token.StartsWith("pt-", System.StringComparison.Ordinal))
                declarations.Add($"padding-top: {spacing}");
            else if (token.StartsWith("pr-", System.StringComparison.Ordinal))
                declarations.Add($"padding-right: {spacing}");
            else if (token.StartsWith("pb-", System.StringComparison.Ordinal))
                declarations.Add($"padding-bottom: {spacing}");
            else if (token.StartsWith("pl-", System.StringComparison.Ordinal))
                declarations.Add($"padding-left: {spacing}");
            else if (token.StartsWith("ps-", System.StringComparison.Ordinal))
                declarations.Add($"padding-inline-start: {spacing}");
            else if (token.StartsWith("pe-", System.StringComparison.Ordinal))
                declarations.Add($"padding-inline-end: {spacing}");
            else if (token.StartsWith("p-", System.StringComparison.Ordinal))
                declarations.Add($"padding: {spacing}");
            else
                return null;
        }

        return declarations.Count == 0 ? null : declarations;
    }

    private static string? ConvertSpacingToken(string utility)
    {
        int dash = utility.IndexOf('-');
        if (dash < 0 || dash == utility.Length - 1)
            return null;

        string token = utility.Substring(dash + 1);

        return ConvertSpacingScaleToken(token);
    }

    private static string? ConvertSpacingScaleToken(string token)
    {
        return token switch
        {
            "0" => "0",
            "0.5" => "0.125rem",
            "1" => "0.25rem",
            "1.5" => "0.375rem",
            "2" => "0.5rem",
            "2.5" => "0.625rem",
            "3" => "0.75rem",
            "3.5" => "0.875rem",
            "4" => "1rem",
            "5" => "1.25rem",
            "6" => "1.5rem",
            "7" => "1.75rem",
            "8" => "2rem",
            "9" => "2.25rem",
            "10" => "2.5rem",
            "12" => "3rem",
            "14" => "3.5rem",
            "16" => "4rem",
            "20" => "5rem",
            "px" => "1px",
            _ => null
        };
    }

    private static string? ConvertRoundedUtility(string value)
    {
        return value switch
        {
            "rounded-none" => "0",
            "rounded-sm" => "0.125rem",
            "rounded" => "0.25rem",
            "rounded-md" => "0.375rem",
            "rounded-lg" => "0.5rem",
            "rounded-xl" => "0.75rem",
            "rounded-2xl" => "1rem",
            "rounded-3xl" => "1.5rem",
            "rounded-full" => "9999px",
            _ => null
        };
    }

    private static string? ConvertShadowUtility(string value)
    {
        return value switch
        {
            "shadow-none" => "none",
            "shadow-xs" => "0 1px 2px 0 rgb(0 0 0 / 0.05)",
            "shadow-sm" => "0 1px 3px 0 rgb(0 0 0 / 0.1), 0 1px 2px -1px rgb(0 0 0 / 0.1)",
            "shadow" => "0 1px 3px 0 rgb(0 0 0 / 0.1), 0 1px 2px -1px rgb(0 0 0 / 0.1)",
            "shadow-md" => "0 4px 6px -1px rgb(0 0 0 / 0.1), 0 2px 4px -2px rgb(0 0 0 / 0.1)",
            "shadow-lg" => "0 10px 15px -3px rgb(0 0 0 / 0.1), 0 4px 6px -4px rgb(0 0 0 / 0.1)",
            "shadow-xl" => "0 20px 25px -5px rgb(0 0 0 / 0.1), 0 8px 10px -6px rgb(0 0 0 / 0.1)",
            "shadow-2xl" => "0 25px 50px -12px rgb(0 0 0 / 0.25)",
            "shadow-inner" => "inset 0 2px 4px 0 rgb(0 0 0 / 0.05)",
            _ => null
        };
    }

    private static string? ConvertDurationUtility(string value)
    {
        if (!value.StartsWith("duration-", System.StringComparison.Ordinal) || value.Contains(':'))
            return null;

        var token = value["duration-".Length..];

        if (token.Length == 0)
            return null;

        if (token.Length >= 2 && token[0] == '[' && token[^1] == ']')
            return token[1..^1];

        if (token.Length >= 2 && token[0] == '(' && token[^1] == ')')
            return $"var({token[1..^1]})";

        return token == "0" ? "0s" : token + "ms";
    }

    private static IEnumerable<string>? TryConvertFlexComposite(string rawValue, string fallbackProperty, System.Func<string, string?> converter)
    {
        var hasFlexDisplay = false;
        string? resolvedValue = null;
        var tokenStart = -1;

        for (var i = 0; i <= rawValue.Length; i++)
        {
            if (i < rawValue.Length && !char.IsWhiteSpace(rawValue[i]))
            {
                if (tokenStart < 0)
                    tokenStart = i;

                continue;
            }

            if (tokenStart < 0)
                continue;

            var token = rawValue.Substring(tokenStart, i - tokenStart);

            if (token.Contains(':'))
                return null;

            if (token.Equals("flex", System.StringComparison.Ordinal))
            {
                hasFlexDisplay = true;
                tokenStart = -1;
                continue;
            }

            var converted = converter(token);
            if (converted is not null)
                resolvedValue = converted;

            tokenStart = -1;
        }

        if (!hasFlexDisplay || resolvedValue is null)
            return null;

        return ["display: flex", $"{fallbackProperty}: {resolvedValue}"];
    }

    private static IEnumerable<string> SplitDeclarations(string value)
    {
        var segmentStart = 0;

        for (var i = 0; i <= value.Length; i++)
        {
            if (i < value.Length && value[i] != ';')
                continue;

            var segment = value.Substring(segmentStart, i - segmentStart).Trim();

            if (segment.Length > 0)
                yield return segment;

            segmentStart = i + 1;
        }
    }

    private static string ResolveSelector<TBuilder>(string baseSelector, CssValue<TBuilder> value)
        where TBuilder : class, ICssBuilder
    {
        var custom = value.CssSelector;

        if (custom.IsNullOrWhiteSpace())
            return baseSelector;

        var trimmed = custom.Trim();

        if (value.SelectorIsAbsolute)
            return trimmed;

        if (trimmed.Contains('&'))
            return trimmed.Replace("&", baseSelector);

        var first = trimmed[0];

        if (_cssSelectorPrefixChars.Contains(first))
            return baseSelector + trimmed;

        return $"{baseSelector} {trimmed}";
    }
}
