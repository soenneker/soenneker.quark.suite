using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
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
    CssValue<TextBreakBuilder>? TextBreak { get; set; }
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
    CssValue<HeightBuilder>? MinHeight { get; set; }
    /// <summary>
    /// Gets or sets max height.
    /// </summary>
    CssValue<HeightBuilder>? MaxHeight { get; set; }
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
    /// Gets or sets flex direction.
    /// </summary>
    CssValue<FlexDirectionBuilder>? FlexDirection { get; set; }
    /// <summary>
    /// Gets or sets flex wrap.
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
    /// Gets or sets filter.
    /// </summary>
    CssValue<FilterBuilder>? Filter { get; set; }
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
    /// Executes the refresh operation.
    /// </summary>
    void Refresh();
    /// <summary>
    /// Executes the refresh off thread operation.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task RefreshOffThread();
}
