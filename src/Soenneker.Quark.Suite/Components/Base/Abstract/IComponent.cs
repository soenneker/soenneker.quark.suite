using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark;

/// <summary>
/// Universal component contract for broad styling and universal interaction concerns.
/// </summary>
public interface IComponent : ICoreComponent
{
    string? Class { get; set; }
    string? Style { get; set; }
    string? Title { get; set; }
    bool Hidden { get; set; }

    CssValue<DisplayBuilder>? Display { get; set; }
    CssValue<VisibilityBuilder>? Visibility { get; set; }
    CssValue<FloatBuilder>? Float { get; set; }
    CssValue<VerticalAlignBuilder>? VerticalAlign { get; set; }
    CssValue<TextAlignmentBuilder>? TextAlignment { get; set; }
    CssValue<TextColorBuilder>? TextColor { get; set; }
    CssValue<TextSizeBuilder>? TextSize { get; set; }
    CssValue<TextDecorationBuilder>? TextDecoration { get; set; }
    CssValue<TextTransformBuilder>? TextTransform { get; set; }
    CssValue<FontFamilyBuilder>? FontFamily { get; set; }
    CssValue<FontWeightBuilder>? FontWeight { get; set; }
    CssValue<FontStyleBuilder>? FontStyle { get; set; }
    CssValue<LineHeightBuilder>? LineHeight { get; set; }
    CssValue<LetterSpacingBuilder>? LetterSpacing { get; set; }
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
    CssValue<PositionOffsetBuilder>? PositionOffset { get; set; }
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
    CssValue<GapBuilder>? Gap { get; set; }
    CssValue<SpaceBuilder>? Space { get; set; }
    CssValue<DivideBuilder>? Divide { get; set; }
    CssValue<AlignBuilder>? AlignUtility { get; set; }
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
