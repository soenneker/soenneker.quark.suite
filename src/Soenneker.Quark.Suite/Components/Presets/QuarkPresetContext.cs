namespace Soenneker.Quark;

/// <summary>
/// Represents the quark preset context.
/// </summary>
public sealed class QuarkPresetContext
{
    /// <summary>
    /// Gets or sets class.
    /// </summary>
    public string? Class { get; set; }
    /// <summary>
    /// Gets or sets inset.
    /// </summary>
    public CssValue<InsetBuilder>? Inset { get; set; }
    /// <summary>
    /// Gets or sets top.
    /// </summary>
    public CssValue<TopBuilder>? Top { get; set; }
    /// <summary>
    /// Gets or sets right.
    /// </summary>
    public CssValue<RightBuilder>? Right { get; set; }
    /// <summary>
    /// Gets or sets bottom.
    /// </summary>
    public CssValue<BottomBuilder>? Bottom { get; set; }
    /// <summary>
    /// Gets or sets left.
    /// </summary>
    public CssValue<LeftBuilder>? Left { get; set; }
    /// <summary>
    /// Gets or sets display.
    /// </summary>
    public CssValue<DisplayBuilder>? Display { get; set; }
    /// <summary>
    /// Gets or sets visibility.
    /// </summary>
    public CssValue<VisibilityBuilder>? Visibility { get; set; }
    /// <summary>
    /// Gets or sets float.
    /// </summary>
    public CssValue<FloatBuilder>? Float { get; set; }
    /// <summary>
    /// Gets or sets vertical align.
    /// </summary>
    public CssValue<VerticalAlignBuilder>? VerticalAlign { get; set; }
    /// <summary>
    /// Gets or sets text align.
    /// </summary>
    public CssValue<TextAlignBuilder>? TextAlign { get; set; }
    /// <summary>
    /// Gets or sets text color.
    /// </summary>
    public CssValue<TextColorBuilder>? TextColor { get; set; }
    /// <summary>
    /// Gets or sets text size.
    /// </summary>
    public CssValue<TextSizeBuilder>? TextSize { get; set; }
    /// <summary>
    /// Gets or sets decoration line.
    /// </summary>
    public CssValue<DecorationLineBuilder>? DecorationLine { get; set; }
    /// <summary>
    /// Gets or sets text transform.
    /// </summary>
    public CssValue<TextTransformBuilder>? TextTransform { get; set; }
    /// <summary>
    /// Gets or sets font family.
    /// </summary>
    public CssValue<FontFamilyBuilder>? FontFamily { get; set; }
    /// <summary>
    /// Gets or sets font weight.
    /// </summary>
    public CssValue<FontWeightBuilder>? FontWeight { get; set; }
    /// <summary>
    /// Gets or sets font style.
    /// </summary>
    public CssValue<FontStyleBuilder>? FontStyle { get; set; }
    /// <summary>
    /// Gets or sets leading.
    /// </summary>
    public CssValue<LeadingBuilder>? Leading { get; set; }
    /// <summary>
    /// Gets or sets tracking.
    /// </summary>
    public CssValue<TrackingBuilder>? Tracking { get; set; }
    /// <summary>
    /// Gets or sets whitespace.
    /// </summary>
    public CssValue<WhitespaceBuilder>? Whitespace { get; set; }
    /// <summary>
    /// Gets or sets text wrap.
    /// </summary>
    public CssValue<TextWrapBuilder>? TextWrap { get; set; }
    /// <summary>
    /// Gets or sets text break.
    /// </summary>
    public CssValue<TextBreakBuilder>? TextBreak { get; set; }
    /// <summary>
    /// Gets or sets text overflow.
    /// </summary>
    public CssValue<TextOverflowBuilder>? TextOverflow { get; set; }
    /// <summary>
    /// Gets or sets truncate.
    /// </summary>
    public CssValue<TruncateBuilder>? Truncate { get; set; }
    /// <summary>
    /// Gets or sets line clamp.
    /// </summary>
    public CssValue<LineClampBuilder>? LineClamp { get; set; }
    /// <summary>
    /// Gets or sets font variant numeric.
    /// </summary>
    public CssValue<FontVariantNumericBuilder>? FontVariantNumeric { get; set; }
    /// <summary>
    /// Gets or sets margin.
    /// </summary>
    public CssValue<MarginBuilder>? Margin { get; set; }
    /// <summary>
    /// Gets or sets padding.
    /// </summary>
    public CssValue<PaddingBuilder>? Padding { get; set; }
    /// <summary>
    /// Gets or sets position.
    /// </summary>
    public CssValue<PositionBuilder>? Position { get; set; }
    /// <summary>
    /// Gets or sets scroll margin.
    /// </summary>
    public CssValue<ScrollMarginBuilder>? ScrollMargin { get; set; }
    /// <summary>
    /// Gets or sets scroll padding.
    /// </summary>
    public CssValue<ScrollPaddingBuilder>? ScrollPadding { get; set; }
    /// <summary>
    /// Gets or sets size.
    /// </summary>
    public CssValue<SizeBuilder>? Size { get; set; }
    /// <summary>
    /// Gets or sets width.
    /// </summary>
    public CssValue<WidthBuilder>? Width { get; set; }
    /// <summary>
    /// Gets or sets min width.
    /// </summary>
    public CssValue<MinWidthBuilder>? MinWidth { get; set; }
    /// <summary>
    /// Gets or sets max width.
    /// </summary>
    public CssValue<MaxWidthBuilder>? MaxWidth { get; set; }
    /// <summary>
    /// Gets or sets height.
    /// </summary>
    public CssValue<HeightBuilder>? Height { get; set; }
    /// <summary>
    /// Gets or sets min height.
    /// </summary>
    public CssValue<HeightBuilder>? MinHeight { get; set; }
    /// <summary>
    /// Gets or sets max height.
    /// </summary>
    public CssValue<HeightBuilder>? MaxHeight { get; set; }
    /// <summary>
    /// Gets or sets overflow.
    /// </summary>
    public CssValue<OverflowBuilder>? Overflow { get; set; }
    /// <summary>
    /// Gets or sets overflow x.
    /// </summary>
    public CssValue<OverflowBuilder>? OverflowX { get; set; }
    /// <summary>
    /// Gets or sets overflow y.
    /// </summary>
    public CssValue<OverflowBuilder>? OverflowY { get; set; }
    /// <summary>
    /// Gets or sets overscroll.
    /// </summary>
    public CssValue<OverscrollBuilder>? Overscroll { get; set; }
    /// <summary>
    /// Gets or sets flex.
    /// </summary>
    public CssValue<FlexBuilder>? Flex { get; set; }
    /// <summary>
    /// Gets or sets flex direction.
    /// </summary>
    public CssValue<FlexDirectionBuilder>? FlexDirection { get; set; }
    /// <summary>
    /// Gets or sets flex wrap.
    /// </summary>
    public CssValue<FlexWrapBuilder>? FlexWrap { get; set; }
    /// <summary>
    /// Gets or sets grow.
    /// </summary>
    public CssValue<GrowBuilder>? Grow { get; set; }
    /// <summary>
    /// Gets or sets shrink.
    /// </summary>
    public CssValue<ShrinkBuilder>? Shrink { get; set; }
    /// <summary>
    /// Gets or sets gap.
    /// </summary>
    public CssValue<GapBuilder>? Gap { get; set; }
    /// <summary>
    /// Gets or sets space.
    /// </summary>
    public CssValue<SpaceBuilder>? Space { get; set; }
    /// <summary>
    /// Gets or sets divide.
    /// </summary>
    public CssValue<DivideBuilder>? Divide { get; set; }
    /// <summary>
    /// Gets or sets content align.
    /// </summary>
    public CssValue<ContentAlignBuilder>? ContentAlign { get; set; }
    /// <summary>
    /// Gets or sets items align.
    /// </summary>
    public CssValue<ItemsBuilder>? ItemsAlign { get; set; }
    /// <summary>
    /// Gets or sets justify.
    /// </summary>
    public CssValue<JustifyBuilder>? Justify { get; set; }
    /// <summary>
    /// Gets or sets self align.
    /// </summary>
    public CssValue<SelfBuilder>? SelfAlign { get; set; }
    /// <summary>
    /// Gets or sets justify items align.
    /// </summary>
    public CssValue<JustifyItemsAlignBuilder>? JustifyItemsAlign { get; set; }
    /// <summary>
    /// Gets or sets justify self align.
    /// </summary>
    public CssValue<JustifySelfAlignBuilder>? JustifySelfAlign { get; set; }
    /// <summary>
    /// Gets or sets col start.
    /// </summary>
    public CssValue<ColStartBuilder>? ColStart { get; set; }
    /// <summary>
    /// Gets or sets row span.
    /// </summary>
    public CssValue<RowSpanBuilder>? RowSpan { get; set; }
    /// <summary>
    /// Gets or sets row start.
    /// </summary>
    public CssValue<RowStartBuilder>? RowStart { get; set; }
    /// <summary>
    /// Gets or sets opacity.
    /// </summary>
    public CssValue<OpacityBuilder>? Opacity { get; set; }
    /// <summary>
    /// Gets or sets z index.
    /// </summary>
    public CssValue<ZIndexBuilder>? ZIndex { get; set; }
    /// <summary>
    /// Gets or sets pointer events.
    /// </summary>
    public CssValue<PointerEventsBuilder>? PointerEvents { get; set; }
    /// <summary>
    /// Gets or sets user select.
    /// </summary>
    public CssValue<UserSelectBuilder>? UserSelect { get; set; }
    /// <summary>
    /// Gets or sets cursor.
    /// </summary>
    public CssValue<CursorBuilder>? Cursor { get; set; }
    /// <summary>
    /// Gets or sets screen reader.
    /// </summary>
    public CssValue<ScreenReaderBuilder>? ScreenReader { get; set; }
    /// <summary>
    /// Gets or sets background color.
    /// </summary>
    public CssValue<BackgroundColorBuilder>? BackgroundColor { get; set; }
    /// <summary>
    /// Gets or sets border color.
    /// </summary>
    public CssValue<BorderColorBuilder>? BorderColor { get; set; }
    /// <summary>
    /// Gets or sets border.
    /// </summary>
    public CssValue<BorderBuilder>? Border { get; set; }
    /// <summary>
    /// Gets or sets rounded.
    /// </summary>
    public CssValue<RoundedBuilder>? Rounded { get; set; }
    /// <summary>
    /// Gets or sets ring color.
    /// </summary>
    public CssValue<RingColorBuilder>? RingColor { get; set; }
    /// <summary>
    /// Gets or sets ring.
    /// </summary>
    public CssValue<RingBuilder>? Ring { get; set; }
    /// <summary>
    /// Gets or sets shadow.
    /// </summary>
    public CssValue<ShadowBuilder>? Shadow { get; set; }
    /// <summary>
    /// Gets or sets backdrop filter.
    /// </summary>
    public CssValue<BackdropFilterBuilder>? BackdropFilter { get; set; }
    /// <summary>
    /// Gets or sets filter.
    /// </summary>
    public CssValue<FilterBuilder>? Filter { get; set; }
    /// <summary>
    /// Gets or sets resize.
    /// </summary>
    public CssValue<ResizeBuilder>? Resize { get; set; }
    /// <summary>
    /// Gets or sets transform.
    /// </summary>
    public CssValue<TransformBuilder>? Transform { get; set; }
    /// <summary>
    /// Gets or sets animation.
    /// </summary>
    public CssValue<AnimationBuilder>? Animation { get; set; }
    /// <summary>
    /// Gets or sets duration.
    /// </summary>
    public CssValue<DurationBuilder>? Duration { get; set; }
    /// <summary>
    /// Gets or sets transition.
    /// </summary>
    public CssValue<TransitionBuilder>? Transition { get; set; }
}
