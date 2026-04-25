using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark;

/// <summary>
/// Universal component contract for broad styling and universal interaction concerns.
/// </summary>
public interface IComponent : ICoreComponent
{
    bool Container { get; set; }
    IReadOnlyList<QuarkPresetToken>? Presets { get; set; }
    string? Class { get; set; }
    string? Style { get; set; }
    string? Title { get; set; }
    bool Hidden { get; set; }
    CssValue<InsetBuilder>? Inset { get; set; }

    CssValue<DisplayBuilder>? Display { get; set; }
    CssValue<VisibilityBuilder>? Visibility { get; set; }
    CssValue<FloatBuilder>? Float { get; set; }
    CssValue<VerticalAlignBuilder>? VerticalAlign { get; set; }
    CssValue<TextAlignBuilder>? TextAlign { get; set; }
    CssValue<TextColorBuilder>? TextColor { get; set; }
    CssValue<TextSizeBuilder>? TextSize { get; set; }
    CssValue<DecorationLineBuilder>? DecorationLine { get; set; }
    CssValue<TextTransformBuilder>? TextTransform { get; set; }
    CssValue<FontFamilyBuilder>? FontFamily { get; set; }
    CssValue<FontWeightBuilder>? FontWeight { get; set; }
    CssValue<FontStyleBuilder>? FontStyle { get; set; }
    CssValue<LeadingBuilder>? Leading { get; set; }
    CssValue<TrackingBuilder>? Tracking { get; set; }
    CssValue<WhitespaceBuilder>? Whitespace { get; set; }
    CssValue<TextWrapBuilder>? TextWrap { get; set; }
    CssValue<TextBreakBuilder>? TextBreak { get; set; }
    CssValue<TextOverflowBuilder>? TextOverflow { get; set; }
    CssValue<TruncateBuilder>? Truncate { get; set; }
    CssValue<LineClampBuilder>? LineClamp { get; set; }
    CssValue<FontVariantNumericBuilder>? FontVariantNumeric { get; set; }
    CssValue<MarginBuilder>? Margin { get; set; }
    CssValue<PaddingBuilder>? Padding { get; set; }
    CssValue<PositionBuilder>? Position { get; set; }
    CssValue<ScrollMarginBuilder>? ScrollMargin { get; set; }
    CssValue<SizeBuilder>? BoxSize { get; set; }
    CssValue<WidthBuilder>? Width { get; set; }
    CssValue<MinWidthBuilder>? MinWidth { get; set; }
    CssValue<MaxWidthBuilder>? MaxWidth { get; set; }
    CssValue<HeightBuilder>? Height { get; set; }
    CssValue<HeightBuilder>? MinHeight { get; set; }
    CssValue<HeightBuilder>? MaxHeight { get; set; }
    CssValue<OverflowBuilder>? Overflow { get; set; }
    CssValue<OverflowBuilder>? OverflowX { get; set; }
    CssValue<OverflowBuilder>? OverflowY { get; set; }
    CssValue<OverscrollBuilder>? Overscroll { get; set; }
    CssValue<FlexBuilder>? Flex { get; set; }
    CssValue<FlexDirectionBuilder>? FlexDirection { get; set; }
    CssValue<FlexWrapBuilder>? FlexWrap { get; set; }
    CssValue<GrowBuilder>? Grow { get; set; }
    CssValue<ShrinkBuilder>? Shrink { get; set; }
    CssValue<GapBuilder>? Gap { get; set; }
    CssValue<SpaceBuilder>? Space { get; set; }
    CssValue<DivideBuilder>? Divide { get; set; }
    CssValue<ContentAlignBuilder>? ContentAlign { get; set; }
    CssValue<ItemsBuilder>? ItemsAlign { get; set; }
    CssValue<JustifyBuilder>? Justify { get; set; }
    CssValue<SelfBuilder>? SelfAlign { get; set; }
    CssValue<JustifyItemsAlignBuilder>? JustifyItemsAlign { get; set; }
    CssValue<JustifySelfAlignBuilder>? JustifySelfAlign { get; set; }
    CssValue<OpacityBuilder>? Opacity { get; set; }
    CssValue<ZIndexBuilder>? ZIndex { get; set; }
    CssValue<PointerEventsBuilder>? PointerEvents { get; set; }
    CssValue<UserSelectBuilder>? UserSelect { get; set; }
    CssValue<CursorBuilder>? Cursor { get; set; }
    CssValue<ScreenReaderBuilder>? ScreenReader { get; set; }
    CssValue<BackgroundColorBuilder>? BackgroundColor { get; set; }
    CssValue<BorderBuilder>? Border { get; set; }
    CssValue<BorderColorBuilder>? BorderColor { get; set; }
    CssValue<RoundedBuilder>? Rounded { get; set; }
    CssValue<RingBuilder>? Ring { get; set; }
    CssValue<RingColorBuilder>? RingColor { get; set; }
    CssValue<BoxShadowBuilder>? BoxShadow { get; set; }
    CssValue<BackdropFilterBuilder>? BackdropFilter { get; set; }
    CssValue<FilterBuilder>? Filter { get; set; }
    CssValue<ResizeBuilder>? Resize { get; set; }
    CssValue<TransformBuilder>? Transform { get; set; }
    CssValue<AnimationBuilder>? Animation { get; set; }
    CssValue<TransitionBuilder>? Transition { get; set; }

    EventCallback<MouseEventArgs> OnClick { get; set; }
    EventCallback<ElementReference> OnElementRefReady { get; set; }

    void Refresh();
    Task RefreshOffThread();
}
