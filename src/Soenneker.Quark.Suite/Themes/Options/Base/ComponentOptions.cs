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
        if (Selector.IsNullOrWhiteSpace())
            return [];

        var buffer = new List<ComponentCssRule>(32);
        CollectCssRules(buffer, Selector);
        return buffer;
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
    /// Gets or sets the CSS position configuration.
    /// </summary>
    public CssValue<PositionBuilder>? Position { get; set; }

    /// <summary>
    /// Gets or sets the CSS scroll-margin configuration.
    /// </summary>
    public CssValue<ScrollMarginBuilder>? ScrollMargin { get; set; }

    /// <summary>
    /// Gets or sets the CSS TextColor size (font-size) configuration.
    /// </summary>
    public CssValue<TextSizeBuilder>? TextSize { get; set; }

    /// <summary>
    /// Gets or sets the CSS width configuration.
    /// </summary>
    public CssValue<SizeBuilder>? BoxSize { get; set; }

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
        AddRules(buffer, baseSelector, Shadow, "shadow");
        AddRules(buffer, baseSelector, Margin, "margin");
        AddRules(buffer, baseSelector, Padding, "padding");
        AddRules(buffer, baseSelector, Inset, null);
        AddRules(buffer, baseSelector, Position, "position");
        AddRules(buffer, baseSelector, ScrollMargin, null);
        AddRules(buffer, baseSelector, BoxSize, null);
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
        AddRules(buffer, baseSelector, ItemsAlign, null);
        AddRules(buffer, baseSelector, Justify, null);
        AddRules(buffer, baseSelector, SelfAlign, null);
        AddRules(buffer, baseSelector, JustifyItemsAlign, null);
        AddRules(buffer, baseSelector, JustifySelfAlign, null);
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
        AddRules(buffer, baseSelector, Border, "border-color");
        AddRules(buffer, baseSelector, BackgroundColor, "background-color");
        AddRules(buffer, baseSelector, Animation, "animation");
        AddRules(buffer, baseSelector, AspectRatio, "aspect-ratio");
        AddRules(buffer, baseSelector, BackdropFilter, "backdrop-filter");
        AddRules(buffer, baseSelector, Rounded, "border-radius");
        AddRules(buffer, baseSelector, Ring, null);
        AddRules(buffer, baseSelector, Ring, null);
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
            return TryConvertClassOnlyDeclarations<TBuilder>(rawValue, fallbackProperty);

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

        return null;
    }

    private static IEnumerable<string>? TryConvertFlexComposite(string rawValue, string fallbackProperty, System.Func<string, string?> converter)
    {
        var tokens = rawValue.Split(' ', System.StringSplitOptions.RemoveEmptyEntries | System.StringSplitOptions.TrimEntries);
        var hasFlexDisplay = false;
        string? resolvedValue = null;

        foreach (var token in tokens)
        {
            if (token.Contains(':'))
                return null;

            if (token.Equals("flex", System.StringComparison.Ordinal))
            {
                hasFlexDisplay = true;
                continue;
            }

            var converted = converter(token);
            if (converted is not null)
                resolvedValue = converted;
        }

        if (!hasFlexDisplay || resolvedValue is null)
            return null;

        return ["display: flex", $"{fallbackProperty}: {resolvedValue}"];
    }

    private static IEnumerable<string> SplitDeclarations(string value)
    {
        var segments = value.Split(';');

        foreach (var segment in segments)
        {
            var trimmed = segment.Trim();

            if (trimmed.Length > 0)
                yield return trimmed;
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
