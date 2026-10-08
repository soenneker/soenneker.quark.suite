using System.Numerics;

namespace Soenneker.Quark;

/// <summary>Collects typed preset assignments without boxing. Each evaluation starts with an empty context.</summary>
public sealed class QuarkPresetContext
{
    private readonly string?[] _values = new string?[(int)PresetProperty.Count];
    private readonly ulong[] _assigned = new ulong[((int)PresetProperty.Count + 63) / 64];
    internal int Revision { get; private set; }

    internal bool HasValue(PresetProperty property) => _values[(int)property] is not null;

    private void Set(int index, string? value)
    {
        _values[index] = value;
        _assigned[index / 64] |= 1UL << (index % 64);
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

    /// <summary>Gets or sets the WordBreak preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<WordBreakBuilder>? WordBreak
    {
        get => _values[22] is { } value ? CssValue<WordBreakBuilder>.Raw(value) : (CssValue<WordBreakBuilder>?)null;
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

    /// <summary>Gets or sets the OutlineStyle preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<OutlineStyleBuilder>? OutlineStyle
    {
        get => _values[103] is { } value ? CssValue<OutlineStyleBuilder>.Raw(value) : (CssValue<OutlineStyleBuilder>?)null;
        set => Set(103, value?.ToString());
    }

    /// <summary>Gets or sets the AccentColor preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<AccentColorBuilder>? AccentColor
    {
        get => _values[104] is { } value ? CssValue<AccentColorBuilder>.Raw(value) : (CssValue<AccentColorBuilder>?)null;
        set => Set(104, value?.ToString());
    }

    /// <summary>Gets or sets the NativeAppearance preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<AppearanceBuilder>? NativeAppearance
    {
        get => _values[105] is { } value ? CssValue<AppearanceBuilder>.Raw(value) : (CssValue<AppearanceBuilder>?)null;
        set => Set(105, value?.ToString());
    }

    /// <summary>Gets or sets the AspectRatio preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<AspectRatioBuilder>? AspectRatio
    {
        get => _values[106] is { } value ? CssValue<AspectRatioBuilder>.Raw(value) : (CssValue<AspectRatioBuilder>?)null;
        set => Set(106, value?.ToString());
    }

    /// <summary>Gets or sets the AutoCols preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<AutoColsBuilder>? AutoCols
    {
        get => _values[107] is { } value ? CssValue<AutoColsBuilder>.Raw(value) : (CssValue<AutoColsBuilder>?)null;
        set => Set(107, value?.ToString());
    }

    /// <summary>Gets or sets the AutoRows preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<AutoRowsBuilder>? AutoRows
    {
        get => _values[108] is { } value ? CssValue<AutoRowsBuilder>.Raw(value) : (CssValue<AutoRowsBuilder>?)null;
        set => Set(108, value?.ToString());
    }

    /// <summary>Gets or sets the BackfaceVisibility preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<BackfaceVisibilityBuilder>? BackfaceVisibility
    {
        get => _values[109] is { } value ? CssValue<BackfaceVisibilityBuilder>.Raw(value) : (CssValue<BackfaceVisibilityBuilder>?)null;
        set => Set(109, value?.ToString());
    }

    /// <summary>Gets or sets the BackgroundAttachment preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<BackgroundAttachmentBuilder>? BackgroundAttachment
    {
        get => _values[110] is { } value ? CssValue<BackgroundAttachmentBuilder>.Raw(value) : (CssValue<BackgroundAttachmentBuilder>?)null;
        set => Set(110, value?.ToString());
    }

    /// <summary>Gets or sets the BackgroundBlendMode preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<BackgroundBlendModeBuilder>? BackgroundBlendMode
    {
        get => _values[111] is { } value ? CssValue<BackgroundBlendModeBuilder>.Raw(value) : (CssValue<BackgroundBlendModeBuilder>?)null;
        set => Set(111, value?.ToString());
    }

    /// <summary>Gets or sets the BackgroundClip preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<BackgroundClipBuilder>? BackgroundClip
    {
        get => _values[112] is { } value ? CssValue<BackgroundClipBuilder>.Raw(value) : (CssValue<BackgroundClipBuilder>?)null;
        set => Set(112, value?.ToString());
    }

    /// <summary>Gets or sets the BackgroundImage preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<BackgroundImageBuilder>? BackgroundImage
    {
        get => _values[113] is { } value ? CssValue<BackgroundImageBuilder>.Raw(value) : (CssValue<BackgroundImageBuilder>?)null;
        set => Set(113, value?.ToString());
    }

    /// <summary>Gets or sets the BackgroundOrigin preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<BackgroundOriginBuilder>? BackgroundOrigin
    {
        get => _values[114] is { } value ? CssValue<BackgroundOriginBuilder>.Raw(value) : (CssValue<BackgroundOriginBuilder>?)null;
        set => Set(114, value?.ToString());
    }

    /// <summary>Gets or sets the BackgroundPosition preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<BackgroundPositionBuilder>? BackgroundPosition
    {
        get => _values[115] is { } value ? CssValue<BackgroundPositionBuilder>.Raw(value) : (CssValue<BackgroundPositionBuilder>?)null;
        set => Set(115, value?.ToString());
    }

    /// <summary>Gets or sets the BackgroundRepeat preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<BackgroundRepeatBuilder>? BackgroundRepeat
    {
        get => _values[116] is { } value ? CssValue<BackgroundRepeatBuilder>.Raw(value) : (CssValue<BackgroundRepeatBuilder>?)null;
        set => Set(116, value?.ToString());
    }

    /// <summary>Gets or sets the BackgroundSize preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<BackgroundSizeBuilder>? BackgroundSize
    {
        get => _values[117] is { } value ? CssValue<BackgroundSizeBuilder>.Raw(value) : (CssValue<BackgroundSizeBuilder>?)null;
        set => Set(117, value?.ToString());
    }

    /// <summary>Gets or sets the BlockSize preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<BlockSizeBuilder>? BlockSize
    {
        get => _values[118] is { } value ? CssValue<BlockSizeBuilder>.Raw(value) : (CssValue<BlockSizeBuilder>?)null;
        set => Set(118, value?.ToString());
    }

    /// <summary>Gets or sets the BorderCollapse preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<BorderCollapseBuilder>? BorderCollapse
    {
        get => _values[119] is { } value ? CssValue<BorderCollapseBuilder>.Raw(value) : (CssValue<BorderCollapseBuilder>?)null;
        set => Set(119, value?.ToString());
    }

    /// <summary>Gets or sets the BorderSpacing preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<BorderSpacingBuilder>? BorderSpacing
    {
        get => _values[120] is { } value ? CssValue<BorderSpacingBuilder>.Raw(value) : (CssValue<BorderSpacingBuilder>?)null;
        set => Set(120, value?.ToString());
    }

    /// <summary>Gets or sets the BoxDecorationBreak preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<BoxDecorationBreakBuilder>? BoxDecorationBreak
    {
        get => _values[121] is { } value ? CssValue<BoxDecorationBreakBuilder>.Raw(value) : (CssValue<BoxDecorationBreakBuilder>?)null;
        set => Set(121, value?.ToString());
    }

    /// <summary>Gets or sets the BoxSizing preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<BoxSizingBuilder>? BoxSizing
    {
        get => _values[122] is { } value ? CssValue<BoxSizingBuilder>.Raw(value) : (CssValue<BoxSizingBuilder>?)null;
        set => Set(122, value?.ToString());
    }

    /// <summary>Gets or sets the BreakAfter preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<BreakAfterBuilder>? BreakAfter
    {
        get => _values[123] is { } value ? CssValue<BreakAfterBuilder>.Raw(value) : (CssValue<BreakAfterBuilder>?)null;
        set => Set(123, value?.ToString());
    }

    /// <summary>Gets or sets the BreakBefore preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<BreakBeforeBuilder>? BreakBefore
    {
        get => _values[124] is { } value ? CssValue<BreakBeforeBuilder>.Raw(value) : (CssValue<BreakBeforeBuilder>?)null;
        set => Set(124, value?.ToString());
    }

    /// <summary>Gets or sets the BreakInside preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<BreakInsideBuilder>? BreakInside
    {
        get => _values[125] is { } value ? CssValue<BreakInsideBuilder>.Raw(value) : (CssValue<BreakInsideBuilder>?)null;
        set => Set(125, value?.ToString());
    }

    /// <summary>Gets or sets the CaptionSide preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<CaptionSideBuilder>? CaptionSide
    {
        get => _values[126] is { } value ? CssValue<CaptionSideBuilder>.Raw(value) : (CssValue<CaptionSideBuilder>?)null;
        set => Set(126, value?.ToString());
    }

    /// <summary>Gets or sets the CaretColor preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<CaretColorBuilder>? CaretColor
    {
        get => _values[127] is { } value ? CssValue<CaretColorBuilder>.Raw(value) : (CssValue<CaretColorBuilder>?)null;
        set => Set(127, value?.ToString());
    }

    /// <summary>Gets or sets the FloatClear preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ClearBuilder>? FloatClear
    {
        get => _values[128] is { } value ? CssValue<ClearBuilder>.Raw(value) : (CssValue<ClearBuilder>?)null;
        set => Set(128, value?.ToString());
    }

    /// <summary>Gets or sets the ClipPath preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ClipPathBuilder>? ClipPath
    {
        get => _values[129] is { } value ? CssValue<ClipPathBuilder>.Raw(value) : (CssValue<ClipPathBuilder>?)null;
        set => Set(129, value?.ToString());
    }

    /// <summary>Gets or sets the ColEnd preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ColEndBuilder>? ColEnd
    {
        get => _values[130] is { } value ? CssValue<ColEndBuilder>.Raw(value) : (CssValue<ColEndBuilder>?)null;
        set => Set(130, value?.ToString());
    }

    /// <summary>Gets or sets the ColorScheme preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ColorSchemeBuilder>? ColorScheme
    {
        get => _values[131] is { } value ? CssValue<ColorSchemeBuilder>.Raw(value) : (CssValue<ColorSchemeBuilder>?)null;
        set => Set(131, value?.ToString());
    }

    /// <summary>Gets or sets the ColumnCount preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ColumnsBuilder>? ColumnCount
    {
        get => _values[132] is { } value ? CssValue<ColumnsBuilder>.Raw(value) : (CssValue<ColumnsBuilder>?)null;
        set => Set(132, value?.ToString());
    }

    /// <summary>Gets or sets the ContainerQuery preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ContainerTypeBuilder>? ContainerQuery
    {
        get => _values[133] is { } value ? CssValue<ContainerTypeBuilder>.Raw(value) : (CssValue<ContainerTypeBuilder>?)null;
        set => Set(133, value?.ToString());
    }

    /// <summary>Gets or sets the Contain preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ContainBuilder>? Contain
    {
        get => _values[134] is { } value ? CssValue<ContainBuilder>.Raw(value) : (CssValue<ContainBuilder>?)null;
        set => Set(134, value?.ToString());
    }

    /// <summary>Gets or sets the GeneratedContent preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ContentBuilder>? GeneratedContent
    {
        get => _values[135] is { } value ? CssValue<ContentBuilder>.Raw(value) : (CssValue<ContentBuilder>?)null;
        set => Set(135, value?.ToString());
    }

    /// <summary>Gets or sets the DecorationColor preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<DecorationColorBuilder>? DecorationColor
    {
        get => _values[136] is { } value ? CssValue<DecorationColorBuilder>.Raw(value) : (CssValue<DecorationColorBuilder>?)null;
        set => Set(136, value?.ToString());
    }

    /// <summary>Gets or sets the DecorationStyle preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<DecorationStyleBuilder>? DecorationStyle
    {
        get => _values[137] is { } value ? CssValue<DecorationStyleBuilder>.Raw(value) : (CssValue<DecorationStyleBuilder>?)null;
        set => Set(137, value?.ToString());
    }

    /// <summary>Gets or sets the DecorationThickness preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<DecorationThicknessBuilder>? DecorationThickness
    {
        get => _values[138] is { } value ? CssValue<DecorationThicknessBuilder>.Raw(value) : (CssValue<DecorationThicknessBuilder>?)null;
        set => Set(138, value?.ToString());
    }

    /// <summary>Gets or sets the TransitionDelay preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<DelayBuilder>? TransitionDelay
    {
        get => _values[139] is { } value ? CssValue<DelayBuilder>.Raw(value) : (CssValue<DelayBuilder>?)null;
        set => Set(139, value?.ToString());
    }

    /// <summary>Gets or sets the Ease preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<EaseBuilder>? Ease
    {
        get => _values[140] is { } value ? CssValue<EaseBuilder>.Raw(value) : (CssValue<EaseBuilder>?)null;
        set => Set(140, value?.ToString());
    }

    /// <summary>Gets or sets the FieldSizing preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<FieldSizingBuilder>? FieldSizing
    {
        get => _values[141] is { } value ? CssValue<FieldSizingBuilder>.Raw(value) : (CssValue<FieldSizingBuilder>?)null;
        set => Set(141, value?.ToString());
    }

    /// <summary>Gets or sets the FillRule preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<FillRuleBuilder>? FillRule
    {
        get => _values[142] is { } value ? CssValue<FillRuleBuilder>.Raw(value) : (CssValue<FillRuleBuilder>?)null;
        set => Set(142, value?.ToString());
    }

    /// <summary>Gets or sets the Fill preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<FillBuilder>? Fill
    {
        get => _values[143] is { } value ? CssValue<FillBuilder>.Raw(value) : (CssValue<FillBuilder>?)null;
        set => Set(143, value?.ToString());
    }

    /// <summary>Gets or sets the FlexBasis preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<FlexBasisBuilder>? FlexBasis
    {
        get => _values[144] is { } value ? CssValue<FlexBasisBuilder>.Raw(value) : (CssValue<FlexBasisBuilder>?)null;
        set => Set(144, value?.ToString());
    }

    /// <summary>Gets or sets the FontFeatureSettings preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<FontFeatureSettingsBuilder>? FontFeatureSettings
    {
        get => _values[145] is { } value ? CssValue<FontFeatureSettingsBuilder>.Raw(value) : (CssValue<FontFeatureSettingsBuilder>?)null;
        set => Set(145, value?.ToString());
    }

    /// <summary>Gets or sets the FontSmoothing preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<FontSmoothingBuilder>? FontSmoothing
    {
        get => _values[146] is { } value ? CssValue<FontSmoothingBuilder>.Raw(value) : (CssValue<FontSmoothingBuilder>?)null;
        set => Set(146, value?.ToString());
    }

    /// <summary>Gets or sets the FontStretch preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<FontStretchBuilder>? FontStretch
    {
        get => _values[147] is { } value ? CssValue<FontStretchBuilder>.Raw(value) : (CssValue<FontStretchBuilder>?)null;
        set => Set(147, value?.ToString());
    }

    /// <summary>Gets or sets the ForcedColorAdjust preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ForcedColorAdjustBuilder>? ForcedColorAdjust
    {
        get => _values[148] is { } value ? CssValue<ForcedColorAdjustBuilder>.Raw(value) : (CssValue<ForcedColorAdjustBuilder>?)null;
        set => Set(148, value?.ToString());
    }

    /// <summary>Gets or sets the BackgroundGradient preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<GradientBuilder>? BackgroundGradient
    {
        get => _values[149] is { } value ? CssValue<GradientBuilder>.Raw(value) : (CssValue<GradientBuilder>?)null;
        set => Set(149, value?.ToString());
    }

    /// <summary>Gets or sets the GridAutoFlow preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<GridAutoFlowBuilder>? GridAutoFlow
    {
        get => _values[150] is { } value ? CssValue<GridAutoFlowBuilder>.Raw(value) : (CssValue<GridAutoFlowBuilder>?)null;
        set => Set(150, value?.ToString());
    }

    /// <summary>Gets or sets the GridColumns preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<GridColsBuilder>? GridColumns
    {
        get => _values[151] is { } value ? CssValue<GridColsBuilder>.Raw(value) : (CssValue<GridColsBuilder>?)null;
        set => Set(151, value?.ToString());
    }

    /// <summary>Gets or sets the GridRows preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<GridRowsBuilder>? GridRows
    {
        get => _values[152] is { } value ? CssValue<GridRowsBuilder>.Raw(value) : (CssValue<GridRowsBuilder>?)null;
        set => Set(152, value?.ToString());
    }

    /// <summary>Gets or sets the ColumnSpan preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ColumnSpanBuilder>? ColumnSpan
    {
        get => _values[153] is { } value ? CssValue<ColumnSpanBuilder>.Raw(value) : (CssValue<ColumnSpanBuilder>?)null;
        set => Set(153, value?.ToString());
    }

    /// <summary>Gets or sets the Hyphen preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<HyphenBuilder>? Hyphen
    {
        get => _values[154] is { } value ? CssValue<HyphenBuilder>.Raw(value) : (CssValue<HyphenBuilder>?)null;
        set => Set(154, value?.ToString());
    }

    /// <summary>Gets or sets the InlineSize preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<InlineSizeBuilder>? InlineSize
    {
        get => _values[155] is { } value ? CssValue<InlineSizeBuilder>.Raw(value) : (CssValue<InlineSizeBuilder>?)null;
        set => Set(155, value?.ToString());
    }

    /// <summary>Gets or sets the InsetBlockEnd preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<InsetBlockEndBuilder>? InsetBlockEnd
    {
        get => _values[156] is { } value ? CssValue<InsetBlockEndBuilder>.Raw(value) : (CssValue<InsetBlockEndBuilder>?)null;
        set => Set(156, value?.ToString());
    }

    /// <summary>Gets or sets the InsetBlockStart preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<InsetBlockStartBuilder>? InsetBlockStart
    {
        get => _values[157] is { } value ? CssValue<InsetBlockStartBuilder>.Raw(value) : (CssValue<InsetBlockStartBuilder>?)null;
        set => Set(157, value?.ToString());
    }

    /// <summary>Gets or sets the InsetEnd preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<InsetEndBuilder>? InsetEnd
    {
        get => _values[158] is { } value ? CssValue<InsetEndBuilder>.Raw(value) : (CssValue<InsetEndBuilder>?)null;
        set => Set(158, value?.ToString());
    }

    /// <summary>Gets or sets the InsetRingColor preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<InsetRingColorBuilder>? InsetRingColor
    {
        get => _values[159] is { } value ? CssValue<InsetRingColorBuilder>.Raw(value) : (CssValue<InsetRingColorBuilder>?)null;
        set => Set(159, value?.ToString());
    }

    /// <summary>Gets or sets the InsetRing preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<InsetRingBuilder>? InsetRing
    {
        get => _values[160] is { } value ? CssValue<InsetRingBuilder>.Raw(value) : (CssValue<InsetRingBuilder>?)null;
        set => Set(160, value?.ToString());
    }

    /// <summary>Gets or sets the InsetShadowColor preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<InsetShadowColorBuilder>? InsetShadowColor
    {
        get => _values[161] is { } value ? CssValue<InsetShadowColorBuilder>.Raw(value) : (CssValue<InsetShadowColorBuilder>?)null;
        set => Set(161, value?.ToString());
    }

    /// <summary>Gets or sets the InsetShadow preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<InsetShadowBuilder>? InsetShadow
    {
        get => _values[162] is { } value ? CssValue<InsetShadowBuilder>.Raw(value) : (CssValue<InsetShadowBuilder>?)null;
        set => Set(162, value?.ToString());
    }

    /// <summary>Gets or sets the InsetStart preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<InsetStartBuilder>? InsetStart
    {
        get => _values[163] is { } value ? CssValue<InsetStartBuilder>.Raw(value) : (CssValue<InsetStartBuilder>?)null;
        set => Set(163, value?.ToString());
    }

    /// <summary>Gets or sets the Isolation preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<IsolationBuilder>? Isolation
    {
        get => _values[164] is { } value ? CssValue<IsolationBuilder>.Raw(value) : (CssValue<IsolationBuilder>?)null;
        set => Set(164, value?.ToString());
    }

    /// <summary>Gets or sets the ListStyleImage preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ListStyleImageBuilder>? ListStyleImage
    {
        get => _values[165] is { } value ? CssValue<ListStyleImageBuilder>.Raw(value) : (CssValue<ListStyleImageBuilder>?)null;
        set => Set(165, value?.ToString());
    }

    /// <summary>Gets or sets the ListStylePosition preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ListStylePositionBuilder>? ListStylePosition
    {
        get => _values[166] is { } value ? CssValue<ListStylePositionBuilder>.Raw(value) : (CssValue<ListStylePositionBuilder>?)null;
        set => Set(166, value?.ToString());
    }

    /// <summary>Gets or sets the ListStyleType preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ListStyleTypeBuilder>? ListStyleType
    {
        get => _values[167] is { } value ? CssValue<ListStyleTypeBuilder>.Raw(value) : (CssValue<ListStyleTypeBuilder>?)null;
        set => Set(167, value?.ToString());
    }

    /// <summary>Gets or sets the MaskClip preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<MaskClipBuilder>? MaskClip
    {
        get => _values[168] is { } value ? CssValue<MaskClipBuilder>.Raw(value) : (CssValue<MaskClipBuilder>?)null;
        set => Set(168, value?.ToString());
    }

    /// <summary>Gets or sets the MaskComposite preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<MaskCompositeBuilder>? MaskComposite
    {
        get => _values[169] is { } value ? CssValue<MaskCompositeBuilder>.Raw(value) : (CssValue<MaskCompositeBuilder>?)null;
        set => Set(169, value?.ToString());
    }

    /// <summary>Gets or sets the MaskImage preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<MaskImageBuilder>? MaskImage
    {
        get => _values[170] is { } value ? CssValue<MaskImageBuilder>.Raw(value) : (CssValue<MaskImageBuilder>?)null;
        set => Set(170, value?.ToString());
    }

    /// <summary>Gets or sets the MaskMode preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<MaskModeBuilder>? MaskMode
    {
        get => _values[171] is { } value ? CssValue<MaskModeBuilder>.Raw(value) : (CssValue<MaskModeBuilder>?)null;
        set => Set(171, value?.ToString());
    }

    /// <summary>Gets or sets the MaskOrigin preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<MaskOriginBuilder>? MaskOrigin
    {
        get => _values[172] is { } value ? CssValue<MaskOriginBuilder>.Raw(value) : (CssValue<MaskOriginBuilder>?)null;
        set => Set(172, value?.ToString());
    }

    /// <summary>Gets or sets the MaskPosition preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<MaskPositionBuilder>? MaskPosition
    {
        get => _values[173] is { } value ? CssValue<MaskPositionBuilder>.Raw(value) : (CssValue<MaskPositionBuilder>?)null;
        set => Set(173, value?.ToString());
    }

    /// <summary>Gets or sets the MaskRepeat preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<MaskRepeatBuilder>? MaskRepeat
    {
        get => _values[174] is { } value ? CssValue<MaskRepeatBuilder>.Raw(value) : (CssValue<MaskRepeatBuilder>?)null;
        set => Set(174, value?.ToString());
    }

    /// <summary>Gets or sets the MaskSize preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<MaskSizeBuilder>? MaskSize
    {
        get => _values[175] is { } value ? CssValue<MaskSizeBuilder>.Raw(value) : (CssValue<MaskSizeBuilder>?)null;
        set => Set(175, value?.ToString());
    }

    /// <summary>Gets or sets the MaskType preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<MaskTypeBuilder>? MaskType
    {
        get => _values[176] is { } value ? CssValue<MaskTypeBuilder>.Raw(value) : (CssValue<MaskTypeBuilder>?)null;
        set => Set(176, value?.ToString());
    }

    /// <summary>Gets or sets the MaxBlockSize preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<MaxBlockSizeBuilder>? MaxBlockSize
    {
        get => _values[177] is { } value ? CssValue<MaxBlockSizeBuilder>.Raw(value) : (CssValue<MaxBlockSizeBuilder>?)null;
        set => Set(177, value?.ToString());
    }

    /// <summary>Gets or sets the MaxInlineSize preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<MaxInlineSizeBuilder>? MaxInlineSize
    {
        get => _values[178] is { } value ? CssValue<MaxInlineSizeBuilder>.Raw(value) : (CssValue<MaxInlineSizeBuilder>?)null;
        set => Set(178, value?.ToString());
    }

    /// <summary>Gets or sets the MinBlockSize preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<MinBlockSizeBuilder>? MinBlockSize
    {
        get => _values[179] is { } value ? CssValue<MinBlockSizeBuilder>.Raw(value) : (CssValue<MinBlockSizeBuilder>?)null;
        set => Set(179, value?.ToString());
    }

    /// <summary>Gets or sets the MinInlineSize preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<MinInlineSizeBuilder>? MinInlineSize
    {
        get => _values[180] is { } value ? CssValue<MinInlineSizeBuilder>.Raw(value) : (CssValue<MinInlineSizeBuilder>?)null;
        set => Set(180, value?.ToString());
    }

    /// <summary>Gets or sets the MixBlendMode preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<MixBlendModeBuilder>? MixBlendMode
    {
        get => _values[181] is { } value ? CssValue<MixBlendModeBuilder>.Raw(value) : (CssValue<MixBlendModeBuilder>?)null;
        set => Set(181, value?.ToString());
    }

    /// <summary>Gets or sets the ObjectFit preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ObjectFitBuilder>? ObjectFit
    {
        get => _values[182] is { } value ? CssValue<ObjectFitBuilder>.Raw(value) : (CssValue<ObjectFitBuilder>?)null;
        set => Set(182, value?.ToString());
    }

    /// <summary>Gets or sets the ObjectPosition preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ObjectPositionBuilder>? ObjectPosition
    {
        get => _values[183] is { } value ? CssValue<ObjectPositionBuilder>.Raw(value) : (CssValue<ObjectPositionBuilder>?)null;
        set => Set(183, value?.ToString());
    }

    /// <summary>Gets or sets the Order preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<OrderBuilder>? Order
    {
        get => _values[184] is { } value ? CssValue<OrderBuilder>.Raw(value) : (CssValue<OrderBuilder>?)null;
        set => Set(184, value?.ToString());
    }

    /// <summary>Gets or sets the Origin preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<OriginBuilder>? Origin
    {
        get => _values[185] is { } value ? CssValue<OriginBuilder>.Raw(value) : (CssValue<OriginBuilder>?)null;
        set => Set(185, value?.ToString());
    }

    /// <summary>Gets or sets the OutlineColor preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<OutlineColorBuilder>? OutlineColor
    {
        get => _values[186] is { } value ? CssValue<OutlineColorBuilder>.Raw(value) : (CssValue<OutlineColorBuilder>?)null;
        set => Set(186, value?.ToString());
    }

    /// <summary>Gets or sets the OutlineOffset preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<OutlineOffsetBuilder>? OutlineOffset
    {
        get => _values[187] is { } value ? CssValue<OutlineOffsetBuilder>.Raw(value) : (CssValue<OutlineOffsetBuilder>?)null;
        set => Set(187, value?.ToString());
    }

    /// <summary>Gets or sets the OutlineWidth preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<OutlineWidthBuilder>? OutlineWidth
    {
        get => _values[188] is { } value ? CssValue<OutlineWidthBuilder>.Raw(value) : (CssValue<OutlineWidthBuilder>?)null;
        set => Set(188, value?.ToString());
    }

    /// <summary>Gets or sets the OverflowWrap preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<OverflowWrapBuilder>? OverflowWrap
    {
        get => _values[189] is { } value ? CssValue<OverflowWrapBuilder>.Raw(value) : (CssValue<OverflowWrapBuilder>?)null;
        set => Set(189, value?.ToString());
    }

    /// <summary>Gets or sets the PerspectiveOrigin preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<PerspectiveOriginBuilder>? PerspectiveOrigin
    {
        get => _values[190] is { } value ? CssValue<PerspectiveOriginBuilder>.Raw(value) : (CssValue<PerspectiveOriginBuilder>?)null;
        set => Set(190, value?.ToString());
    }

    /// <summary>Gets or sets the Perspective preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<PerspectiveBuilder>? Perspective
    {
        get => _values[191] is { } value ? CssValue<PerspectiveBuilder>.Raw(value) : (CssValue<PerspectiveBuilder>?)null;
        set => Set(191, value?.ToString());
    }

    /// <summary>Gets or sets the PlaceContentAlign preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<PlaceContentAlignBuilder>? PlaceContentAlign
    {
        get => _values[192] is { } value ? CssValue<PlaceContentAlignBuilder>.Raw(value) : (CssValue<PlaceContentAlignBuilder>?)null;
        set => Set(192, value?.ToString());
    }

    /// <summary>Gets or sets the PlaceItemsAlign preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<PlaceItemsAlignBuilder>? PlaceItemsAlign
    {
        get => _values[193] is { } value ? CssValue<PlaceItemsAlignBuilder>.Raw(value) : (CssValue<PlaceItemsAlignBuilder>?)null;
        set => Set(193, value?.ToString());
    }

    /// <summary>Gets or sets the PlaceSelfAlign preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<PlaceSelfAlignBuilder>? PlaceSelfAlign
    {
        get => _values[194] is { } value ? CssValue<PlaceSelfAlignBuilder>.Raw(value) : (CssValue<PlaceSelfAlignBuilder>?)null;
        set => Set(194, value?.ToString());
    }

    /// <summary>Gets or sets the Rotate preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<RotateBuilder>? Rotate
    {
        get => _values[195] is { } value ? CssValue<RotateBuilder>.Raw(value) : (CssValue<RotateBuilder>?)null;
        set => Set(195, value?.ToString());
    }

    /// <summary>Gets or sets the RowEnd preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<RowEndBuilder>? RowEnd
    {
        get => _values[196] is { } value ? CssValue<RowEndBuilder>.Raw(value) : (CssValue<RowEndBuilder>?)null;
        set => Set(196, value?.ToString());
    }

    /// <summary>Gets or sets the Scale preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ScaleBuilder>? Scale
    {
        get => _values[197] is { } value ? CssValue<ScaleBuilder>.Raw(value) : (CssValue<ScaleBuilder>?)null;
        set => Set(197, value?.ToString());
    }

    /// <summary>Gets or sets the ScrollbarGutter preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ScrollbarGutterBuilder>? ScrollbarGutter
    {
        get => _values[198] is { } value ? CssValue<ScrollbarGutterBuilder>.Raw(value) : (CssValue<ScrollbarGutterBuilder>?)null;
        set => Set(198, value?.ToString());
    }

    /// <summary>Gets or sets the ScrollbarThumbColor preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ScrollbarThumbColorBuilder>? ScrollbarThumbColor
    {
        get => _values[199] is { } value ? CssValue<ScrollbarThumbColorBuilder>.Raw(value) : (CssValue<ScrollbarThumbColorBuilder>?)null;
        set => Set(199, value?.ToString());
    }

    /// <summary>Gets or sets the ScrollbarTrackColor preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ScrollbarTrackColorBuilder>? ScrollbarTrackColor
    {
        get => _values[200] is { } value ? CssValue<ScrollbarTrackColorBuilder>.Raw(value) : (CssValue<ScrollbarTrackColorBuilder>?)null;
        set => Set(200, value?.ToString());
    }

    /// <summary>Gets or sets the ScrollbarWidth preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ScrollbarWidthBuilder>? ScrollbarWidth
    {
        get => _values[201] is { } value ? CssValue<ScrollbarWidthBuilder>.Raw(value) : (CssValue<ScrollbarWidthBuilder>?)null;
        set => Set(201, value?.ToString());
    }

    /// <summary>Gets or sets the ScrollBehavior preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ScrollBehaviorBuilder>? ScrollBehavior
    {
        get => _values[202] is { } value ? CssValue<ScrollBehaviorBuilder>.Raw(value) : (CssValue<ScrollBehaviorBuilder>?)null;
        set => Set(202, value?.ToString());
    }

    /// <summary>Gets or sets the ScrollSnapAlign preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ScrollSnapAlignBuilder>? ScrollSnapAlign
    {
        get => _values[203] is { } value ? CssValue<ScrollSnapAlignBuilder>.Raw(value) : (CssValue<ScrollSnapAlignBuilder>?)null;
        set => Set(203, value?.ToString());
    }

    /// <summary>Gets or sets the ScrollSnap preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ScrollSnapBuilder>? ScrollSnap
    {
        get => _values[204] is { } value ? CssValue<ScrollSnapBuilder>.Raw(value) : (CssValue<ScrollSnapBuilder>?)null;
        set => Set(204, value?.ToString());
    }

    /// <summary>Gets or sets the ScrollSnapStop preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ScrollSnapStopBuilder>? ScrollSnapStop
    {
        get => _values[205] is { } value ? CssValue<ScrollSnapStopBuilder>.Raw(value) : (CssValue<ScrollSnapStopBuilder>?)null;
        set => Set(205, value?.ToString());
    }

    /// <summary>Gets or sets the ShadowColor preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ShadowColorBuilder>? ShadowColor
    {
        get => _values[206] is { } value ? CssValue<ShadowColorBuilder>.Raw(value) : (CssValue<ShadowColorBuilder>?)null;
        set => Set(206, value?.ToString());
    }

    /// <summary>Gets or sets the Skew preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<SkewBuilder>? Skew
    {
        get => _values[207] is { } value ? CssValue<SkewBuilder>.Raw(value) : (CssValue<SkewBuilder>?)null;
        set => Set(207, value?.ToString());
    }

    /// <summary>Gets or sets the StrokeLineCap preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<StrokeLineCapBuilder>? StrokeLineCap
    {
        get => _values[208] is { } value ? CssValue<StrokeLineCapBuilder>.Raw(value) : (CssValue<StrokeLineCapBuilder>?)null;
        set => Set(208, value?.ToString());
    }

    /// <summary>Gets or sets the StrokeLineJoin preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<StrokeLineJoinBuilder>? StrokeLineJoin
    {
        get => _values[209] is { } value ? CssValue<StrokeLineJoinBuilder>.Raw(value) : (CssValue<StrokeLineJoinBuilder>?)null;
        set => Set(209, value?.ToString());
    }

    /// <summary>Gets or sets the Stroke preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<StrokeBuilder>? Stroke
    {
        get => _values[210] is { } value ? CssValue<StrokeBuilder>.Raw(value) : (CssValue<StrokeBuilder>?)null;
        set => Set(210, value?.ToString());
    }

    /// <summary>Gets or sets the SvgStrokeWidth preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<StrokeWidthBuilder>? SvgStrokeWidth
    {
        get => _values[211] is { } value ? CssValue<StrokeWidthBuilder>.Raw(value) : (CssValue<StrokeWidthBuilder>?)null;
        set => Set(211, value?.ToString());
    }

    /// <summary>Gets or sets the TableLayout preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<TableLayoutBuilder>? TableLayout
    {
        get => _values[212] is { } value ? CssValue<TableLayoutBuilder>.Raw(value) : (CssValue<TableLayoutBuilder>?)null;
        set => Set(212, value?.ToString());
    }

    /// <summary>Gets or sets the TabSize preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<TabSizeBuilder>? TabSize
    {
        get => _values[213] is { } value ? CssValue<TabSizeBuilder>.Raw(value) : (CssValue<TabSizeBuilder>?)null;
        set => Set(213, value?.ToString());
    }

    /// <summary>Gets or sets the TextIndent preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<TextIndentBuilder>? TextIndent
    {
        get => _values[214] is { } value ? CssValue<TextIndentBuilder>.Raw(value) : (CssValue<TextIndentBuilder>?)null;
        set => Set(214, value?.ToString());
    }

    /// <summary>Gets or sets the TextShadowColor preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<TextShadowColorBuilder>? TextShadowColor
    {
        get => _values[215] is { } value ? CssValue<TextShadowColorBuilder>.Raw(value) : (CssValue<TextShadowColorBuilder>?)null;
        set => Set(215, value?.ToString());
    }

    /// <summary>Gets or sets the TextShadow preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<TextShadowBuilder>? TextShadow
    {
        get => _values[216] is { } value ? CssValue<TextShadowBuilder>.Raw(value) : (CssValue<TextShadowBuilder>?)null;
        set => Set(216, value?.ToString());
    }

    /// <summary>Gets or sets the TouchAction preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<TouchActionBuilder>? TouchAction
    {
        get => _values[217] is { } value ? CssValue<TouchActionBuilder>.Raw(value) : (CssValue<TouchActionBuilder>?)null;
        set => Set(217, value?.ToString());
    }

    /// <summary>Gets or sets the TransformStyle preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<TransformStyleBuilder>? TransformStyle
    {
        get => _values[218] is { } value ? CssValue<TransformStyleBuilder>.Raw(value) : (CssValue<TransformStyleBuilder>?)null;
        set => Set(218, value?.ToString());
    }

    /// <summary>Gets or sets the TransitionBehavior preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<TransitionBehaviorBuilder>? TransitionBehavior
    {
        get => _values[219] is { } value ? CssValue<TransitionBehaviorBuilder>.Raw(value) : (CssValue<TransitionBehaviorBuilder>?)null;
        set => Set(219, value?.ToString());
    }

    /// <summary>Gets or sets the Translate preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<TranslateBuilder>? Translate
    {
        get => _values[220] is { } value ? CssValue<TranslateBuilder>.Raw(value) : (CssValue<TranslateBuilder>?)null;
        set => Set(220, value?.ToString());
    }

    /// <summary>Gets or sets the WillChange preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<WillChangeBuilder>? WillChange
    {
        get => _values[221] is { } value ? CssValue<WillChangeBuilder>.Raw(value) : (CssValue<WillChangeBuilder>?)null;
        set => Set(221, value?.ToString());
    }

    /// <summary>Gets or sets the Zoom preset value. Null clears the assignment; an empty value remains explicit.</summary>
    public CssValue<ZoomBuilder>? Zoom
    {
        get => _values[222] is { } value ? CssValue<ZoomBuilder>.Raw(value) : (CssValue<ZoomBuilder>?)null;
        set => Set(222, value?.ToString());
    }

    internal void Clear()
    {
        for (int block = 0; block < _assigned.Length; block++)
        {
            ClearSlots(_assigned[block], block * 64);
            _assigned[block] = 0;
        }
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
        for (int block = 0; block < _assigned.Length; block++)
            CopySlots(target, _assigned[block], block * 64);
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
