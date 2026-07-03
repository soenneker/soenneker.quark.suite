using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Soenneker.Blazor.Extensions.EventCallback;
using Soenneker.Extensions.String;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

///<inheritdoc cref="IComponent"/>
///<remarks>Do not use the <c>new</c> keyword to Shadow inherited <see cref="ParameterAttribute"/> members. Blazor treats those names as duplicate parameters and fails at runtime.</remarks>
public abstract class Component : RenderComponent, IComponent
{
    private HashSet<string>? _explicitParameters;

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
    public CssValue<HeightBuilder>? MinHeight { get; set; }

    [Parameter]
    public CssValue<HeightBuilder>? MaxHeight { get; set; }

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
    public CssValue<FilterBuilder>? Filter { get; set; }

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

    protected override bool AlwaysRender => QuarkOptions.AlwaysRender;

    public override Task SetParametersAsync(ParameterView parameters)
    {
        _explicitParameters ??= new HashSet<string>(StringComparer.Ordinal);
        _explicitParameters.Clear();

        foreach (var parameter in parameters)
        {
            _explicitParameters.Add(parameter.Name);
        }

        return base.SetParametersAsync(parameters);
    }

    protected bool HasExplicitParameter(string parameterName) => _explicitParameters?.Contains(parameterName) == true;

    protected CssValue<T>? ResolvePresetValue<T>(CssValue<T>? value, CssValue<T>? presetValue, string parameterName) where T : class, ICssBuilder
    {
        if (HasExplicitParameter(parameterName))
            return value;

        return presetValue ?? value;
    }

    protected override void BuildOwnedAttributes(Dictionary<string, object> attrs)
    {
        base.BuildOwnedAttributes(attrs);

        if (Title.HasContent())
            attrs["title"] = Title!;

        if (Hidden)
            attrs["hidden"] = true;

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

    protected override void BuildOwnedClassAndStyle(ref PooledStringBuilder sty, ref PooledStringBuilder cls)
    {
        base.BuildOwnedClassAndStyle(ref sty, ref cls);
        var preset = BuildPresetContext();

        if (Style.HasContent())
            AppendStyleDecl(ref sty, Style!);

        AddCss(ref sty, ref cls, ResolvePresetValue(Display, preset?.Display, nameof(Display)));
        AddCss(ref sty, ref cls, ResolvePresetValue(Visibility, preset?.Visibility, nameof(Visibility)));
        AddCss(ref sty, ref cls, ResolvePresetValue(Float, preset?.Float, nameof(Float)));
        AddCss(ref sty, ref cls, ResolvePresetValue(VerticalAlign, preset?.VerticalAlign, nameof(VerticalAlign)));
        AddCss(ref sty, ref cls, ResolvePresetValue(TextAlign, preset?.TextAlign, nameof(TextAlign)));
        ApplyTextColor(ref sty, ref cls, preset?.TextColor);
        AddCss(ref sty, ref cls, ResolvePresetValue(TextSize, preset?.TextSize, nameof(TextSize)));
        AddCss(ref sty, ref cls, ResolvePresetValue(DecorationLine, preset?.DecorationLine, nameof(DecorationLine)));
        AddCss(ref sty, ref cls, ResolvePresetValue(TextTransform, preset?.TextTransform, nameof(TextTransform)));
        AddCss(ref sty, ref cls, ResolvePresetValue(FontFamily, preset?.FontFamily, nameof(FontFamily)));
        AddCss(ref sty, ref cls, ResolvePresetValue(FontWeight, preset?.FontWeight, nameof(FontWeight)));
        AddCss(ref sty, ref cls, ResolvePresetValue(FontStyle, preset?.FontStyle, nameof(FontStyle)));
        AddCss(ref sty, ref cls, ResolvePresetValue(Leading, preset?.Leading, nameof(Leading)));
        AddCss(ref sty, ref cls, ResolvePresetValue(Tracking, preset?.Tracking, nameof(Tracking)));
        AddCss(ref sty, ref cls, ResolvePresetValue(Whitespace, preset?.Whitespace, nameof(Whitespace)));
        AddCss(ref sty, ref cls, ResolvePresetValue(TextWrap, preset?.TextWrap, nameof(TextWrap)));
        AddCss(ref sty, ref cls, ResolvePresetValue(TextBreak, preset?.TextBreak, nameof(TextBreak)));
        AddCss(ref sty, ref cls, ResolvePresetValue(TextOverflow, preset?.TextOverflow, nameof(TextOverflow)));
        AddCss(ref sty, ref cls, ResolvePresetValue(Truncate, preset?.Truncate, nameof(Truncate)));
        AddCss(ref sty, ref cls, ResolvePresetValue(LineClamp, preset?.LineClamp, nameof(LineClamp)));
        AddCss(ref sty, ref cls, ResolvePresetValue(FontVariantNumeric, preset?.FontVariantNumeric, nameof(FontVariantNumeric)));
        AddCss(ref sty, ref cls, ResolvePresetValue(Margin, preset?.Margin, nameof(Margin)));
        AddCss(ref sty, ref cls, ResolvePresetValue(Padding, preset?.Padding, nameof(Padding)));
        AddCss(ref sty, ref cls, ResolvePresetValue(Inset, preset?.Inset, nameof(Inset)));
        AddCss(ref sty, ref cls, ResolvePresetValue(Top, preset?.Top, nameof(Top)));
        AddCss(ref sty, ref cls, ResolvePresetValue(Right, preset?.Right, nameof(Right)));
        AddCss(ref sty, ref cls, ResolvePresetValue(Bottom, preset?.Bottom, nameof(Bottom)));
        AddCss(ref sty, ref cls, ResolvePresetValue(Left, preset?.Left, nameof(Left)));
        AddCss(ref sty, ref cls, ResolvePresetValue(Position, preset?.Position, nameof(Position)));
        AddCss(ref sty, ref cls, ResolvePresetValue(ScrollMargin, preset?.ScrollMargin, nameof(ScrollMargin)));
        AddCss(ref sty, ref cls, ResolvePresetValue(ScrollPadding, preset?.ScrollPadding, nameof(ScrollPadding)));
        AddCss(ref sty, ref cls, ResolvePresetValue(Size, preset?.Size, nameof(Size)));
        AddCss(ref sty, ref cls, ResolvePresetValue(Width, preset?.Width, nameof(Width)));
        AddCss(ref sty, ref cls, ResolvePresetValue(MinWidth, preset?.MinWidth, nameof(MinWidth)));
        AddCss(ref sty, ref cls, ResolvePresetValue(MaxWidth, preset?.MaxWidth, nameof(MaxWidth)));
        AddCss(ref sty, ref cls, ResolvePresetValue(Height, preset?.Height, nameof(Height)));
        AddCss(ref sty, ref cls, ResolvePresetValue(MinHeight, preset?.MinHeight, nameof(MinHeight)));
        AddCss(ref sty, ref cls, ResolvePresetValue(MaxHeight, preset?.MaxHeight, nameof(MaxHeight)));
        AddCss(ref sty, ref cls, ResolvePresetValue(Overflow, preset?.Overflow, nameof(Overflow)));
        AddCss(ref sty, ref cls, ResolvePresetValue(OverflowX, preset?.OverflowX, nameof(OverflowX)));
        AddCss(ref sty, ref cls, ResolvePresetValue(OverflowY, preset?.OverflowY, nameof(OverflowY)));
        AddCss(ref sty, ref cls, ResolvePresetValue(Overscroll, preset?.Overscroll, nameof(Overscroll)));
        AddCss(ref sty, ref cls, ResolvePresetValue(Flex, preset?.Flex, nameof(Flex)));
        AddCss(ref sty, ref cls, ResolvePresetValue(FlexDirection, preset?.FlexDirection, nameof(FlexDirection)));
        AddCss(ref sty, ref cls, ResolvePresetValue(FlexWrap, preset?.FlexWrap, nameof(FlexWrap)));
        AddCss(ref sty, ref cls, ResolvePresetValue(Grow, preset?.Grow, nameof(Grow)));
        AddCss(ref sty, ref cls, ResolvePresetValue(Shrink, preset?.Shrink, nameof(Shrink)));
        AddCss(ref sty, ref cls, ResolvePresetValue(Gap, preset?.Gap, nameof(Gap)));
        AddCss(ref sty, ref cls, ResolvePresetValue(Space, preset?.Space, nameof(Space)));
        AddCss(ref sty, ref cls, ResolvePresetValue(Divide, preset?.Divide, nameof(Divide)));
        AddCss(ref sty, ref cls, ResolvePresetValue(ContentAlign, preset?.ContentAlign, nameof(ContentAlign)));
        AddCss(ref sty, ref cls, ResolvePresetValue(ItemsAlign, preset?.ItemsAlign, nameof(ItemsAlign)));
        AddCss(ref sty, ref cls, ResolvePresetValue(Justify, preset?.Justify, nameof(Justify)));
        AddCss(ref sty, ref cls, ResolvePresetValue(SelfAlign, preset?.SelfAlign, nameof(SelfAlign)));
        AddCss(ref sty, ref cls, ResolvePresetValue(JustifyItemsAlign, preset?.JustifyItemsAlign, nameof(JustifyItemsAlign)));
        AddCss(ref sty, ref cls, ResolvePresetValue(JustifySelfAlign, preset?.JustifySelfAlign, nameof(JustifySelfAlign)));
        AddCss(ref sty, ref cls, ResolvePresetValue(ColStart, preset?.ColStart, nameof(ColStart)));
        AddCss(ref sty, ref cls, ResolvePresetValue(RowSpan, preset?.RowSpan, nameof(RowSpan)));
        AddCss(ref sty, ref cls, ResolvePresetValue(RowStart, preset?.RowStart, nameof(RowStart)));
        AddCss(ref sty, ref cls, ResolvePresetValue(Opacity, preset?.Opacity, nameof(Opacity)));
        AddCss(ref sty, ref cls, ResolvePresetValue(ZIndex, preset?.ZIndex, nameof(ZIndex)));
        AddCss(ref sty, ref cls, ResolvePresetValue(PointerEvents, preset?.PointerEvents, nameof(PointerEvents)));
        AddCss(ref sty, ref cls, ResolvePresetValue(UserSelect, preset?.UserSelect, nameof(UserSelect)));
        AddCss(ref sty, ref cls, ResolvePresetValue(Cursor, preset?.Cursor, nameof(Cursor)));
        AddCss(ref sty, ref cls, ResolvePresetValue(ScreenReader, preset?.ScreenReader, nameof(ScreenReader)));
        AddCss(ref sty, ref cls, ResolvePresetValue(Border, preset?.Border, nameof(Border)));
        AddCss(ref sty, ref cls, ResolvePresetValue(BorderStyle, preset?.BorderStyle, nameof(BorderStyle)));
        ApplyBorderColor(ref sty, ref cls, preset?.BorderColor);
        ApplyBackgroundColor(ref sty, ref cls, preset?.BackgroundColor);
        AddCss(ref sty, ref cls, ResolvePresetValue(Rounded, preset?.Rounded, nameof(Rounded)));
        AddCss(ref sty, ref cls, ResolvePresetValue(RingColor, preset?.RingColor, nameof(RingColor)));
        AddCss(ref sty, ref cls, ResolvePresetValue(Ring, preset?.Ring, nameof(Ring)));
        AddCss(ref sty, ref cls, ResolvePresetValue(RingOffset, preset?.RingOffset, nameof(RingOffset)));
        AddCss(ref sty, ref cls, OutlineStyle);
        AddCss(ref sty, ref cls, ResolvePresetValue(Shadow, preset?.Shadow, nameof(Shadow)));
        AddCss(ref sty, ref cls, ResolvePresetValue(BackdropFilter, preset?.BackdropFilter, nameof(BackdropFilter)));
        AddCss(ref sty, ref cls, ResolvePresetValue(Filter, preset?.Filter, nameof(Filter)));
        AddCss(ref sty, ref cls, ResolvePresetValue(Resize, preset?.Resize, nameof(Resize)));
        AddCss(ref sty, ref cls, ResolvePresetValue(Transform, preset?.Transform, nameof(Transform)));
        AddCss(ref sty, ref cls, ResolvePresetValue(Animation, preset?.Animation, nameof(Animation)));
        AddCss(ref sty, ref cls, ResolvePresetValue(Duration, preset?.Duration, nameof(Duration)));
        AddCss(ref sty, ref cls, ResolvePresetValue(Transition, preset?.Transition, nameof(Transition)));

        if (Container)
            AppendClass(ref cls, "container");

        if (preset?.Class.HasContent() == true)
            AppendClass(ref cls, preset.Class!);

        if (Class.HasContent())
            AppendClass(ref cls, Class!);
    }

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            _ = OnElementRefReady.InvokeIfHasDelegate(ElementRef);

        return Task.CompletedTask;
    }

    protected QuarkPresetContext? BuildPresetContext()
    {
        var preset = Preset;
        var presets = Presets;

        if (preset is null && (presets is null || presets.Count == 0))
            return null;

        var context = new QuarkPresetContext();

        preset?.Apply(context);

        if (presets is null)
            return context;

        for (var i = 0; i < presets.Count; i++)
        {
            presets[i].Apply(context);
        }

        return context;
    }

    private bool HasExplicitDataSlotAttribute()
    {
        return HasDataSlotAttribute(AdditionalAttributes) || HasDataSlotAttribute(Attributes);
    }

    private static bool HasDataSlotAttribute(IReadOnlyDictionary<string, object>? attributes)
    {
        if (attributes is null)
            return false;

        foreach (var key in attributes.Keys)
        {
            if (key.Equals("data-slot", StringComparison.OrdinalIgnoreCase))
                return true;
        }

        return false;
    }

    protected override void ApplyBorderColor(ref PooledStringBuilder sty, ref PooledStringBuilder cls, CssValue<BorderColorBuilder>? value)
    {
        base.ApplyBorderColor(ref sty, ref cls, ResolvePresetValue(BorderColor, value, nameof(BorderColor)));
    }

    protected override void ApplyBackgroundColor(ref PooledStringBuilder sty, ref PooledStringBuilder cls, CssValue<BackgroundColorBuilder>? value)
    {
        base.ApplyBackgroundColor(ref sty, ref cls, ResolvePresetValue(BackgroundColor, value, nameof(BackgroundColor)));
    }

    protected override void ApplyTextColor(ref PooledStringBuilder sty, ref PooledStringBuilder cls, CssValue<TextColorBuilder>? value)
    {
        base.ApplyTextColor(ref sty, ref cls, ResolvePresetValue(TextColor, value, nameof(TextColor)));
    }

    protected new void BuildClassAttribute(Dictionary<string, object> attrs, BuildClassAction builder)
    {
        var cls = new PooledStringBuilder(64);

        try
        {
            builder(ref cls);

            attrs.TryGetValue("class", out var existing);
            var combined = AppendToClass(cls.ToString(), existing?.ToString() ?? string.Empty);

            if (combined.Length > 0)
                attrs["class"] = combined;
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
        AddIf(ref hc, Filter);
        AddIf(ref hc, Resize);
        AddIf(ref hc, Transform);
        AddIf(ref hc, Animation);
        AddIf(ref hc, Duration);
        AddIf(ref hc, Transition);
    }
}
