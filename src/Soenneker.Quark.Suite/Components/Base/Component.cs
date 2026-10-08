using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Soenneker.Blazor.Extensions.EventCallback;
using Soenneker.Extensions.String;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <inheritdoc cref="IComponent"/>
/// <remarks>Do not use the <c>new</c> keyword to Shadow inherited <see cref="ParameterAttribute"/> members. Blazor treats those names as duplicate parameters and fails at runtime.</remarks>
public abstract class Component : RenderComponent, IComponent
{
    private PresetParameterBits _explicitParameters;
    private AdvancedTypographyUtilities? _advancedTypographyUtilities;
    private Transform3DUtilities? _transform3DUtilities;
    private MaskUtilities? _maskUtilities;

    [Inject]
    protected ILogger<Component> Logger { get; set; } = null!;

    [Inject]
    protected QuarkOptions QuarkOptions { get; set; } = null!;

    [Parameter]
    public CssValue<AspectRatioBuilder>? AspectRatio { get; set; }

#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<BackfaceVisibilityBuilder>? BackfaceVisibility
    {
        get => _transform3DUtilities?.BackfaceVisibility;
        set
        {
            if (value.HasValue)
                (_transform3DUtilities ??= new Transform3DUtilities()).BackfaceVisibility = value;
            else if (_transform3DUtilities is not null)
                _transform3DUtilities.BackfaceVisibility = null;
        }
    }
#pragma warning restore BL0007

    [Parameter]
    public CssValue<BackgroundAttachmentBuilder>? BackgroundAttachment { get; set; }

    [Parameter]
    public CssValue<BackgroundBlendModeBuilder>? BackgroundBlendMode { get; set; }

    [Parameter]
    public CssValue<BackgroundClipBuilder>? BackgroundClip { get; set; }

    [Parameter]
    public CssValue<BackgroundImageBuilder>? BackgroundImage { get; set; }

    [Parameter]
    public CssValue<BackgroundOriginBuilder>? BackgroundOrigin { get; set; }

    [Parameter]
    public CssValue<BackgroundPositionBuilder>? BackgroundPosition { get; set; }

    [Parameter]
    public CssValue<BackgroundRepeatBuilder>? BackgroundRepeat { get; set; }

    [Parameter]
    public CssValue<BackgroundSizeBuilder>? BackgroundSize { get; set; }

    [Parameter]
    public CssValue<BlockSizeBuilder>? BlockSize { get; set; }

    [Parameter]
    public CssValue<BoxDecorationBreakBuilder>? BoxDecorationBreak { get; set; }

    [Parameter]
    public CssValue<BoxSizingBuilder>? BoxSizing { get; set; }

    [Parameter]
    public CssValue<BreakAfterBuilder>? BreakAfter { get; set; }

    [Parameter]
    public CssValue<BreakBeforeBuilder>? BreakBefore { get; set; }

    [Parameter]
    public CssValue<BreakInsideBuilder>? BreakInside { get; set; }

    [Parameter]
    public CssValue<ClearBuilder>? FloatClear { get; set; }

    [Parameter]
    public CssValue<ClipPathBuilder>? ClipPath { get; set; }

    [Parameter]
    public CssValue<ColEndBuilder>? ColEnd { get; set; }

    [Parameter]
    public CssValue<ColorSchemeBuilder>? ColorScheme { get; set; }

    [Parameter]
    public CssValue<ColumnsBuilder>? ColumnCount { get; set; }

    [Parameter]
    public CssValue<ContainerTypeBuilder>? ContainerQuery { get; set; }

    [Parameter]
    public CssValue<ContainBuilder>? Contain { get; set; }

    [Parameter]
    public CssValue<ContentBuilder>? GeneratedContent { get; set; }

    [Parameter]
    public CssValue<DecorationColorBuilder>? DecorationColor { get; set; }

    [Parameter]
    public CssValue<DecorationStyleBuilder>? DecorationStyle { get; set; }

    [Parameter]
    public CssValue<DecorationThicknessBuilder>? DecorationThickness { get; set; }

    [Parameter]
    public CssValue<DelayBuilder>? TransitionDelay { get; set; }

    [Parameter]
    public CssValue<EaseBuilder>? Ease { get; set; }

    [Parameter]
    public CssValue<FlexBasisBuilder>? FlexBasis { get; set; }

#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<FontFeatureSettingsBuilder>? FontFeatureSettings
    {
        get => _advancedTypographyUtilities?.FontFeatureSettings;
        set
        {
            if (value.HasValue)
                (_advancedTypographyUtilities ??= new AdvancedTypographyUtilities()).FontFeatureSettings = value;
            else if (_advancedTypographyUtilities is not null)
                _advancedTypographyUtilities.FontFeatureSettings = null;
        }
    }
#pragma warning restore BL0007

#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<FontSmoothingBuilder>? FontSmoothing
    {
        get => _advancedTypographyUtilities?.FontSmoothing;
        set
        {
            if (value.HasValue)
                (_advancedTypographyUtilities ??= new AdvancedTypographyUtilities()).FontSmoothing = value;
            else if (_advancedTypographyUtilities is not null)
                _advancedTypographyUtilities.FontSmoothing = null;
        }
    }
#pragma warning restore BL0007

#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<FontStretchBuilder>? FontStretch
    {
        get => _advancedTypographyUtilities?.FontStretch;
        set
        {
            if (value.HasValue)
                (_advancedTypographyUtilities ??= new AdvancedTypographyUtilities()).FontStretch = value;
            else if (_advancedTypographyUtilities is not null)
                _advancedTypographyUtilities.FontStretch = null;
        }
    }
#pragma warning restore BL0007

    [Parameter]
    public CssValue<ForcedColorAdjustBuilder>? ForcedColorAdjust { get; set; }

    [Parameter]
    public CssValue<GradientBuilder>? BackgroundGradient { get; set; }

    [Parameter]
    public CssValue<ColumnSpanBuilder>? ColumnSpan { get; set; }

    [Parameter]
    public CssValue<HyphenBuilder>? Hyphen { get; set; }

    [Parameter]
    public CssValue<InlineSizeBuilder>? InlineSize { get; set; }

    [Parameter]
    public CssValue<InsetBlockEndBuilder>? InsetBlockEnd { get; set; }

    [Parameter]
    public CssValue<InsetBlockStartBuilder>? InsetBlockStart { get; set; }

    [Parameter]
    public CssValue<InsetEndBuilder>? InsetEnd { get; set; }

    [Parameter]
    public CssValue<InsetRingColorBuilder>? InsetRingColor { get; set; }

    [Parameter]
    public CssValue<InsetRingBuilder>? InsetRing { get; set; }

    [Parameter]
    public CssValue<InsetShadowColorBuilder>? InsetShadowColor { get; set; }

    [Parameter]
    public CssValue<InsetShadowBuilder>? InsetShadow { get; set; }

    [Parameter]
    public CssValue<InsetStartBuilder>? InsetStart { get; set; }

    [Parameter]
    public CssValue<IsolationBuilder>? Isolation { get; set; }

#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<MaskClipBuilder>? MaskClip
    {
        get => _maskUtilities?.MaskClip;
        set
        {
            if (value.HasValue)
                (_maskUtilities ??= new MaskUtilities()).MaskClip = value;
            else if (_maskUtilities is not null)
                _maskUtilities.MaskClip = null;
        }
    }
#pragma warning restore BL0007

#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<MaskCompositeBuilder>? MaskComposite
    {
        get => _maskUtilities?.MaskComposite;
        set
        {
            if (value.HasValue)
                (_maskUtilities ??= new MaskUtilities()).MaskComposite = value;
            else if (_maskUtilities is not null)
                _maskUtilities.MaskComposite = null;
        }
    }
#pragma warning restore BL0007

#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<MaskImageBuilder>? MaskImage
    {
        get => _maskUtilities?.MaskImage;
        set
        {
            if (value.HasValue)
                (_maskUtilities ??= new MaskUtilities()).MaskImage = value;
            else if (_maskUtilities is not null)
                _maskUtilities.MaskImage = null;
        }
    }
#pragma warning restore BL0007

#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<MaskModeBuilder>? MaskMode
    {
        get => _maskUtilities?.MaskMode;
        set
        {
            if (value.HasValue)
                (_maskUtilities ??= new MaskUtilities()).MaskMode = value;
            else if (_maskUtilities is not null)
                _maskUtilities.MaskMode = null;
        }
    }
#pragma warning restore BL0007

#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<MaskOriginBuilder>? MaskOrigin
    {
        get => _maskUtilities?.MaskOrigin;
        set
        {
            if (value.HasValue)
                (_maskUtilities ??= new MaskUtilities()).MaskOrigin = value;
            else if (_maskUtilities is not null)
                _maskUtilities.MaskOrigin = null;
        }
    }
#pragma warning restore BL0007

#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<MaskPositionBuilder>? MaskPosition
    {
        get => _maskUtilities?.MaskPosition;
        set
        {
            if (value.HasValue)
                (_maskUtilities ??= new MaskUtilities()).MaskPosition = value;
            else if (_maskUtilities is not null)
                _maskUtilities.MaskPosition = null;
        }
    }
#pragma warning restore BL0007

#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<MaskRepeatBuilder>? MaskRepeat
    {
        get => _maskUtilities?.MaskRepeat;
        set
        {
            if (value.HasValue)
                (_maskUtilities ??= new MaskUtilities()).MaskRepeat = value;
            else if (_maskUtilities is not null)
                _maskUtilities.MaskRepeat = null;
        }
    }
#pragma warning restore BL0007

#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<MaskSizeBuilder>? MaskSize
    {
        get => _maskUtilities?.MaskSize;
        set
        {
            if (value.HasValue)
                (_maskUtilities ??= new MaskUtilities()).MaskSize = value;
            else if (_maskUtilities is not null)
                _maskUtilities.MaskSize = null;
        }
    }
#pragma warning restore BL0007

#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<MaskTypeBuilder>? MaskType
    {
        get => _maskUtilities?.MaskType;
        set
        {
            if (value.HasValue)
                (_maskUtilities ??= new MaskUtilities()).MaskType = value;
            else if (_maskUtilities is not null)
                _maskUtilities.MaskType = null;
        }
    }
#pragma warning restore BL0007

    [Parameter]
    public CssValue<MaxBlockSizeBuilder>? MaxBlockSize { get; set; }

    [Parameter]
    public CssValue<MaxInlineSizeBuilder>? MaxInlineSize { get; set; }

    [Parameter]
    public CssValue<MinBlockSizeBuilder>? MinBlockSize { get; set; }

    [Parameter]
    public CssValue<MinInlineSizeBuilder>? MinInlineSize { get; set; }

    [Parameter]
    public CssValue<MixBlendModeBuilder>? MixBlendMode { get; set; }

    [Parameter]
    public CssValue<OrderBuilder>? Order { get; set; }

    [Parameter]
    public CssValue<OriginBuilder>? Origin { get; set; }

    [Parameter]
    public CssValue<OutlineColorBuilder>? OutlineColor { get; set; }

    [Parameter]
    public CssValue<OutlineOffsetBuilder>? OutlineOffset { get; set; }

    [Parameter]
    public CssValue<OutlineWidthBuilder>? OutlineWidth { get; set; }

    [Parameter]
    public CssValue<OverflowWrapBuilder>? OverflowWrap { get; set; }

#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<PerspectiveOriginBuilder>? PerspectiveOrigin
    {
        get => _transform3DUtilities?.PerspectiveOrigin;
        set
        {
            if (value.HasValue)
                (_transform3DUtilities ??= new Transform3DUtilities()).PerspectiveOrigin = value;
            else if (_transform3DUtilities is not null)
                _transform3DUtilities.PerspectiveOrigin = null;
        }
    }
#pragma warning restore BL0007

#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<PerspectiveBuilder>? Perspective
    {
        get => _transform3DUtilities?.Perspective;
        set
        {
            if (value.HasValue)
                (_transform3DUtilities ??= new Transform3DUtilities()).Perspective = value;
            else if (_transform3DUtilities is not null)
                _transform3DUtilities.Perspective = null;
        }
    }
#pragma warning restore BL0007

    [Parameter]
    public CssValue<PlaceContentAlignBuilder>? PlaceContentAlign { get; set; }

    [Parameter]
    public CssValue<PlaceItemsAlignBuilder>? PlaceItemsAlign { get; set; }

    [Parameter]
    public CssValue<PlaceSelfAlignBuilder>? PlaceSelfAlign { get; set; }

    [Parameter]
    public CssValue<RotateBuilder>? Rotate { get; set; }

    [Parameter]
    public CssValue<RowEndBuilder>? RowEnd { get; set; }

    [Parameter]
    public CssValue<ScaleBuilder>? Scale { get; set; }

    [Parameter]
    public CssValue<ScrollbarGutterBuilder>? ScrollbarGutter { get; set; }

    [Parameter]
    public CssValue<ScrollbarThumbColorBuilder>? ScrollbarThumbColor { get; set; }

    [Parameter]
    public CssValue<ScrollbarTrackColorBuilder>? ScrollbarTrackColor { get; set; }

    [Parameter]
    public CssValue<ScrollbarWidthBuilder>? ScrollbarWidth { get; set; }

    [Parameter]
    public CssValue<ScrollBehaviorBuilder>? ScrollBehavior { get; set; }

    [Parameter]
    public CssValue<ScrollSnapAlignBuilder>? ScrollSnapAlign { get; set; }

    [Parameter]
    public CssValue<ScrollSnapBuilder>? ScrollSnap { get; set; }

    [Parameter]
    public CssValue<ScrollSnapStopBuilder>? ScrollSnapStop { get; set; }

    [Parameter]
    public CssValue<ShadowColorBuilder>? ShadowColor { get; set; }

    [Parameter]
    public CssValue<SkewBuilder>? Skew { get; set; }

    [Parameter]
    public CssValue<TabSizeBuilder>? TabSize { get; set; }

#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<TextIndentBuilder>? TextIndent
    {
        get => _advancedTypographyUtilities?.TextIndent;
        set
        {
            if (value.HasValue)
                (_advancedTypographyUtilities ??= new AdvancedTypographyUtilities()).TextIndent = value;
            else if (_advancedTypographyUtilities is not null)
                _advancedTypographyUtilities.TextIndent = null;
        }
    }
#pragma warning restore BL0007

#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<TextShadowColorBuilder>? TextShadowColor
    {
        get => _advancedTypographyUtilities?.TextShadowColor;
        set
        {
            if (value.HasValue)
                (_advancedTypographyUtilities ??= new AdvancedTypographyUtilities()).TextShadowColor = value;
            else if (_advancedTypographyUtilities is not null)
                _advancedTypographyUtilities.TextShadowColor = null;
        }
    }
#pragma warning restore BL0007

#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<TextShadowBuilder>? TextShadow
    {
        get => _advancedTypographyUtilities?.TextShadow;
        set
        {
            if (value.HasValue)
                (_advancedTypographyUtilities ??= new AdvancedTypographyUtilities()).TextShadow = value;
            else if (_advancedTypographyUtilities is not null)
                _advancedTypographyUtilities.TextShadow = null;
        }
    }
#pragma warning restore BL0007

    [Parameter]
    public CssValue<TouchActionBuilder>? TouchAction { get; set; }

#pragma warning disable BL0007 // Lazy storage only; setters do not trigger rendering or alter other parameters.
    [Parameter]
    public CssValue<TransformStyleBuilder>? TransformStyle
    {
        get => _transform3DUtilities?.TransformStyle;
        set
        {
            if (value.HasValue)
                (_transform3DUtilities ??= new Transform3DUtilities()).TransformStyle = value;
            else if (_transform3DUtilities is not null)
                _transform3DUtilities.TransformStyle = null;
        }
    }
#pragma warning restore BL0007

    [Parameter]
    public CssValue<TransitionBehaviorBuilder>? TransitionBehavior { get; set; }

    [Parameter]
    public CssValue<TranslateBuilder>? Translate { get; set; }

    [Parameter]
    public CssValue<WillChangeBuilder>? WillChange { get; set; }

    [Parameter]
    public CssValue<ZoomBuilder>? Zoom { get; set; }

    [Parameter]
    public bool Container { get; set; }

    [Parameter]
    public QuarkPresetToken? Preset { get; set; }

    [Parameter]
    public IReadOnlyList<QuarkPresetToken>? Presets { get; set; }

    [Parameter]
    public string? Title { get; set; }

    [Parameter]
    public bool Hidden { get; set; }

    [Parameter]
    public string? DataSlot { get; set; }

    [Parameter]
    public CssValue<InsetBuilder>? Inset { get; set; }

    [Parameter]
    public CssValue<TopBuilder>? Top { get; set; }

    [Parameter]
    public CssValue<RightBuilder>? Right { get; set; }

    [Parameter]
    public CssValue<BottomBuilder>? Bottom { get; set; }

    [Parameter]
    public CssValue<LeftBuilder>? Left { get; set; }

    [Parameter]
    public CssValue<DisplayBuilder>? Display { get; set; }

    [Parameter]
    public CssValue<VisibilityBuilder>? Visibility { get; set; }

    [Parameter]
    public CssValue<FloatBuilder>? Float { get; set; }

    [Parameter]
    public CssValue<VerticalAlignBuilder>? VerticalAlign { get; set; }

    [Parameter]
    public CssValue<TextAlignBuilder>? TextAlign { get; set; }

    [Parameter]
    public CssValue<TextColorBuilder>? TextColor { get; set; }

    [Parameter]
    public CssValue<TextSizeBuilder>? TextSize { get; set; }

    [Parameter]
    public CssValue<DecorationLineBuilder>? DecorationLine { get; set; }

    [Parameter]
    public CssValue<UnderlineOffsetBuilder>? UnderlineOffset { get; set; }

    [Parameter]
    public CssValue<TextTransformBuilder>? TextTransform { get; set; }

    [Parameter]
    public CssValue<FontFamilyBuilder>? FontFamily { get; set; }

    [Parameter]
    public CssValue<FontWeightBuilder>? FontWeight { get; set; }

    [Parameter]
    public CssValue<FontStyleBuilder>? FontStyle { get; set; }

    [Parameter]
    public CssValue<LeadingBuilder>? Leading { get; set; }

    [Parameter]
    public CssValue<TrackingBuilder>? Tracking { get; set; }

    [Parameter]
    public CssValue<WhitespaceBuilder>? Whitespace { get; set; }

    [Parameter]
    public CssValue<TextWrapBuilder>? TextWrap { get; set; }

    [Parameter]
    public CssValue<WordBreakBuilder>? WordBreak { get; set; }

    [Parameter]
    public CssValue<TextOverflowBuilder>? TextOverflow { get; set; }

    [Parameter]
    public CssValue<TruncateBuilder>? Truncate { get; set; }

    [Parameter]
    public CssValue<LineClampBuilder>? LineClamp { get; set; }

    [Parameter]
    public CssValue<FontVariantNumericBuilder>? FontVariantNumeric { get; set; }

    [Parameter]
    public CssValue<MarginBuilder>? Margin { get; set; }

    [Parameter]
    public CssValue<PaddingBuilder>? Padding { get; set; }

    [Parameter]
    public CssValue<PositionBuilder>? Position { get; set; }

    [Parameter]
    public CssValue<ScrollMarginBuilder>? ScrollMargin { get; set; }

    [Parameter]
    public CssValue<ScrollPaddingBuilder>? ScrollPadding { get; set; }

    [Parameter]
    public CssValue<SizeBuilder>? Size { get; set; }

    [Parameter]
    public CssValue<WidthBuilder>? Width { get; set; }

    [Parameter]
    public CssValue<MinWidthBuilder>? MinWidth { get; set; }

    [Parameter]
    public CssValue<MaxWidthBuilder>? MaxWidth { get; set; }

    [Parameter]
    public CssValue<HeightBuilder>? Height { get; set; }

    [Parameter]
    public CssValue<MinHeightBuilder>? MinHeight { get; set; }

    [Parameter]
    public CssValue<MaxHeightBuilder>? MaxHeight { get; set; }

    [Parameter]
    public CssValue<OverflowBuilder>? Overflow { get; set; }

    [Parameter]
    public CssValue<OverflowBuilder>? OverflowX { get; set; }

    [Parameter]
    public CssValue<OverflowBuilder>? OverflowY { get; set; }

    [Parameter]
    public CssValue<OverscrollBuilder>? Overscroll { get; set; }

    [Parameter]
    public CssValue<FlexBuilder>? Flex { get; set; }

    [Parameter]
    public CssValue<FlexDirectionBuilder>? FlexDirection { get; set; }

    [Parameter]
    public CssValue<FlexWrapBuilder>? FlexWrap { get; set; }

    [Parameter]
    public CssValue<GrowBuilder>? Grow { get; set; }

    [Parameter]
    public CssValue<ShrinkBuilder>? Shrink { get; set; }

    [Parameter]
    public CssValue<GapBuilder>? Gap { get; set; }

    [Parameter]
    public CssValue<SpaceBuilder>? Space { get; set; }

    [Parameter]
    public CssValue<DivideBuilder>? Divide { get; set; }

    [Parameter]
    public CssValue<ContentAlignBuilder>? ContentAlign { get; set; }

    [Parameter]
    public CssValue<ItemsBuilder>? ItemsAlign { get; set; }

    [Parameter]
    public CssValue<JustifyBuilder>? Justify { get; set; }

    [Parameter]
    public CssValue<SelfBuilder>? SelfAlign { get; set; }

    [Parameter]
    public CssValue<JustifyItemsAlignBuilder>? JustifyItemsAlign { get; set; }

    [Parameter]
    public CssValue<JustifySelfAlignBuilder>? JustifySelfAlign { get; set; }

    [Parameter]
    public CssValue<ColStartBuilder>? ColStart { get; set; }

    [Parameter]
    public CssValue<RowSpanBuilder>? RowSpan { get; set; }

    [Parameter]
    public CssValue<RowStartBuilder>? RowStart { get; set; }

    [Parameter]
    public CssValue<OpacityBuilder>? Opacity { get; set; }

    [Parameter]
    public CssValue<ZIndexBuilder>? ZIndex { get; set; }

    [Parameter]
    public CssValue<PointerEventsBuilder>? PointerEvents { get; set; }

    [Parameter]
    public CssValue<UserSelectBuilder>? UserSelect { get; set; }

    [Parameter]
    public CssValue<CursorBuilder>? Cursor { get; set; }

    [Parameter]
    public CssValue<ScreenReaderBuilder>? ScreenReader { get; set; }

    [Parameter]
    public CssValue<BackgroundColorBuilder>? BackgroundColor { get; set; }

    [Parameter]
    public CssValue<BorderBuilder>? Border { get; set; }

    [Parameter]
    public CssValue<BorderStyleBuilder>? BorderStyle { get; set; }

    [Parameter]
    public CssValue<BorderColorBuilder>? BorderColor { get; set; }

    [Parameter]
    public virtual CssValue<RoundedBuilder>? Rounded { get; set; }

    [Parameter]
    public CssValue<RingBuilder>? Ring { get; set; }

    [Parameter]
    public CssValue<RingOffsetBuilder>? RingOffset { get; set; }

    [Parameter]
    public CssValue<RingColorBuilder>? RingColor { get; set; }

    [Parameter]
    public CssValue<OutlineStyleBuilder>? OutlineStyle { get; set; }

    [Parameter]
    public virtual CssValue<ShadowBuilder>? Shadow { get; set; }

    [Parameter]
    public CssValue<BackdropFilterBuilder>? BackdropFilter { get; set; }

    [Parameter]
    public CssValue<BackdropBlurBuilder>? BackdropBlur { get; set; }

    [Parameter]
    public CssValue<BackdropBrightnessBuilder>? BackdropBrightness { get; set; }

    [Parameter]
    public CssValue<BackdropContrastBuilder>? BackdropContrast { get; set; }

    [Parameter]
    public CssValue<BackdropGrayscaleBuilder>? BackdropGrayscale { get; set; }

    [Parameter]
    public CssValue<BackdropHueRotateBuilder>? BackdropHueRotate { get; set; }

    [Parameter]
    public CssValue<BackdropInvertBuilder>? BackdropInvert { get; set; }

    [Parameter]
    public CssValue<BackdropOpacityBuilder>? BackdropOpacity { get; set; }

    [Parameter]
    public CssValue<BackdropSaturateBuilder>? BackdropSaturate { get; set; }

    [Parameter]
    public CssValue<BackdropSepiaBuilder>? BackdropSepia { get; set; }

    [Parameter]
    public CssValue<FilterBuilder>? Filter { get; set; }

    [Parameter]
    public CssValue<BlurBuilder>? Blur { get; set; }

    [Parameter]
    public CssValue<BrightnessBuilder>? Brightness { get; set; }

    [Parameter]
    public CssValue<ContrastBuilder>? Contrast { get; set; }

    [Parameter]
    public CssValue<DropShadowBuilder>? DropShadow { get; set; }

    [Parameter]
    public CssValue<DropShadowColorBuilder>? DropShadowColor { get; set; }

    [Parameter]
    public CssValue<GrayscaleBuilder>? Grayscale { get; set; }

    [Parameter]
    public CssValue<HueRotateBuilder>? HueRotate { get; set; }

    [Parameter]
    public CssValue<InvertBuilder>? Invert { get; set; }

    [Parameter]
    public CssValue<SaturateBuilder>? Saturate { get; set; }

    [Parameter]
    public CssValue<SepiaBuilder>? Sepia { get; set; }

    [Parameter]
    public CssValue<ResizeBuilder>? Resize { get; set; }

    [Parameter]
    public CssValue<TransformBuilder>? Transform { get; set; }

    [Parameter]
    public CssValue<AnimationBuilder>? Animation { get; set; }

    [Parameter]
    public CssValue<DurationBuilder>? Duration { get; set; }

    [Parameter]
    public CssValue<TransitionBuilder>? Transition { get; set; }

    [Parameter]
    public EventCallback<ElementReference> OnElementRefReady { get; set; }

    protected ElementReference ElementRef { get; set; }

    /// <summary>
    /// Gets the preset context already applied during the current attribute build.
    /// </summary>
    protected QuarkPresetContext? AppliedPresetContext { get; private set; }

    protected override bool AlwaysRender => QuarkOptions.AlwaysRender;

    public override Task SetParametersAsync(ParameterView parameters)
    {
        var resolvedPreset = Preset;
        var resolvedPresets = Presets;

        if (parameters.TryGetValue(nameof(Preset), out QuarkPresetToken? preset))
            resolvedPreset = preset;

        if (parameters.TryGetValue(nameof(Presets), out IReadOnlyList<QuarkPresetToken>? presets))
            resolvedPresets = presets;

        // Explicit-parameter tracking is only needed while presets are active. Avoid a second
        // ParameterView enumeration and HashSet work for the overwhelmingly common no-preset path.
        if (resolvedPreset is null && (resolvedPresets is null || resolvedPresets.Count == 0))
        {
            _explicitParameters = default;
            return base.SetParametersAsync(parameters);
        }

        _explicitParameters = default;

        foreach (var parameter in parameters)
        {
            int slot = GetPresetParameterSlot(parameter.Name);
            if (slot >= 0)
            {
                _explicitParameters[slot / 64] |= 1UL << (slot % 64);
            }
        }

        return base.SetParametersAsync(parameters);
    }

    protected bool HasExplicitParameter(string parameterName)
    {
        int slot = GetPresetParameterSlot(parameterName);
        return HasExplicitPresetSlot(slot);
    }

    private bool HasExplicitPresetSlot(int slot) => slot >= 0 && (_explicitParameters[slot / 64] & (1UL << (slot % 64))) != 0;

    private static int GetPresetParameterSlot(string name) => name switch
    {
        nameof(OutlineStyle) => (int)PresetProperty.OutlineStyle,
        nameof(PresetProperty.AccentColor) => (int)PresetProperty.AccentColor,
        nameof(PresetProperty.NativeAppearance) => (int)PresetProperty.NativeAppearance,
        nameof(AspectRatio) => (int)PresetProperty.AspectRatio,
        nameof(PresetProperty.AutoCols) => (int)PresetProperty.AutoCols,
        nameof(PresetProperty.AutoRows) => (int)PresetProperty.AutoRows,
        nameof(BackfaceVisibility) => (int)PresetProperty.BackfaceVisibility,
        nameof(BackgroundAttachment) => (int)PresetProperty.BackgroundAttachment,
        nameof(BackgroundBlendMode) => (int)PresetProperty.BackgroundBlendMode,
        nameof(BackgroundClip) => (int)PresetProperty.BackgroundClip,
        nameof(BackgroundImage) => (int)PresetProperty.BackgroundImage,
        nameof(BackgroundOrigin) => (int)PresetProperty.BackgroundOrigin,
        nameof(BackgroundPosition) => (int)PresetProperty.BackgroundPosition,
        nameof(BackgroundRepeat) => (int)PresetProperty.BackgroundRepeat,
        nameof(BackgroundSize) => (int)PresetProperty.BackgroundSize,
        nameof(BlockSize) => (int)PresetProperty.BlockSize,
        nameof(PresetProperty.BorderCollapse) => (int)PresetProperty.BorderCollapse,
        nameof(PresetProperty.BorderSpacing) => (int)PresetProperty.BorderSpacing,
        nameof(BoxDecorationBreak) => (int)PresetProperty.BoxDecorationBreak,
        nameof(BoxSizing) => (int)PresetProperty.BoxSizing,
        nameof(BreakAfter) => (int)PresetProperty.BreakAfter,
        nameof(BreakBefore) => (int)PresetProperty.BreakBefore,
        nameof(BreakInside) => (int)PresetProperty.BreakInside,
        nameof(PresetProperty.CaptionSide) => (int)PresetProperty.CaptionSide,
        nameof(PresetProperty.CaretColor) => (int)PresetProperty.CaretColor,
        nameof(FloatClear) => (int)PresetProperty.FloatClear,
        nameof(ClipPath) => (int)PresetProperty.ClipPath,
        nameof(ColEnd) => (int)PresetProperty.ColEnd,
        nameof(ColorScheme) => (int)PresetProperty.ColorScheme,
        nameof(ColumnCount) => (int)PresetProperty.ColumnCount,
        nameof(ContainerQuery) => (int)PresetProperty.ContainerQuery,
        nameof(Contain) => (int)PresetProperty.Contain,
        nameof(GeneratedContent) => (int)PresetProperty.GeneratedContent,
        nameof(DecorationColor) => (int)PresetProperty.DecorationColor,
        nameof(DecorationStyle) => (int)PresetProperty.DecorationStyle,
        nameof(DecorationThickness) => (int)PresetProperty.DecorationThickness,
        nameof(TransitionDelay) => (int)PresetProperty.TransitionDelay,
        nameof(Ease) => (int)PresetProperty.Ease,
        nameof(PresetProperty.FieldSizing) => (int)PresetProperty.FieldSizing,
        nameof(PresetProperty.FillRule) => (int)PresetProperty.FillRule,
        nameof(PresetProperty.Fill) => (int)PresetProperty.Fill,
        nameof(FlexBasis) => (int)PresetProperty.FlexBasis,
        nameof(FontFeatureSettings) => (int)PresetProperty.FontFeatureSettings,
        nameof(FontSmoothing) => (int)PresetProperty.FontSmoothing,
        nameof(FontStretch) => (int)PresetProperty.FontStretch,
        nameof(ForcedColorAdjust) => (int)PresetProperty.ForcedColorAdjust,
        nameof(BackgroundGradient) => (int)PresetProperty.BackgroundGradient,
        nameof(PresetProperty.GridAutoFlow) => (int)PresetProperty.GridAutoFlow,
        nameof(PresetProperty.GridColumns) => (int)PresetProperty.GridColumns,
        nameof(PresetProperty.GridRows) => (int)PresetProperty.GridRows,
        nameof(ColumnSpan) => (int)PresetProperty.ColumnSpan,
        nameof(Hyphen) => (int)PresetProperty.Hyphen,
        nameof(InlineSize) => (int)PresetProperty.InlineSize,
        nameof(InsetBlockEnd) => (int)PresetProperty.InsetBlockEnd,
        nameof(InsetBlockStart) => (int)PresetProperty.InsetBlockStart,
        nameof(InsetEnd) => (int)PresetProperty.InsetEnd,
        nameof(InsetRingColor) => (int)PresetProperty.InsetRingColor,
        nameof(InsetRing) => (int)PresetProperty.InsetRing,
        nameof(InsetShadowColor) => (int)PresetProperty.InsetShadowColor,
        nameof(InsetShadow) => (int)PresetProperty.InsetShadow,
        nameof(InsetStart) => (int)PresetProperty.InsetStart,
        nameof(Isolation) => (int)PresetProperty.Isolation,
        nameof(PresetProperty.ListStyleImage) => (int)PresetProperty.ListStyleImage,
        nameof(PresetProperty.ListStylePosition) => (int)PresetProperty.ListStylePosition,
        nameof(PresetProperty.ListStyleType) => (int)PresetProperty.ListStyleType,
        nameof(MaskClip) => (int)PresetProperty.MaskClip,
        nameof(MaskComposite) => (int)PresetProperty.MaskComposite,
        nameof(MaskImage) => (int)PresetProperty.MaskImage,
        nameof(MaskMode) => (int)PresetProperty.MaskMode,
        nameof(MaskOrigin) => (int)PresetProperty.MaskOrigin,
        nameof(MaskPosition) => (int)PresetProperty.MaskPosition,
        nameof(MaskRepeat) => (int)PresetProperty.MaskRepeat,
        nameof(MaskSize) => (int)PresetProperty.MaskSize,
        nameof(MaskType) => (int)PresetProperty.MaskType,
        nameof(MaxBlockSize) => (int)PresetProperty.MaxBlockSize,
        nameof(MaxInlineSize) => (int)PresetProperty.MaxInlineSize,
        nameof(MinBlockSize) => (int)PresetProperty.MinBlockSize,
        nameof(MinInlineSize) => (int)PresetProperty.MinInlineSize,
        nameof(MixBlendMode) => (int)PresetProperty.MixBlendMode,
        nameof(PresetProperty.ObjectFit) => (int)PresetProperty.ObjectFit,
        nameof(PresetProperty.ObjectPosition) => (int)PresetProperty.ObjectPosition,
        nameof(Order) => (int)PresetProperty.Order,
        nameof(Origin) => (int)PresetProperty.Origin,
        nameof(OutlineColor) => (int)PresetProperty.OutlineColor,
        nameof(OutlineOffset) => (int)PresetProperty.OutlineOffset,
        nameof(OutlineWidth) => (int)PresetProperty.OutlineWidth,
        nameof(OverflowWrap) => (int)PresetProperty.OverflowWrap,
        nameof(PerspectiveOrigin) => (int)PresetProperty.PerspectiveOrigin,
        nameof(Perspective) => (int)PresetProperty.Perspective,
        nameof(PlaceContentAlign) => (int)PresetProperty.PlaceContentAlign,
        nameof(PlaceItemsAlign) => (int)PresetProperty.PlaceItemsAlign,
        nameof(PlaceSelfAlign) => (int)PresetProperty.PlaceSelfAlign,
        nameof(Rotate) => (int)PresetProperty.Rotate,
        nameof(RowEnd) => (int)PresetProperty.RowEnd,
        nameof(Scale) => (int)PresetProperty.Scale,
        nameof(ScrollbarGutter) => (int)PresetProperty.ScrollbarGutter,
        nameof(ScrollbarThumbColor) => (int)PresetProperty.ScrollbarThumbColor,
        nameof(ScrollbarTrackColor) => (int)PresetProperty.ScrollbarTrackColor,
        nameof(ScrollbarWidth) => (int)PresetProperty.ScrollbarWidth,
        nameof(ScrollBehavior) => (int)PresetProperty.ScrollBehavior,
        nameof(ScrollSnapAlign) => (int)PresetProperty.ScrollSnapAlign,
        nameof(ScrollSnap) => (int)PresetProperty.ScrollSnap,
        nameof(ScrollSnapStop) => (int)PresetProperty.ScrollSnapStop,
        nameof(ShadowColor) => (int)PresetProperty.ShadowColor,
        nameof(Skew) => (int)PresetProperty.Skew,
        nameof(PresetProperty.StrokeLineCap) => (int)PresetProperty.StrokeLineCap,
        nameof(PresetProperty.StrokeLineJoin) => (int)PresetProperty.StrokeLineJoin,
        nameof(PresetProperty.Stroke) => (int)PresetProperty.Stroke,
        nameof(PresetProperty.SvgStrokeWidth) => (int)PresetProperty.SvgStrokeWidth,
        nameof(PresetProperty.TableLayout) => (int)PresetProperty.TableLayout,
        nameof(TabSize) => (int)PresetProperty.TabSize,
        nameof(TextIndent) => (int)PresetProperty.TextIndent,
        nameof(TextShadowColor) => (int)PresetProperty.TextShadowColor,
        nameof(TextShadow) => (int)PresetProperty.TextShadow,
        nameof(TouchAction) => (int)PresetProperty.TouchAction,
        nameof(TransformStyle) => (int)PresetProperty.TransformStyle,
        nameof(TransitionBehavior) => (int)PresetProperty.TransitionBehavior,
        nameof(Translate) => (int)PresetProperty.Translate,
        nameof(WillChange) => (int)PresetProperty.WillChange,
        nameof(Zoom) => (int)PresetProperty.Zoom,
        nameof(Inset) => (int)PresetProperty.Inset,
        nameof(Top) => (int)PresetProperty.Top,
        nameof(Right) => (int)PresetProperty.Right,
        nameof(Bottom) => (int)PresetProperty.Bottom,
        nameof(Left) => (int)PresetProperty.Left,
        nameof(Display) => (int)PresetProperty.Display,
        nameof(Visibility) => (int)PresetProperty.Visibility,
        nameof(Float) => (int)PresetProperty.Float,
        nameof(VerticalAlign) => (int)PresetProperty.VerticalAlign,
        nameof(TextAlign) => (int)PresetProperty.TextAlign,
        nameof(TextColor) => (int)PresetProperty.TextColor,
        nameof(TextSize) => (int)PresetProperty.TextSize,
        nameof(DecorationLine) => (int)PresetProperty.DecorationLine,
        nameof(UnderlineOffset) => (int)PresetProperty.UnderlineOffset,
        nameof(TextTransform) => (int)PresetProperty.TextTransform,
        nameof(FontFamily) => (int)PresetProperty.FontFamily,
        nameof(FontWeight) => (int)PresetProperty.FontWeight,
        nameof(FontStyle) => (int)PresetProperty.FontStyle,
        nameof(Leading) => (int)PresetProperty.Leading,
        nameof(Tracking) => (int)PresetProperty.Tracking,
        nameof(Whitespace) => (int)PresetProperty.Whitespace,
        nameof(TextWrap) => (int)PresetProperty.TextWrap,
        nameof(WordBreak) => (int)PresetProperty.WordBreak,
        nameof(TextOverflow) => (int)PresetProperty.TextOverflow,
        nameof(Truncate) => (int)PresetProperty.Truncate,
        nameof(LineClamp) => (int)PresetProperty.LineClamp,
        nameof(FontVariantNumeric) => (int)PresetProperty.FontVariantNumeric,
        nameof(Margin) => (int)PresetProperty.Margin,
        nameof(Padding) => (int)PresetProperty.Padding,
        nameof(Position) => (int)PresetProperty.Position,
        nameof(ScrollMargin) => (int)PresetProperty.ScrollMargin,
        nameof(ScrollPadding) => (int)PresetProperty.ScrollPadding,
        nameof(Size) => (int)PresetProperty.Size,
        nameof(Width) => (int)PresetProperty.Width,
        nameof(MinWidth) => (int)PresetProperty.MinWidth,
        nameof(MaxWidth) => (int)PresetProperty.MaxWidth,
        nameof(Height) => (int)PresetProperty.Height,
        nameof(MinHeight) => (int)PresetProperty.MinHeight,
        nameof(MaxHeight) => (int)PresetProperty.MaxHeight,
        nameof(Overflow) => (int)PresetProperty.Overflow,
        nameof(OverflowX) => (int)PresetProperty.OverflowX,
        nameof(OverflowY) => (int)PresetProperty.OverflowY,
        nameof(Overscroll) => (int)PresetProperty.Overscroll,
        nameof(Flex) => (int)PresetProperty.Flex,
        nameof(FlexDirection) => (int)PresetProperty.FlexDirection,
        nameof(FlexWrap) => (int)PresetProperty.FlexWrap,
        nameof(Grow) => (int)PresetProperty.Grow,
        nameof(Shrink) => (int)PresetProperty.Shrink,
        nameof(Gap) => (int)PresetProperty.Gap,
        nameof(Space) => (int)PresetProperty.Space,
        nameof(Divide) => (int)PresetProperty.Divide,
        nameof(ContentAlign) => (int)PresetProperty.ContentAlign,
        nameof(ItemsAlign) => (int)PresetProperty.ItemsAlign,
        nameof(Justify) => (int)PresetProperty.Justify,
        nameof(SelfAlign) => (int)PresetProperty.SelfAlign,
        nameof(JustifyItemsAlign) => (int)PresetProperty.JustifyItemsAlign,
        nameof(JustifySelfAlign) => (int)PresetProperty.JustifySelfAlign,
        nameof(ColStart) => (int)PresetProperty.ColStart,
        nameof(RowSpan) => (int)PresetProperty.RowSpan,
        nameof(RowStart) => (int)PresetProperty.RowStart,
        nameof(Opacity) => (int)PresetProperty.Opacity,
        nameof(ZIndex) => (int)PresetProperty.ZIndex,
        nameof(PointerEvents) => (int)PresetProperty.PointerEvents,
        nameof(UserSelect) => (int)PresetProperty.UserSelect,
        nameof(Cursor) => (int)PresetProperty.Cursor,
        nameof(ScreenReader) => (int)PresetProperty.ScreenReader,
        nameof(BackgroundColor) => (int)PresetProperty.BackgroundColor,
        nameof(BorderColor) => (int)PresetProperty.BorderColor,
        nameof(Border) => (int)PresetProperty.Border,
        nameof(BorderStyle) => (int)PresetProperty.BorderStyle,
        nameof(Rounded) => (int)PresetProperty.Rounded,
        nameof(RingColor) => (int)PresetProperty.RingColor,
        nameof(Ring) => (int)PresetProperty.Ring,
        nameof(RingOffset) => (int)PresetProperty.RingOffset,
        nameof(Shadow) => (int)PresetProperty.Shadow,
        nameof(BackdropFilter) => (int)PresetProperty.BackdropFilter,
        nameof(BackdropBlur) => (int)PresetProperty.BackdropBlur,
        nameof(BackdropBrightness) => (int)PresetProperty.BackdropBrightness,
        nameof(BackdropContrast) => (int)PresetProperty.BackdropContrast,
        nameof(BackdropGrayscale) => (int)PresetProperty.BackdropGrayscale,
        nameof(BackdropHueRotate) => (int)PresetProperty.BackdropHueRotate,
        nameof(BackdropInvert) => (int)PresetProperty.BackdropInvert,
        nameof(BackdropOpacity) => (int)PresetProperty.BackdropOpacity,
        nameof(BackdropSaturate) => (int)PresetProperty.BackdropSaturate,
        nameof(BackdropSepia) => (int)PresetProperty.BackdropSepia,
        nameof(Filter) => (int)PresetProperty.Filter,
        nameof(Blur) => (int)PresetProperty.Blur,
        nameof(Brightness) => (int)PresetProperty.Brightness,
        nameof(Contrast) => (int)PresetProperty.Contrast,
        nameof(DropShadow) => (int)PresetProperty.DropShadow,
        nameof(DropShadowColor) => (int)PresetProperty.DropShadowColor,
        nameof(Grayscale) => (int)PresetProperty.Grayscale,
        nameof(HueRotate) => (int)PresetProperty.HueRotate,
        nameof(Invert) => (int)PresetProperty.Invert,
        nameof(Saturate) => (int)PresetProperty.Saturate,
        nameof(Sepia) => (int)PresetProperty.Sepia,
        nameof(Resize) => (int)PresetProperty.Resize,
        nameof(Transform) => (int)PresetProperty.Transform,
        nameof(Animation) => (int)PresetProperty.Animation,
        nameof(Duration) => (int)PresetProperty.Duration,
        nameof(Transition) => (int)PresetProperty.Transition,
        nameof(Class) => (int)PresetProperty.Class,
        nameof(Style) => (int)PresetProperty.Style,
        _ => -1
    };

    protected CssValue<T>? ResolvePresetValue<T>(CssValue<T>? value, CssValue<T>? presetValue, string parameterName) where T : class, ICssBuilder =>
        ResolvePresetSlot(value, presetValue, GetPresetParameterSlot(parameterName));

    private CssValue<T>? ResolvePresetSlot<T>(CssValue<T>? value, CssValue<T>? presetValue, int slot) where T : class, ICssBuilder =>
        presetValue is null || HasExplicitPresetSlot(slot) ? value : presetValue;

    protected override void BuildOwnedAttributes(Dictionary<string, object> attrs)
    {
        base.BuildOwnedAttributes(attrs);

        if (Title.HasContent())
            attrs["title"] = Title!;

        if (Hidden)
            attrs["hidden"] = QuarkAttributeValues.True;

    }

    protected override void BuildFinalAttributes(Dictionary<string, object> attrs)
    {
        base.BuildFinalAttributes(attrs);

        if (DataSlot.HasContent() && !HasExplicitDataSlotAttribute())
        {
            attrs["data-slot"] = DataSlot!;
            return;
        }

    }

    protected virtual void BuildDefaultClasses(ref PooledStringBuilder cls) { }

    protected override void BuildOwnedClassAndStyle(ref PooledStringBuilder sty, ref PooledStringBuilder cls)
    {
        base.BuildOwnedClassAndStyle(ref sty, ref cls);
        var preset = BuildPresetContext();
        AppliedPresetContext = preset;
        BuildDefaultClasses(ref cls);

        BuildTypographyClassAndStyle(ref sty, ref cls, preset);
        BuildLayoutClassAndStyle(ref sty, ref cls, preset);
        BuildInteractionClassAndStyle(ref sty, ref cls, preset);
        BuildVisualClassAndStyle(ref sty, ref cls, preset);
        BuildAdditionalUtilities1(ref cls, preset);
        BuildAdditionalUtilities2(ref cls, preset);
        BuildAdditionalUtilities3(ref cls, preset);
        BuildAdditionalUtilities4(ref cls, preset);
        BuildAdditionalUtilities5(ref cls, preset);
        BuildAdditionalUtilities6(ref cls, preset);
        if (_advancedTypographyUtilities is not null || preset is not null)
            BuildAdvancedTypographyUtilities(ref cls, preset);
        if (_transform3DUtilities is not null || preset is not null)
            BuildTransform3DUtilities(ref cls, preset);
        if (_maskUtilities is not null || preset is not null)
            BuildMaskUtilities(ref cls, preset);

        // Explicit inline CSS follows builder-provided styles; the browser resolves precedence.
        if (Style.HasContent())
            AppendStyleDecl(ref sty, Style!);

        if (Container)
            AppendClass(ref cls, "container");

        if (preset?.Class.HasContent() == true)
            AppendClass(ref cls, preset.Class!);

        if (Class.HasContent())
            AppendClass(ref cls, Class!);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void BuildAdditionalUtilities1(ref PooledStringBuilder cls, QuarkPresetContext? preset)
    {
        if (AspectRatio.HasValue || preset is not null && preset.HasValue(PresetProperty.AspectRatio))
            AddCss(ref cls, ResolvePresetSlot(AspectRatio, preset?.AspectRatio, (int)PresetProperty.AspectRatio));
        if (BackgroundAttachment.HasValue || preset is not null && preset.HasValue(PresetProperty.BackgroundAttachment))
            AddCss(ref cls, ResolvePresetSlot(BackgroundAttachment, preset?.BackgroundAttachment, (int)PresetProperty.BackgroundAttachment));
        if (BackgroundBlendMode.HasValue || preset is not null && preset.HasValue(PresetProperty.BackgroundBlendMode))
            AddCss(ref cls, ResolvePresetSlot(BackgroundBlendMode, preset?.BackgroundBlendMode, (int)PresetProperty.BackgroundBlendMode));
        if (BackgroundClip.HasValue || preset is not null && preset.HasValue(PresetProperty.BackgroundClip))
            AddCss(ref cls, ResolvePresetSlot(BackgroundClip, preset?.BackgroundClip, (int)PresetProperty.BackgroundClip));
        if (BackgroundImage.HasValue || preset is not null && preset.HasValue(PresetProperty.BackgroundImage))
            AddCss(ref cls, ResolvePresetSlot(BackgroundImage, preset?.BackgroundImage, (int)PresetProperty.BackgroundImage));
        if (BackgroundOrigin.HasValue || preset is not null && preset.HasValue(PresetProperty.BackgroundOrigin))
            AddCss(ref cls, ResolvePresetSlot(BackgroundOrigin, preset?.BackgroundOrigin, (int)PresetProperty.BackgroundOrigin));
        if (BackgroundPosition.HasValue || preset is not null && preset.HasValue(PresetProperty.BackgroundPosition))
            AddCss(ref cls, ResolvePresetSlot(BackgroundPosition, preset?.BackgroundPosition, (int)PresetProperty.BackgroundPosition));
        if (BackgroundRepeat.HasValue || preset is not null && preset.HasValue(PresetProperty.BackgroundRepeat))
            AddCss(ref cls, ResolvePresetSlot(BackgroundRepeat, preset?.BackgroundRepeat, (int)PresetProperty.BackgroundRepeat));
        if (BackgroundSize.HasValue || preset is not null && preset.HasValue(PresetProperty.BackgroundSize))
            AddCss(ref cls, ResolvePresetSlot(BackgroundSize, preset?.BackgroundSize, (int)PresetProperty.BackgroundSize));
        if (BlockSize.HasValue || preset is not null && preset.HasValue(PresetProperty.BlockSize))
            AddCss(ref cls, ResolvePresetSlot(BlockSize, preset?.BlockSize, (int)PresetProperty.BlockSize));
        if (BoxDecorationBreak.HasValue || preset is not null && preset.HasValue(PresetProperty.BoxDecorationBreak))
            AddCss(ref cls, ResolvePresetSlot(BoxDecorationBreak, preset?.BoxDecorationBreak, (int)PresetProperty.BoxDecorationBreak));
        if (BoxSizing.HasValue || preset is not null && preset.HasValue(PresetProperty.BoxSizing))
            AddCss(ref cls, ResolvePresetSlot(BoxSizing, preset?.BoxSizing, (int)PresetProperty.BoxSizing));
        if (BreakAfter.HasValue || preset is not null && preset.HasValue(PresetProperty.BreakAfter))
            AddCss(ref cls, ResolvePresetSlot(BreakAfter, preset?.BreakAfter, (int)PresetProperty.BreakAfter));
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void BuildAdditionalUtilities2(ref PooledStringBuilder cls, QuarkPresetContext? preset)
    {
        if (BreakBefore.HasValue || preset is not null && preset.HasValue(PresetProperty.BreakBefore))
            AddCss(ref cls, ResolvePresetSlot(BreakBefore, preset?.BreakBefore, (int)PresetProperty.BreakBefore));
        if (BreakInside.HasValue || preset is not null && preset.HasValue(PresetProperty.BreakInside))
            AddCss(ref cls, ResolvePresetSlot(BreakInside, preset?.BreakInside, (int)PresetProperty.BreakInside));
        if (FloatClear.HasValue || preset is not null && preset.HasValue(PresetProperty.FloatClear))
            AddCss(ref cls, ResolvePresetSlot(FloatClear, preset?.FloatClear, (int)PresetProperty.FloatClear));
        if (ClipPath.HasValue || preset is not null && preset.HasValue(PresetProperty.ClipPath))
            AddCss(ref cls, ResolvePresetSlot(ClipPath, preset?.ClipPath, (int)PresetProperty.ClipPath));
        if (ColEnd.HasValue || preset is not null && preset.HasValue(PresetProperty.ColEnd))
            AddCss(ref cls, ResolvePresetSlot(ColEnd, preset?.ColEnd, (int)PresetProperty.ColEnd));
        if (ColorScheme.HasValue || preset is not null && preset.HasValue(PresetProperty.ColorScheme))
            AddCss(ref cls, ResolvePresetSlot(ColorScheme, preset?.ColorScheme, (int)PresetProperty.ColorScheme));
        if (ColumnCount.HasValue || preset is not null && preset.HasValue(PresetProperty.ColumnCount))
            AddCss(ref cls, ResolvePresetSlot(ColumnCount, preset?.ColumnCount, (int)PresetProperty.ColumnCount));
        if (ContainerQuery.HasValue || preset is not null && preset.HasValue(PresetProperty.ContainerQuery))
            AddCss(ref cls, ResolvePresetSlot(ContainerQuery, preset?.ContainerQuery, (int)PresetProperty.ContainerQuery));
        if (Contain.HasValue || preset is not null && preset.HasValue(PresetProperty.Contain))
            AddCss(ref cls, ResolvePresetSlot(Contain, preset?.Contain, (int)PresetProperty.Contain));
        if (GeneratedContent.HasValue || preset is not null && preset.HasValue(PresetProperty.GeneratedContent))
            AddCss(ref cls, ResolvePresetSlot(GeneratedContent, preset?.GeneratedContent, (int)PresetProperty.GeneratedContent));
        if (DecorationColor.HasValue || preset is not null && preset.HasValue(PresetProperty.DecorationColor))
            AddCss(ref cls, ResolvePresetSlot(DecorationColor, preset?.DecorationColor, (int)PresetProperty.DecorationColor));
        if (DecorationStyle.HasValue || preset is not null && preset.HasValue(PresetProperty.DecorationStyle))
            AddCss(ref cls, ResolvePresetSlot(DecorationStyle, preset?.DecorationStyle, (int)PresetProperty.DecorationStyle));
        if (DecorationThickness.HasValue || preset is not null && preset.HasValue(PresetProperty.DecorationThickness))
            AddCss(ref cls, ResolvePresetSlot(DecorationThickness, preset?.DecorationThickness, (int)PresetProperty.DecorationThickness));
        if (TransitionDelay.HasValue || preset is not null && preset.HasValue(PresetProperty.TransitionDelay))
            AddCss(ref cls, ResolvePresetSlot(TransitionDelay, preset?.TransitionDelay, (int)PresetProperty.TransitionDelay));
        if (Ease.HasValue || preset is not null && preset.HasValue(PresetProperty.Ease))
            AddCss(ref cls, ResolvePresetSlot(Ease, preset?.Ease, (int)PresetProperty.Ease));
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void BuildAdditionalUtilities3(ref PooledStringBuilder cls, QuarkPresetContext? preset)
    {
        if (FlexBasis.HasValue || preset is not null && preset.HasValue(PresetProperty.FlexBasis))
            AddCss(ref cls, ResolvePresetSlot(FlexBasis, preset?.FlexBasis, (int)PresetProperty.FlexBasis));
        if (ForcedColorAdjust.HasValue || preset is not null && preset.HasValue(PresetProperty.ForcedColorAdjust))
            AddCss(ref cls, ResolvePresetSlot(ForcedColorAdjust, preset?.ForcedColorAdjust, (int)PresetProperty.ForcedColorAdjust));
        if (BackgroundGradient.HasValue || preset is not null && preset.HasValue(PresetProperty.BackgroundGradient))
            AddCss(ref cls, ResolvePresetSlot(BackgroundGradient, preset?.BackgroundGradient, (int)PresetProperty.BackgroundGradient));
        if (ColumnSpan.HasValue || preset is not null && preset.HasValue(PresetProperty.ColumnSpan))
            AddCss(ref cls, ResolvePresetSlot(ColumnSpan, preset?.ColumnSpan, (int)PresetProperty.ColumnSpan));
        if (Hyphen.HasValue || preset is not null && preset.HasValue(PresetProperty.Hyphen))
            AddCss(ref cls, ResolvePresetSlot(Hyphen, preset?.Hyphen, (int)PresetProperty.Hyphen));
        if (InlineSize.HasValue || preset is not null && preset.HasValue(PresetProperty.InlineSize))
            AddCss(ref cls, ResolvePresetSlot(InlineSize, preset?.InlineSize, (int)PresetProperty.InlineSize));
        if (InsetBlockEnd.HasValue || preset is not null && preset.HasValue(PresetProperty.InsetBlockEnd))
            AddCss(ref cls, ResolvePresetSlot(InsetBlockEnd, preset?.InsetBlockEnd, (int)PresetProperty.InsetBlockEnd));
        if (InsetBlockStart.HasValue || preset is not null && preset.HasValue(PresetProperty.InsetBlockStart))
            AddCss(ref cls, ResolvePresetSlot(InsetBlockStart, preset?.InsetBlockStart, (int)PresetProperty.InsetBlockStart));
        if (InsetEnd.HasValue || preset is not null && preset.HasValue(PresetProperty.InsetEnd))
            AddCss(ref cls, ResolvePresetSlot(InsetEnd, preset?.InsetEnd, (int)PresetProperty.InsetEnd));
        if (InsetRingColor.HasValue || preset is not null && preset.HasValue(PresetProperty.InsetRingColor))
            AddCss(ref cls, ResolvePresetSlot(InsetRingColor, preset?.InsetRingColor, (int)PresetProperty.InsetRingColor));
        if (InsetRing.HasValue || preset is not null && preset.HasValue(PresetProperty.InsetRing))
            AddCss(ref cls, ResolvePresetSlot(InsetRing, preset?.InsetRing, (int)PresetProperty.InsetRing));
        if (InsetShadowColor.HasValue || preset is not null && preset.HasValue(PresetProperty.InsetShadowColor))
            AddCss(ref cls, ResolvePresetSlot(InsetShadowColor, preset?.InsetShadowColor, (int)PresetProperty.InsetShadowColor));
        if (InsetShadow.HasValue || preset is not null && preset.HasValue(PresetProperty.InsetShadow))
            AddCss(ref cls, ResolvePresetSlot(InsetShadow, preset?.InsetShadow, (int)PresetProperty.InsetShadow));
        if (InsetStart.HasValue || preset is not null && preset.HasValue(PresetProperty.InsetStart))
            AddCss(ref cls, ResolvePresetSlot(InsetStart, preset?.InsetStart, (int)PresetProperty.InsetStart));
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void BuildAdditionalUtilities4(ref PooledStringBuilder cls, QuarkPresetContext? preset)
    {
        if (Isolation.HasValue || preset is not null && preset.HasValue(PresetProperty.Isolation))
            AddCss(ref cls, ResolvePresetSlot(Isolation, preset?.Isolation, (int)PresetProperty.Isolation));
        if (MaxBlockSize.HasValue || preset is not null && preset.HasValue(PresetProperty.MaxBlockSize))
            AddCss(ref cls, ResolvePresetSlot(MaxBlockSize, preset?.MaxBlockSize, (int)PresetProperty.MaxBlockSize));
        if (MaxInlineSize.HasValue || preset is not null && preset.HasValue(PresetProperty.MaxInlineSize))
            AddCss(ref cls, ResolvePresetSlot(MaxInlineSize, preset?.MaxInlineSize, (int)PresetProperty.MaxInlineSize));
        if (MinBlockSize.HasValue || preset is not null && preset.HasValue(PresetProperty.MinBlockSize))
            AddCss(ref cls, ResolvePresetSlot(MinBlockSize, preset?.MinBlockSize, (int)PresetProperty.MinBlockSize));
        if (MinInlineSize.HasValue || preset is not null && preset.HasValue(PresetProperty.MinInlineSize))
            AddCss(ref cls, ResolvePresetSlot(MinInlineSize, preset?.MinInlineSize, (int)PresetProperty.MinInlineSize));
        if (MixBlendMode.HasValue || preset is not null && preset.HasValue(PresetProperty.MixBlendMode))
            AddCss(ref cls, ResolvePresetSlot(MixBlendMode, preset?.MixBlendMode, (int)PresetProperty.MixBlendMode));
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void BuildAdditionalUtilities5(ref PooledStringBuilder cls, QuarkPresetContext? preset)
    {
        if (Order.HasValue || preset is not null && preset.HasValue(PresetProperty.Order))
            AddCss(ref cls, ResolvePresetSlot(Order, preset?.Order, (int)PresetProperty.Order));
        if (Origin.HasValue || preset is not null && preset.HasValue(PresetProperty.Origin))
            AddCss(ref cls, ResolvePresetSlot(Origin, preset?.Origin, (int)PresetProperty.Origin));
        if (OutlineColor.HasValue || preset is not null && preset.HasValue(PresetProperty.OutlineColor))
            AddCss(ref cls, ResolvePresetSlot(OutlineColor, preset?.OutlineColor, (int)PresetProperty.OutlineColor));
        if (OutlineOffset.HasValue || preset is not null && preset.HasValue(PresetProperty.OutlineOffset))
            AddCss(ref cls, ResolvePresetSlot(OutlineOffset, preset?.OutlineOffset, (int)PresetProperty.OutlineOffset));
        if (OutlineWidth.HasValue || preset is not null && preset.HasValue(PresetProperty.OutlineWidth))
            AddCss(ref cls, ResolvePresetSlot(OutlineWidth, preset?.OutlineWidth, (int)PresetProperty.OutlineWidth));
        if (OverflowWrap.HasValue || preset is not null && preset.HasValue(PresetProperty.OverflowWrap))
            AddCss(ref cls, ResolvePresetSlot(OverflowWrap, preset?.OverflowWrap, (int)PresetProperty.OverflowWrap));
        if (PlaceContentAlign.HasValue || preset is not null && preset.HasValue(PresetProperty.PlaceContentAlign))
            AddCss(ref cls, ResolvePresetSlot(PlaceContentAlign, preset?.PlaceContentAlign, (int)PresetProperty.PlaceContentAlign));
        if (PlaceItemsAlign.HasValue || preset is not null && preset.HasValue(PresetProperty.PlaceItemsAlign))
            AddCss(ref cls, ResolvePresetSlot(PlaceItemsAlign, preset?.PlaceItemsAlign, (int)PresetProperty.PlaceItemsAlign));
        if (PlaceSelfAlign.HasValue || preset is not null && preset.HasValue(PresetProperty.PlaceSelfAlign))
            AddCss(ref cls, ResolvePresetSlot(PlaceSelfAlign, preset?.PlaceSelfAlign, (int)PresetProperty.PlaceSelfAlign));
        if (Rotate.HasValue || preset is not null && preset.HasValue(PresetProperty.Rotate))
            AddCss(ref cls, ResolvePresetSlot(Rotate, preset?.Rotate, (int)PresetProperty.Rotate));
        if (RowEnd.HasValue || preset is not null && preset.HasValue(PresetProperty.RowEnd))
            AddCss(ref cls, ResolvePresetSlot(RowEnd, preset?.RowEnd, (int)PresetProperty.RowEnd));
        if (Scale.HasValue || preset is not null && preset.HasValue(PresetProperty.Scale))
            AddCss(ref cls, ResolvePresetSlot(Scale, preset?.Scale, (int)PresetProperty.Scale));
        if (ScrollbarGutter.HasValue || preset is not null && preset.HasValue(PresetProperty.ScrollbarGutter))
            AddCss(ref cls, ResolvePresetSlot(ScrollbarGutter, preset?.ScrollbarGutter, (int)PresetProperty.ScrollbarGutter));
        if (ScrollbarThumbColor.HasValue || preset is not null && preset.HasValue(PresetProperty.ScrollbarThumbColor))
            AddCss(ref cls, ResolvePresetSlot(ScrollbarThumbColor, preset?.ScrollbarThumbColor, (int)PresetProperty.ScrollbarThumbColor));
        if (ScrollbarTrackColor.HasValue || preset is not null && preset.HasValue(PresetProperty.ScrollbarTrackColor))
            AddCss(ref cls, ResolvePresetSlot(ScrollbarTrackColor, preset?.ScrollbarTrackColor, (int)PresetProperty.ScrollbarTrackColor));
        if (ScrollbarWidth.HasValue || preset is not null && preset.HasValue(PresetProperty.ScrollbarWidth))
            AddCss(ref cls, ResolvePresetSlot(ScrollbarWidth, preset?.ScrollbarWidth, (int)PresetProperty.ScrollbarWidth));
        if (ScrollBehavior.HasValue || preset is not null && preset.HasValue(PresetProperty.ScrollBehavior))
            AddCss(ref cls, ResolvePresetSlot(ScrollBehavior, preset?.ScrollBehavior, (int)PresetProperty.ScrollBehavior));
        if (ScrollSnapAlign.HasValue || preset is not null && preset.HasValue(PresetProperty.ScrollSnapAlign))
            AddCss(ref cls, ResolvePresetSlot(ScrollSnapAlign, preset?.ScrollSnapAlign, (int)PresetProperty.ScrollSnapAlign));
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void BuildAdditionalUtilities6(ref PooledStringBuilder cls, QuarkPresetContext? preset)
    {
        if (ScrollSnap.HasValue || preset is not null && preset.HasValue(PresetProperty.ScrollSnap))
            AddCss(ref cls, ResolvePresetSlot(ScrollSnap, preset?.ScrollSnap, (int)PresetProperty.ScrollSnap));
        if (ScrollSnapStop.HasValue || preset is not null && preset.HasValue(PresetProperty.ScrollSnapStop))
            AddCss(ref cls, ResolvePresetSlot(ScrollSnapStop, preset?.ScrollSnapStop, (int)PresetProperty.ScrollSnapStop));
        if (ShadowColor.HasValue || preset is not null && preset.HasValue(PresetProperty.ShadowColor))
            AddCss(ref cls, ResolvePresetSlot(ShadowColor, preset?.ShadowColor, (int)PresetProperty.ShadowColor));
        if (Skew.HasValue || preset is not null && preset.HasValue(PresetProperty.Skew))
            AddCss(ref cls, ResolvePresetSlot(Skew, preset?.Skew, (int)PresetProperty.Skew));
        if (TabSize.HasValue || preset is not null && preset.HasValue(PresetProperty.TabSize))
            AddCss(ref cls, ResolvePresetSlot(TabSize, preset?.TabSize, (int)PresetProperty.TabSize));
        if (TouchAction.HasValue || preset is not null && preset.HasValue(PresetProperty.TouchAction))
            AddCss(ref cls, ResolvePresetSlot(TouchAction, preset?.TouchAction, (int)PresetProperty.TouchAction));
        if (TransitionBehavior.HasValue || preset is not null && preset.HasValue(PresetProperty.TransitionBehavior))
            AddCss(ref cls, ResolvePresetSlot(TransitionBehavior, preset?.TransitionBehavior, (int)PresetProperty.TransitionBehavior));
        if (Translate.HasValue || preset is not null && preset.HasValue(PresetProperty.Translate))
            AddCss(ref cls, ResolvePresetSlot(Translate, preset?.Translate, (int)PresetProperty.Translate));
        if (WillChange.HasValue || preset is not null && preset.HasValue(PresetProperty.WillChange))
            AddCss(ref cls, ResolvePresetSlot(WillChange, preset?.WillChange, (int)PresetProperty.WillChange));
        if (Zoom.HasValue || preset is not null && preset.HasValue(PresetProperty.Zoom))
            AddCss(ref cls, ResolvePresetSlot(Zoom, preset?.Zoom, (int)PresetProperty.Zoom));
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void BuildMaskUtilities(ref PooledStringBuilder cls, QuarkPresetContext? preset)
    {
        if (MaskClip.HasValue || preset is not null && preset.HasValue(PresetProperty.MaskClip))
            AddCss(ref cls, ResolvePresetSlot(MaskClip, preset?.MaskClip, (int)PresetProperty.MaskClip));
        if (MaskComposite.HasValue || preset is not null && preset.HasValue(PresetProperty.MaskComposite))
            AddCss(ref cls, ResolvePresetSlot(MaskComposite, preset?.MaskComposite, (int)PresetProperty.MaskComposite));
        if (MaskImage.HasValue || preset is not null && preset.HasValue(PresetProperty.MaskImage))
            AddCss(ref cls, ResolvePresetSlot(MaskImage, preset?.MaskImage, (int)PresetProperty.MaskImage));
        if (MaskMode.HasValue || preset is not null && preset.HasValue(PresetProperty.MaskMode))
            AddCss(ref cls, ResolvePresetSlot(MaskMode, preset?.MaskMode, (int)PresetProperty.MaskMode));
        if (MaskOrigin.HasValue || preset is not null && preset.HasValue(PresetProperty.MaskOrigin))
            AddCss(ref cls, ResolvePresetSlot(MaskOrigin, preset?.MaskOrigin, (int)PresetProperty.MaskOrigin));
        if (MaskPosition.HasValue || preset is not null && preset.HasValue(PresetProperty.MaskPosition))
            AddCss(ref cls, ResolvePresetSlot(MaskPosition, preset?.MaskPosition, (int)PresetProperty.MaskPosition));
        if (MaskRepeat.HasValue || preset is not null && preset.HasValue(PresetProperty.MaskRepeat))
            AddCss(ref cls, ResolvePresetSlot(MaskRepeat, preset?.MaskRepeat, (int)PresetProperty.MaskRepeat));
        if (MaskSize.HasValue || preset is not null && preset.HasValue(PresetProperty.MaskSize))
            AddCss(ref cls, ResolvePresetSlot(MaskSize, preset?.MaskSize, (int)PresetProperty.MaskSize));
        if (MaskType.HasValue || preset is not null && preset.HasValue(PresetProperty.MaskType))
            AddCss(ref cls, ResolvePresetSlot(MaskType, preset?.MaskType, (int)PresetProperty.MaskType));
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void BuildTransform3DUtilities(ref PooledStringBuilder cls, QuarkPresetContext? preset)
    {
        if (BackfaceVisibility.HasValue || preset is not null && preset.HasValue(PresetProperty.BackfaceVisibility))
            AddCss(ref cls, ResolvePresetSlot(BackfaceVisibility, preset?.BackfaceVisibility, (int)PresetProperty.BackfaceVisibility));
        if (PerspectiveOrigin.HasValue || preset is not null && preset.HasValue(PresetProperty.PerspectiveOrigin))
            AddCss(ref cls, ResolvePresetSlot(PerspectiveOrigin, preset?.PerspectiveOrigin, (int)PresetProperty.PerspectiveOrigin));
        if (Perspective.HasValue || preset is not null && preset.HasValue(PresetProperty.Perspective))
            AddCss(ref cls, ResolvePresetSlot(Perspective, preset?.Perspective, (int)PresetProperty.Perspective));
        if (TransformStyle.HasValue || preset is not null && preset.HasValue(PresetProperty.TransformStyle))
            AddCss(ref cls, ResolvePresetSlot(TransformStyle, preset?.TransformStyle, (int)PresetProperty.TransformStyle));
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void BuildAdvancedTypographyUtilities(ref PooledStringBuilder cls, QuarkPresetContext? preset)
    {
        if (FontFeatureSettings.HasValue || preset is not null && preset.HasValue(PresetProperty.FontFeatureSettings))
            AddCss(ref cls, ResolvePresetSlot(FontFeatureSettings, preset?.FontFeatureSettings, (int)PresetProperty.FontFeatureSettings));
        if (FontSmoothing.HasValue || preset is not null && preset.HasValue(PresetProperty.FontSmoothing))
            AddCss(ref cls, ResolvePresetSlot(FontSmoothing, preset?.FontSmoothing, (int)PresetProperty.FontSmoothing));
        if (FontStretch.HasValue || preset is not null && preset.HasValue(PresetProperty.FontStretch))
            AddCss(ref cls, ResolvePresetSlot(FontStretch, preset?.FontStretch, (int)PresetProperty.FontStretch));
        if (TextIndent.HasValue || preset is not null && preset.HasValue(PresetProperty.TextIndent))
            AddCss(ref cls, ResolvePresetSlot(TextIndent, preset?.TextIndent, (int)PresetProperty.TextIndent));
        if (TextShadowColor.HasValue || preset is not null && preset.HasValue(PresetProperty.TextShadowColor))
            AddCss(ref cls, ResolvePresetSlot(TextShadowColor, preset?.TextShadowColor, (int)PresetProperty.TextShadowColor));
        if (TextShadow.HasValue || preset is not null && preset.HasValue(PresetProperty.TextShadow))
            AddCss(ref cls, ResolvePresetSlot(TextShadow, preset?.TextShadow, (int)PresetProperty.TextShadow));
    }

    // Keep each group as a separate native frame. In particular, do not combine these methods
    // or allow them to inline: CssValue<T>? is a large nullable struct containing references,
    // and a single method covering every CSS property exceeds safe Mono AOT code/frame sizes.
    [MethodImpl(MethodImplOptions.NoInlining)]
    private void BuildTypographyClassAndStyle(ref PooledStringBuilder sty, ref PooledStringBuilder cls, QuarkPresetContext? preset)
    {
        if (Display.HasValue || preset is not null && preset.HasValue(PresetProperty.Display))
            AddCss(ref cls, ResolvePresetSlot(Display, preset?.Display, (int)PresetProperty.Display));
        if (Visibility.HasValue || preset is not null && preset.HasValue(PresetProperty.Visibility))
            AddCss(ref cls, ResolvePresetSlot(Visibility, preset?.Visibility, (int)PresetProperty.Visibility));
        if (Float.HasValue || preset is not null && preset.HasValue(PresetProperty.Float))
            AddCss(ref cls, ResolvePresetSlot(Float, preset?.Float, (int)PresetProperty.Float));
        if (VerticalAlign.HasValue || preset is not null && preset.HasValue(PresetProperty.VerticalAlign))
            AddCss(ref cls, ResolvePresetSlot(VerticalAlign, preset?.VerticalAlign, (int)PresetProperty.VerticalAlign));
        if (TextAlign.HasValue || preset is not null && preset.HasValue(PresetProperty.TextAlign))
            AddCss(ref cls, ResolvePresetSlot(TextAlign, preset?.TextAlign, (int)PresetProperty.TextAlign));
        ApplyTextColor(ref sty, ref cls, preset?.TextColor);
        if (TextSize.HasValue || preset is not null && preset.HasValue(PresetProperty.TextSize))
            AddCss(ref cls, ResolvePresetSlot(TextSize, preset?.TextSize, (int)PresetProperty.TextSize));
        if (DecorationLine.HasValue || preset is not null && preset.HasValue(PresetProperty.DecorationLine))
            AddCss(ref cls, ResolvePresetSlot(DecorationLine, preset?.DecorationLine, (int)PresetProperty.DecorationLine));
        if (UnderlineOffset.HasValue || preset is not null && preset.HasValue(PresetProperty.UnderlineOffset))
            AddCss(ref cls, ResolvePresetSlot(UnderlineOffset, preset?.UnderlineOffset, (int)PresetProperty.UnderlineOffset));
        if (TextTransform.HasValue || preset is not null && preset.HasValue(PresetProperty.TextTransform))
            AddCss(ref cls, ResolvePresetSlot(TextTransform, preset?.TextTransform, (int)PresetProperty.TextTransform));
        if (FontFamily.HasValue || preset is not null && preset.HasValue(PresetProperty.FontFamily))
            AddCss(ref cls, ResolvePresetSlot(FontFamily, preset?.FontFamily, (int)PresetProperty.FontFamily));
        if (FontWeight.HasValue || preset is not null && preset.HasValue(PresetProperty.FontWeight))
            AddCss(ref cls, ResolvePresetSlot(FontWeight, preset?.FontWeight, (int)PresetProperty.FontWeight));
        if (FontStyle.HasValue || preset is not null && preset.HasValue(PresetProperty.FontStyle))
            AddCss(ref cls, ResolvePresetSlot(FontStyle, preset?.FontStyle, (int)PresetProperty.FontStyle));
        if (Leading.HasValue || preset is not null && preset.HasValue(PresetProperty.Leading))
            AddCss(ref cls, ResolvePresetSlot(Leading, preset?.Leading, (int)PresetProperty.Leading));
        if (Tracking.HasValue || preset is not null && preset.HasValue(PresetProperty.Tracking))
            AddCss(ref cls, ResolvePresetSlot(Tracking, preset?.Tracking, (int)PresetProperty.Tracking));
        if (Whitespace.HasValue || preset is not null && preset.HasValue(PresetProperty.Whitespace))
            AddCss(ref cls, ResolvePresetSlot(Whitespace, preset?.Whitespace, (int)PresetProperty.Whitespace));
        if (TextWrap.HasValue || preset is not null && preset.HasValue(PresetProperty.TextWrap))
            AddCss(ref cls, ResolvePresetSlot(TextWrap, preset?.TextWrap, (int)PresetProperty.TextWrap));
        if (WordBreak.HasValue || preset is not null && preset.HasValue(PresetProperty.WordBreak))
            AddCss(ref cls, ResolvePresetSlot(WordBreak, preset?.WordBreak, (int)PresetProperty.WordBreak));
        if (TextOverflow.HasValue || preset is not null && preset.HasValue(PresetProperty.TextOverflow))
            AddCss(ref cls, ResolvePresetSlot(TextOverflow, preset?.TextOverflow, (int)PresetProperty.TextOverflow));
        if (Truncate.HasValue || preset is not null && preset.HasValue(PresetProperty.Truncate))
            AddCss(ref cls, ResolvePresetSlot(Truncate, preset?.Truncate, (int)PresetProperty.Truncate));
        if (LineClamp.HasValue || preset is not null && preset.HasValue(PresetProperty.LineClamp))
            AddCss(ref cls, ResolvePresetSlot(LineClamp, preset?.LineClamp, (int)PresetProperty.LineClamp));
        if (FontVariantNumeric.HasValue || preset is not null && preset.HasValue(PresetProperty.FontVariantNumeric))
            AddCss(ref cls, ResolvePresetSlot(FontVariantNumeric, preset?.FontVariantNumeric, (int)PresetProperty.FontVariantNumeric));
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void BuildLayoutClassAndStyle(ref PooledStringBuilder sty, ref PooledStringBuilder cls, QuarkPresetContext? preset)
    {
        if (Margin.HasValue || preset is not null && preset.HasValue(PresetProperty.Margin))
            AddCss(ref cls, ResolvePresetSlot(Margin, preset?.Margin, (int)PresetProperty.Margin));
        if (Padding.HasValue || preset is not null && preset.HasValue(PresetProperty.Padding))
            AddCss(ref cls, ResolvePresetSlot(Padding, preset?.Padding, (int)PresetProperty.Padding));
        if (Inset.HasValue || preset is not null && preset.HasValue(PresetProperty.Inset))
            AddCss(ref cls, ResolvePresetSlot(Inset, preset?.Inset, (int)PresetProperty.Inset));
        if (Top.HasValue || preset is not null && preset.HasValue(PresetProperty.Top))
            AddCss(ref cls, ResolvePresetSlot(Top, preset?.Top, (int)PresetProperty.Top));
        if (Right.HasValue || preset is not null && preset.HasValue(PresetProperty.Right))
            AddCss(ref cls, ResolvePresetSlot(Right, preset?.Right, (int)PresetProperty.Right));
        if (Bottom.HasValue || preset is not null && preset.HasValue(PresetProperty.Bottom))
            AddCss(ref cls, ResolvePresetSlot(Bottom, preset?.Bottom, (int)PresetProperty.Bottom));
        if (Left.HasValue || preset is not null && preset.HasValue(PresetProperty.Left))
            AddCss(ref cls, ResolvePresetSlot(Left, preset?.Left, (int)PresetProperty.Left));
        if (Position.HasValue || preset is not null && preset.HasValue(PresetProperty.Position))
            AddCss(ref cls, ResolvePresetSlot(Position, preset?.Position, (int)PresetProperty.Position));
        if (ScrollMargin.HasValue || preset is not null && preset.HasValue(PresetProperty.ScrollMargin))
            AddCss(ref cls, ResolvePresetSlot(ScrollMargin, preset?.ScrollMargin, (int)PresetProperty.ScrollMargin));
        if (ScrollPadding.HasValue || preset is not null && preset.HasValue(PresetProperty.ScrollPadding))
            AddCss(ref cls, ResolvePresetSlot(ScrollPadding, preset?.ScrollPadding, (int)PresetProperty.ScrollPadding));
        if (Size.HasValue || preset is not null && preset.HasValue(PresetProperty.Size))
            AddCss(ref cls, ResolvePresetSlot(Size, preset?.Size, (int)PresetProperty.Size));
        if (Width.HasValue || preset is not null && preset.HasValue(PresetProperty.Width))
            AddCss(ref cls, ResolvePresetSlot(Width, preset?.Width, (int)PresetProperty.Width));
        if (MinWidth.HasValue || preset is not null && preset.HasValue(PresetProperty.MinWidth))
            AddCss(ref cls, ResolvePresetSlot(MinWidth, preset?.MinWidth, (int)PresetProperty.MinWidth));
        if (MaxWidth.HasValue || preset is not null && preset.HasValue(PresetProperty.MaxWidth))
            AddCss(ref cls, ResolvePresetSlot(MaxWidth, preset?.MaxWidth, (int)PresetProperty.MaxWidth));
        if (Height.HasValue || preset is not null && preset.HasValue(PresetProperty.Height))
            AddCss(ref cls, ResolvePresetSlot(Height, preset?.Height, (int)PresetProperty.Height));
        if (MinHeight.HasValue || preset is not null && preset.HasValue(PresetProperty.MinHeight))
            AddCss(ref cls, ResolvePresetSlot(MinHeight, preset?.MinHeight, (int)PresetProperty.MinHeight));
        if (MaxHeight.HasValue || preset is not null && preset.HasValue(PresetProperty.MaxHeight))
            AddCss(ref cls, ResolvePresetSlot(MaxHeight, preset?.MaxHeight, (int)PresetProperty.MaxHeight));
        if (Overflow.HasValue || preset is not null && preset.HasValue(PresetProperty.Overflow))
            AddCss(ref cls, ResolvePresetSlot(Overflow, preset?.Overflow, (int)PresetProperty.Overflow));
        if (OverflowX.HasValue || preset is not null && preset.HasValue(PresetProperty.OverflowX))
            AddCss(ref cls, ResolvePresetSlot(OverflowX, preset?.OverflowX, (int)PresetProperty.OverflowX));
        if (OverflowY.HasValue || preset is not null && preset.HasValue(PresetProperty.OverflowY))
            AddCss(ref cls, ResolvePresetSlot(OverflowY, preset?.OverflowY, (int)PresetProperty.OverflowY));
        if (Overscroll.HasValue || preset is not null && preset.HasValue(PresetProperty.Overscroll))
            AddCss(ref cls, ResolvePresetSlot(Overscroll, preset?.Overscroll, (int)PresetProperty.Overscroll));
        if (Flex.HasValue || preset is not null && preset.HasValue(PresetProperty.Flex))
            AddCss(ref cls, ResolvePresetSlot(Flex, preset?.Flex, (int)PresetProperty.Flex));
        if (FlexDirection.HasValue || preset is not null && preset.HasValue(PresetProperty.FlexDirection))
            AddCss(ref cls, ResolvePresetSlot(FlexDirection, preset?.FlexDirection, (int)PresetProperty.FlexDirection));
        if (FlexWrap.HasValue || preset is not null && preset.HasValue(PresetProperty.FlexWrap))
            AddCss(ref cls, ResolvePresetSlot(FlexWrap, preset?.FlexWrap, (int)PresetProperty.FlexWrap));
        if (Grow.HasValue || preset is not null && preset.HasValue(PresetProperty.Grow))
            AddCss(ref cls, ResolvePresetSlot(Grow, preset?.Grow, (int)PresetProperty.Grow));
        if (Shrink.HasValue || preset is not null && preset.HasValue(PresetProperty.Shrink))
            AddCss(ref cls, ResolvePresetSlot(Shrink, preset?.Shrink, (int)PresetProperty.Shrink));
        if (Gap.HasValue || preset is not null && preset.HasValue(PresetProperty.Gap))
            AddCss(ref cls, ResolvePresetSlot(Gap, preset?.Gap, (int)PresetProperty.Gap));
        if (Space.HasValue || preset is not null && preset.HasValue(PresetProperty.Space))
            AddCss(ref cls, ResolvePresetSlot(Space, preset?.Space, (int)PresetProperty.Space));
        if (Divide.HasValue || preset is not null && preset.HasValue(PresetProperty.Divide))
            AddCss(ref cls, ResolvePresetSlot(Divide, preset?.Divide, (int)PresetProperty.Divide));
        if (ContentAlign.HasValue || preset is not null && preset.HasValue(PresetProperty.ContentAlign))
            AddCss(ref cls, ResolvePresetSlot(ContentAlign, preset?.ContentAlign, (int)PresetProperty.ContentAlign));
        if (ItemsAlign.HasValue || preset is not null && preset.HasValue(PresetProperty.ItemsAlign))
            AddCss(ref cls, ResolvePresetSlot(ItemsAlign, preset?.ItemsAlign, (int)PresetProperty.ItemsAlign));
        if (Justify.HasValue || preset is not null && preset.HasValue(PresetProperty.Justify))
            AddCss(ref cls, ResolvePresetSlot(Justify, preset?.Justify, (int)PresetProperty.Justify));
        if (SelfAlign.HasValue || preset is not null && preset.HasValue(PresetProperty.SelfAlign))
            AddCss(ref cls, ResolvePresetSlot(SelfAlign, preset?.SelfAlign, (int)PresetProperty.SelfAlign));
        if (JustifyItemsAlign.HasValue || preset is not null && preset.HasValue(PresetProperty.JustifyItemsAlign))
            AddCss(ref cls, ResolvePresetSlot(JustifyItemsAlign, preset?.JustifyItemsAlign, (int)PresetProperty.JustifyItemsAlign));
        if (JustifySelfAlign.HasValue || preset is not null && preset.HasValue(PresetProperty.JustifySelfAlign))
            AddCss(ref cls, ResolvePresetSlot(JustifySelfAlign, preset?.JustifySelfAlign, (int)PresetProperty.JustifySelfAlign));
        if (ColStart.HasValue || preset is not null && preset.HasValue(PresetProperty.ColStart))
            AddCss(ref cls, ResolvePresetSlot(ColStart, preset?.ColStart, (int)PresetProperty.ColStart));
        if (RowSpan.HasValue || preset is not null && preset.HasValue(PresetProperty.RowSpan))
            AddCss(ref cls, ResolvePresetSlot(RowSpan, preset?.RowSpan, (int)PresetProperty.RowSpan));
        if (RowStart.HasValue || preset is not null && preset.HasValue(PresetProperty.RowStart))
            AddCss(ref cls, ResolvePresetSlot(RowStart, preset?.RowStart, (int)PresetProperty.RowStart));
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void BuildInteractionClassAndStyle(ref PooledStringBuilder sty, ref PooledStringBuilder cls, QuarkPresetContext? preset)
    {
        if (Opacity.HasValue || preset is not null && preset.HasValue(PresetProperty.Opacity))
            AddCss(ref cls, ResolvePresetSlot(Opacity, preset?.Opacity, (int)PresetProperty.Opacity));
        if (ZIndex.HasValue || preset is not null && preset.HasValue(PresetProperty.ZIndex))
            AddCss(ref cls, ResolvePresetSlot(ZIndex, preset?.ZIndex, (int)PresetProperty.ZIndex));
        if (PointerEvents.HasValue || preset is not null && preset.HasValue(PresetProperty.PointerEvents))
            AddCss(ref cls, ResolvePresetSlot(PointerEvents, preset?.PointerEvents, (int)PresetProperty.PointerEvents));
        if (UserSelect.HasValue || preset is not null && preset.HasValue(PresetProperty.UserSelect))
            AddCss(ref cls, ResolvePresetSlot(UserSelect, preset?.UserSelect, (int)PresetProperty.UserSelect));
        if (Cursor.HasValue || preset is not null && preset.HasValue(PresetProperty.Cursor))
            AddCss(ref cls, ResolvePresetSlot(Cursor, preset?.Cursor, (int)PresetProperty.Cursor));
        if (ScreenReader.HasValue || preset is not null && preset.HasValue(PresetProperty.ScreenReader))
            AddCss(ref cls, ResolvePresetSlot(ScreenReader, preset?.ScreenReader, (int)PresetProperty.ScreenReader));
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void BuildVisualClassAndStyle(ref PooledStringBuilder sty, ref PooledStringBuilder cls, QuarkPresetContext? preset)
    {
        if (Border.HasValue || preset is not null && preset.HasValue(PresetProperty.Border))
            AddCss(ref cls, ResolvePresetSlot(Border, preset?.Border, (int)PresetProperty.Border));
        if (BorderStyle.HasValue || preset is not null && preset.HasValue(PresetProperty.BorderStyle))
            AddCss(ref cls, ResolvePresetSlot(BorderStyle, preset?.BorderStyle, (int)PresetProperty.BorderStyle));
        ApplyBorderColor(ref sty, ref cls, preset?.BorderColor);
        ApplyBackgroundColor(ref sty, ref cls, preset?.BackgroundColor);
        if (Rounded.HasValue || preset is not null && preset.HasValue(PresetProperty.Rounded))
            AddCss(ref cls, ResolvePresetSlot(Rounded, preset?.Rounded, (int)PresetProperty.Rounded));
        if (RingColor.HasValue || preset is not null && preset.HasValue(PresetProperty.RingColor))
            AddCss(ref cls, ResolvePresetSlot(RingColor, preset?.RingColor, (int)PresetProperty.RingColor));
        if (Ring.HasValue || preset is not null && preset.HasValue(PresetProperty.Ring))
            AddCss(ref cls, ResolvePresetSlot(Ring, preset?.Ring, (int)PresetProperty.Ring));
        if (RingOffset.HasValue || preset is not null && preset.HasValue(PresetProperty.RingOffset))
            AddCss(ref cls, ResolvePresetSlot(RingOffset, preset?.RingOffset, (int)PresetProperty.RingOffset));
        if (OutlineStyle.HasValue || preset is not null && preset.HasValue(PresetProperty.OutlineStyle))
            AddCss(ref cls, ResolvePresetSlot(OutlineStyle, preset?.OutlineStyle, (int)PresetProperty.OutlineStyle));
        if (Shadow.HasValue || preset is not null && preset.HasValue(PresetProperty.Shadow))
            AddCss(ref cls, ResolvePresetSlot(Shadow, preset?.Shadow, (int)PresetProperty.Shadow));
        if (BackdropFilter.HasValue || preset is not null && preset.HasValue(PresetProperty.BackdropFilter))
            AddCss(ref cls, ResolvePresetSlot(BackdropFilter, preset?.BackdropFilter, (int)PresetProperty.BackdropFilter));
        if (BackdropBlur.HasValue || preset is not null && preset.HasValue(PresetProperty.BackdropBlur))
            AddCss(ref cls, ResolvePresetSlot(BackdropBlur, preset?.BackdropBlur, (int)PresetProperty.BackdropBlur));
        if (BackdropBrightness.HasValue || preset is not null && preset.HasValue(PresetProperty.BackdropBrightness))
            AddCss(ref cls, ResolvePresetSlot(BackdropBrightness, preset?.BackdropBrightness, (int)PresetProperty.BackdropBrightness));
        if (BackdropContrast.HasValue || preset is not null && preset.HasValue(PresetProperty.BackdropContrast))
            AddCss(ref cls, ResolvePresetSlot(BackdropContrast, preset?.BackdropContrast, (int)PresetProperty.BackdropContrast));
        if (BackdropGrayscale.HasValue || preset is not null && preset.HasValue(PresetProperty.BackdropGrayscale))
            AddCss(ref cls, ResolvePresetSlot(BackdropGrayscale, preset?.BackdropGrayscale, (int)PresetProperty.BackdropGrayscale));
        if (BackdropHueRotate.HasValue || preset is not null && preset.HasValue(PresetProperty.BackdropHueRotate))
            AddCss(ref cls, ResolvePresetSlot(BackdropHueRotate, preset?.BackdropHueRotate, (int)PresetProperty.BackdropHueRotate));
        if (BackdropInvert.HasValue || preset is not null && preset.HasValue(PresetProperty.BackdropInvert))
            AddCss(ref cls, ResolvePresetSlot(BackdropInvert, preset?.BackdropInvert, (int)PresetProperty.BackdropInvert));
        if (BackdropOpacity.HasValue || preset is not null && preset.HasValue(PresetProperty.BackdropOpacity))
            AddCss(ref cls, ResolvePresetSlot(BackdropOpacity, preset?.BackdropOpacity, (int)PresetProperty.BackdropOpacity));
        if (BackdropSaturate.HasValue || preset is not null && preset.HasValue(PresetProperty.BackdropSaturate))
            AddCss(ref cls, ResolvePresetSlot(BackdropSaturate, preset?.BackdropSaturate, (int)PresetProperty.BackdropSaturate));
        if (BackdropSepia.HasValue || preset is not null && preset.HasValue(PresetProperty.BackdropSepia))
            AddCss(ref cls, ResolvePresetSlot(BackdropSepia, preset?.BackdropSepia, (int)PresetProperty.BackdropSepia));
        if (Filter.HasValue || preset is not null && preset.HasValue(PresetProperty.Filter))
            AddCss(ref cls, ResolvePresetSlot(Filter, preset?.Filter, (int)PresetProperty.Filter));
        if (Blur.HasValue || preset is not null && preset.HasValue(PresetProperty.Blur))
            AddCss(ref cls, ResolvePresetSlot(Blur, preset?.Blur, (int)PresetProperty.Blur));
        if (Brightness.HasValue || preset is not null && preset.HasValue(PresetProperty.Brightness))
            AddCss(ref cls, ResolvePresetSlot(Brightness, preset?.Brightness, (int)PresetProperty.Brightness));
        if (Contrast.HasValue || preset is not null && preset.HasValue(PresetProperty.Contrast))
            AddCss(ref cls, ResolvePresetSlot(Contrast, preset?.Contrast, (int)PresetProperty.Contrast));
        if (DropShadow.HasValue || preset is not null && preset.HasValue(PresetProperty.DropShadow))
            AddCss(ref cls, ResolvePresetSlot(DropShadow, preset?.DropShadow, (int)PresetProperty.DropShadow));
        if (DropShadowColor.HasValue || preset is not null && preset.HasValue(PresetProperty.DropShadowColor))
            AddCss(ref cls, ResolvePresetSlot(DropShadowColor, preset?.DropShadowColor, (int)PresetProperty.DropShadowColor));
        if (Grayscale.HasValue || preset is not null && preset.HasValue(PresetProperty.Grayscale))
            AddCss(ref cls, ResolvePresetSlot(Grayscale, preset?.Grayscale, (int)PresetProperty.Grayscale));
        if (HueRotate.HasValue || preset is not null && preset.HasValue(PresetProperty.HueRotate))
            AddCss(ref cls, ResolvePresetSlot(HueRotate, preset?.HueRotate, (int)PresetProperty.HueRotate));
        if (Invert.HasValue || preset is not null && preset.HasValue(PresetProperty.Invert))
            AddCss(ref cls, ResolvePresetSlot(Invert, preset?.Invert, (int)PresetProperty.Invert));
        if (Saturate.HasValue || preset is not null && preset.HasValue(PresetProperty.Saturate))
            AddCss(ref cls, ResolvePresetSlot(Saturate, preset?.Saturate, (int)PresetProperty.Saturate));
        if (Sepia.HasValue || preset is not null && preset.HasValue(PresetProperty.Sepia))
            AddCss(ref cls, ResolvePresetSlot(Sepia, preset?.Sepia, (int)PresetProperty.Sepia));
        if (Resize.HasValue || preset is not null && preset.HasValue(PresetProperty.Resize))
            AddCss(ref cls, ResolvePresetSlot(Resize, preset?.Resize, (int)PresetProperty.Resize));
        if (Transform.HasValue || preset is not null && preset.HasValue(PresetProperty.Transform))
            AddCss(ref cls, ResolvePresetSlot(Transform, preset?.Transform, (int)PresetProperty.Transform));
        if (Animation.HasValue || preset is not null && preset.HasValue(PresetProperty.Animation))
            AddCss(ref cls, ResolvePresetSlot(Animation, preset?.Animation, (int)PresetProperty.Animation));
        if (Duration.HasValue || preset is not null && preset.HasValue(PresetProperty.Duration))
            AddCss(ref cls, ResolvePresetSlot(Duration, preset?.Duration, (int)PresetProperty.Duration));
        if (Transition.HasValue || preset is not null && preset.HasValue(PresetProperty.Transition))
            AddCss(ref cls, ResolvePresetSlot(Transition, preset?.Transition, (int)PresetProperty.Transition));
    }

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            _ = OnElementRefReady.InvokeIfHasDelegate(ElementRef);

        return Task.CompletedTask;
    }

    private PresetEvaluationState? _presetState;

    protected QuarkPresetContext? BuildPresetContext()
    {
        var preset = Preset;
        var presets = Presets;

        if (preset is null && (presets is null || presets.Count == 0))
            return null;

        return (_presetState ??= new PresetEvaluationState()).Evaluate(preset, presets);
    }

    private bool HasExplicitDataSlotAttribute()
    {
        return HasDataSlotAttribute(AdditionalAttributes) || HasDataSlotAttribute(Attributes);
    }

    private static bool HasDataSlotAttribute(IReadOnlyDictionary<string, object>? attributes)
    {
        if (attributes is null)
            return false;

        if (attributes is Dictionary<string, object> dictionary)
        {
            if (dictionary.ContainsKey("data-slot"))
                return true;

            foreach (var key in dictionary.Keys)
            {
                if (key.Equals("data-slot", StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        foreach (var key in attributes.Keys)
        {
            if (key.Equals("data-slot", StringComparison.OrdinalIgnoreCase))
                return true;
        }

        return false;
    }

    protected override void ApplyBorderColor(ref PooledStringBuilder sty, ref PooledStringBuilder cls, CssValue<BorderColorBuilder>? value)
    {
        base.ApplyBorderColor(ref sty, ref cls, ResolvePresetSlot(BorderColor, value, (int)PresetProperty.BorderColor));
    }

    protected override void ApplyBackgroundColor(ref PooledStringBuilder sty, ref PooledStringBuilder cls, CssValue<BackgroundColorBuilder>? value)
    {
        base.ApplyBackgroundColor(ref sty, ref cls, ResolvePresetSlot(BackgroundColor, value, (int)PresetProperty.BackgroundColor));
    }

    protected override void ApplyTextColor(ref PooledStringBuilder sty, ref PooledStringBuilder cls, CssValue<TextColorBuilder>? value)
    {
        base.ApplyTextColor(ref sty, ref cls, ResolvePresetSlot(TextColor, value, (int)PresetProperty.TextColor));
    }

    protected new void BuildClassAttribute(Dictionary<string, object> attrs, BuildClassAction builder)
    {
        var cls = new PooledStringBuilder(stackalloc char[64]);
        try
        {
            builder(ref cls);
            attrs.TryGetValue("class", out var existing);
            var existingString = existing?.ToString();
            if (cls.Length == 0)
            {
                if (!string.IsNullOrEmpty(existingString))
                    attrs["class"] = existingString;
                return;
            }

            if (!string.IsNullOrEmpty(existingString))
                AppendClass(ref cls, existingString);
            attrs["class"] = cls.ToString();
        }
        finally
        {
            cls.Dispose();
        }
    }

    protected override void ComputeRenderKeyCore(ref HashCode hc)
    {
        base.ComputeRenderKeyCore(ref hc);
        if (_advancedTypographyUtilities is not null)
        {
            AddIf(ref hc, FontFeatureSettings);
            AddIf(ref hc, FontSmoothing);
            AddIf(ref hc, FontStretch);
            AddIf(ref hc, TextIndent);
            AddIf(ref hc, TextShadowColor);
            AddIf(ref hc, TextShadow);
        }

        if (_transform3DUtilities is not null)
        {
            AddIf(ref hc, BackfaceVisibility);
            AddIf(ref hc, PerspectiveOrigin);
            AddIf(ref hc, Perspective);
            AddIf(ref hc, TransformStyle);
        }

        if (_maskUtilities is not null)
        {
            AddIf(ref hc, MaskClip);
            AddIf(ref hc, MaskComposite);
            AddIf(ref hc, MaskImage);
            AddIf(ref hc, MaskMode);
            AddIf(ref hc, MaskOrigin);
            AddIf(ref hc, MaskPosition);
            AddIf(ref hc, MaskRepeat);
            AddIf(ref hc, MaskSize);
            AddIf(ref hc, MaskType);
        }

        hc.Add(Container);
        hc.Add(Preset);

        if (Presets is { Count: > 0 })
        {
            for (var i = 0; i < Presets.Count; i++)
            {
                hc.Add(Presets[i]);
            }
        }

        hc.Add(Class);
        hc.Add(Style);
        hc.Add(Title);
        hc.Add(Hidden);
        hc.Add(DataSlot);
        AddIf(ref hc, Display);
        AddIf(ref hc, Visibility);
        AddIf(ref hc, Float);
        AddIf(ref hc, VerticalAlign);
        AddIf(ref hc, TextAlign);
        AddIf(ref hc, TextColor);
        AddIf(ref hc, TextSize);
        AddIf(ref hc, DecorationLine);
        AddIf(ref hc, UnderlineOffset);
        AddIf(ref hc, TextTransform);
        AddIf(ref hc, FontFamily);
        AddIf(ref hc, FontWeight);
        AddIf(ref hc, FontStyle);
        AddIf(ref hc, Leading);
        AddIf(ref hc, Tracking);
        AddIf(ref hc, Whitespace);
        AddIf(ref hc, TextWrap);
        AddIf(ref hc, WordBreak);
        AddIf(ref hc, TextOverflow);
        AddIf(ref hc, Truncate);
        AddIf(ref hc, LineClamp);
        AddIf(ref hc, FontVariantNumeric);
        AddIf(ref hc, Margin);
        AddIf(ref hc, Padding);
        AddIf(ref hc, AspectRatio);
        AddIf(ref hc, BackgroundAttachment);
        AddIf(ref hc, BackgroundBlendMode);
        AddIf(ref hc, BackgroundClip);
        AddIf(ref hc, BackgroundImage);
        AddIf(ref hc, BackgroundOrigin);
        AddIf(ref hc, BackgroundPosition);
        AddIf(ref hc, BackgroundRepeat);
        AddIf(ref hc, BackgroundSize);
        AddIf(ref hc, BlockSize);
        AddIf(ref hc, BoxDecorationBreak);
        AddIf(ref hc, BoxSizing);
        AddIf(ref hc, BreakAfter);
        AddIf(ref hc, BreakBefore);
        AddIf(ref hc, BreakInside);
        AddIf(ref hc, FloatClear);
        AddIf(ref hc, ClipPath);
        AddIf(ref hc, ColEnd);
        AddIf(ref hc, ColorScheme);
        AddIf(ref hc, ColumnCount);
        AddIf(ref hc, ContainerQuery);
        AddIf(ref hc, Contain);
        AddIf(ref hc, GeneratedContent);
        AddIf(ref hc, DecorationColor);
        AddIf(ref hc, DecorationStyle);
        AddIf(ref hc, DecorationThickness);
        AddIf(ref hc, TransitionDelay);
        AddIf(ref hc, Ease);
        AddIf(ref hc, FlexBasis);
        AddIf(ref hc, ForcedColorAdjust);
        AddIf(ref hc, BackgroundGradient);
        AddIf(ref hc, ColumnSpan);
        AddIf(ref hc, Hyphen);
        AddIf(ref hc, InlineSize);
        AddIf(ref hc, InsetBlockEnd);
        AddIf(ref hc, InsetBlockStart);
        AddIf(ref hc, InsetEnd);
        AddIf(ref hc, InsetRingColor);
        AddIf(ref hc, InsetRing);
        AddIf(ref hc, InsetShadowColor);
        AddIf(ref hc, InsetShadow);
        AddIf(ref hc, InsetStart);
        AddIf(ref hc, Isolation);
        AddIf(ref hc, MaxBlockSize);
        AddIf(ref hc, MaxInlineSize);
        AddIf(ref hc, MinBlockSize);
        AddIf(ref hc, MinInlineSize);
        AddIf(ref hc, MixBlendMode);
        AddIf(ref hc, Order);
        AddIf(ref hc, Origin);
        AddIf(ref hc, OutlineColor);
        AddIf(ref hc, OutlineOffset);
        AddIf(ref hc, OutlineWidth);
        AddIf(ref hc, OverflowWrap);
        AddIf(ref hc, PlaceContentAlign);
        AddIf(ref hc, PlaceItemsAlign);
        AddIf(ref hc, PlaceSelfAlign);
        AddIf(ref hc, Rotate);
        AddIf(ref hc, RowEnd);
        AddIf(ref hc, Scale);
        AddIf(ref hc, ScrollbarGutter);
        AddIf(ref hc, ScrollbarThumbColor);
        AddIf(ref hc, ScrollbarTrackColor);
        AddIf(ref hc, ScrollbarWidth);
        AddIf(ref hc, ScrollBehavior);
        AddIf(ref hc, ScrollSnapAlign);
        AddIf(ref hc, ScrollSnap);
        AddIf(ref hc, ScrollSnapStop);
        AddIf(ref hc, ShadowColor);
        AddIf(ref hc, Skew);
        AddIf(ref hc, TabSize);
        AddIf(ref hc, TouchAction);
        AddIf(ref hc, TransitionBehavior);
        AddIf(ref hc, Translate);
        AddIf(ref hc, WillChange);
        AddIf(ref hc, Zoom);
        AddIf(ref hc, Inset);
        AddIf(ref hc, Top);
        AddIf(ref hc, Right);
        AddIf(ref hc, Bottom);
        AddIf(ref hc, Left);
        AddIf(ref hc, Position);
        AddIf(ref hc, ScrollMargin);
        AddIf(ref hc, ScrollPadding);
        AddIf(ref hc, Size);
        AddIf(ref hc, Width);
        AddIf(ref hc, MinWidth);
        AddIf(ref hc, MaxWidth);
        AddIf(ref hc, Height);
        AddIf(ref hc, MinHeight);
        AddIf(ref hc, MaxHeight);
        AddIf(ref hc, Overflow);
        AddIf(ref hc, OverflowX);
        AddIf(ref hc, OverflowY);
        AddIf(ref hc, Overscroll);
        AddIf(ref hc, Flex);
        AddIf(ref hc, FlexDirection);
        AddIf(ref hc, FlexWrap);
        AddIf(ref hc, Grow);
        AddIf(ref hc, Shrink);
        AddIf(ref hc, Gap);
        AddIf(ref hc, Space);
        AddIf(ref hc, Divide);
        AddIf(ref hc, ContentAlign);
        AddIf(ref hc, ItemsAlign);
        AddIf(ref hc, Justify);
        AddIf(ref hc, SelfAlign);
        AddIf(ref hc, JustifyItemsAlign);
        AddIf(ref hc, JustifySelfAlign);
        AddIf(ref hc, ColStart);
        AddIf(ref hc, RowSpan);
        AddIf(ref hc, RowStart);
        AddIf(ref hc, Opacity);
        AddIf(ref hc, ZIndex);
        AddIf(ref hc, PointerEvents);
        AddIf(ref hc, UserSelect);
        AddIf(ref hc, Cursor);
        AddIf(ref hc, ScreenReader);
        AddIf(ref hc, BackgroundColor);
        AddIf(ref hc, Border);
        AddIf(ref hc, BorderStyle);
        AddIf(ref hc, BorderColor);
        AddIf(ref hc, Rounded);
        AddIf(ref hc, RingColor);
        AddIf(ref hc, Ring);
        AddIf(ref hc, RingOffset);
        AddIf(ref hc, OutlineStyle);
        AddIf(ref hc, Shadow);
        AddIf(ref hc, BackdropFilter);
        AddIf(ref hc, BackdropBlur);
        AddIf(ref hc, BackdropBrightness);
        AddIf(ref hc, BackdropContrast);
        AddIf(ref hc, BackdropGrayscale);
        AddIf(ref hc, BackdropHueRotate);
        AddIf(ref hc, BackdropInvert);
        AddIf(ref hc, BackdropOpacity);
        AddIf(ref hc, BackdropSaturate);
        AddIf(ref hc, BackdropSepia);
        AddIf(ref hc, Filter);
        AddIf(ref hc, Blur);
        AddIf(ref hc, Brightness);
        AddIf(ref hc, Contrast);
        AddIf(ref hc, DropShadow);
        AddIf(ref hc, DropShadowColor);
        AddIf(ref hc, Grayscale);
        AddIf(ref hc, HueRotate);
        AddIf(ref hc, Invert);
        AddIf(ref hc, Saturate);
        AddIf(ref hc, Sepia);
        AddIf(ref hc, Resize);
        AddIf(ref hc, Transform);
        AddIf(ref hc, Animation);
        AddIf(ref hc, Duration);
        AddIf(ref hc, Transition);
    }
}
