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
    private ulong _explicitParameters0;
    private ulong _explicitParameters1;

    [Inject]
    protected ILogger<Component> Logger { get; set; } = null!;

    [Inject]
    protected QuarkOptions QuarkOptions { get; set; } = null!;

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
    public CssValue<TextBreakBuilder>? TextBreak { get; set; }

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
            _explicitParameters0 = _explicitParameters1 = 0;
            return base.SetParametersAsync(parameters);
        }

        _explicitParameters0 = _explicitParameters1 = 0;

        foreach (var parameter in parameters)
        {
            int slot = GetPresetParameterSlot(parameter.Name);
            if (slot >= 0)
            {
                if (slot < 64) _explicitParameters0 |= 1UL << slot;
                else _explicitParameters1 |= 1UL << (slot - 64);
            }
        }

        return base.SetParametersAsync(parameters);
    }

    protected bool HasExplicitParameter(string parameterName)
    {
        int slot = GetPresetParameterSlot(parameterName);
        return HasExplicitPresetSlot(slot);
    }

    private bool HasExplicitPresetSlot(int slot) => slot >= 0 && (slot < 64
        ? (_explicitParameters0 & (1UL << slot)) != 0
        : (_explicitParameters1 & (1UL << (slot - 64))) != 0);

    private static int GetPresetParameterSlot(string name) => name switch
    {
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
        nameof(TextBreak) => (int)PresetProperty.TextBreak,
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
        if (TextBreak.HasValue || preset is not null && preset.HasValue(PresetProperty.TextBreak))
            AddCss(ref cls, ResolvePresetSlot(TextBreak, preset?.TextBreak, (int)PresetProperty.TextBreak));
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
        AddCss(ref cls, OutlineStyle);
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
        AddIf(ref hc, TextBreak);
        AddIf(ref hc, TextOverflow);
        AddIf(ref hc, Truncate);
        AddIf(ref hc, LineClamp);
        AddIf(ref hc, FontVariantNumeric);
        AddIf(ref hc, Margin);
        AddIf(ref hc, Padding);
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
