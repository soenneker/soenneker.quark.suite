using System;
using System.Buffers;
using System.Runtime.CompilerServices;
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

    internal void CollectCssRules(ref ComponentCssRuleCollector buffer)
    {
        if (Selector.IsNullOrWhiteSpace())
            return;

        CollectCssRules(ref buffer, Selector);
        CollectChildCssRules(ref buffer, Selector);
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
        options.CollectChildCssRules(ref buffer, scopedSelector);
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
    public CssValue<MinHeightBuilder>? MinHeight { get; set; }

    /// <summary>
    /// Gets or sets the CSS maximum height configuration.
    /// </summary>
    public CssValue<MaxHeightBuilder>? MaxHeight { get; set; }

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

    /// <summary>Gets or sets the distance between text and its underline, including responsive and state variants.</summary>
    public CssValue<UnderlineOffsetBuilder>? UnderlineOffset { get; set; }

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
    /// Gets or sets border-style utility classes.
    /// </summary>
    public CssValue<BorderStyleBuilder>? BorderStyle { get; set; }

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

    private void CollectCssRules(ref ComponentCssRuleCollector buffer, string baseSelector)
    {
        if (Display.HasValue)
            AddRules(ref buffer, baseSelector, Display, "display");
        if (Visibility.HasValue)
            AddRules(ref buffer, baseSelector, Visibility, "visibility");
        if (Float.HasValue)
            AddRules(ref buffer, baseSelector, Float, "float");
        if (VerticalAlign.HasValue)
            AddRules(ref buffer, baseSelector, VerticalAlign, "vertical-align");
        if (TextOverflow.HasValue)
            AddRules(ref buffer, baseSelector, TextOverflow, "text-overflow");
        if (Shadow.HasValue)
            AddRules(ref buffer, baseSelector, Shadow, "box-shadow");
        if (Margin.HasValue)
            AddRules(ref buffer, baseSelector, Margin, "margin");
        if (Padding.HasValue)
            AddRules(ref buffer, baseSelector, Padding, "padding");
        if (Inset.HasValue)
            AddRules(ref buffer, baseSelector, Inset, null);
        if (Top.HasValue)
            AddRules(ref buffer, baseSelector, Top, null);
        if (Right.HasValue)
            AddRules(ref buffer, baseSelector, Right, null);
        if (Bottom.HasValue)
            AddRules(ref buffer, baseSelector, Bottom, null);
        if (Left.HasValue)
            AddRules(ref buffer, baseSelector, Left, null);
        if (Position.HasValue)
            AddRules(ref buffer, baseSelector, Position, "position");
        if (ScrollMargin.HasValue)
            AddRules(ref buffer, baseSelector, ScrollMargin, null);
        if (ScrollPadding.HasValue)
            AddRules(ref buffer, baseSelector, ScrollPadding, null);
        if (Size.HasValue)
            AddRules(ref buffer, baseSelector, Size, null);
        if (TextSize.HasValue)
            AddRules(ref buffer, baseSelector, TextSize, "font-size");
        if (Width.HasValue)
            AddRules(ref buffer, baseSelector, Width, "width");
        if (MinWidth.HasValue)
            AddRules(ref buffer, baseSelector, MinWidth, "min-width");
        if (MaxWidth.HasValue)
            AddRules(ref buffer, baseSelector, MaxWidth, "max-width");
        if (Height.HasValue)
            AddRules(ref buffer, baseSelector, Height, "height");
        if (MinHeight.HasValue)
            AddRules(ref buffer, baseSelector, MinHeight, "min-height");
        if (MaxHeight.HasValue)
            AddRules(ref buffer, baseSelector, MaxHeight, "max-height");
        if (Overflow.HasValue)
            AddRules(ref buffer, baseSelector, Overflow, "overflow");
        if (OverflowX.HasValue)
            AddRules(ref buffer, baseSelector, OverflowX, "overflow-x");
        if (OverflowY.HasValue)
            AddRules(ref buffer, baseSelector, OverflowY, "overflow-y");
        if (Overscroll.HasValue)
            AddRules(ref buffer, baseSelector, Overscroll, null);
        if (ObjectFit.HasValue)
            AddRules(ref buffer, baseSelector, ObjectFit, "object-fit");
        if (TextAlign.HasValue)
            AddRules(ref buffer, baseSelector, TextAlign, "text-align");
        if (TextColor.HasValue)
            AddRules(ref buffer, baseSelector, TextColor, "color");
        if (Flex.HasValue)
            AddRules(ref buffer, baseSelector, Flex, "flex");
        if (FlexDirection.HasValue)
            AddRules(ref buffer, baseSelector, FlexDirection, "flex-direction");
        if (FlexWrap.HasValue)
            AddRules(ref buffer, baseSelector, FlexWrap, "flex-wrap");
        if (Grow.HasValue)
            AddRules(ref buffer, baseSelector, Grow, "flex-grow");
        if (Shrink.HasValue)
            AddRules(ref buffer, baseSelector, Shrink, "flex-shrink");
        if (Gap.HasValue)
            AddRules(ref buffer, baseSelector, Gap, "gap");
        if (Space.HasValue)
            AddRules(ref buffer, baseSelector, Space, null);
        if (Divide.HasValue)
            AddRules(ref buffer, baseSelector, Divide, null);
        if (RingOffset.HasValue)
            AddRules(ref buffer, baseSelector, RingOffset, null);
        if (Fill.HasValue)
            AddRules(ref buffer, baseSelector, Fill, null);
        if (Stroke.HasValue)
            AddRules(ref buffer, baseSelector, Stroke, null);
        if (Gradient.HasValue)
            AddRules(ref buffer, baseSelector, Gradient, null);
        if (DecorationLine.HasValue)
            AddRules(ref buffer, baseSelector, DecorationLine, "text-decoration");
        if (UnderlineOffset.HasValue)
            AddRules(ref buffer, baseSelector, UnderlineOffset, "text-underline-offset");
        if (Tracking.HasValue)
            AddRules(ref buffer, baseSelector, Tracking, null);
        if (ContentAlign.HasValue)
            AddRules(ref buffer, baseSelector, ContentAlign, null);
        if (ItemsAlign.HasValue)
            AddRules(ref buffer, baseSelector, ItemsAlign, "align-items");
        if (Justify.HasValue)
            AddRules(ref buffer, baseSelector, Justify, null);
        if (SelfAlign.HasValue)
            AddRules(ref buffer, baseSelector, SelfAlign, null);
        if (JustifyItemsAlign.HasValue)
            AddRules(ref buffer, baseSelector, JustifyItemsAlign, null);
        if (JustifySelfAlign.HasValue)
            AddRules(ref buffer, baseSelector, JustifySelfAlign, null);
        if (ColStart.HasValue)
            AddRules(ref buffer, baseSelector, ColStart, null);
        if (RowSpan.HasValue)
            AddRules(ref buffer, baseSelector, RowSpan, null);
        if (RowStart.HasValue)
            AddRules(ref buffer, baseSelector, RowStart, null);
        if (Border.HasValue)
            AddRules(ref buffer, baseSelector, Border, "border");
        if (BorderStyle.HasValue)
            AddRules(ref buffer, baseSelector, BorderStyle, "border-style");
        if (Opacity.HasValue)
            AddRules(ref buffer, baseSelector, Opacity, "opacity");
        if (ZIndex.HasValue)
            AddRules(ref buffer, baseSelector, ZIndex, "z-index");
        if (PointerEvents.HasValue)
            AddRules(ref buffer, baseSelector, PointerEvents, "pointer-events");
        if (UserSelect.HasValue)
            AddRules(ref buffer, baseSelector, UserSelect, "user-select");
        if (TextTransform.HasValue)
            AddRules(ref buffer, baseSelector, TextTransform, "text-transform");
        if (FontFamily.HasValue)
            AddRules(ref buffer, baseSelector, FontFamily, "font-family");
        if (FontWeight.HasValue)
            AddRules(ref buffer, baseSelector, FontWeight, "font-weight");
        if (FontStyle.HasValue)
            AddRules(ref buffer, baseSelector, FontStyle, "font-style");
        if (Leading.HasValue)
            AddRules(ref buffer, baseSelector, Leading, "line-height");
        if (Whitespace.HasValue)
            AddRules(ref buffer, baseSelector, Whitespace, "white-space");
        if (TextWrap.HasValue)
            AddRules(ref buffer, baseSelector, TextWrap, "text-wrap");
        if (TextBreak.HasValue)
            AddRules(ref buffer, baseSelector, TextBreak, "word-break");
        if (BorderColor.HasValue)
            AddRules(ref buffer, baseSelector, BorderColor, "border-color");
        if (BackgroundColor.HasValue)
            AddRules(ref buffer, baseSelector, BackgroundColor, "background-color");
        if (Animation.HasValue)
            AddRules(ref buffer, baseSelector, Animation, "animation");
        if (Duration.HasValue)
            AddRules(ref buffer, baseSelector, Duration, "transition-duration");
        if (AspectRatio.HasValue)
            AddRules(ref buffer, baseSelector, AspectRatio, "aspect-ratio");
        if (BackdropFilter.HasValue)
            AddRules(ref buffer, baseSelector, BackdropFilter, "backdrop-filter");
        if (Rounded.HasValue)
            AddRules(ref buffer, baseSelector, Rounded, "border-radius");
        if (Ring.HasValue)
            AddRules(ref buffer, baseSelector, Ring, null);
        if (RingColor.HasValue)
            AddRules(ref buffer, baseSelector, RingColor, null);
        if (ClipPath.HasValue)
            AddRules(ref buffer, baseSelector, ClipPath, "clip-path");
        if (Cursor.HasValue)
            AddRules(ref buffer, baseSelector, Cursor, "cursor");
        if (Filter.HasValue)
            AddRules(ref buffer, baseSelector, Filter, "filter");
        if (ObjectPosition.HasValue)
            AddRules(ref buffer, baseSelector, ObjectPosition, "object-position");
        if (Resize.HasValue)
            AddRules(ref buffer, baseSelector, Resize, "resize");
        if (ScreenReader.HasValue)
            AddRules(ref buffer, baseSelector, ScreenReader, null);
        if (ScrollBehavior.HasValue)
            AddRules(ref buffer, baseSelector, ScrollBehavior, "scroll-behavior");
        if (Transform.HasValue)
            AddRules(ref buffer, baseSelector, Transform, "transform");
        if (Transition.HasValue)
            AddRules(ref buffer, baseSelector, Transition, "transition");
        if (Truncate.HasValue)
            AddRules(ref buffer, baseSelector, Truncate, null);
        if (LineClamp.HasValue)
            AddRules(ref buffer, baseSelector, LineClamp, null);
        if (FontVariantNumeric.HasValue)
            AddRules(ref buffer, baseSelector, FontVariantNumeric, "font-variant-numeric");
    }

    // CollectCssRules calls this for every closed CssValue<TBuilder> used by Quark. Keep the
    // generic nullable-struct work out of that large caller for the same Mono AOT safety reason
    // as RenderComponent.AddCss.
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void AddRules<TBuilder>(ref ComponentCssRuleCollector buffer, string baseSelector, CssValue<TBuilder>? value, string? fallbackProperty = null)
        where TBuilder : class, ICssBuilder
    {
        if (value is not { IsEmpty: false })
            return;

        var style = value.Value.StyleValue;
        if (style.HasContent())
        {
            buffer.AddStyle(ResolveSelector(baseSelector, value.Value), style);
            return;
        }

        if (fallbackProperty.IsNullOrEmpty())
            return;

        var rawValue = value.Value.ToString();
        if (!rawValue.HasContent())
            return;

        var selector = ResolveSelector(baseSelector, value.Value);
        if (value.Value.IsCssStyle)
            AddDeclaration(ref buffer, selector, fallbackProperty, rawValue.Trim());
        else
            CollectClassOnlyDeclarations<TBuilder>(ref buffer, selector, rawValue, fallbackProperty);
    }

    private static void AddDeclaration(ref ComponentCssRuleCollector buffer, string selector, string property, string? value)
    {
        if (value is not null)
            buffer.Add(selector, new ComponentCssDeclaration(property, value));
    }

    private static void CollectClassOnlyDeclarations<TBuilder>(ref ComponentCssRuleCollector buffer, string selector, string rawValue, string fallbackProperty)
        where TBuilder : class, ICssBuilder
    {
        var resolved = rawValue.Trim();

        if (typeof(TBuilder) == typeof(UnderlineOffsetBuilder) &&
            fallbackProperty.Equals("text-underline-offset", StringComparison.Ordinal))
        {
            var offset = resolved switch
            {
                "underline-offset-auto" => "auto",
                "underline-offset-0" => "0px",
                "underline-offset-1" => "1px",
                "underline-offset-2" => "2px",
                "underline-offset-4" => "4px",
                "underline-offset-8" => "8px",
                _ => null
            };

            AddDeclaration(ref buffer, selector, "text-underline-offset", offset);
            return;
        }

        if (typeof(TBuilder) == typeof(DecorationLineBuilder) &&
            fallbackProperty.Equals("text-decoration", StringComparison.Ordinal))
        {
            var decoration = resolved switch
            {
                "no-underline" => "none",
                "underline" => "underline",
                "line-through" => "line-through",
                "overline" => "overline",
                _ => null
            };

            AddDeclaration(ref buffer, selector, fallbackProperty, decoration);
            return;
        }

        if (typeof(TBuilder) == typeof(DisplayBuilder) &&
            fallbackProperty.Equals("display", StringComparison.Ordinal))
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

            AddDeclaration(ref buffer, selector, fallbackProperty, display);
            return;
        }

        if (typeof(TBuilder) == typeof(TextColorBuilder) &&
            fallbackProperty.Equals("color", StringComparison.Ordinal))
        {
            var color = ConvertColorUtility(resolved, "text-");
            AddDeclaration(ref buffer, selector, fallbackProperty, color);
            return;
        }

        if (typeof(TBuilder) == typeof(BackgroundColorBuilder) &&
            fallbackProperty.Equals("background-color", StringComparison.Ordinal))
        {
            var color = ConvertColorUtility(resolved, "bg-");
            AddDeclaration(ref buffer, selector, fallbackProperty, color);
            return;
        }

        if (typeof(TBuilder) == typeof(BorderColorBuilder) &&
            fallbackProperty.Equals("border-color", StringComparison.Ordinal))
        {
            var color = ConvertColorUtility(resolved, "border-");
            AddDeclaration(ref buffer, selector, fallbackProperty, color);
            return;
        }

        if (typeof(TBuilder) == typeof(PaddingBuilder) &&
            fallbackProperty.Equals("padding", StringComparison.Ordinal))
        {
            CollectPaddingUtilities(ref buffer, selector, resolved);
            return;
        }

        if (typeof(TBuilder) == typeof(WidthBuilder) &&
            (fallbackProperty.Equals("width", StringComparison.Ordinal) ||
             fallbackProperty.Equals("min-width", StringComparison.Ordinal) ||
             fallbackProperty.Equals("max-width", StringComparison.Ordinal)))
        {
            var width = ConvertWidthUtility(resolved);
            AddDeclaration(ref buffer, selector, fallbackProperty, width);
            return;
        }

        if ((typeof(TBuilder) == typeof(HeightBuilder) ||
             typeof(TBuilder) == typeof(MinHeightBuilder) ||
             typeof(TBuilder) == typeof(MaxHeightBuilder)) &&
            (fallbackProperty.Equals("height", StringComparison.Ordinal) ||
             fallbackProperty.Equals("min-height", StringComparison.Ordinal) ||
             fallbackProperty.Equals("max-height", StringComparison.Ordinal)))
        {
            var height = ConvertHeightUtility(resolved);
            AddDeclaration(ref buffer, selector, fallbackProperty, height);
            return;
        }

        if (typeof(TBuilder) == typeof(OverflowBuilder) &&
            (fallbackProperty.Equals("overflow", StringComparison.Ordinal) ||
             fallbackProperty.Equals("overflow-x", StringComparison.Ordinal) ||
             fallbackProperty.Equals("overflow-y", StringComparison.Ordinal)))
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

            AddDeclaration(ref buffer, selector, fallbackProperty, overflow);
            return;
        }

        if (typeof(TBuilder) == typeof(TextSizeBuilder) &&
            fallbackProperty.Equals("font-size", StringComparison.Ordinal))
        {
            var textSize = ConvertTextSizeUtility(resolved);
            AddDeclaration(ref buffer, selector, fallbackProperty, textSize);
            return;
        }

        if (typeof(TBuilder) == typeof(ItemsBuilder) &&
            fallbackProperty.Equals("align-items", StringComparison.Ordinal))
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

            AddDeclaration(ref buffer, selector, fallbackProperty, alignItems);
            return;
        }

        if (typeof(TBuilder) == typeof(GapBuilder) &&
            fallbackProperty.Equals("gap", StringComparison.Ordinal))
        {
            var gap = ConvertSpacingUtility(resolved, "gap-");
            AddDeclaration(ref buffer, selector, fallbackProperty, gap);
            return;
        }

        if (typeof(TBuilder) == typeof(BorderBuilder) &&
            fallbackProperty.Equals("border", StringComparison.Ordinal))
        {
            CollectBorderUtilities(ref buffer, selector, resolved);
            return;
        }

        if (typeof(TBuilder) == typeof(VerticalAlignBuilder) &&
            fallbackProperty.Equals("vertical-align", StringComparison.Ordinal))
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

            AddDeclaration(ref buffer, selector, fallbackProperty, verticalAlign);
            return;
        }

        if (typeof(TBuilder) == typeof(TextOverflowBuilder) &&
            fallbackProperty.Equals("text-overflow", StringComparison.Ordinal))
        {
            var textOverflow = ConvertSingleTokenUtility(resolved, static token => token switch
            {
                "text-ellipsis" => "ellipsis",
                "text-clip" => "clip",
                _ => null
            });

            AddDeclaration(ref buffer, selector, fallbackProperty, textOverflow);
            return;
        }

        if (typeof(TBuilder) == typeof(RoundedBuilder) &&
            fallbackProperty.Equals("border-radius", StringComparison.Ordinal))
        {
            var radius = ConvertRoundedUtility(resolved);
            AddDeclaration(ref buffer, selector, fallbackProperty, radius);
            return;
        }

        if (typeof(TBuilder) == typeof(ShadowBuilder) &&
            fallbackProperty.Equals("box-shadow", StringComparison.Ordinal))
        {
            var shadow = ConvertShadowUtility(resolved);
            AddDeclaration(ref buffer, selector, fallbackProperty, shadow);
            return;
        }

        if (typeof(TBuilder) == typeof(FontWeightBuilder) &&
            fallbackProperty.Equals("font-weight", StringComparison.Ordinal))
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

            AddDeclaration(ref buffer, selector, fallbackProperty, fontWeight);
            return;
        }

        if (typeof(TBuilder) == typeof(WhitespaceBuilder) &&
            fallbackProperty.Equals("white-space", StringComparison.Ordinal))
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

            AddDeclaration(ref buffer, selector, fallbackProperty, whitespace);
            return;
        }

        if (typeof(TBuilder) == typeof(FlexDirectionBuilder) &&
            fallbackProperty.Equals("flex-direction", StringComparison.Ordinal))
        {
            CollectFlexComposite(ref buffer, selector, resolved, fallbackProperty, static token => token switch
            {
                "flex-row" => "row",
                "flex-row-reverse" => "row-reverse",
                "flex-col" => "column",
                "flex-col-reverse" => "column-reverse",
                _ => null
            });
            return;
        }

        if (typeof(TBuilder) == typeof(FlexWrapBuilder) &&
            fallbackProperty.Equals("flex-wrap", StringComparison.Ordinal))
        {
            CollectFlexComposite(ref buffer, selector, resolved, fallbackProperty, static token => token switch
            {
                "flex-wrap" => "wrap",
                "flex-wrap-reverse" => "wrap-reverse",
                "flex-nowrap" => "nowrap",
                _ => null
            });
            return;
        }

        if (typeof(TBuilder) == typeof(GrowBuilder) &&
            fallbackProperty.Equals("flex-grow", StringComparison.Ordinal))
        {
            var grow = resolved switch
            {
                "grow" => "1",
                "grow-0" => "0",
                _ => null
            };

            AddDeclaration(ref buffer, selector, fallbackProperty, grow);
            return;
        }

        if (typeof(TBuilder) == typeof(ShrinkBuilder) &&
            fallbackProperty.Equals("flex-shrink", StringComparison.Ordinal))
        {
            var shrink = resolved switch
            {
                "shrink" => "1",
                "shrink-0" => "0",
                _ => null
            };

            AddDeclaration(ref buffer, selector, fallbackProperty, shrink);
            return;
        }

        if (typeof(TBuilder) == typeof(DurationBuilder) &&
            fallbackProperty.Equals("transition-duration", StringComparison.Ordinal))
        {
            var duration = ConvertDurationUtility(resolved);
            AddDeclaration(ref buffer, selector, fallbackProperty, duration);
            return;
        }

    }

    private static string? ConvertSingleTokenUtility(string value, Func<string, string?> converter)
    {
        if (value.Contains(':') || value.Contains(' '))
            return null;

        return converter(value);
    }

    private static string? ConvertColorUtility(ReadOnlySpan<char> value, string prefix)
    {
        if (!value.StartsWith(prefix, StringComparison.Ordinal) || value.Contains(':') || value.Contains('/'))
            return null;

        var token = value[prefix.Length..];
        var arbitrary = ConvertArbitraryToken(token);

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

    private static string? ConvertWidthUtility(ReadOnlySpan<char> value)
    {
        if (value.Contains(':') || value.Contains(' '))
            return null;

        var token = value;

        if (token.StartsWith("min-w-", StringComparison.Ordinal))
            token = token["min-w-".Length..];
        else if (token.StartsWith("max-w-", StringComparison.Ordinal))
            token = token["max-w-".Length..];
        else if (token.StartsWith("w-", StringComparison.Ordinal))
            token = token["w-".Length..];
        else
            return null;

        var arbitrary = ConvertArbitraryToken(token);
        if (arbitrary is not null)
            return arbitrary;

        var spacing = ConvertSpacingScaleToken(token);
        if (spacing is not null)
            return spacing;

        var fraction = ConvertFractionToken(token);
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

    private static string? ConvertHeightUtility(ReadOnlySpan<char> value)
    {
        if (value.Contains(':') || value.Contains(' '))
            return null;

        var token = value;

        if (token.StartsWith("min-h-", StringComparison.Ordinal))
            token = token["min-h-".Length..];
        else if (token.StartsWith("max-h-", StringComparison.Ordinal))
            token = token["max-h-".Length..];
        else if (token.StartsWith("h-", StringComparison.Ordinal))
            token = token["h-".Length..];
        else
            return null;

        var arbitrary = ConvertArbitraryToken(token);
        if (arbitrary is not null)
            return arbitrary;

        var spacing = ConvertSpacingScaleToken(token);
        if (spacing is not null)
            return spacing;

        var fraction = ConvertFractionToken(token);
        if (fraction is not null)
            return fraction;

        return token switch
        {
            "auto" => "auto",
            "full" => "100%",
            "screen" => "100vh",
            "svh" => "100svh",
            "lvh" => "100lvh",
            "dvh" => "100dvh",
            "min" => "min-content",
            "max" => "max-content",
            "fit" => "fit-content",
            "none" => "none",
            _ => null
        };
    }

    private static string? ConvertTextSizeUtility(ReadOnlySpan<char> value)
    {
        if (!value.StartsWith("text-", StringComparison.Ordinal) || value.Contains(':') || value.Contains(' '))
            return null;

        var token = value["text-".Length..];

        var arbitrary = ConvertArbitraryToken(token);
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

    private static string? ConvertSpacingUtility(ReadOnlySpan<char> value, string prefix)
    {
        if (!value.StartsWith(prefix, StringComparison.Ordinal) || value.Contains(':') || value.Contains(' '))
            return null;

        var token = value[prefix.Length..];
        return ConvertArbitraryToken(token) ?? ConvertSpacingScaleToken(token);
    }

    private static void CollectBorderUtilities(ref ComponentCssRuleCollector buffer, string selector, ReadOnlySpan<char> value)
    {
        if (value.Contains(':') || value.Contains(' '))
            return;

        var property = value switch
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

        ReadOnlySpan<char> token;

        if (property is not null)
            token = "1";
        else if (value.StartsWith("border-", StringComparison.Ordinal))
        {
            var suffix = value["border-".Length..];
            var dash = suffix.IndexOf('-');

            if (dash > 0)
            {
                var side = suffix[..dash];
                token = suffix[(dash + 1)..];
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
            return;

        if (property is null)
            return;

        var width = ConvertBorderWidthToken(token);
        if (width is null)
            return;
        AddDeclaration(ref buffer, selector, property, width);
        var styleProperty = property switch
        {
            "border-inline-width" => "border-inline-style",
            "border-block-width" => "border-block-style",
            "border-inline-start-width" => "border-inline-start-style",
            "border-inline-end-width" => "border-inline-end-style",
            "border-top-width" => "border-top-style",
            "border-right-width" => "border-right-style",
            "border-bottom-width" => "border-bottom-style",
            "border-left-width" => "border-left-style",
            _ => "border-style"
        };
        AddDeclaration(ref buffer, selector, styleProperty, "solid");
    }

    private static string? ConvertBorderWidthToken(ReadOnlySpan<char> token)
    {
        var arbitrary = ConvertArbitraryToken(token);
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

    private static string? ConvertFractionToken(ReadOnlySpan<char> token)
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

    private static string? ConvertArbitraryToken(ReadOnlySpan<char> token)
    {
        if (token.Length < 2)
            return null;

        if (token[0] == '[' && token[^1] == ']')
            return string.Create(token.Length - 2, token, static (destination, source) =>
            {
                source[1..^1].CopyTo(destination);
                destination.Replace('_', ' ');
            });

        if (token[0] == '(' && token[^1] == ')')
            return $"var({token[1..^1]})";

        return null;
    }

    private static bool IsPaletteColorToken(ReadOnlySpan<char> token)
    {
        var dash = token.LastIndexOf('-');
        if (dash <= 0 || dash == token.Length - 1)
            return false;

        var family = token[..dash];
        var shade = token[(dash + 1)..];

        return IsPaletteColorFamily(family) && IsPaletteColorShade(shade);
    }

    private static bool IsPaletteColorFamily(ReadOnlySpan<char> family)
    {
        return family is "slate" or "gray" or "zinc" or "neutral" or "stone" or "red" or "orange" or "amber"
            or "yellow" or "lime" or "green" or "emerald" or "teal" or "cyan" or "sky" or "blue"
            or "indigo" or "violet" or "purple" or "fuchsia" or "pink" or "rose";
    }

    private static bool IsPaletteColorShade(ReadOnlySpan<char> shade)
    {
        return shade is "50" or "100" or "200" or "300" or "400" or "500" or "600" or "700" or "800" or "900" or "950";
    }

    private static void CollectPaddingUtilities(ref ComponentCssRuleCollector buffer, string selector, string value)
    {
        ReadOnlySpan<char> tokens = value;
        // Validate the whole composite first: unsupported tokens must not leave partial CSS.
        foreach (var range in tokens.Split(' '))
        {
            var token = tokens[range].Trim();
            if (token.IsEmpty)
                continue;
            var dash = token.IndexOf('-');
            if (token.Contains(':') || dash < 0 || ConvertSpacingToken(token) is null ||
                token[..dash] is not ("p" or "px" or "py" or "pt" or "pr" or "pb" or "pl" or "ps" or "pe"))
                return;
        }

        foreach (var range in tokens.Split(' '))
        {
            var token = tokens[range].Trim();
            if (token.Length == 0)
                continue;

            var spacing = ConvertSpacingToken(token);

            if (token.StartsWith("px-", StringComparison.Ordinal))
            {
                AddDeclaration(ref buffer, selector, "padding-left", spacing);
                AddDeclaration(ref buffer, selector, "padding-right", spacing);
            }
            else if (token.StartsWith("py-", StringComparison.Ordinal))
            {
                AddDeclaration(ref buffer, selector, "padding-top", spacing);
                AddDeclaration(ref buffer, selector, "padding-bottom", spacing);
            }
            else if (token.StartsWith("pt-", StringComparison.Ordinal))
                AddDeclaration(ref buffer, selector, "padding-top", spacing);
            else if (token.StartsWith("pr-", StringComparison.Ordinal))
                AddDeclaration(ref buffer, selector, "padding-right", spacing);
            else if (token.StartsWith("pb-", StringComparison.Ordinal))
                AddDeclaration(ref buffer, selector, "padding-bottom", spacing);
            else if (token.StartsWith("pl-", StringComparison.Ordinal))
                AddDeclaration(ref buffer, selector, "padding-left", spacing);
            else if (token.StartsWith("ps-", StringComparison.Ordinal))
                AddDeclaration(ref buffer, selector, "padding-inline-start", spacing);
            else if (token.StartsWith("pe-", StringComparison.Ordinal))
                AddDeclaration(ref buffer, selector, "padding-inline-end", spacing);
            else if (token.StartsWith("p-", StringComparison.Ordinal))
                AddDeclaration(ref buffer, selector, "padding", spacing);
            else
                return;
        }
    }

    private static string? ConvertSpacingToken(ReadOnlySpan<char> utility)
    {
        var dash = utility.IndexOf('-');
        if (dash < 0 || dash == utility.Length - 1)
            return null;

        var token = utility[(dash + 1)..];

        return ConvertSpacingScaleToken(token);
    }

    private static string? ConvertSpacingScaleToken(ReadOnlySpan<char> token)
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

    private static string? ConvertDurationUtility(ReadOnlySpan<char> value)
    {
        if (!value.StartsWith("duration-", StringComparison.Ordinal) || value.Contains(':'))
            return null;

        var token = value["duration-".Length..];

        if (token.Length == 0)
            return null;

        if (token.Length >= 2 && token[0] == '[' && token[^1] == ']')
            return token[1..^1].ToString();

        if (token.Length >= 2 && token[0] == '(' && token[^1] == ')')
            return $"var({token[1..^1]})";

        return token is "0" ? "0s" : string.Concat(token, "ms");
    }

    private static void CollectFlexComposite(ref ComponentCssRuleCollector buffer, string selector, string rawValue, string fallbackProperty, Func<ReadOnlySpan<char>, string?> converter)
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

            var token = rawValue.AsSpan(tokenStart, i - tokenStart);

            if (token.Contains(':'))
                return;

            if (token.Equals("flex", StringComparison.Ordinal))
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

        if (resolvedValue is null)
            return;

        if (hasFlexDisplay)
            AddDeclaration(ref buffer, selector, "display", "flex");
        AddDeclaration(ref buffer, selector, fallbackProperty, resolvedValue);
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
