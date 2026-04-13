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
    public string? Class { get; set; }

    [Parameter]
    public string? Style { get; set; }

    [Parameter]
    public string? Title { get; set; }

    [Parameter]
    public bool Hidden { get; set; }

    [Parameter]
    public CssValue<DisplayBuilder>? Display { get; set; }

    [Parameter]
    public CssValue<VisibilityBuilder>? Visibility { get; set; }

    [Parameter]
    public CssValue<FloatBuilder>? Float { get; set; }

    [Parameter]
    public CssValue<VerticalAlignBuilder>? VerticalAlign { get; set; }

    [Parameter]
    public CssValue<TextAlignmentBuilder>? TextAlignment { get; set; }

    [Parameter]
    public CssValue<TextColorBuilder>? TextColor { get; set; }

    [Parameter]
    public CssValue<TextSizeBuilder>? TextSize { get; set; }

    [Parameter]
    public CssValue<TextDecorationBuilder>? TextDecoration { get; set; }

    [Parameter]
    public CssValue<TextTransformBuilder>? TextTransform { get; set; }

    [Parameter]
    public CssValue<FontFamilyBuilder>? FontFamily { get; set; }

    [Parameter]
    public CssValue<FontWeightBuilder>? FontWeight { get; set; }

    [Parameter]
    public CssValue<FontStyleBuilder>? FontStyle { get; set; }

    [Parameter]
    public CssValue<LineHeightBuilder>? LineHeight { get; set; }

    [Parameter]
    public CssValue<LetterSpacingBuilder>? LetterSpacing { get; set; }

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
    public CssValue<PositionOffsetBuilder>? PositionOffset { get; set; }

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
    public CssValue<GapBuilder>? Gap { get; set; }

    [Parameter]
    public CssValue<SpaceBuilder>? Space { get; set; }

    [Parameter]
    public CssValue<DivideBuilder>? Divide { get; set; }

    [Parameter]
    public CssValue<AlignBuilder>? AlignUtility { get; set; }

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
    public CssValue<RoundedBuilder>? Rounded { get; set; }

    [Parameter]
    public CssValue<RingBuilder>? Ring { get; set; }

    [Parameter]
    public CssValue<RingColorBuilder>? RingColor { get; set; }

    [Parameter]
    public CssValue<BoxShadowBuilder>? BoxShadow { get; set; }

    [Parameter]
    public CssValue<BackgroundOpacityBuilder>? BackgroundOpacity { get; set; }

    [Parameter]
    public CssValue<BorderOpacityBuilder>? BorderOpacity { get; set; }

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

        if (Style.HasContent())
            AppendStyleDecl(ref sty, Style!);

        AddCss(ref sty, ref cls, Display);
        AddCss(ref sty, ref cls, Visibility);
        AddCss(ref sty, ref cls, Float);
        AddCss(ref sty, ref cls, VerticalAlign);
        AddCss(ref sty, ref cls, TextAlignment);
        ApplyTextColor(ref sty, ref cls, TextColor);
        AddCss(ref sty, ref cls, TextSize);
        AddCss(ref sty, ref cls, TextDecoration);
        AddCss(ref sty, ref cls, TextTransform);
        AddCss(ref sty, ref cls, FontFamily);
        AddCss(ref sty, ref cls, FontWeight);
        AddCss(ref sty, ref cls, FontStyle);
        AddCss(ref sty, ref cls, LineHeight);
        AddCss(ref sty, ref cls, LetterSpacing);
        AddCss(ref sty, ref cls, Whitespace);
        AddCss(ref sty, ref cls, TextWrap);
        AddCss(ref sty, ref cls, TextBreak);
        AddCss(ref sty, ref cls, TextOverflow);
        AddCss(ref sty, ref cls, Truncate);
        AddCss(ref sty, ref cls, LineClamp);
        AddCss(ref sty, ref cls, FontVariantNumeric);
        AddCss(ref sty, ref cls, Margin);
        AddCss(ref sty, ref cls, Padding);
        AddCss(ref sty, ref cls, Position);
        AddCss(ref sty, ref cls, PositionOffset);
        AddCss(ref sty, ref cls, ScrollMargin);
        AddCss(ref sty, ref cls, BoxSize);
        AddCss(ref sty, ref cls, Width, "width");
        AddCss(ref sty, ref cls, MinWidth, "min-width");
        AddCss(ref sty, ref cls, MaxWidth, "max-width");
        AddCss(ref sty, ref cls, Height, "height");
        AddCss(ref sty, ref cls, MinHeight, "min-height");
        AddCss(ref sty, ref cls, MaxHeight, "max-height");
        AddCss(ref sty, ref cls, Overflow);
        AddCss(ref sty, ref cls, OverflowX);
        AddCss(ref sty, ref cls, OverflowY);
        AddCss(ref sty, ref cls, Overscroll);
        AddCss(ref sty, ref cls, Flex);
        AddCss(ref sty, ref cls, Gap);
        AddCss(ref sty, ref cls, Space);
        AddCss(ref sty, ref cls, Divide);
        AddCss(ref sty, ref cls, AlignUtility);
        AddCss(ref sty, ref cls, Opacity);
        AddCss(ref sty, ref cls, ZIndex);
        AddCss(ref sty, ref cls, PointerEvents);
        AddCss(ref sty, ref cls, UserSelect);
        AddCss(ref sty, ref cls, Cursor);
        AddCss(ref sty, ref cls, ScreenReader);
        AddCss(ref sty, ref cls, Border);
        ApplyBorderColor(ref sty, ref cls);
        ApplyBackgroundColor(ref sty, ref cls);
        AddCss(ref sty, ref cls, Rounded);
        AddCss(ref sty, ref cls, Ring);
        AddCss(ref sty, ref cls, RingColor);
        AddCss(ref sty, ref cls, BoxShadow);
        AddCss(ref sty, ref cls, BackgroundOpacity);
        AddCss(ref sty, ref cls, BorderOpacity);
        AddCss(ref sty, ref cls, BackdropFilter);
        AddCss(ref sty, ref cls, Filter);
        AddCss(ref sty, ref cls, Resize);
        AddCss(ref sty, ref cls, Transform);
        AddCss(ref sty, ref cls, Animation);
        AddCss(ref sty, ref cls, Transition);

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

    protected override void ComputeRenderKeyCore(ref HashCode hc)
    {
        base.ComputeRenderKeyCore(ref hc);

        hc.Add(Class);
        hc.Add(Style);
        hc.Add(Title);
        hc.Add(Hidden);

        AddIf(ref hc, Display);
        AddIf(ref hc, Visibility);
        AddIf(ref hc, Float);
        AddIf(ref hc, VerticalAlign);
        AddIf(ref hc, TextAlignment);
        AddIf(ref hc, TextColor);
        AddIf(ref hc, TextSize);
        AddIf(ref hc, TextDecoration);
        AddIf(ref hc, TextTransform);
        AddIf(ref hc, FontFamily);
        AddIf(ref hc, FontWeight);
        AddIf(ref hc, FontStyle);
        AddIf(ref hc, LineHeight);
        AddIf(ref hc, LetterSpacing);
        AddIf(ref hc, Whitespace);
        AddIf(ref hc, TextWrap);
        AddIf(ref hc, TextBreak);
        AddIf(ref hc, TextOverflow);
        AddIf(ref hc, Truncate);
        AddIf(ref hc, LineClamp);
        AddIf(ref hc, FontVariantNumeric);
        AddIf(ref hc, Margin);
        AddIf(ref hc, Padding);
        AddIf(ref hc, Position);
        AddIf(ref hc, PositionOffset);
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
        AddIf(ref hc, Gap);
        AddIf(ref hc, Space);
        AddIf(ref hc, Divide);
        AddIf(ref hc, AlignUtility);
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
        AddIf(ref hc, BackgroundOpacity);
        AddIf(ref hc, BorderOpacity);
        AddIf(ref hc, BackdropFilter);
        AddIf(ref hc, Filter);
        AddIf(ref hc, Resize);
        AddIf(ref hc, Transform);
        AddIf(ref hc, Animation);
        AddIf(ref hc, Transition);
    }
}
