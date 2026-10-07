using System.Numerics;

namespace Soenneker.Quark;

/// <summary>Collects typed preset assignments without boxing. Each evaluation starts with an empty context.</summary>
public sealed class QuarkPresetContext
{
    private readonly string?[] _values = new string?[102];
    private ulong _assigned0;
    private ulong _assigned1;
    internal int Revision { get; private set; }

    internal bool HasValue(PresetProperty property) => _values[(int)property] is not null;

    private void Set(int index, string? value)
    {
        _values[index] = value;
        if (index < 64) _assigned0 |= 1UL << index;
        else _assigned1 |= 1UL << (index - 64);
        unchecked { Revision++; }
    }

    /// <summary>Gets or sets the classes appended after typed preset properties.</summary>
    public string? Class { get => _values[101]; set => Set(101, value); }

    /// <summary>Gets or sets the Inset preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<InsetBuilder>? Inset
    {
        get => _values[0] is { } value ? CssValue<InsetBuilder>.Raw(value) : (CssValue<InsetBuilder>?)null;
        set => Set(0, value?.ToString());
    }

    /// <summary>Gets or sets the Top preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<TopBuilder>? Top
    {
        get => _values[1] is { } value ? CssValue<TopBuilder>.Raw(value) : (CssValue<TopBuilder>?)null;
        set => Set(1, value?.ToString());
    }

    /// <summary>Gets or sets the Right preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<RightBuilder>? Right
    {
        get => _values[2] is { } value ? CssValue<RightBuilder>.Raw(value) : (CssValue<RightBuilder>?)null;
        set => Set(2, value?.ToString());
    }

    /// <summary>Gets or sets the Bottom preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<BottomBuilder>? Bottom
    {
        get => _values[3] is { } value ? CssValue<BottomBuilder>.Raw(value) : (CssValue<BottomBuilder>?)null;
        set => Set(3, value?.ToString());
    }

    /// <summary>Gets or sets the Left preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<LeftBuilder>? Left
    {
        get => _values[4] is { } value ? CssValue<LeftBuilder>.Raw(value) : (CssValue<LeftBuilder>?)null;
        set => Set(4, value?.ToString());
    }

    /// <summary>Gets or sets the Display preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<DisplayBuilder>? Display
    {
        get => _values[5] is { } value ? CssValue<DisplayBuilder>.Raw(value) : (CssValue<DisplayBuilder>?)null;
        set => Set(5, value?.ToString());
    }

    /// <summary>Gets or sets the Visibility preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<VisibilityBuilder>? Visibility
    {
        get => _values[6] is { } value ? CssValue<VisibilityBuilder>.Raw(value) : (CssValue<VisibilityBuilder>?)null;
        set => Set(6, value?.ToString());
    }

    /// <summary>Gets or sets the Float preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<FloatBuilder>? Float
    {
        get => _values[7] is { } value ? CssValue<FloatBuilder>.Raw(value) : (CssValue<FloatBuilder>?)null;
        set => Set(7, value?.ToString());
    }

    /// <summary>Gets or sets the VerticalAlign preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<VerticalAlignBuilder>? VerticalAlign
    {
        get => _values[8] is { } value ? CssValue<VerticalAlignBuilder>.Raw(value) : (CssValue<VerticalAlignBuilder>?)null;
        set => Set(8, value?.ToString());
    }

    /// <summary>Gets or sets the TextAlign preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<TextAlignBuilder>? TextAlign
    {
        get => _values[9] is { } value ? CssValue<TextAlignBuilder>.Raw(value) : (CssValue<TextAlignBuilder>?)null;
        set => Set(9, value?.ToString());
    }

    /// <summary>Gets or sets the TextColor preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<TextColorBuilder>? TextColor
    {
        get => _values[10] is { } value ? CssValue<TextColorBuilder>.Raw(value) : (CssValue<TextColorBuilder>?)null;
        set => Set(10, value?.ToString());
    }

    /// <summary>Gets or sets the TextSize preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<TextSizeBuilder>? TextSize
    {
        get => _values[11] is { } value ? CssValue<TextSizeBuilder>.Raw(value) : (CssValue<TextSizeBuilder>?)null;
        set => Set(11, value?.ToString());
    }

    /// <summary>Gets or sets the DecorationLine preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<DecorationLineBuilder>? DecorationLine
    {
        get => _values[12] is { } value ? CssValue<DecorationLineBuilder>.Raw(value) : (CssValue<DecorationLineBuilder>?)null;
        set => Set(12, value?.ToString());
    }

    /// <summary>Gets or sets the UnderlineOffset preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<UnderlineOffsetBuilder>? UnderlineOffset
    {
        get => _values[13] is { } value ? CssValue<UnderlineOffsetBuilder>.Raw(value) : (CssValue<UnderlineOffsetBuilder>?)null;
        set => Set(13, value?.ToString());
    }

    /// <summary>Gets or sets the TextTransform preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<TextTransformBuilder>? TextTransform
    {
        get => _values[14] is { } value ? CssValue<TextTransformBuilder>.Raw(value) : (CssValue<TextTransformBuilder>?)null;
        set => Set(14, value?.ToString());
    }

    /// <summary>Gets or sets the FontFamily preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<FontFamilyBuilder>? FontFamily
    {
        get => _values[15] is { } value ? CssValue<FontFamilyBuilder>.Raw(value) : (CssValue<FontFamilyBuilder>?)null;
        set => Set(15, value?.ToString());
    }

    /// <summary>Gets or sets the FontWeight preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<FontWeightBuilder>? FontWeight
    {
        get => _values[16] is { } value ? CssValue<FontWeightBuilder>.Raw(value) : (CssValue<FontWeightBuilder>?)null;
        set => Set(16, value?.ToString());
    }

    /// <summary>Gets or sets the FontStyle preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<FontStyleBuilder>? FontStyle
    {
        get => _values[17] is { } value ? CssValue<FontStyleBuilder>.Raw(value) : (CssValue<FontStyleBuilder>?)null;
        set => Set(17, value?.ToString());
    }

    /// <summary>Gets or sets the Leading preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<LeadingBuilder>? Leading
    {
        get => _values[18] is { } value ? CssValue<LeadingBuilder>.Raw(value) : (CssValue<LeadingBuilder>?)null;
        set => Set(18, value?.ToString());
    }

    /// <summary>Gets or sets the Tracking preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<TrackingBuilder>? Tracking
    {
        get => _values[19] is { } value ? CssValue<TrackingBuilder>.Raw(value) : (CssValue<TrackingBuilder>?)null;
        set => Set(19, value?.ToString());
    }

    /// <summary>Gets or sets the Whitespace preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<WhitespaceBuilder>? Whitespace
    {
        get => _values[20] is { } value ? CssValue<WhitespaceBuilder>.Raw(value) : (CssValue<WhitespaceBuilder>?)null;
        set => Set(20, value?.ToString());
    }

    /// <summary>Gets or sets the TextWrap preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<TextWrapBuilder>? TextWrap
    {
        get => _values[21] is { } value ? CssValue<TextWrapBuilder>.Raw(value) : (CssValue<TextWrapBuilder>?)null;
        set => Set(21, value?.ToString());
    }

    /// <summary>Gets or sets the TextBreak preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<TextBreakBuilder>? TextBreak
    {
        get => _values[22] is { } value ? CssValue<TextBreakBuilder>.Raw(value) : (CssValue<TextBreakBuilder>?)null;
        set => Set(22, value?.ToString());
    }

    /// <summary>Gets or sets the TextOverflow preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<TextOverflowBuilder>? TextOverflow
    {
        get => _values[23] is { } value ? CssValue<TextOverflowBuilder>.Raw(value) : (CssValue<TextOverflowBuilder>?)null;
        set => Set(23, value?.ToString());
    }

    /// <summary>Gets or sets the Truncate preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<TruncateBuilder>? Truncate
    {
        get => _values[24] is { } value ? CssValue<TruncateBuilder>.Raw(value) : (CssValue<TruncateBuilder>?)null;
        set => Set(24, value?.ToString());
    }

    /// <summary>Gets or sets the LineClamp preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<LineClampBuilder>? LineClamp
    {
        get => _values[25] is { } value ? CssValue<LineClampBuilder>.Raw(value) : (CssValue<LineClampBuilder>?)null;
        set => Set(25, value?.ToString());
    }

    /// <summary>Gets or sets the FontVariantNumeric preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<FontVariantNumericBuilder>? FontVariantNumeric
    {
        get => _values[26] is { } value ? CssValue<FontVariantNumericBuilder>.Raw(value) : (CssValue<FontVariantNumericBuilder>?)null;
        set => Set(26, value?.ToString());
    }

    /// <summary>Gets or sets the Margin preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<MarginBuilder>? Margin
    {
        get => _values[27] is { } value ? CssValue<MarginBuilder>.Raw(value) : (CssValue<MarginBuilder>?)null;
        set => Set(27, value?.ToString());
    }

    /// <summary>Gets or sets the Padding preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<PaddingBuilder>? Padding
    {
        get => _values[28] is { } value ? CssValue<PaddingBuilder>.Raw(value) : (CssValue<PaddingBuilder>?)null;
        set => Set(28, value?.ToString());
    }

    /// <summary>Gets or sets the Position preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<PositionBuilder>? Position
    {
        get => _values[29] is { } value ? CssValue<PositionBuilder>.Raw(value) : (CssValue<PositionBuilder>?)null;
        set => Set(29, value?.ToString());
    }

    /// <summary>Gets or sets the ScrollMargin preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ScrollMarginBuilder>? ScrollMargin
    {
        get => _values[30] is { } value ? CssValue<ScrollMarginBuilder>.Raw(value) : (CssValue<ScrollMarginBuilder>?)null;
        set => Set(30, value?.ToString());
    }

    /// <summary>Gets or sets the ScrollPadding preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ScrollPaddingBuilder>? ScrollPadding
    {
        get => _values[31] is { } value ? CssValue<ScrollPaddingBuilder>.Raw(value) : (CssValue<ScrollPaddingBuilder>?)null;
        set => Set(31, value?.ToString());
    }

    /// <summary>Gets or sets the Size preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<SizeBuilder>? Size
    {
        get => _values[32] is { } value ? CssValue<SizeBuilder>.Raw(value) : (CssValue<SizeBuilder>?)null;
        set => Set(32, value?.ToString());
    }

    /// <summary>Gets or sets the Width preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<WidthBuilder>? Width
    {
        get => _values[33] is { } value ? CssValue<WidthBuilder>.Raw(value) : (CssValue<WidthBuilder>?)null;
        set => Set(33, value?.ToString());
    }

    /// <summary>Gets or sets the MinWidth preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<MinWidthBuilder>? MinWidth
    {
        get => _values[34] is { } value ? CssValue<MinWidthBuilder>.Raw(value) : (CssValue<MinWidthBuilder>?)null;
        set => Set(34, value?.ToString());
    }

    /// <summary>Gets or sets the MaxWidth preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<MaxWidthBuilder>? MaxWidth
    {
        get => _values[35] is { } value ? CssValue<MaxWidthBuilder>.Raw(value) : (CssValue<MaxWidthBuilder>?)null;
        set => Set(35, value?.ToString());
    }

    /// <summary>Gets or sets the Height preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<HeightBuilder>? Height
    {
        get => _values[36] is { } value ? CssValue<HeightBuilder>.Raw(value) : (CssValue<HeightBuilder>?)null;
        set => Set(36, value?.ToString());
    }

    /// <summary>Gets or sets the MinHeight preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<MinHeightBuilder>? MinHeight
    {
        get => _values[37] is { } value ? CssValue<MinHeightBuilder>.Raw(value) : (CssValue<MinHeightBuilder>?)null;
        set => Set(37, value?.ToString());
    }

    /// <summary>Gets or sets the MaxHeight preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<MaxHeightBuilder>? MaxHeight
    {
        get => _values[38] is { } value ? CssValue<MaxHeightBuilder>.Raw(value) : (CssValue<MaxHeightBuilder>?)null;
        set => Set(38, value?.ToString());
    }

    /// <summary>Gets or sets the Overflow preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<OverflowBuilder>? Overflow
    {
        get => _values[39] is { } value ? CssValue<OverflowBuilder>.Raw(value) : (CssValue<OverflowBuilder>?)null;
        set => Set(39, value?.ToString());
    }

    /// <summary>Gets or sets the OverflowX preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<OverflowBuilder>? OverflowX
    {
        get => _values[40] is { } value ? CssValue<OverflowBuilder>.Raw(value) : (CssValue<OverflowBuilder>?)null;
        set => Set(40, value?.ToString());
    }

    /// <summary>Gets or sets the OverflowY preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<OverflowBuilder>? OverflowY
    {
        get => _values[41] is { } value ? CssValue<OverflowBuilder>.Raw(value) : (CssValue<OverflowBuilder>?)null;
        set => Set(41, value?.ToString());
    }

    /// <summary>Gets or sets the Overscroll preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<OverscrollBuilder>? Overscroll
    {
        get => _values[42] is { } value ? CssValue<OverscrollBuilder>.Raw(value) : (CssValue<OverscrollBuilder>?)null;
        set => Set(42, value?.ToString());
    }

    /// <summary>Gets or sets the Flex preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<FlexBuilder>? Flex
    {
        get => _values[43] is { } value ? CssValue<FlexBuilder>.Raw(value) : (CssValue<FlexBuilder>?)null;
        set => Set(43, value?.ToString());
    }

    /// <summary>Gets or sets the FlexDirection preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<FlexDirectionBuilder>? FlexDirection
    {
        get => _values[44] is { } value ? CssValue<FlexDirectionBuilder>.Raw(value) : (CssValue<FlexDirectionBuilder>?)null;
        set => Set(44, value?.ToString());
    }

    /// <summary>Gets or sets the FlexWrap preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<FlexWrapBuilder>? FlexWrap
    {
        get => _values[45] is { } value ? CssValue<FlexWrapBuilder>.Raw(value) : (CssValue<FlexWrapBuilder>?)null;
        set => Set(45, value?.ToString());
    }

    /// <summary>Gets or sets the Grow preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<GrowBuilder>? Grow
    {
        get => _values[46] is { } value ? CssValue<GrowBuilder>.Raw(value) : (CssValue<GrowBuilder>?)null;
        set => Set(46, value?.ToString());
    }

    /// <summary>Gets or sets the Shrink preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ShrinkBuilder>? Shrink
    {
        get => _values[47] is { } value ? CssValue<ShrinkBuilder>.Raw(value) : (CssValue<ShrinkBuilder>?)null;
        set => Set(47, value?.ToString());
    }

    /// <summary>Gets or sets the Gap preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<GapBuilder>? Gap
    {
        get => _values[48] is { } value ? CssValue<GapBuilder>.Raw(value) : (CssValue<GapBuilder>?)null;
        set => Set(48, value?.ToString());
    }

    /// <summary>Gets or sets the Space preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<SpaceBuilder>? Space
    {
        get => _values[49] is { } value ? CssValue<SpaceBuilder>.Raw(value) : (CssValue<SpaceBuilder>?)null;
        set => Set(49, value?.ToString());
    }

    /// <summary>Gets or sets the Divide preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<DivideBuilder>? Divide
    {
        get => _values[50] is { } value ? CssValue<DivideBuilder>.Raw(value) : (CssValue<DivideBuilder>?)null;
        set => Set(50, value?.ToString());
    }

    /// <summary>Gets or sets the ContentAlign preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ContentAlignBuilder>? ContentAlign
    {
        get => _values[51] is { } value ? CssValue<ContentAlignBuilder>.Raw(value) : (CssValue<ContentAlignBuilder>?)null;
        set => Set(51, value?.ToString());
    }

    /// <summary>Gets or sets the ItemsAlign preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ItemsBuilder>? ItemsAlign
    {
        get => _values[52] is { } value ? CssValue<ItemsBuilder>.Raw(value) : (CssValue<ItemsBuilder>?)null;
        set => Set(52, value?.ToString());
    }

    /// <summary>Gets or sets the Justify preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<JustifyBuilder>? Justify
    {
        get => _values[53] is { } value ? CssValue<JustifyBuilder>.Raw(value) : (CssValue<JustifyBuilder>?)null;
        set => Set(53, value?.ToString());
    }

    /// <summary>Gets or sets the SelfAlign preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<SelfBuilder>? SelfAlign
    {
        get => _values[54] is { } value ? CssValue<SelfBuilder>.Raw(value) : (CssValue<SelfBuilder>?)null;
        set => Set(54, value?.ToString());
    }

    /// <summary>Gets or sets the JustifyItemsAlign preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<JustifyItemsAlignBuilder>? JustifyItemsAlign
    {
        get => _values[55] is { } value ? CssValue<JustifyItemsAlignBuilder>.Raw(value) : (CssValue<JustifyItemsAlignBuilder>?)null;
        set => Set(55, value?.ToString());
    }

    /// <summary>Gets or sets the JustifySelfAlign preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<JustifySelfAlignBuilder>? JustifySelfAlign
    {
        get => _values[56] is { } value ? CssValue<JustifySelfAlignBuilder>.Raw(value) : (CssValue<JustifySelfAlignBuilder>?)null;
        set => Set(56, value?.ToString());
    }

    /// <summary>Gets or sets the ColStart preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ColStartBuilder>? ColStart
    {
        get => _values[57] is { } value ? CssValue<ColStartBuilder>.Raw(value) : (CssValue<ColStartBuilder>?)null;
        set => Set(57, value?.ToString());
    }

    /// <summary>Gets or sets the RowSpan preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<RowSpanBuilder>? RowSpan
    {
        get => _values[58] is { } value ? CssValue<RowSpanBuilder>.Raw(value) : (CssValue<RowSpanBuilder>?)null;
        set => Set(58, value?.ToString());
    }

    /// <summary>Gets or sets the RowStart preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<RowStartBuilder>? RowStart
    {
        get => _values[59] is { } value ? CssValue<RowStartBuilder>.Raw(value) : (CssValue<RowStartBuilder>?)null;
        set => Set(59, value?.ToString());
    }

    /// <summary>Gets or sets the Opacity preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<OpacityBuilder>? Opacity
    {
        get => _values[60] is { } value ? CssValue<OpacityBuilder>.Raw(value) : (CssValue<OpacityBuilder>?)null;
        set => Set(60, value?.ToString());
    }

    /// <summary>Gets or sets the ZIndex preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ZIndexBuilder>? ZIndex
    {
        get => _values[61] is { } value ? CssValue<ZIndexBuilder>.Raw(value) : (CssValue<ZIndexBuilder>?)null;
        set => Set(61, value?.ToString());
    }

    /// <summary>Gets or sets the PointerEvents preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<PointerEventsBuilder>? PointerEvents
    {
        get => _values[62] is { } value ? CssValue<PointerEventsBuilder>.Raw(value) : (CssValue<PointerEventsBuilder>?)null;
        set => Set(62, value?.ToString());
    }

    /// <summary>Gets or sets the UserSelect preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<UserSelectBuilder>? UserSelect
    {
        get => _values[63] is { } value ? CssValue<UserSelectBuilder>.Raw(value) : (CssValue<UserSelectBuilder>?)null;
        set => Set(63, value?.ToString());
    }

    /// <summary>Gets or sets the Cursor preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<CursorBuilder>? Cursor
    {
        get => _values[64] is { } value ? CssValue<CursorBuilder>.Raw(value) : (CssValue<CursorBuilder>?)null;
        set => Set(64, value?.ToString());
    }

    /// <summary>Gets or sets the ScreenReader preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ScreenReaderBuilder>? ScreenReader
    {
        get => _values[65] is { } value ? CssValue<ScreenReaderBuilder>.Raw(value) : (CssValue<ScreenReaderBuilder>?)null;
        set => Set(65, value?.ToString());
    }

    /// <summary>Gets or sets the BackgroundColor preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<BackgroundColorBuilder>? BackgroundColor
    {
        get => _values[66] is { } value ? CssValue<BackgroundColorBuilder>.Raw(value) : (CssValue<BackgroundColorBuilder>?)null;
        set => Set(66, value?.ToString());
    }

    /// <summary>Gets or sets the BorderColor preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<BorderColorBuilder>? BorderColor
    {
        get => _values[67] is { } value ? CssValue<BorderColorBuilder>.Raw(value) : (CssValue<BorderColorBuilder>?)null;
        set => Set(67, value?.ToString());
    }

    /// <summary>Gets or sets the Border preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<BorderBuilder>? Border
    {
        get => _values[68] is { } value ? CssValue<BorderBuilder>.Raw(value) : (CssValue<BorderBuilder>?)null;
        set => Set(68, value?.ToString());
    }

    /// <summary>Gets or sets the BorderStyle preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<BorderStyleBuilder>? BorderStyle
    {
        get => _values[69] is { } value ? CssValue<BorderStyleBuilder>.Raw(value) : (CssValue<BorderStyleBuilder>?)null;
        set => Set(69, value?.ToString());
    }

    /// <summary>Gets or sets the Rounded preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<RoundedBuilder>? Rounded
    {
        get => _values[70] is { } value ? CssValue<RoundedBuilder>.Raw(value) : (CssValue<RoundedBuilder>?)null;
        set => Set(70, value?.ToString());
    }

    /// <summary>Gets or sets the RingColor preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<RingColorBuilder>? RingColor
    {
        get => _values[71] is { } value ? CssValue<RingColorBuilder>.Raw(value) : (CssValue<RingColorBuilder>?)null;
        set => Set(71, value?.ToString());
    }

    /// <summary>Gets or sets the Ring preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<RingBuilder>? Ring
    {
        get => _values[72] is { } value ? CssValue<RingBuilder>.Raw(value) : (CssValue<RingBuilder>?)null;
        set => Set(72, value?.ToString());
    }

    /// <summary>Gets or sets the RingOffset preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<RingOffsetBuilder>? RingOffset
    {
        get => _values[73] is { } value ? CssValue<RingOffsetBuilder>.Raw(value) : (CssValue<RingOffsetBuilder>?)null;
        set => Set(73, value?.ToString());
    }

    /// <summary>Gets or sets the Shadow preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ShadowBuilder>? Shadow
    {
        get => _values[74] is { } value ? CssValue<ShadowBuilder>.Raw(value) : (CssValue<ShadowBuilder>?)null;
        set => Set(74, value?.ToString());
    }

    /// <summary>Gets or sets the BackdropFilter preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<BackdropFilterBuilder>? BackdropFilter
    {
        get => _values[75] is { } value ? CssValue<BackdropFilterBuilder>.Raw(value) : (CssValue<BackdropFilterBuilder>?)null;
        set => Set(75, value?.ToString());
    }

    /// <summary>Gets or sets the BackdropBlur preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<BackdropBlurBuilder>? BackdropBlur
    {
        get => _values[76] is { } value ? CssValue<BackdropBlurBuilder>.Raw(value) : (CssValue<BackdropBlurBuilder>?)null;
        set => Set(76, value?.ToString());
    }

    /// <summary>Gets or sets the BackdropBrightness preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<BackdropBrightnessBuilder>? BackdropBrightness
    {
        get => _values[77] is { } value ? CssValue<BackdropBrightnessBuilder>.Raw(value) : (CssValue<BackdropBrightnessBuilder>?)null;
        set => Set(77, value?.ToString());
    }

    /// <summary>Gets or sets the BackdropContrast preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<BackdropContrastBuilder>? BackdropContrast
    {
        get => _values[78] is { } value ? CssValue<BackdropContrastBuilder>.Raw(value) : (CssValue<BackdropContrastBuilder>?)null;
        set => Set(78, value?.ToString());
    }

    /// <summary>Gets or sets the BackdropGrayscale preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<BackdropGrayscaleBuilder>? BackdropGrayscale
    {
        get => _values[79] is { } value ? CssValue<BackdropGrayscaleBuilder>.Raw(value) : (CssValue<BackdropGrayscaleBuilder>?)null;
        set => Set(79, value?.ToString());
    }

    /// <summary>Gets or sets the BackdropHueRotate preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<BackdropHueRotateBuilder>? BackdropHueRotate
    {
        get => _values[80] is { } value ? CssValue<BackdropHueRotateBuilder>.Raw(value) : (CssValue<BackdropHueRotateBuilder>?)null;
        set => Set(80, value?.ToString());
    }

    /// <summary>Gets or sets the BackdropInvert preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<BackdropInvertBuilder>? BackdropInvert
    {
        get => _values[81] is { } value ? CssValue<BackdropInvertBuilder>.Raw(value) : (CssValue<BackdropInvertBuilder>?)null;
        set => Set(81, value?.ToString());
    }

    /// <summary>Gets or sets the BackdropOpacity preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<BackdropOpacityBuilder>? BackdropOpacity
    {
        get => _values[82] is { } value ? CssValue<BackdropOpacityBuilder>.Raw(value) : (CssValue<BackdropOpacityBuilder>?)null;
        set => Set(82, value?.ToString());
    }

    /// <summary>Gets or sets the BackdropSaturate preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<BackdropSaturateBuilder>? BackdropSaturate
    {
        get => _values[83] is { } value ? CssValue<BackdropSaturateBuilder>.Raw(value) : (CssValue<BackdropSaturateBuilder>?)null;
        set => Set(83, value?.ToString());
    }

    /// <summary>Gets or sets the BackdropSepia preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<BackdropSepiaBuilder>? BackdropSepia
    {
        get => _values[84] is { } value ? CssValue<BackdropSepiaBuilder>.Raw(value) : (CssValue<BackdropSepiaBuilder>?)null;
        set => Set(84, value?.ToString());
    }

    /// <summary>Gets or sets the Filter preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<FilterBuilder>? Filter
    {
        get => _values[85] is { } value ? CssValue<FilterBuilder>.Raw(value) : (CssValue<FilterBuilder>?)null;
        set => Set(85, value?.ToString());
    }

    /// <summary>Gets or sets the Blur preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<BlurBuilder>? Blur
    {
        get => _values[86] is { } value ? CssValue<BlurBuilder>.Raw(value) : (CssValue<BlurBuilder>?)null;
        set => Set(86, value?.ToString());
    }

    /// <summary>Gets or sets the Brightness preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<BrightnessBuilder>? Brightness
    {
        get => _values[87] is { } value ? CssValue<BrightnessBuilder>.Raw(value) : (CssValue<BrightnessBuilder>?)null;
        set => Set(87, value?.ToString());
    }

    /// <summary>Gets or sets the Contrast preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ContrastBuilder>? Contrast
    {
        get => _values[88] is { } value ? CssValue<ContrastBuilder>.Raw(value) : (CssValue<ContrastBuilder>?)null;
        set => Set(88, value?.ToString());
    }

    /// <summary>Gets or sets the DropShadow preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<DropShadowBuilder>? DropShadow
    {
        get => _values[89] is { } value ? CssValue<DropShadowBuilder>.Raw(value) : (CssValue<DropShadowBuilder>?)null;
        set => Set(89, value?.ToString());
    }

    /// <summary>Gets or sets the DropShadowColor preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<DropShadowColorBuilder>? DropShadowColor
    {
        get => _values[90] is { } value ? CssValue<DropShadowColorBuilder>.Raw(value) : (CssValue<DropShadowColorBuilder>?)null;
        set => Set(90, value?.ToString());
    }

    /// <summary>Gets or sets the Grayscale preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<GrayscaleBuilder>? Grayscale
    {
        get => _values[91] is { } value ? CssValue<GrayscaleBuilder>.Raw(value) : (CssValue<GrayscaleBuilder>?)null;
        set => Set(91, value?.ToString());
    }

    /// <summary>Gets or sets the HueRotate preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<HueRotateBuilder>? HueRotate
    {
        get => _values[92] is { } value ? CssValue<HueRotateBuilder>.Raw(value) : (CssValue<HueRotateBuilder>?)null;
        set => Set(92, value?.ToString());
    }

    /// <summary>Gets or sets the Invert preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<InvertBuilder>? Invert
    {
        get => _values[93] is { } value ? CssValue<InvertBuilder>.Raw(value) : (CssValue<InvertBuilder>?)null;
        set => Set(93, value?.ToString());
    }

    /// <summary>Gets or sets the Saturate preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<SaturateBuilder>? Saturate
    {
        get => _values[94] is { } value ? CssValue<SaturateBuilder>.Raw(value) : (CssValue<SaturateBuilder>?)null;
        set => Set(94, value?.ToString());
    }

    /// <summary>Gets or sets the Sepia preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<SepiaBuilder>? Sepia
    {
        get => _values[95] is { } value ? CssValue<SepiaBuilder>.Raw(value) : (CssValue<SepiaBuilder>?)null;
        set => Set(95, value?.ToString());
    }

    /// <summary>Gets or sets the Resize preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ResizeBuilder>? Resize
    {
        get => _values[96] is { } value ? CssValue<ResizeBuilder>.Raw(value) : (CssValue<ResizeBuilder>?)null;
        set => Set(96, value?.ToString());
    }

    /// <summary>Gets or sets the Transform preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<TransformBuilder>? Transform
    {
        get => _values[97] is { } value ? CssValue<TransformBuilder>.Raw(value) : (CssValue<TransformBuilder>?)null;
        set => Set(97, value?.ToString());
    }

    /// <summary>Gets or sets the Animation preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<AnimationBuilder>? Animation
    {
        get => _values[98] is { } value ? CssValue<AnimationBuilder>.Raw(value) : (CssValue<AnimationBuilder>?)null;
        set => Set(98, value?.ToString());
    }

    /// <summary>Gets or sets the Duration preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<DurationBuilder>? Duration
    {
        get => _values[99] is { } value ? CssValue<DurationBuilder>.Raw(value) : (CssValue<DurationBuilder>?)null;
        set => Set(99, value?.ToString());
    }

    /// <summary>Gets or sets the Transition preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<TransitionBuilder>? Transition
    {
        get => _values[100] is { } value ? CssValue<TransitionBuilder>.Raw(value) : (CssValue<TransitionBuilder>?)null;
        set => Set(100, value?.ToString());
    }

    internal void Clear()
    {
        ClearSlots(_assigned0, 0);
        ClearSlots(_assigned1, 64);
        _assigned0 = _assigned1 = 0;
        unchecked { Revision++; }
    }

    private void ClearSlots(ulong assigned, int offset)
    {
        while (assigned != 0)
        {
            int index = BitOperations.TrailingZeroCount(assigned) + offset;
            _values[index] = null;
            assigned &= assigned - 1;
        }
    }

    internal void CopyTo(QuarkPresetContext target)
    {
        CopySlots(target, _assigned0, 0);
        CopySlots(target, _assigned1, 64);
    }

    private void CopySlots(QuarkPresetContext target, ulong assigned, int offset)
    {
        while (assigned != 0)
        {
            int index = BitOperations.TrailingZeroCount(assigned) + offset;
            target.Set(index, _values[index]);
            assigned &= assigned - 1;
        }
    }
}
