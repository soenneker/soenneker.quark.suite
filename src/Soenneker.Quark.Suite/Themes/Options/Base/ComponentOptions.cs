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
    /// <summary>Gets or sets text-overflow using utilities or a literal CSS value.</summary>
    public ThemeValue<TruncateBuilder>? Truncate { get; set; }

    /// <summary>Gets or sets size using utilities or a literal CSS value.</summary>
    public ThemeValue<SizeBuilder>? Size { get; set; }

    /// <summary>Gets or sets gap using utilities or a literal CSS value.</summary>
    public ThemeValue<SpaceBuilder>? Space { get; set; }

    /// <summary>Gets or sets border-width using utilities or a literal CSS value.</summary>
    public ThemeValue<DivideBuilder>? Divide { get; set; }

    /// <summary>Gets or sets visibility using utilities or a literal CSS value.</summary>
    public ThemeValue<ScreenReaderBuilder>? ScreenReader { get; set; }

    /// <summary>Gets or sets --tw-ring-offset-width using utilities or a literal CSS value.</summary>
    public ThemeValue<RingOffsetBuilder>? RingOffset { get; set; }

    /// <summary>Gets or sets --tw-ring-color using utilities or a literal CSS value.</summary>
    public ThemeValue<RingColorBuilder>? RingColor { get; set; }

    /// <summary>Gets or sets outline-style using utilities or a literal CSS value.</summary>
    public ThemeValue<OutlineStyleBuilder>? OutlineStyle { get; set; }

    /// <summary>Gets or sets backdrop-filter using utilities or a literal CSS value.</summary>
    public ThemeValue<BackdropBlurBuilder>? BackdropBlur { get; set; }

    /// <summary>Gets or sets backdrop-filter using utilities or a literal CSS value.</summary>
    public ThemeValue<BackdropBrightnessBuilder>? BackdropBrightness { get; set; }

    /// <summary>Gets or sets backdrop-filter using utilities or a literal CSS value.</summary>
    public ThemeValue<BackdropContrastBuilder>? BackdropContrast { get; set; }

    /// <summary>Gets or sets backdrop-filter using utilities or a literal CSS value.</summary>
    public ThemeValue<BackdropGrayscaleBuilder>? BackdropGrayscale { get; set; }

    /// <summary>Gets or sets backdrop-filter using utilities or a literal CSS value.</summary>
    public ThemeValue<BackdropHueRotateBuilder>? BackdropHueRotate { get; set; }

    /// <summary>Gets or sets backdrop-filter using utilities or a literal CSS value.</summary>
    public ThemeValue<BackdropInvertBuilder>? BackdropInvert { get; set; }

    /// <summary>Gets or sets backdrop-filter using utilities or a literal CSS value.</summary>
    public ThemeValue<BackdropOpacityBuilder>? BackdropOpacity { get; set; }

    /// <summary>Gets or sets backdrop-filter using utilities or a literal CSS value.</summary>
    public ThemeValue<BackdropSaturateBuilder>? BackdropSaturate { get; set; }

    /// <summary>Gets or sets backdrop-filter using utilities or a literal CSS value.</summary>
    public ThemeValue<BackdropSepiaBuilder>? BackdropSepia { get; set; }

    /// <summary>Gets or sets filter using utilities or a literal CSS value.</summary>
    public ThemeValue<BlurBuilder>? Blur { get; set; }

    /// <summary>Gets or sets filter using utilities or a literal CSS value.</summary>
    public ThemeValue<BrightnessBuilder>? Brightness { get; set; }

    /// <summary>Gets or sets filter using utilities or a literal CSS value.</summary>
    public ThemeValue<ContrastBuilder>? Contrast { get; set; }

    /// <summary>Gets or sets filter using utilities or a literal CSS value.</summary>
    public ThemeValue<DropShadowBuilder>? DropShadow { get; set; }

    /// <summary>Gets or sets --tw-drop-shadow-color using utilities or a literal CSS value.</summary>
    public ThemeValue<DropShadowColorBuilder>? DropShadowColor { get; set; }

    /// <summary>Gets or sets filter using utilities or a literal CSS value.</summary>
    public ThemeValue<GrayscaleBuilder>? Grayscale { get; set; }

    /// <summary>Gets or sets filter using utilities or a literal CSS value.</summary>
    public ThemeValue<HueRotateBuilder>? HueRotate { get; set; }

    /// <summary>Gets or sets filter using utilities or a literal CSS value.</summary>
    public ThemeValue<InvertBuilder>? Invert { get; set; }

    /// <summary>Gets or sets filter using utilities or a literal CSS value.</summary>
    public ThemeValue<SaturateBuilder>? Saturate { get; set; }

    /// <summary>Gets or sets filter using utilities or a literal CSS value.</summary>
    public ThemeValue<SepiaBuilder>? Sepia { get; set; }

    /// <summary>Gets or sets accent-color using utilities or a literal CSS value.</summary>
    public ThemeValue<AccentColorBuilder>? AccentColor { get; set; }

    /// <summary>Gets or sets appearance using utilities or a literal CSS value.</summary>
    public ThemeValue<AppearanceBuilder>? NativeAppearance { get; set; }

    /// <summary>Gets or sets grid-auto-columns using utilities or a literal CSS value.</summary>
    public ThemeValue<AutoColsBuilder>? AutoCols { get; set; }

    /// <summary>Gets or sets grid-auto-rows using utilities or a literal CSS value.</summary>
    public ThemeValue<AutoRowsBuilder>? AutoRows { get; set; }

    /// <summary>Gets or sets backface-visibility using utilities or a literal CSS value.</summary>
    public ThemeValue<BackfaceVisibilityBuilder>? BackfaceVisibility { get; set; }

    /// <summary>Gets or sets background-attachment using utilities or a literal CSS value.</summary>
    public ThemeValue<BackgroundAttachmentBuilder>? BackgroundAttachment { get; set; }

    /// <summary>Gets or sets background-blend-mode using utilities or a literal CSS value.</summary>
    public ThemeValue<BackgroundBlendModeBuilder>? BackgroundBlendMode { get; set; }

    /// <summary>Gets or sets background-clip using utilities or a literal CSS value.</summary>
    public ThemeValue<BackgroundClipBuilder>? BackgroundClip { get; set; }

    /// <summary>Gets or sets background-image using utilities or a literal CSS value.</summary>
    public ThemeValue<BackgroundImageBuilder>? BackgroundImage { get; set; }

    /// <summary>Gets or sets background-origin using utilities or a literal CSS value.</summary>
    public ThemeValue<BackgroundOriginBuilder>? BackgroundOrigin { get; set; }

    /// <summary>Gets or sets background-position using utilities or a literal CSS value.</summary>
    public ThemeValue<BackgroundPositionBuilder>? BackgroundPosition { get; set; }

    /// <summary>Gets or sets background-repeat using utilities or a literal CSS value.</summary>
    public ThemeValue<BackgroundRepeatBuilder>? BackgroundRepeat { get; set; }

    /// <summary>Gets or sets background-size using utilities or a literal CSS value.</summary>
    public ThemeValue<BackgroundSizeBuilder>? BackgroundSize { get; set; }

    /// <summary>Gets or sets block-size using utilities or a literal CSS value.</summary>
    public ThemeValue<BlockSizeBuilder>? BlockSize { get; set; }

    /// <summary>Gets or sets border-collapse using utilities or a literal CSS value.</summary>
    public ThemeValue<BorderCollapseBuilder>? BorderCollapse { get; set; }

    /// <summary>Gets or sets border-spacing using utilities or a literal CSS value.</summary>
    public ThemeValue<BorderSpacingBuilder>? BorderSpacing { get; set; }

    /// <summary>Gets or sets box-decoration-break using utilities or a literal CSS value.</summary>
    public ThemeValue<BoxDecorationBreakBuilder>? BoxDecorationBreak { get; set; }

    /// <summary>Gets or sets box-sizing using utilities or a literal CSS value.</summary>
    public ThemeValue<BoxSizingBuilder>? BoxSizing { get; set; }

    /// <summary>Gets or sets break-after using utilities or a literal CSS value.</summary>
    public ThemeValue<BreakAfterBuilder>? BreakAfter { get; set; }

    /// <summary>Gets or sets break-before using utilities or a literal CSS value.</summary>
    public ThemeValue<BreakBeforeBuilder>? BreakBefore { get; set; }

    /// <summary>Gets or sets break-inside using utilities or a literal CSS value.</summary>
    public ThemeValue<BreakInsideBuilder>? BreakInside { get; set; }

    /// <summary>Gets or sets caption-side using utilities or a literal CSS value.</summary>
    public ThemeValue<CaptionSideBuilder>? CaptionSide { get; set; }

    /// <summary>Gets or sets caret-color using utilities or a literal CSS value.</summary>
    public ThemeValue<CaretColorBuilder>? CaretColor { get; set; }

    /// <summary>Gets or sets clear using utilities or a literal CSS value.</summary>
    public ThemeValue<ClearBuilder>? FloatClear { get; set; }

    /// <summary>Gets or sets grid-column-end using utilities or a literal CSS value.</summary>
    public ThemeValue<ColEndBuilder>? ColEnd { get; set; }

    /// <summary>Gets or sets color-scheme using utilities or a literal CSS value.</summary>
    public ThemeValue<ColorSchemeBuilder>? ColorScheme { get; set; }

    /// <summary>Gets or sets columns using utilities or a literal CSS value.</summary>
    public ThemeValue<ColumnsBuilder>? ColumnCount { get; set; }

    /// <summary>Gets or sets container-type using utilities or a literal CSS value.</summary>
    public ThemeValue<ContainerTypeBuilder>? ContainerQuery { get; set; }

    /// <summary>Gets or sets contain using utilities or a literal CSS value.</summary>
    public ThemeValue<ContainBuilder>? Contain { get; set; }

    /// <summary>Gets or sets content using utilities or a literal CSS value.</summary>
    public ThemeValue<ContentBuilder>? GeneratedContent { get; set; }

    /// <summary>Gets or sets text-decoration-color using utilities or a literal CSS value.</summary>
    public ThemeValue<DecorationColorBuilder>? DecorationColor { get; set; }

    /// <summary>Gets or sets text-decoration-style using utilities or a literal CSS value.</summary>
    public ThemeValue<DecorationStyleBuilder>? DecorationStyle { get; set; }

    /// <summary>Gets or sets text-decoration-thickness using utilities or a literal CSS value.</summary>
    public ThemeValue<DecorationThicknessBuilder>? DecorationThickness { get; set; }

    /// <summary>Gets or sets transition-delay using utilities or a literal CSS value.</summary>
    public ThemeValue<DelayBuilder>? TransitionDelay { get; set; }

    /// <summary>Gets or sets transition-timing-function using utilities or a literal CSS value.</summary>
    public ThemeValue<EaseBuilder>? Ease { get; set; }

    /// <summary>Gets or sets field-sizing using utilities or a literal CSS value.</summary>
    public ThemeValue<FieldSizingBuilder>? FieldSizing { get; set; }

    /// <summary>Gets or sets fill-rule using utilities or a literal CSS value.</summary>
    public ThemeValue<FillRuleBuilder>? FillRule { get; set; }

    /// <summary>Gets or sets flex-basis using utilities or a literal CSS value.</summary>
    public ThemeValue<FlexBasisBuilder>? FlexBasis { get; set; }

    /// <summary>Gets or sets font-feature-settings using utilities or a literal CSS value.</summary>
    public ThemeValue<FontFeatureSettingsBuilder>? FontFeatureSettings { get; set; }

    /// <summary>Gets or sets -webkit-font-smoothing using utilities or a literal CSS value.</summary>
    public ThemeValue<FontSmoothingBuilder>? FontSmoothing { get; set; }

    /// <summary>Gets or sets font-stretch using utilities or a literal CSS value.</summary>
    public ThemeValue<FontStretchBuilder>? FontStretch { get; set; }

    /// <summary>Gets or sets forced-color-adjust using utilities or a literal CSS value.</summary>
    public ThemeValue<ForcedColorAdjustBuilder>? ForcedColorAdjust { get; set; }

    /// <summary>Gets or sets background-image using utilities or a literal CSS value.</summary>
    public ThemeValue<GradientBuilder>? BackgroundGradient { get; set; }

    /// <summary>Gets or sets grid-auto-flow using utilities or a literal CSS value.</summary>
    public ThemeValue<GridAutoFlowBuilder>? GridAutoFlow { get; set; }

    /// <summary>Gets or sets grid-template-columns using utilities or a literal CSS value.</summary>
    public ThemeValue<GridColsBuilder>? GridColumns { get; set; }

    /// <summary>Gets or sets grid-template-rows using utilities or a literal CSS value.</summary>
    public ThemeValue<GridRowsBuilder>? GridRows { get; set; }

    /// <summary>Gets or sets grid-column using utilities or a literal CSS value.</summary>
    public ThemeValue<ColumnSpanBuilder>? ColumnSpan { get; set; }

    /// <summary>Gets or sets hyphens using utilities or a literal CSS value.</summary>
    public ThemeValue<HyphenBuilder>? Hyphen { get; set; }

    /// <summary>Gets or sets inline-size using utilities or a literal CSS value.</summary>
    public ThemeValue<InlineSizeBuilder>? InlineSize { get; set; }

    /// <summary>Gets or sets inset-block-end using utilities or a literal CSS value.</summary>
    public ThemeValue<InsetBlockEndBuilder>? InsetBlockEnd { get; set; }

    /// <summary>Gets or sets inset-block-start using utilities or a literal CSS value.</summary>
    public ThemeValue<InsetBlockStartBuilder>? InsetBlockStart { get; set; }

    /// <summary>Gets or sets inset-inline-end using utilities or a literal CSS value.</summary>
    public ThemeValue<InsetEndBuilder>? InsetEnd { get; set; }

    /// <summary>Gets or sets --tw-inset-ring-color using utilities or a literal CSS value.</summary>
    public ThemeValue<InsetRingColorBuilder>? InsetRingColor { get; set; }

    /// <summary>Gets or sets --tw-inset-ring-shadow using utilities or a literal CSS value.</summary>
    public ThemeValue<InsetRingBuilder>? InsetRing { get; set; }

    /// <summary>Gets or sets --tw-inset-shadow-color using utilities or a literal CSS value.</summary>
    public ThemeValue<InsetShadowColorBuilder>? InsetShadowColor { get; set; }

    /// <summary>Gets or sets --tw-inset-shadow using utilities or a literal CSS value.</summary>
    public ThemeValue<InsetShadowBuilder>? InsetShadow { get; set; }

    /// <summary>Gets or sets inset-inline-start using utilities or a literal CSS value.</summary>
    public ThemeValue<InsetStartBuilder>? InsetStart { get; set; }

    /// <summary>Gets or sets isolation using utilities or a literal CSS value.</summary>
    public ThemeValue<IsolationBuilder>? Isolation { get; set; }

    /// <summary>Gets or sets list-style-image using utilities or a literal CSS value.</summary>
    public ThemeValue<ListStyleImageBuilder>? ListStyleImage { get; set; }

    /// <summary>Gets or sets list-style-position using utilities or a literal CSS value.</summary>
    public ThemeValue<ListStylePositionBuilder>? ListStylePosition { get; set; }

    /// <summary>Gets or sets list-style-type using utilities or a literal CSS value.</summary>
    public ThemeValue<ListStyleTypeBuilder>? ListStyleType { get; set; }

    /// <summary>Gets or sets mask-clip using utilities or a literal CSS value.</summary>
    public ThemeValue<MaskClipBuilder>? MaskClip { get; set; }

    /// <summary>Gets or sets mask-composite using utilities or a literal CSS value.</summary>
    public ThemeValue<MaskCompositeBuilder>? MaskComposite { get; set; }

    /// <summary>Gets or sets mask-image using utilities or a literal CSS value.</summary>
    public ThemeValue<MaskImageBuilder>? MaskImage { get; set; }

    /// <summary>Gets or sets mask-mode using utilities or a literal CSS value.</summary>
    public ThemeValue<MaskModeBuilder>? MaskMode { get; set; }

    /// <summary>Gets or sets mask-origin using utilities or a literal CSS value.</summary>
    public ThemeValue<MaskOriginBuilder>? MaskOrigin { get; set; }

    /// <summary>Gets or sets mask-position using utilities or a literal CSS value.</summary>
    public ThemeValue<MaskPositionBuilder>? MaskPosition { get; set; }

    /// <summary>Gets or sets mask-repeat using utilities or a literal CSS value.</summary>
    public ThemeValue<MaskRepeatBuilder>? MaskRepeat { get; set; }

    /// <summary>Gets or sets mask-size using utilities or a literal CSS value.</summary>
    public ThemeValue<MaskSizeBuilder>? MaskSize { get; set; }

    /// <summary>Gets or sets mask-type using utilities or a literal CSS value.</summary>
    public ThemeValue<MaskTypeBuilder>? MaskType { get; set; }

    /// <summary>Gets or sets max-block-size using utilities or a literal CSS value.</summary>
    public ThemeValue<MaxBlockSizeBuilder>? MaxBlockSize { get; set; }

    /// <summary>Gets or sets max-inline-size using utilities or a literal CSS value.</summary>
    public ThemeValue<MaxInlineSizeBuilder>? MaxInlineSize { get; set; }

    /// <summary>Gets or sets min-block-size using utilities or a literal CSS value.</summary>
    public ThemeValue<MinBlockSizeBuilder>? MinBlockSize { get; set; }

    /// <summary>Gets or sets min-inline-size using utilities or a literal CSS value.</summary>
    public ThemeValue<MinInlineSizeBuilder>? MinInlineSize { get; set; }

    /// <summary>Gets or sets mix-blend-mode using utilities or a literal CSS value.</summary>
    public ThemeValue<MixBlendModeBuilder>? MixBlendMode { get; set; }

    /// <summary>Gets or sets order using utilities or a literal CSS value.</summary>
    public ThemeValue<OrderBuilder>? Order { get; set; }

    /// <summary>Gets or sets transform-origin using utilities or a literal CSS value.</summary>
    public ThemeValue<OriginBuilder>? Origin { get; set; }

    /// <summary>Gets or sets outline-color using utilities or a literal CSS value.</summary>
    public ThemeValue<OutlineColorBuilder>? OutlineColor { get; set; }

    /// <summary>Gets or sets outline-offset using utilities or a literal CSS value.</summary>
    public ThemeValue<OutlineOffsetBuilder>? OutlineOffset { get; set; }

    /// <summary>Gets or sets outline-width using utilities or a literal CSS value.</summary>
    public ThemeValue<OutlineWidthBuilder>? OutlineWidth { get; set; }

    /// <summary>Gets or sets overflow-wrap using utilities or a literal CSS value.</summary>
    public ThemeValue<OverflowWrapBuilder>? OverflowWrap { get; set; }

    /// <summary>Gets or sets perspective-origin using utilities or a literal CSS value.</summary>
    public ThemeValue<PerspectiveOriginBuilder>? PerspectiveOrigin { get; set; }

    /// <summary>Gets or sets perspective using utilities or a literal CSS value.</summary>
    public ThemeValue<PerspectiveBuilder>? Perspective { get; set; }

    /// <summary>Gets or sets place-content using utilities or a literal CSS value.</summary>
    public ThemeValue<PlaceContentAlignBuilder>? PlaceContentAlign { get; set; }

    /// <summary>Gets or sets place-items using utilities or a literal CSS value.</summary>
    public ThemeValue<PlaceItemsAlignBuilder>? PlaceItemsAlign { get; set; }

    /// <summary>Gets or sets place-self using utilities or a literal CSS value.</summary>
    public ThemeValue<PlaceSelfAlignBuilder>? PlaceSelfAlign { get; set; }

    /// <summary>Gets or sets rotate using utilities or a literal CSS value.</summary>
    public ThemeValue<RotateBuilder>? Rotate { get; set; }

    /// <summary>Gets or sets grid-row-end using utilities or a literal CSS value.</summary>
    public ThemeValue<RowEndBuilder>? RowEnd { get; set; }

    /// <summary>Gets or sets scale using utilities or a literal CSS value.</summary>
    public ThemeValue<ScaleBuilder>? Scale { get; set; }

    /// <summary>Gets or sets scrollbar-gutter using utilities or a literal CSS value.</summary>
    public ThemeValue<ScrollbarGutterBuilder>? ScrollbarGutter { get; set; }

    /// <summary>Gets or sets --tw-scrollbar-thumb using utilities or a literal CSS value.</summary>
    public ThemeValue<ScrollbarThumbColorBuilder>? ScrollbarThumbColor { get; set; }

    /// <summary>Gets or sets --tw-scrollbar-track using utilities or a literal CSS value.</summary>
    public ThemeValue<ScrollbarTrackColorBuilder>? ScrollbarTrackColor { get; set; }

    /// <summary>Gets or sets scrollbar-width using utilities or a literal CSS value.</summary>
    public ThemeValue<ScrollbarWidthBuilder>? ScrollbarWidth { get; set; }

    /// <summary>Gets or sets scroll-snap-align using utilities or a literal CSS value.</summary>
    public ThemeValue<ScrollSnapAlignBuilder>? ScrollSnapAlign { get; set; }

    /// <summary>Gets or sets scroll-snap-type using utilities or a literal CSS value.</summary>
    public ThemeValue<ScrollSnapBuilder>? ScrollSnap { get; set; }

    /// <summary>Gets or sets scroll-snap-stop using utilities or a literal CSS value.</summary>
    public ThemeValue<ScrollSnapStopBuilder>? ScrollSnapStop { get; set; }

    /// <summary>Gets or sets --tw-shadow-color using utilities or a literal CSS value.</summary>
    public ThemeValue<ShadowColorBuilder>? ShadowColor { get; set; }

    /// <summary>Gets or sets transform using utilities or a literal CSS value.</summary>
    public ThemeValue<SkewBuilder>? Skew { get; set; }

    /// <summary>Gets or sets stroke-linecap using utilities or a literal CSS value.</summary>
    public ThemeValue<StrokeLineCapBuilder>? StrokeLineCap { get; set; }

    /// <summary>Gets or sets stroke-linejoin using utilities or a literal CSS value.</summary>
    public ThemeValue<StrokeLineJoinBuilder>? StrokeLineJoin { get; set; }

    /// <summary>Gets or sets stroke-width using utilities or a literal CSS value.</summary>
    public ThemeValue<StrokeWidthBuilder>? SvgStrokeWidth { get; set; }

    /// <summary>Gets or sets table-layout using utilities or a literal CSS value.</summary>
    public ThemeValue<TableLayoutBuilder>? TableLayout { get; set; }

    /// <summary>Gets or sets tab-size using utilities or a literal CSS value.</summary>
    public ThemeValue<TabSizeBuilder>? TabSize { get; set; }

    /// <summary>Gets or sets text-indent using utilities or a literal CSS value.</summary>
    public ThemeValue<TextIndentBuilder>? TextIndent { get; set; }

    /// <summary>Gets or sets --tw-text-shadow-color using utilities or a literal CSS value.</summary>
    public ThemeValue<TextShadowColorBuilder>? TextShadowColor { get; set; }

    /// <summary>Gets or sets text-shadow using utilities or a literal CSS value.</summary>
    public ThemeValue<TextShadowBuilder>? TextShadow { get; set; }

    /// <summary>Gets or sets touch-action using utilities or a literal CSS value.</summary>
    public ThemeValue<TouchActionBuilder>? TouchAction { get; set; }

    /// <summary>Gets or sets transform-style using utilities or a literal CSS value.</summary>
    public ThemeValue<TransformStyleBuilder>? TransformStyle { get; set; }

    /// <summary>Gets or sets transition-behavior using utilities or a literal CSS value.</summary>
    public ThemeValue<TransitionBehaviorBuilder>? TransitionBehavior { get; set; }

    /// <summary>Gets or sets translate using utilities or a literal CSS value.</summary>
    public ThemeValue<TranslateBuilder>? Translate { get; set; }

    /// <summary>Gets or sets will-change using utilities or a literal CSS value.</summary>
    public ThemeValue<WillChangeBuilder>? WillChange { get; set; }

    /// <summary>Gets or sets zoom using utilities or a literal CSS value.</summary>
    public ThemeValue<ZoomBuilder>? Zoom { get; set; }

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
    public ThemeValue<MinWidthBuilder>? MinWidth { get; set; }

    /// <summary>Gets or sets the builder or literal CSS <c>max-width</c> value, including any required units.</summary>
    public ThemeValue<MaxWidthBuilder>? MaxWidth { get; set; }

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
    public ThemeValue<WordBreakBuilder>? WordBreak { get; set; }

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
        ThemeUtilityConverter.AddRules(ref buffer, selector, "word-break", WordBreak);
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

        ThemeUtilityConverter.AddRules(ref buffer, selector, "text-overflow", Truncate);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "size", Size);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "gap", Space);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "border-width", Divide);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "visibility", ScreenReader);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "--tw-ring-offset-width", RingOffset);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "--tw-ring-color", RingColor);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "outline-style", OutlineStyle);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "backdrop-filter", BackdropBlur);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "backdrop-filter", BackdropBrightness);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "backdrop-filter", BackdropContrast);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "backdrop-filter", BackdropGrayscale);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "backdrop-filter", BackdropHueRotate);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "backdrop-filter", BackdropInvert);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "backdrop-filter", BackdropOpacity);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "backdrop-filter", BackdropSaturate);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "backdrop-filter", BackdropSepia);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "filter", Blur);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "filter", Brightness);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "filter", Contrast);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "filter", DropShadow);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "--tw-drop-shadow-color", DropShadowColor);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "filter", Grayscale);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "filter", HueRotate);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "filter", Invert);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "filter", Saturate);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "filter", Sepia);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "accent-color", AccentColor);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "appearance", NativeAppearance);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "grid-auto-columns", AutoCols);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "grid-auto-rows", AutoRows);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "backface-visibility", BackfaceVisibility);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "background-attachment", BackgroundAttachment);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "background-blend-mode", BackgroundBlendMode);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "background-clip", BackgroundClip);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "background-image", BackgroundImage);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "background-origin", BackgroundOrigin);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "background-position", BackgroundPosition);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "background-repeat", BackgroundRepeat);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "background-size", BackgroundSize);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "block-size", BlockSize);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "border-collapse", BorderCollapse);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "border-spacing", BorderSpacing);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "box-decoration-break", BoxDecorationBreak);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "box-sizing", BoxSizing);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "break-after", BreakAfter);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "break-before", BreakBefore);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "break-inside", BreakInside);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "caption-side", CaptionSide);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "caret-color", CaretColor);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "clear", FloatClear);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "grid-column-end", ColEnd);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "color-scheme", ColorScheme);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "columns", ColumnCount);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "container-type", ContainerQuery);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "contain", Contain);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "content", GeneratedContent);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "text-decoration-color", DecorationColor);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "text-decoration-style", DecorationStyle);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "text-decoration-thickness", DecorationThickness);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "transition-delay", TransitionDelay);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "transition-timing-function", Ease);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "field-sizing", FieldSizing);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "fill-rule", FillRule);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "flex-basis", FlexBasis);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "font-feature-settings", FontFeatureSettings);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "-webkit-font-smoothing", FontSmoothing);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "font-stretch", FontStretch);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "forced-color-adjust", ForcedColorAdjust);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "background-image", BackgroundGradient);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "grid-auto-flow", GridAutoFlow);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "grid-template-columns", GridColumns);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "grid-template-rows", GridRows);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "grid-column", ColumnSpan);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "hyphens", Hyphen);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "inline-size", InlineSize);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "inset-block-end", InsetBlockEnd);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "inset-block-start", InsetBlockStart);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "inset-inline-end", InsetEnd);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "--tw-inset-ring-color", InsetRingColor);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "--tw-inset-ring-shadow", InsetRing);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "--tw-inset-shadow-color", InsetShadowColor);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "--tw-inset-shadow", InsetShadow);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "inset-inline-start", InsetStart);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "isolation", Isolation);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "list-style-image", ListStyleImage);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "list-style-position", ListStylePosition);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "list-style-type", ListStyleType);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "mask-clip", MaskClip);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "mask-composite", MaskComposite);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "mask-image", MaskImage);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "mask-mode", MaskMode);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "mask-origin", MaskOrigin);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "mask-position", MaskPosition);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "mask-repeat", MaskRepeat);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "mask-size", MaskSize);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "mask-type", MaskType);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "max-block-size", MaxBlockSize);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "max-inline-size", MaxInlineSize);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "min-block-size", MinBlockSize);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "min-inline-size", MinInlineSize);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "mix-blend-mode", MixBlendMode);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "order", Order);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "transform-origin", Origin);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "outline-color", OutlineColor);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "outline-offset", OutlineOffset);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "outline-width", OutlineWidth);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "overflow-wrap", OverflowWrap);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "perspective-origin", PerspectiveOrigin);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "perspective", Perspective);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "place-content", PlaceContentAlign);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "place-items", PlaceItemsAlign);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "place-self", PlaceSelfAlign);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "rotate", Rotate);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "grid-row-end", RowEnd);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "scale", Scale);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "scrollbar-gutter", ScrollbarGutter);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "--tw-scrollbar-thumb", ScrollbarThumbColor);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "--tw-scrollbar-track", ScrollbarTrackColor);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "scrollbar-width", ScrollbarWidth);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "scroll-snap-align", ScrollSnapAlign);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "scroll-snap-type", ScrollSnap);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "scroll-snap-stop", ScrollSnapStop);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "--tw-shadow-color", ShadowColor);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "transform", Skew);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "stroke-linecap", StrokeLineCap);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "stroke-linejoin", StrokeLineJoin);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "stroke-width", SvgStrokeWidth);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "table-layout", TableLayout);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "tab-size", TabSize);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "text-indent", TextIndent);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "--tw-text-shadow-color", TextShadowColor);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "text-shadow", TextShadow);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "touch-action", TouchAction);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "transform-style", TransformStyle);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "transition-behavior", TransitionBehavior);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "translate", Translate);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "will-change", WillChange);
        ThemeUtilityConverter.AddRules(ref buffer, selector, "zoom", Zoom);

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
