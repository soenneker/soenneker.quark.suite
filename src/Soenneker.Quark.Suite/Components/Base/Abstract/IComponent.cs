using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Soenneker.Lepton.Suite.Abstract;

namespace Soenneker.Quark;

/// <summary>
/// Universal component contract for broad styling and universal interaction concerns.
/// </summary>
public interface IComponent : ILeptonDisposableIdentifiableContentElement
{
    /// <summary>
    /// Gets or sets a value indicating whether container.
    /// </summary>
    bool Container { get; set; }
    /// <summary>
    /// Gets or sets preset.
    /// </summary>
    QuarkPresetToken? Preset { get; set; }
    /// <summary>
    /// Gets or sets presets.
    /// </summary>
    IReadOnlyList<QuarkPresetToken>? Presets { get; set; }
    /// <summary>
    /// Gets or sets title.
    /// </summary>
    string? Title { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether hidden.
    /// </summary>
    bool Hidden { get; set; }
    /// <summary>
    /// Gets or sets data slot.
    /// </summary>
    string? DataSlot { get; set; }
    /// <summary>
    /// Gets or sets inset.
    /// </summary>
    CssValue<InsetBuilder>? Inset { get; set; }
    /// <summary>
    /// Gets or sets top.
    /// </summary>
    CssValue<TopBuilder>? Top { get; set; }
    /// <summary>
    /// Gets or sets right.
    /// </summary>
    CssValue<RightBuilder>? Right { get; set; }
    /// <summary>
    /// Gets or sets bottom.
    /// </summary>
    CssValue<BottomBuilder>? Bottom { get; set; }
    /// <summary>
    /// Gets or sets left.
    /// </summary>
    CssValue<LeftBuilder>? Left { get; set; }

    /// <summary>
    /// Gets or sets display.
    /// </summary>
    CssValue<DisplayBuilder>? Display { get; set; }
    /// <summary>
    /// Gets or sets visibility.
    /// </summary>
    CssValue<VisibilityBuilder>? Visibility { get; set; }
    /// <summary>
    /// Gets or sets float.
    /// </summary>
    CssValue<FloatBuilder>? Float { get; set; }
    /// <summary>
    /// Gets or sets vertical align.
    /// </summary>
    CssValue<VerticalAlignBuilder>? VerticalAlign { get; set; }
    /// <summary>
    /// Gets or sets text align.
    /// </summary>
    CssValue<TextAlignBuilder>? TextAlign { get; set; }
    /// <summary>
    /// Gets or sets text color.
    /// </summary>
    CssValue<TextColorBuilder>? TextColor { get; set; }
    /// <summary>
    /// Gets or sets text size.
    /// </summary>
    CssValue<TextSizeBuilder>? TextSize { get; set; }
    /// <summary>
    /// Gets or sets decoration line.
    /// </summary>
    CssValue<DecorationLineBuilder>? DecorationLine { get; set; }

    /// <summary>Gets or sets the distance between text and its underline, including responsive and state variants.</summary>
    CssValue<UnderlineOffsetBuilder>? UnderlineOffset { get; set; }
    /// <summary>
    /// Gets or sets text transform.
    /// </summary>
    CssValue<TextTransformBuilder>? TextTransform { get; set; }
    /// <summary>
    /// Gets or sets font family.
    /// </summary>
    CssValue<FontFamilyBuilder>? FontFamily { get; set; }
    /// <summary>
    /// Gets or sets font weight.
    /// </summary>
    CssValue<FontWeightBuilder>? FontWeight { get; set; }
    /// <summary>
    /// Gets or sets font style.
    /// </summary>
    CssValue<FontStyleBuilder>? FontStyle { get; set; }
    /// <summary>
    /// Gets or sets leading.
    /// </summary>
    CssValue<LeadingBuilder>? Leading { get; set; }
    /// <summary>
    /// Gets or sets tracking.
    /// </summary>
    CssValue<TrackingBuilder>? Tracking { get; set; }
    /// <summary>
    /// Gets or sets whitespace.
    /// </summary>
    CssValue<WhitespaceBuilder>? Whitespace { get; set; }
    /// <summary>
    /// Gets or sets text wrap.
    /// </summary>
    CssValue<TextWrapBuilder>? TextWrap { get; set; }
    /// <summary>
    /// Gets or sets text break.
    /// </summary>
    CssValue<WordBreakBuilder>? WordBreak { get; set; }
    /// <summary>
    /// Gets or sets text overflow.
    /// </summary>
    CssValue<TextOverflowBuilder>? TextOverflow { get; set; }
    /// <summary>
    /// Gets or sets truncate.
    /// </summary>
    CssValue<TruncateBuilder>? Truncate { get; set; }
    /// <summary>
    /// Gets or sets line clamp.
    /// </summary>
    CssValue<LineClampBuilder>? LineClamp { get; set; }
    /// <summary>
    /// Gets or sets font variant numeric.
    /// </summary>
    CssValue<FontVariantNumericBuilder>? FontVariantNumeric { get; set; }
    /// <summary>
    /// Gets or sets margin.
    /// </summary>
    CssValue<MarginBuilder>? Margin { get; set; }
    /// <summary>
    /// Gets or sets padding.
    /// </summary>
    CssValue<PaddingBuilder>? Padding { get; set; }
    /// <summary>
    /// Gets or sets position.
    /// </summary>
    CssValue<PositionBuilder>? Position { get; set; }
    /// <summary>
    /// Gets or sets scroll margin.
    /// </summary>
    CssValue<ScrollMarginBuilder>? ScrollMargin { get; set; }
    /// <summary>
    /// Gets or sets scroll padding.
    /// </summary>
    CssValue<ScrollPaddingBuilder>? ScrollPadding { get; set; }
    /// <summary>
    /// Gets or sets size.
    /// </summary>
    CssValue<SizeBuilder>? Size { get; set; }
    /// <summary>
    /// Gets or sets width.
    /// </summary>
    CssValue<WidthBuilder>? Width { get; set; }
    /// <summary>
    /// Gets or sets min width.
    /// </summary>
    CssValue<MinWidthBuilder>? MinWidth { get; set; }
    /// <summary>
    /// Gets or sets max width.
    /// </summary>
    CssValue<MaxWidthBuilder>? MaxWidth { get; set; }
    /// <summary>
    /// Gets or sets height.
    /// </summary>
    CssValue<HeightBuilder>? Height { get; set; }
    /// <summary>
    /// Gets or sets min height.
    /// </summary>
    CssValue<MinHeightBuilder>? MinHeight { get; set; }
    /// <summary>
    /// Gets or sets max height.
    /// </summary>
    CssValue<MaxHeightBuilder>? MaxHeight { get; set; }
    /// <summary>
    /// Gets or sets overflow.
    /// </summary>
    CssValue<OverflowBuilder>? Overflow { get; set; }
    /// <summary>
    /// Gets or sets overflow x.
    /// </summary>
    CssValue<OverflowBuilder>? OverflowX { get; set; }
    /// <summary>
    /// Gets or sets overflow y.
    /// </summary>
    CssValue<OverflowBuilder>? OverflowY { get; set; }
    /// <summary>
    /// Gets or sets overscroll.
    /// </summary>
    CssValue<OverscrollBuilder>? Overscroll { get; set; }
    /// <summary>
    /// Gets or sets flex.
    /// </summary>
    CssValue<FlexBuilder>? Flex { get; set; }
    /// <summary>
    /// Gets or sets flex direction without changing display. Set Display to Flex or InlineFlex explicitly.
    /// </summary>
    CssValue<FlexDirectionBuilder>? FlexDirection { get; set; }
    /// <summary>
    /// Gets or sets flex wrapping without changing display. Set Display to Flex or InlineFlex explicitly.
    /// </summary>
    CssValue<FlexWrapBuilder>? FlexWrap { get; set; }
    /// <summary>
    /// Gets or sets grow.
    /// </summary>
    CssValue<GrowBuilder>? Grow { get; set; }
    /// <summary>
    /// Gets or sets shrink.
    /// </summary>
    CssValue<ShrinkBuilder>? Shrink { get; set; }
    /// <summary>
    /// Gets or sets gap.
    /// </summary>
    CssValue<GapBuilder>? Gap { get; set; }
    /// <summary>
    /// Gets or sets space.
    /// </summary>
    CssValue<SpaceBuilder>? Space { get; set; }
    /// <summary>
    /// Gets or sets divide.
    /// </summary>
    CssValue<DivideBuilder>? Divide { get; set; }
    /// <summary>
    /// Gets or sets content align.
    /// </summary>
    CssValue<ContentAlignBuilder>? ContentAlign { get; set; }
    /// <summary>
    /// Gets or sets items align.
    /// </summary>
    CssValue<ItemsBuilder>? ItemsAlign { get; set; }
    /// <summary>
    /// Gets or sets justify.
    /// </summary>
    CssValue<JustifyBuilder>? Justify { get; set; }
    /// <summary>
    /// Gets or sets self align.
    /// </summary>
    CssValue<SelfBuilder>? SelfAlign { get; set; }
    /// <summary>
    /// Gets or sets justify items align.
    /// </summary>
    CssValue<JustifyItemsAlignBuilder>? JustifyItemsAlign { get; set; }
    /// <summary>
    /// Gets or sets justify self align.
    /// </summary>
    CssValue<JustifySelfAlignBuilder>? JustifySelfAlign { get; set; }
    /// <summary>
    /// Gets or sets col start.
    /// </summary>
    CssValue<ColStartBuilder>? ColStart { get; set; }
    /// <summary>
    /// Gets or sets row span.
    /// </summary>
    CssValue<RowSpanBuilder>? RowSpan { get; set; }
    /// <summary>
    /// Gets or sets row start.
    /// </summary>
    CssValue<RowStartBuilder>? RowStart { get; set; }
    /// <summary>
    /// Gets or sets opacity.
    /// </summary>
    CssValue<OpacityBuilder>? Opacity { get; set; }
    /// <summary>
    /// Gets or sets z index.
    /// </summary>
    CssValue<ZIndexBuilder>? ZIndex { get; set; }
    /// <summary>
    /// Gets or sets pointer events.
    /// </summary>
    CssValue<PointerEventsBuilder>? PointerEvents { get; set; }
    /// <summary>
    /// Gets or sets user select.
    /// </summary>
    CssValue<UserSelectBuilder>? UserSelect { get; set; }
    /// <summary>
    /// Gets or sets cursor.
    /// </summary>
    CssValue<CursorBuilder>? Cursor { get; set; }
    /// <summary>
    /// Gets or sets screen reader.
    /// </summary>
    CssValue<ScreenReaderBuilder>? ScreenReader { get; set; }
    /// <summary>
    /// Gets or sets background color.
    /// </summary>
    CssValue<BackgroundColorBuilder>? BackgroundColor { get; set; }
    /// <summary>
    /// Gets or sets border color.
    /// </summary>
    CssValue<BorderColorBuilder>? BorderColor { get; set; }
    /// <summary>
    /// Gets or sets border.
    /// </summary>
    CssValue<BorderBuilder>? Border { get; set; }
    /// <summary>
    /// Gets or sets border style.
    /// </summary>
    CssValue<BorderStyleBuilder>? BorderStyle { get; set; }
    /// <summary>
    /// Gets or sets rounded.
    /// </summary>
    CssValue<RoundedBuilder>? Rounded { get; set; }
    /// <summary>
    /// Gets or sets ring color.
    /// </summary>
    CssValue<RingColorBuilder>? RingColor { get; set; }
    /// <summary>
    /// Gets or sets ring.
    /// </summary>
    CssValue<RingBuilder>? Ring { get; set; }
    /// <summary>
    /// Gets or sets ring offset.
    /// </summary>
    CssValue<RingOffsetBuilder>? RingOffset { get; set; }
    /// <summary>
    /// Gets or sets outline style.
    /// </summary>
    CssValue<OutlineStyleBuilder>? OutlineStyle { get; set; }
    /// <summary>
    /// Gets or sets shadow.
    /// </summary>
    CssValue<ShadowBuilder>? Shadow { get; set; }
    /// <summary>
    /// Gets or sets backdrop filter.
    /// </summary>
    CssValue<BackdropFilterBuilder>? BackdropFilter { get; set; }

    /// <summary>
    /// Gets or sets backdrop blur utility classes.
    /// </summary>
    CssValue<BackdropBlurBuilder>? BackdropBlur { get; set; }

    /// <summary>
    /// Gets or sets backdrop brightness utility classes.
    /// </summary>
    CssValue<BackdropBrightnessBuilder>? BackdropBrightness { get; set; }

    /// <summary>
    /// Gets or sets backdrop contrast utility classes.
    /// </summary>
    CssValue<BackdropContrastBuilder>? BackdropContrast { get; set; }

    /// <summary>
    /// Gets or sets backdrop grayscale utility classes.
    /// </summary>
    CssValue<BackdropGrayscaleBuilder>? BackdropGrayscale { get; set; }

    /// <summary>
    /// Gets or sets backdrop hue rotate utility classes.
    /// </summary>
    CssValue<BackdropHueRotateBuilder>? BackdropHueRotate { get; set; }

    /// <summary>
    /// Gets or sets backdrop invert utility classes.
    /// </summary>
    CssValue<BackdropInvertBuilder>? BackdropInvert { get; set; }

    /// <summary>
    /// Gets or sets backdrop opacity utility classes.
    /// </summary>
    CssValue<BackdropOpacityBuilder>? BackdropOpacity { get; set; }

    /// <summary>
    /// Gets or sets backdrop saturate utility classes.
    /// </summary>
    CssValue<BackdropSaturateBuilder>? BackdropSaturate { get; set; }

    /// <summary>
    /// Gets or sets backdrop sepia utility classes.
    /// </summary>
    CssValue<BackdropSepiaBuilder>? BackdropSepia { get; set; }
    /// <summary>
    /// Gets or sets filter.
    /// </summary>
    CssValue<FilterBuilder>? Filter { get; set; }

    /// <summary>
    /// Gets or sets blur utility classes.
    /// </summary>
    CssValue<BlurBuilder>? Blur { get; set; }

    /// <summary>
    /// Gets or sets brightness utility classes.
    /// </summary>
    CssValue<BrightnessBuilder>? Brightness { get; set; }

    /// <summary>
    /// Gets or sets contrast utility classes.
    /// </summary>
    CssValue<ContrastBuilder>? Contrast { get; set; }

    /// <summary>
    /// Gets or sets drop shadow utility classes.
    /// </summary>
    CssValue<DropShadowBuilder>? DropShadow { get; set; }

    /// <summary>
    /// Gets or sets drop shadow color utility classes.
    /// </summary>
    CssValue<DropShadowColorBuilder>? DropShadowColor { get; set; }

    /// <summary>
    /// Gets or sets grayscale utility classes.
    /// </summary>
    CssValue<GrayscaleBuilder>? Grayscale { get; set; }

    /// <summary>
    /// Gets or sets hue rotate utility classes.
    /// </summary>
    CssValue<HueRotateBuilder>? HueRotate { get; set; }

    /// <summary>
    /// Gets or sets invert utility classes.
    /// </summary>
    CssValue<InvertBuilder>? Invert { get; set; }

    /// <summary>
    /// Gets or sets saturate utility classes.
    /// </summary>
    CssValue<SaturateBuilder>? Saturate { get; set; }

    /// <summary>
    /// Gets or sets sepia utility classes.
    /// </summary>
    CssValue<SepiaBuilder>? Sepia { get; set; }
    /// <summary>
    /// Gets or sets resize.
    /// </summary>
    CssValue<ResizeBuilder>? Resize { get; set; }
    /// <summary>
    /// Gets or sets transform.
    /// </summary>
    CssValue<TransformBuilder>? Transform { get; set; }
    /// <summary>
    /// Gets or sets animation.
    /// </summary>
    CssValue<AnimationBuilder>? Animation { get; set; }
    /// <summary>
    /// Gets or sets duration.
    /// </summary>
    CssValue<DurationBuilder>? Duration { get; set; }
    /// <summary>
    /// Gets or sets transition.
    /// </summary>
    CssValue<TransitionBuilder>? Transition { get; set; }

    /// <summary>
    /// Gets or sets on element ref ready.
    /// </summary>
    EventCallback<ElementReference> OnElementRefReady { get; set; }

    /// <summary>
    /// Refreshes component for the Component.
    /// </summary>
    void Refresh();
    /// <summary>
    /// Refreshes off Thread for the Component.
    /// </summary>
    /// <returns>A task that completes when the refresh off thread operation is complete.</returns>
    Task RefreshOffThread();

    /// <summary>Gets or sets the AspectRatio utilities, including responsive and state variants.</summary>
    CssValue<AspectRatioBuilder>? AspectRatio { get; set; }

    /// <summary>Gets or sets the BackfaceVisibility utilities, including responsive and state variants.</summary>
    CssValue<BackfaceVisibilityBuilder>? BackfaceVisibility { get; set; }

    /// <summary>Gets or sets the BackgroundAttachment utilities, including responsive and state variants.</summary>
    CssValue<BackgroundAttachmentBuilder>? BackgroundAttachment { get; set; }

    /// <summary>Gets or sets the BackgroundBlendMode utilities, including responsive and state variants.</summary>
    CssValue<BackgroundBlendModeBuilder>? BackgroundBlendMode { get; set; }

    /// <summary>Gets or sets the BackgroundClip utilities, including responsive and state variants.</summary>
    CssValue<BackgroundClipBuilder>? BackgroundClip { get; set; }

    /// <summary>Gets or sets the BackgroundImage utilities, including responsive and state variants.</summary>
    CssValue<BackgroundImageBuilder>? BackgroundImage { get; set; }

    /// <summary>Gets or sets the BackgroundOrigin utilities, including responsive and state variants.</summary>
    CssValue<BackgroundOriginBuilder>? BackgroundOrigin { get; set; }

    /// <summary>Gets or sets the BackgroundPosition utilities, including responsive and state variants.</summary>
    CssValue<BackgroundPositionBuilder>? BackgroundPosition { get; set; }

    /// <summary>Gets or sets the BackgroundRepeat utilities, including responsive and state variants.</summary>
    CssValue<BackgroundRepeatBuilder>? BackgroundRepeat { get; set; }

    /// <summary>Gets or sets the BackgroundSize utilities, including responsive and state variants.</summary>
    CssValue<BackgroundSizeBuilder>? BackgroundSize { get; set; }

    /// <summary>Gets or sets the BlockSize utilities, including responsive and state variants.</summary>
    CssValue<BlockSizeBuilder>? BlockSize { get; set; }

    /// <summary>Gets or sets the BoxDecorationBreak utilities, including responsive and state variants.</summary>
    CssValue<BoxDecorationBreakBuilder>? BoxDecorationBreak { get; set; }

    /// <summary>Gets or sets the BoxSizing utilities, including responsive and state variants.</summary>
    CssValue<BoxSizingBuilder>? BoxSizing { get; set; }

    /// <summary>Gets or sets the BreakAfter utilities, including responsive and state variants.</summary>
    CssValue<BreakAfterBuilder>? BreakAfter { get; set; }

    /// <summary>Gets or sets the BreakBefore utilities, including responsive and state variants.</summary>
    CssValue<BreakBeforeBuilder>? BreakBefore { get; set; }

    /// <summary>Gets or sets the BreakInside utilities, including responsive and state variants.</summary>
    CssValue<BreakInsideBuilder>? BreakInside { get; set; }

    /// <summary>Gets or sets the FloatClear utilities, including responsive and state variants.</summary>
    CssValue<ClearBuilder>? FloatClear { get; set; }

    /// <summary>Gets or sets the ClipPath utilities, including responsive and state variants.</summary>
    CssValue<ClipPathBuilder>? ClipPath { get; set; }

    /// <summary>Gets or sets the ColEnd utilities, including responsive and state variants.</summary>
    CssValue<ColEndBuilder>? ColEnd { get; set; }

    /// <summary>Gets or sets the ColorScheme utilities, including responsive and state variants.</summary>
    CssValue<ColorSchemeBuilder>? ColorScheme { get; set; }

    /// <summary>Gets or sets the ColumnCount utilities, including responsive and state variants.</summary>
    CssValue<ColumnsBuilder>? ColumnCount { get; set; }

    /// <summary>Gets or sets the ContainerQuery utilities, including responsive and state variants.</summary>
    CssValue<ContainerTypeBuilder>? ContainerQuery { get; set; }

    /// <summary>Gets or sets the Contain utilities, including responsive and state variants.</summary>
    CssValue<ContainBuilder>? Contain { get; set; }

    /// <summary>Gets or sets the GeneratedContent utilities, including responsive and state variants.</summary>
    CssValue<ContentBuilder>? GeneratedContent { get; set; }

    /// <summary>Gets or sets the DecorationColor utilities, including responsive and state variants.</summary>
    CssValue<DecorationColorBuilder>? DecorationColor { get; set; }

    /// <summary>Gets or sets the DecorationStyle utilities, including responsive and state variants.</summary>
    CssValue<DecorationStyleBuilder>? DecorationStyle { get; set; }

    /// <summary>Gets or sets the DecorationThickness utilities, including responsive and state variants.</summary>
    CssValue<DecorationThicknessBuilder>? DecorationThickness { get; set; }

    /// <summary>Gets or sets the TransitionDelay utilities, including responsive and state variants.</summary>
    CssValue<DelayBuilder>? TransitionDelay { get; set; }

    /// <summary>Gets or sets the Ease utilities, including responsive and state variants.</summary>
    CssValue<EaseBuilder>? Ease { get; set; }

    /// <summary>Gets or sets the FlexBasis utilities, including responsive and state variants.</summary>
    CssValue<FlexBasisBuilder>? FlexBasis { get; set; }

    /// <summary>Gets or sets the FontFeatureSettings utilities, including responsive and state variants.</summary>
    CssValue<FontFeatureSettingsBuilder>? FontFeatureSettings { get; set; }

    /// <summary>Gets or sets the FontSmoothing utilities, including responsive and state variants.</summary>
    CssValue<FontSmoothingBuilder>? FontSmoothing { get; set; }

    /// <summary>Gets or sets the FontStretch utilities, including responsive and state variants.</summary>
    CssValue<FontStretchBuilder>? FontStretch { get; set; }

    /// <summary>Gets or sets the ForcedColorAdjust utilities, including responsive and state variants.</summary>
    CssValue<ForcedColorAdjustBuilder>? ForcedColorAdjust { get; set; }

    /// <summary>Gets or sets the BackgroundGradient utilities, including responsive and state variants.</summary>
    CssValue<GradientBuilder>? BackgroundGradient { get; set; }

    /// <summary>Gets or sets the ColumnSpan utilities, including responsive and state variants.</summary>
    CssValue<ColumnSpanBuilder>? ColumnSpan { get; set; }

    /// <summary>Gets or sets the Hyphen utilities, including responsive and state variants.</summary>
    CssValue<HyphenBuilder>? Hyphen { get; set; }

    /// <summary>Gets or sets the InlineSize utilities, including responsive and state variants.</summary>
    CssValue<InlineSizeBuilder>? InlineSize { get; set; }

    /// <summary>Gets or sets the InsetBlockEnd utilities, including responsive and state variants.</summary>
    CssValue<InsetBlockEndBuilder>? InsetBlockEnd { get; set; }

    /// <summary>Gets or sets the InsetBlockStart utilities, including responsive and state variants.</summary>
    CssValue<InsetBlockStartBuilder>? InsetBlockStart { get; set; }

    /// <summary>Gets or sets the InsetEnd utilities, including responsive and state variants.</summary>
    CssValue<InsetEndBuilder>? InsetEnd { get; set; }

    /// <summary>Gets or sets the InsetRingColor utilities, including responsive and state variants.</summary>
    CssValue<InsetRingColorBuilder>? InsetRingColor { get; set; }

    /// <summary>Gets or sets the InsetRing utilities, including responsive and state variants.</summary>
    CssValue<InsetRingBuilder>? InsetRing { get; set; }

    /// <summary>Gets or sets the InsetShadowColor utilities, including responsive and state variants.</summary>
    CssValue<InsetShadowColorBuilder>? InsetShadowColor { get; set; }

    /// <summary>Gets or sets the InsetShadow utilities, including responsive and state variants.</summary>
    CssValue<InsetShadowBuilder>? InsetShadow { get; set; }

    /// <summary>Gets or sets the InsetStart utilities, including responsive and state variants.</summary>
    CssValue<InsetStartBuilder>? InsetStart { get; set; }

    /// <summary>Gets or sets the Isolation utilities, including responsive and state variants.</summary>
    CssValue<IsolationBuilder>? Isolation { get; set; }

    /// <summary>Gets or sets the MaskClip utilities, including responsive and state variants.</summary>
    CssValue<MaskClipBuilder>? MaskClip { get; set; }

    /// <summary>Gets or sets the MaskComposite utilities, including responsive and state variants.</summary>
    CssValue<MaskCompositeBuilder>? MaskComposite { get; set; }

    /// <summary>Gets or sets the MaskImage utilities, including responsive and state variants.</summary>
    CssValue<MaskImageBuilder>? MaskImage { get; set; }

    /// <summary>Gets or sets the MaskMode utilities, including responsive and state variants.</summary>
    CssValue<MaskModeBuilder>? MaskMode { get; set; }

    /// <summary>Gets or sets the MaskOrigin utilities, including responsive and state variants.</summary>
    CssValue<MaskOriginBuilder>? MaskOrigin { get; set; }

    /// <summary>Gets or sets the MaskPosition utilities, including responsive and state variants.</summary>
    CssValue<MaskPositionBuilder>? MaskPosition { get; set; }

    /// <summary>Gets or sets the MaskRepeat utilities, including responsive and state variants.</summary>
    CssValue<MaskRepeatBuilder>? MaskRepeat { get; set; }

    /// <summary>Gets or sets the MaskSize utilities, including responsive and state variants.</summary>
    CssValue<MaskSizeBuilder>? MaskSize { get; set; }

    /// <summary>Gets or sets the MaskType utilities, including responsive and state variants.</summary>
    CssValue<MaskTypeBuilder>? MaskType { get; set; }

    /// <summary>Gets or sets the MaxBlockSize utilities, including responsive and state variants.</summary>
    CssValue<MaxBlockSizeBuilder>? MaxBlockSize { get; set; }

    /// <summary>Gets or sets the MaxInlineSize utilities, including responsive and state variants.</summary>
    CssValue<MaxInlineSizeBuilder>? MaxInlineSize { get; set; }

    /// <summary>Gets or sets the MinBlockSize utilities, including responsive and state variants.</summary>
    CssValue<MinBlockSizeBuilder>? MinBlockSize { get; set; }

    /// <summary>Gets or sets the MinInlineSize utilities, including responsive and state variants.</summary>
    CssValue<MinInlineSizeBuilder>? MinInlineSize { get; set; }

    /// <summary>Gets or sets the MixBlendMode utilities, including responsive and state variants.</summary>
    CssValue<MixBlendModeBuilder>? MixBlendMode { get; set; }

    /// <summary>Gets or sets the Order utilities, including responsive and state variants.</summary>
    CssValue<OrderBuilder>? Order { get; set; }

    /// <summary>Gets or sets the Origin utilities, including responsive and state variants.</summary>
    CssValue<OriginBuilder>? Origin { get; set; }

    /// <summary>Gets or sets the OutlineColor utilities, including responsive and state variants.</summary>
    CssValue<OutlineColorBuilder>? OutlineColor { get; set; }

    /// <summary>Gets or sets the OutlineOffset utilities, including responsive and state variants.</summary>
    CssValue<OutlineOffsetBuilder>? OutlineOffset { get; set; }

    /// <summary>Gets or sets the OutlineWidth utilities, including responsive and state variants.</summary>
    CssValue<OutlineWidthBuilder>? OutlineWidth { get; set; }

    /// <summary>Gets or sets the OverflowWrap utilities, including responsive and state variants.</summary>
    CssValue<OverflowWrapBuilder>? OverflowWrap { get; set; }

    /// <summary>Gets or sets the PerspectiveOrigin utilities, including responsive and state variants.</summary>
    CssValue<PerspectiveOriginBuilder>? PerspectiveOrigin { get; set; }

    /// <summary>Gets or sets the Perspective utilities, including responsive and state variants.</summary>
    CssValue<PerspectiveBuilder>? Perspective { get; set; }

    /// <summary>Gets or sets the PlaceContentAlign utilities, including responsive and state variants.</summary>
    CssValue<PlaceContentAlignBuilder>? PlaceContentAlign { get; set; }

    /// <summary>Gets or sets the PlaceItemsAlign utilities, including responsive and state variants.</summary>
    CssValue<PlaceItemsAlignBuilder>? PlaceItemsAlign { get; set; }

    /// <summary>Gets or sets the PlaceSelfAlign utilities, including responsive and state variants.</summary>
    CssValue<PlaceSelfAlignBuilder>? PlaceSelfAlign { get; set; }

    /// <summary>Gets or sets the Rotate utilities, including responsive and state variants.</summary>
    CssValue<RotateBuilder>? Rotate { get; set; }

    /// <summary>Gets or sets the RowEnd utilities, including responsive and state variants.</summary>
    CssValue<RowEndBuilder>? RowEnd { get; set; }

    /// <summary>Gets or sets the Scale utilities, including responsive and state variants.</summary>
    CssValue<ScaleBuilder>? Scale { get; set; }

    /// <summary>Gets or sets the ScrollbarGutter utilities, including responsive and state variants.</summary>
    CssValue<ScrollbarGutterBuilder>? ScrollbarGutter { get; set; }

    /// <summary>Gets or sets the ScrollbarThumbColor utilities, including responsive and state variants.</summary>
    CssValue<ScrollbarThumbColorBuilder>? ScrollbarThumbColor { get; set; }

    /// <summary>Gets or sets the ScrollbarTrackColor utilities, including responsive and state variants.</summary>
    CssValue<ScrollbarTrackColorBuilder>? ScrollbarTrackColor { get; set; }

    /// <summary>Gets or sets the ScrollbarWidth utilities, including responsive and state variants.</summary>
    CssValue<ScrollbarWidthBuilder>? ScrollbarWidth { get; set; }

    /// <summary>Gets or sets the ScrollBehavior utilities, including responsive and state variants.</summary>
    CssValue<ScrollBehaviorBuilder>? ScrollBehavior { get; set; }

    /// <summary>Gets or sets the ScrollSnapAlign utilities, including responsive and state variants.</summary>
    CssValue<ScrollSnapAlignBuilder>? ScrollSnapAlign { get; set; }

    /// <summary>Gets or sets the ScrollSnap utilities, including responsive and state variants.</summary>
    CssValue<ScrollSnapBuilder>? ScrollSnap { get; set; }

    /// <summary>Gets or sets the ScrollSnapStop utilities, including responsive and state variants.</summary>
    CssValue<ScrollSnapStopBuilder>? ScrollSnapStop { get; set; }

    /// <summary>Gets or sets the ShadowColor utilities, including responsive and state variants.</summary>
    CssValue<ShadowColorBuilder>? ShadowColor { get; set; }

    /// <summary>Gets or sets the Skew utilities, including responsive and state variants.</summary>
    CssValue<SkewBuilder>? Skew { get; set; }

    /// <summary>Gets or sets the TabSize utilities, including responsive and state variants.</summary>
    CssValue<TabSizeBuilder>? TabSize { get; set; }

    /// <summary>Gets or sets the TextIndent utilities, including responsive and state variants.</summary>
    CssValue<TextIndentBuilder>? TextIndent { get; set; }

    /// <summary>Gets or sets the TextShadowColor utilities, including responsive and state variants.</summary>
    CssValue<TextShadowColorBuilder>? TextShadowColor { get; set; }

    /// <summary>Gets or sets the TextShadow utilities, including responsive and state variants.</summary>
    CssValue<TextShadowBuilder>? TextShadow { get; set; }

    /// <summary>Gets or sets the TouchAction utilities, including responsive and state variants.</summary>
    CssValue<TouchActionBuilder>? TouchAction { get; set; }

    /// <summary>Gets or sets the TransformStyle utilities, including responsive and state variants.</summary>
    CssValue<TransformStyleBuilder>? TransformStyle { get; set; }

    /// <summary>Gets or sets the TransitionBehavior utilities, including responsive and state variants.</summary>
    CssValue<TransitionBehaviorBuilder>? TransitionBehavior { get; set; }

    /// <summary>Gets or sets the Translate utilities, including responsive and state variants.</summary>
    CssValue<TranslateBuilder>? Translate { get; set; }

    /// <summary>Gets or sets the WillChange utilities, including responsive and state variants.</summary>
    CssValue<WillChangeBuilder>? WillChange { get; set; }

    /// <summary>Gets or sets the Zoom utilities, including responsive and state variants.</summary>
    CssValue<ZoomBuilder>? Zoom { get; set; }

}
