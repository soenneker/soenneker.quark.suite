using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;
using Soenneker.Blazor.Extensions.EventCallback;
using Soenneker.Extensions.String;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

///<inheritdoc cref="IComponent"/>
///<remarks>Do not use the <c>new</c> keyword to shadow inherited <see cref="ParameterAttribute"/> members. Blazor treats those names as duplicate parameters and fails at runtime.</remarks>
public abstract class Component : RenderComponent, IComponent
{
    [Inject]
    protected ILogger<Component> Logger { get; set; } = null!;

    [Inject]
    protected QuarkOptions QuarkOptions { get; set; } = null!;

    [Parameter]
    public bool Container { get; set; }

    [Parameter]
    public IReadOnlyList<QuarkPresetToken>? Presets { get; set; }

    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public string? Style { get; set; }

    [Parameter]
    public string? Title { get; set; }

    [Parameter]
    public bool Hidden { get; set; }

    [Parameter]
    public CssValue<InsetBuilder>? Inset { get; set; }

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
    public CssValue<SizeBuilder>? BoxSize { get; set; }

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
    public CssValue<BorderColorBuilder>? BorderColor { get; set; }

    [Parameter]
    public virtual CssValue<RoundedBuilder>? Rounded { get; set; }

    [Parameter]
    public CssValue<RingBuilder>? Ring { get; set; }

    [Parameter]
    public CssValue<RingColorBuilder>? RingColor { get; set; }

    [Parameter]
    public virtual CssValue<BoxShadowBuilder>? BoxShadow { get; set; }

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
    public CssValue<TransitionBuilder>? Transition { get; set; }

    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    [Parameter]
    public EventCallback<ElementReference> OnElementRefReady { get; set; }

    protected ElementReference ElementRef { get; set; }

    protected override bool AlwaysRender => QuarkOptions.AlwaysRender;

    protected override void BuildOwnedAttributes(Dictionary<string, object> attrs)
    {
        base.BuildOwnedAttributes(attrs);

        if (Title.HasContent())
            attrs["title"] = Title!;

        if (Hidden)
            attrs["hidden"] = true;

    }

    protected override void BuildOwnedClassAndStyle(ref PooledStringBuilder sty, ref PooledStringBuilder cls)
    {
        base.BuildOwnedClassAndStyle(ref sty, ref cls);
        QuarkPresetContext? preset = BuildPresetContext();

        if (Style.HasContent())
            AppendStyleDecl(ref sty, Style!);

        AddCss(ref sty, ref cls, Display ?? preset?.Display);
        AddCss(ref sty, ref cls, Visibility ?? preset?.Visibility);
        AddCss(ref sty, ref cls, Float ?? preset?.Float);
        AddCss(ref sty, ref cls, VerticalAlign ?? preset?.VerticalAlign);
        AddCss(ref sty, ref cls, TextAlign ?? preset?.TextAlign);
        ApplyTextColor(ref sty, ref cls, TextColor ?? preset?.TextColor);
        AddCss(ref sty, ref cls, TextSize ?? preset?.TextSize);
        AddCss(ref sty, ref cls, DecorationLine ?? preset?.DecorationLine);
        AddCss(ref sty, ref cls, TextTransform ?? preset?.TextTransform);
        AddCss(ref sty, ref cls, FontFamily ?? preset?.FontFamily);
        AddCss(ref sty, ref cls, FontWeight ?? preset?.FontWeight);
        AddCss(ref sty, ref cls, FontStyle ?? preset?.FontStyle);
        AddCss(ref sty, ref cls, Leading ?? preset?.Leading);
        AddCss(ref sty, ref cls, Tracking ?? preset?.Tracking);
        AddCss(ref sty, ref cls, Whitespace ?? preset?.Whitespace);
        AddCss(ref sty, ref cls, TextWrap ?? preset?.TextWrap);
        AddCss(ref sty, ref cls, TextBreak ?? preset?.TextBreak);
        AddCss(ref sty, ref cls, TextOverflow ?? preset?.TextOverflow);
        AddCss(ref sty, ref cls, Truncate ?? preset?.Truncate);
        AddCss(ref sty, ref cls, LineClamp ?? preset?.LineClamp);
        AddCss(ref sty, ref cls, FontVariantNumeric ?? preset?.FontVariantNumeric);
        AddCss(ref sty, ref cls, Margin ?? preset?.Margin);
        AddCss(ref sty, ref cls, Padding ?? preset?.Padding);
        AddCss(ref sty, ref cls, Inset ?? preset?.Inset);
        AddCss(ref sty, ref cls, Position ?? preset?.Position);
        AddCss(ref sty, ref cls, ScrollMargin ?? preset?.ScrollMargin);
        AddCss(ref sty, ref cls, BoxSize ?? preset?.BoxSize);
        AddCss(ref sty, ref cls, Width ?? preset?.Width, "width");
        AddCss(ref sty, ref cls, MinWidth ?? preset?.MinWidth, "min-width");
        AddCss(ref sty, ref cls, MaxWidth ?? preset?.MaxWidth, "max-width");
        AddCss(ref sty, ref cls, Height ?? preset?.Height, "height");
        AddCss(ref sty, ref cls, MinHeight ?? preset?.MinHeight, "min-height");
        AddCss(ref sty, ref cls, MaxHeight ?? preset?.MaxHeight, "max-height");
        AddCss(ref sty, ref cls, Overflow ?? preset?.Overflow);
        AddCss(ref sty, ref cls, OverflowX ?? preset?.OverflowX);
        AddCss(ref sty, ref cls, OverflowY ?? preset?.OverflowY);
        AddCss(ref sty, ref cls, Overscroll ?? preset?.Overscroll);
        AddCss(ref sty, ref cls, Flex ?? preset?.Flex);
        AddCss(ref sty, ref cls, FlexDirection ?? preset?.FlexDirection);
        AddCss(ref sty, ref cls, FlexWrap ?? preset?.FlexWrap);
        AddCss(ref sty, ref cls, Grow ?? preset?.Grow);
        AddCss(ref sty, ref cls, Shrink ?? preset?.Shrink);
        AddCss(ref sty, ref cls, Gap ?? preset?.Gap);
        AddCss(ref sty, ref cls, Space ?? preset?.Space);
        AddCss(ref sty, ref cls, Divide ?? preset?.Divide);
        AddCss(ref sty, ref cls, ContentAlign ?? preset?.ContentAlign);
        AddCss(ref sty, ref cls, ItemsAlign ?? preset?.ItemsAlign);
        AddCss(ref sty, ref cls, Justify ?? preset?.Justify);
        AddCss(ref sty, ref cls, SelfAlign ?? preset?.SelfAlign);
        AddCss(ref sty, ref cls, JustifyItemsAlign ?? preset?.JustifyItemsAlign);
        AddCss(ref sty, ref cls, JustifySelfAlign ?? preset?.JustifySelfAlign);
        AddCss(ref sty, ref cls, Opacity ?? preset?.Opacity);
        AddCss(ref sty, ref cls, ZIndex ?? preset?.ZIndex);
        AddCss(ref sty, ref cls, PointerEvents ?? preset?.PointerEvents);
        AddCss(ref sty, ref cls, UserSelect ?? preset?.UserSelect);
        AddCss(ref sty, ref cls, Cursor ?? preset?.Cursor);
        AddCss(ref sty, ref cls, ScreenReader ?? preset?.ScreenReader);
        AddCss(ref sty, ref cls, Border ?? preset?.Border);
        ApplyBorderColor(ref sty, ref cls);
        ApplyBackgroundColor(ref sty, ref cls);
        AddCss(ref sty, ref cls, Rounded ?? preset?.Rounded);
        AddCss(ref sty, ref cls, Ring ?? preset?.Ring);
        AddCss(ref sty, ref cls, RingColor ?? preset?.RingColor);
        AddCss(ref sty, ref cls, BoxShadow ?? preset?.BoxShadow);
        AddCss(ref sty, ref cls, BackdropFilter ?? preset?.BackdropFilter);
        AddCss(ref sty, ref cls, Filter ?? preset?.Filter);
        AddCss(ref sty, ref cls, Resize ?? preset?.Resize);
        AddCss(ref sty, ref cls, Transform ?? preset?.Transform);
        AddCss(ref sty, ref cls, Animation ?? preset?.Animation);
        AddCss(ref sty, ref cls, Transition ?? preset?.Transition);

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

    protected virtual Task HandleClick(MouseEventArgs e)
    {
        return OnClick.InvokeIfHasDelegate(e);
    }

    private QuarkPresetContext? BuildPresetContext()
    {
        IReadOnlyList<QuarkPresetToken>? presets = Presets;

        if (presets is null || presets.Count == 0)
            return null;

        var context = new QuarkPresetContext();

        for (var i = 0; i < presets.Count; i++)
        {
            presets[i].Apply(context);
        }

        return context;
    }

    protected virtual void ApplyBorderColor(ref PooledStringBuilder sty, ref PooledStringBuilder cls)
    {
        ApplyBorderColor(ref sty, ref cls, BorderColor);
    }

    protected virtual void ApplyBackgroundColor(ref PooledStringBuilder sty, ref PooledStringBuilder cls)
    {
        ApplyBackgroundColor(ref sty, ref cls, BackgroundColor);
    }

    protected override void ApplyTextColor(ref PooledStringBuilder sty, ref PooledStringBuilder cls, CssValue<TextColorBuilder>? value)
    {
        base.ApplyTextColor(ref sty, ref cls, value);
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
        AddIf(ref hc, Position);
        AddIf(ref hc, ScrollMargin);
        AddIf(ref hc, BoxSize);
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
        AddIf(ref hc, Opacity);
        AddIf(ref hc, ZIndex);
        AddIf(ref hc, PointerEvents);
        AddIf(ref hc, UserSelect);
        AddIf(ref hc, Cursor);
        AddIf(ref hc, ScreenReader);
        AddIf(ref hc, BackgroundColor);
        AddIf(ref hc, Border);
        AddIf(ref hc, BorderColor);
        AddIf(ref hc, Rounded);
        AddIf(ref hc, Ring);
        AddIf(ref hc, RingColor);
        AddIf(ref hc, BoxShadow);
        AddIf(ref hc, BackdropFilter);
        AddIf(ref hc, Filter);
        AddIf(ref hc, Resize);
        AddIf(ref hc, Transform);
        AddIf(ref hc, Animation);
        AddIf(ref hc, Transition);
    }
}
