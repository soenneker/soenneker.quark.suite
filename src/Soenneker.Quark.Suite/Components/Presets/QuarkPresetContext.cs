namespace Soenneker.Quark;

/// <summary>
/// Represents the quark preset context.
/// </summary>
public sealed class QuarkPresetContext
{
    // Most presets set only a few properties. Box those values on assignment instead
    // of reserving a nullable CssValue struct for every unused property.
    private object? _inset;
    private object? _top;
    private object? _right;
    private object? _bottom;
    private object? _left;
    private object? _display;
    private object? _visibility;
    private object? _float;
    private object? _verticalAlign;
    private object? _textAlign;
    private object? _textColor;
    private object? _textSize;
    private object? _decorationLine;
    private object? _underlineOffset;
    private object? _textTransform;
    private object? _fontFamily;
    private object? _fontWeight;
    private object? _fontStyle;
    private object? _leading;
    private object? _tracking;
    private object? _whitespace;
    private object? _textWrap;
    private object? _textBreak;
    private object? _textOverflow;
    private object? _truncate;
    private object? _lineClamp;
    private object? _fontVariantNumeric;
    private object? _margin;
    private object? _padding;
    private object? _position;
    private object? _scrollMargin;
    private object? _scrollPadding;
    private object? _size;
    private object? _width;
    private object? _minWidth;
    private object? _maxWidth;
    private object? _height;
    private object? _minHeight;
    private object? _maxHeight;
    private object? _overflow;
    private object? _overflowX;
    private object? _overflowY;
    private object? _overscroll;
    private object? _flex;
    private object? _flexDirection;
    private object? _flexWrap;
    private object? _grow;
    private object? _shrink;
    private object? _gap;
    private object? _space;
    private object? _divide;
    private object? _contentAlign;
    private object? _itemsAlign;
    private object? _justify;
    private object? _selfAlign;
    private object? _justifyItemsAlign;
    private object? _justifySelfAlign;
    private object? _colStart;
    private object? _rowSpan;
    private object? _rowStart;
    private object? _opacity;
    private object? _zIndex;
    private object? _pointerEvents;
    private object? _userSelect;
    private object? _cursor;
    private object? _screenReader;
    private object? _backgroundColor;
    private object? _borderColor;
    private object? _border;
    private object? _borderStyle;
    private object? _rounded;
    private object? _ringColor;
    private object? _ring;
    private object? _ringOffset;
    private object? _shadow;
    private object? _backdropFilter;
    private object? _backdropBlur;
    private object? _backdropBrightness;
    private object? _backdropContrast;
    private object? _backdropGrayscale;
    private object? _backdropHueRotate;
    private object? _backdropInvert;
    private object? _backdropOpacity;
    private object? _backdropSaturate;
    private object? _backdropSepia;
    private object? _filter;
    private object? _blur;
    private object? _brightness;
    private object? _contrast;
    private object? _dropShadow;
    private object? _dropShadowColor;
    private object? _grayscale;
    private object? _hueRotate;
    private object? _invert;
    private object? _saturate;
    private object? _sepia;
    private object? _resize;
    private object? _transform;
    private object? _animation;
    private object? _duration;
    private object? _transition;

    /// <summary>
    /// Gets or sets class.
    /// </summary>
    public string? Class { get; set; }
    /// <summary>
    /// Gets or sets inset.
    /// </summary>
    public CssValue<InsetBuilder>? Inset
    {
        get => (CssValue<InsetBuilder>?) _inset;
        set => _inset = value;
    }
    /// <summary>
    /// Gets or sets top.
    /// </summary>
    public CssValue<TopBuilder>? Top
    {
        get => (CssValue<TopBuilder>?) _top;
        set => _top = value;
    }
    /// <summary>
    /// Gets or sets right.
    /// </summary>
    public CssValue<RightBuilder>? Right
    {
        get => (CssValue<RightBuilder>?) _right;
        set => _right = value;
    }
    /// <summary>
    /// Gets or sets bottom.
    /// </summary>
    public CssValue<BottomBuilder>? Bottom
    {
        get => (CssValue<BottomBuilder>?) _bottom;
        set => _bottom = value;
    }
    /// <summary>
    /// Gets or sets left.
    /// </summary>
    public CssValue<LeftBuilder>? Left
    {
        get => (CssValue<LeftBuilder>?) _left;
        set => _left = value;
    }
    /// <summary>
    /// Gets or sets display.
    /// </summary>
    public CssValue<DisplayBuilder>? Display
    {
        get => (CssValue<DisplayBuilder>?) _display;
        set => _display = value;
    }
    /// <summary>
    /// Gets or sets visibility.
    /// </summary>
    public CssValue<VisibilityBuilder>? Visibility
    {
        get => (CssValue<VisibilityBuilder>?) _visibility;
        set => _visibility = value;
    }
    /// <summary>
    /// Gets or sets float.
    /// </summary>
    public CssValue<FloatBuilder>? Float
    {
        get => (CssValue<FloatBuilder>?) _float;
        set => _float = value;
    }
    /// <summary>
    /// Gets or sets vertical align.
    /// </summary>
    public CssValue<VerticalAlignBuilder>? VerticalAlign
    {
        get => (CssValue<VerticalAlignBuilder>?) _verticalAlign;
        set => _verticalAlign = value;
    }
    /// <summary>
    /// Gets or sets text align.
    /// </summary>
    public CssValue<TextAlignBuilder>? TextAlign
    {
        get => (CssValue<TextAlignBuilder>?) _textAlign;
        set => _textAlign = value;
    }
    /// <summary>
    /// Gets or sets text color.
    /// </summary>
    public CssValue<TextColorBuilder>? TextColor
    {
        get => (CssValue<TextColorBuilder>?) _textColor;
        set => _textColor = value;
    }
    /// <summary>
    /// Gets or sets text size.
    /// </summary>
    public CssValue<TextSizeBuilder>? TextSize
    {
        get => (CssValue<TextSizeBuilder>?) _textSize;
        set => _textSize = value;
    }
    /// <summary>
    /// Gets or sets decoration line.
    /// </summary>
    public CssValue<DecorationLineBuilder>? DecorationLine
    {
        get => (CssValue<DecorationLineBuilder>?) _decorationLine;
        set => _decorationLine = value;
    }

    /// <summary>Gets or sets the distance between text and its underline, including responsive and state variants.</summary>
    public CssValue<UnderlineOffsetBuilder>? UnderlineOffset
    {
        get => (CssValue<UnderlineOffsetBuilder>?) _underlineOffset;
        set => _underlineOffset = value;
    }
    /// <summary>
    /// Gets or sets text transform.
    /// </summary>
    public CssValue<TextTransformBuilder>? TextTransform
    {
        get => (CssValue<TextTransformBuilder>?) _textTransform;
        set => _textTransform = value;
    }
    /// <summary>
    /// Gets or sets font family.
    /// </summary>
    public CssValue<FontFamilyBuilder>? FontFamily
    {
        get => (CssValue<FontFamilyBuilder>?) _fontFamily;
        set => _fontFamily = value;
    }
    /// <summary>
    /// Gets or sets font weight.
    /// </summary>
    public CssValue<FontWeightBuilder>? FontWeight
    {
        get => (CssValue<FontWeightBuilder>?) _fontWeight;
        set => _fontWeight = value;
    }
    /// <summary>
    /// Gets or sets font style.
    /// </summary>
    public CssValue<FontStyleBuilder>? FontStyle
    {
        get => (CssValue<FontStyleBuilder>?) _fontStyle;
        set => _fontStyle = value;
    }
    /// <summary>
    /// Gets or sets leading.
    /// </summary>
    public CssValue<LeadingBuilder>? Leading
    {
        get => (CssValue<LeadingBuilder>?) _leading;
        set => _leading = value;
    }
    /// <summary>
    /// Gets or sets tracking.
    /// </summary>
    public CssValue<TrackingBuilder>? Tracking
    {
        get => (CssValue<TrackingBuilder>?) _tracking;
        set => _tracking = value;
    }
    /// <summary>
    /// Gets or sets whitespace.
    /// </summary>
    public CssValue<WhitespaceBuilder>? Whitespace
    {
        get => (CssValue<WhitespaceBuilder>?) _whitespace;
        set => _whitespace = value;
    }
    /// <summary>
    /// Gets or sets text wrap.
    /// </summary>
    public CssValue<TextWrapBuilder>? TextWrap
    {
        get => (CssValue<TextWrapBuilder>?) _textWrap;
        set => _textWrap = value;
    }
    /// <summary>
    /// Gets or sets text break.
    /// </summary>
    public CssValue<TextBreakBuilder>? TextBreak
    {
        get => (CssValue<TextBreakBuilder>?) _textBreak;
        set => _textBreak = value;
    }
    /// <summary>
    /// Gets or sets text overflow.
    /// </summary>
    public CssValue<TextOverflowBuilder>? TextOverflow
    {
        get => (CssValue<TextOverflowBuilder>?) _textOverflow;
        set => _textOverflow = value;
    }
    /// <summary>
    /// Gets or sets truncate.
    /// </summary>
    public CssValue<TruncateBuilder>? Truncate
    {
        get => (CssValue<TruncateBuilder>?) _truncate;
        set => _truncate = value;
    }
    /// <summary>
    /// Gets or sets line clamp.
    /// </summary>
    public CssValue<LineClampBuilder>? LineClamp
    {
        get => (CssValue<LineClampBuilder>?) _lineClamp;
        set => _lineClamp = value;
    }
    /// <summary>
    /// Gets or sets font variant numeric.
    /// </summary>
    public CssValue<FontVariantNumericBuilder>? FontVariantNumeric
    {
        get => (CssValue<FontVariantNumericBuilder>?) _fontVariantNumeric;
        set => _fontVariantNumeric = value;
    }
    /// <summary>
    /// Gets or sets margin.
    /// </summary>
    public CssValue<MarginBuilder>? Margin
    {
        get => (CssValue<MarginBuilder>?) _margin;
        set => _margin = value;
    }
    /// <summary>
    /// Gets or sets padding.
    /// </summary>
    public CssValue<PaddingBuilder>? Padding
    {
        get => (CssValue<PaddingBuilder>?) _padding;
        set => _padding = value;
    }
    /// <summary>
    /// Gets or sets position.
    /// </summary>
    public CssValue<PositionBuilder>? Position
    {
        get => (CssValue<PositionBuilder>?) _position;
        set => _position = value;
    }
    /// <summary>
    /// Gets or sets scroll margin.
    /// </summary>
    public CssValue<ScrollMarginBuilder>? ScrollMargin
    {
        get => (CssValue<ScrollMarginBuilder>?) _scrollMargin;
        set => _scrollMargin = value;
    }
    /// <summary>
    /// Gets or sets scroll padding.
    /// </summary>
    public CssValue<ScrollPaddingBuilder>? ScrollPadding
    {
        get => (CssValue<ScrollPaddingBuilder>?) _scrollPadding;
        set => _scrollPadding = value;
    }
    /// <summary>
    /// Gets or sets size.
    /// </summary>
    public CssValue<SizeBuilder>? Size
    {
        get => (CssValue<SizeBuilder>?) _size;
        set => _size = value;
    }
    /// <summary>
    /// Gets or sets width.
    /// </summary>
    public CssValue<WidthBuilder>? Width
    {
        get => (CssValue<WidthBuilder>?) _width;
        set => _width = value;
    }
    /// <summary>
    /// Gets or sets min width.
    /// </summary>
    public CssValue<MinWidthBuilder>? MinWidth
    {
        get => (CssValue<MinWidthBuilder>?) _minWidth;
        set => _minWidth = value;
    }
    /// <summary>
    /// Gets or sets max width.
    /// </summary>
    public CssValue<MaxWidthBuilder>? MaxWidth
    {
        get => (CssValue<MaxWidthBuilder>?) _maxWidth;
        set => _maxWidth = value;
    }
    /// <summary>
    /// Gets or sets height.
    /// </summary>
    public CssValue<HeightBuilder>? Height
    {
        get => (CssValue<HeightBuilder>?) _height;
        set => _height = value;
    }
    /// <summary>
    /// Gets or sets min height.
    /// </summary>
    public CssValue<MinHeightBuilder>? MinHeight
    {
        get => (CssValue<MinHeightBuilder>?) _minHeight;
        set => _minHeight = value;
    }
    /// <summary>
    /// Gets or sets max height.
    /// </summary>
    public CssValue<MaxHeightBuilder>? MaxHeight
    {
        get => (CssValue<MaxHeightBuilder>?) _maxHeight;
        set => _maxHeight = value;
    }
    /// <summary>
    /// Gets or sets overflow.
    /// </summary>
    public CssValue<OverflowBuilder>? Overflow
    {
        get => (CssValue<OverflowBuilder>?) _overflow;
        set => _overflow = value;
    }
    /// <summary>
    /// Gets or sets overflow x.
    /// </summary>
    public CssValue<OverflowBuilder>? OverflowX
    {
        get => (CssValue<OverflowBuilder>?) _overflowX;
        set => _overflowX = value;
    }
    /// <summary>
    /// Gets or sets overflow y.
    /// </summary>
    public CssValue<OverflowBuilder>? OverflowY
    {
        get => (CssValue<OverflowBuilder>?) _overflowY;
        set => _overflowY = value;
    }
    /// <summary>
    /// Gets or sets overscroll.
    /// </summary>
    public CssValue<OverscrollBuilder>? Overscroll
    {
        get => (CssValue<OverscrollBuilder>?) _overscroll;
        set => _overscroll = value;
    }
    /// <summary>
    /// Gets or sets flex.
    /// </summary>
    public CssValue<FlexBuilder>? Flex
    {
        get => (CssValue<FlexBuilder>?) _flex;
        set => _flex = value;
    }
    /// <summary>
    /// Gets or sets flex direction.
    /// </summary>
    public CssValue<FlexDirectionBuilder>? FlexDirection
    {
        get => (CssValue<FlexDirectionBuilder>?) _flexDirection;
        set => _flexDirection = value;
    }
    /// <summary>
    /// Gets or sets flex wrap.
    /// </summary>
    public CssValue<FlexWrapBuilder>? FlexWrap
    {
        get => (CssValue<FlexWrapBuilder>?) _flexWrap;
        set => _flexWrap = value;
    }
    /// <summary>
    /// Gets or sets grow.
    /// </summary>
    public CssValue<GrowBuilder>? Grow
    {
        get => (CssValue<GrowBuilder>?) _grow;
        set => _grow = value;
    }
    /// <summary>
    /// Gets or sets shrink.
    /// </summary>
    public CssValue<ShrinkBuilder>? Shrink
    {
        get => (CssValue<ShrinkBuilder>?) _shrink;
        set => _shrink = value;
    }
    /// <summary>
    /// Gets or sets gap.
    /// </summary>
    public CssValue<GapBuilder>? Gap
    {
        get => (CssValue<GapBuilder>?) _gap;
        set => _gap = value;
    }
    /// <summary>
    /// Gets or sets space.
    /// </summary>
    public CssValue<SpaceBuilder>? Space
    {
        get => (CssValue<SpaceBuilder>?) _space;
        set => _space = value;
    }
    /// <summary>
    /// Gets or sets divide.
    /// </summary>
    public CssValue<DivideBuilder>? Divide
    {
        get => (CssValue<DivideBuilder>?) _divide;
        set => _divide = value;
    }
    /// <summary>
    /// Gets or sets content align.
    /// </summary>
    public CssValue<ContentAlignBuilder>? ContentAlign
    {
        get => (CssValue<ContentAlignBuilder>?) _contentAlign;
        set => _contentAlign = value;
    }
    /// <summary>
    /// Gets or sets items align.
    /// </summary>
    public CssValue<ItemsBuilder>? ItemsAlign
    {
        get => (CssValue<ItemsBuilder>?) _itemsAlign;
        set => _itemsAlign = value;
    }
    /// <summary>
    /// Gets or sets justify.
    /// </summary>
    public CssValue<JustifyBuilder>? Justify
    {
        get => (CssValue<JustifyBuilder>?) _justify;
        set => _justify = value;
    }
    /// <summary>
    /// Gets or sets self align.
    /// </summary>
    public CssValue<SelfBuilder>? SelfAlign
    {
        get => (CssValue<SelfBuilder>?) _selfAlign;
        set => _selfAlign = value;
    }
    /// <summary>
    /// Gets or sets justify items align.
    /// </summary>
    public CssValue<JustifyItemsAlignBuilder>? JustifyItemsAlign
    {
        get => (CssValue<JustifyItemsAlignBuilder>?) _justifyItemsAlign;
        set => _justifyItemsAlign = value;
    }
    /// <summary>
    /// Gets or sets justify self align.
    /// </summary>
    public CssValue<JustifySelfAlignBuilder>? JustifySelfAlign
    {
        get => (CssValue<JustifySelfAlignBuilder>?) _justifySelfAlign;
        set => _justifySelfAlign = value;
    }
    /// <summary>
    /// Gets or sets col start.
    /// </summary>
    public CssValue<ColStartBuilder>? ColStart
    {
        get => (CssValue<ColStartBuilder>?) _colStart;
        set => _colStart = value;
    }
    /// <summary>
    /// Gets or sets row span.
    /// </summary>
    public CssValue<RowSpanBuilder>? RowSpan
    {
        get => (CssValue<RowSpanBuilder>?) _rowSpan;
        set => _rowSpan = value;
    }
    /// <summary>
    /// Gets or sets row start.
    /// </summary>
    public CssValue<RowStartBuilder>? RowStart
    {
        get => (CssValue<RowStartBuilder>?) _rowStart;
        set => _rowStart = value;
    }
    /// <summary>
    /// Gets or sets opacity.
    /// </summary>
    public CssValue<OpacityBuilder>? Opacity
    {
        get => (CssValue<OpacityBuilder>?) _opacity;
        set => _opacity = value;
    }
    /// <summary>
    /// Gets or sets z index.
    /// </summary>
    public CssValue<ZIndexBuilder>? ZIndex
    {
        get => (CssValue<ZIndexBuilder>?) _zIndex;
        set => _zIndex = value;
    }
    /// <summary>
    /// Gets or sets pointer events.
    /// </summary>
    public CssValue<PointerEventsBuilder>? PointerEvents
    {
        get => (CssValue<PointerEventsBuilder>?) _pointerEvents;
        set => _pointerEvents = value;
    }
    /// <summary>
    /// Gets or sets user select.
    /// </summary>
    public CssValue<UserSelectBuilder>? UserSelect
    {
        get => (CssValue<UserSelectBuilder>?) _userSelect;
        set => _userSelect = value;
    }
    /// <summary>
    /// Gets or sets cursor.
    /// </summary>
    public CssValue<CursorBuilder>? Cursor
    {
        get => (CssValue<CursorBuilder>?) _cursor;
        set => _cursor = value;
    }
    /// <summary>
    /// Gets or sets screen reader.
    /// </summary>
    public CssValue<ScreenReaderBuilder>? ScreenReader
    {
        get => (CssValue<ScreenReaderBuilder>?) _screenReader;
        set => _screenReader = value;
    }
    /// <summary>
    /// Gets or sets background color.
    /// </summary>
    public CssValue<BackgroundColorBuilder>? BackgroundColor
    {
        get => (CssValue<BackgroundColorBuilder>?) _backgroundColor;
        set => _backgroundColor = value;
    }
    /// <summary>
    /// Gets or sets border color.
    /// </summary>
    public CssValue<BorderColorBuilder>? BorderColor
    {
        get => (CssValue<BorderColorBuilder>?) _borderColor;
        set => _borderColor = value;
    }
    /// <summary>
    /// Gets or sets border.
    /// </summary>
    public CssValue<BorderBuilder>? Border
    {
        get => (CssValue<BorderBuilder>?) _border;
        set => _border = value;
    }
    /// <summary>
    /// Gets or sets border style.
    /// </summary>
    public CssValue<BorderStyleBuilder>? BorderStyle
    {
        get => (CssValue<BorderStyleBuilder>?) _borderStyle;
        set => _borderStyle = value;
    }
    /// <summary>
    /// Gets or sets rounded.
    /// </summary>
    public CssValue<RoundedBuilder>? Rounded
    {
        get => (CssValue<RoundedBuilder>?) _rounded;
        set => _rounded = value;
    }
    /// <summary>
    /// Gets or sets ring color.
    /// </summary>
    public CssValue<RingColorBuilder>? RingColor
    {
        get => (CssValue<RingColorBuilder>?) _ringColor;
        set => _ringColor = value;
    }
    /// <summary>
    /// Gets or sets ring.
    /// </summary>
    public CssValue<RingBuilder>? Ring
    {
        get => (CssValue<RingBuilder>?) _ring;
        set => _ring = value;
    }
    /// <summary>
    /// Gets or sets ring offset.
    /// </summary>
    public CssValue<RingOffsetBuilder>? RingOffset
    {
        get => (CssValue<RingOffsetBuilder>?) _ringOffset;
        set => _ringOffset = value;
    }
    /// <summary>
    /// Gets or sets shadow.
    /// </summary>
    public CssValue<ShadowBuilder>? Shadow
    {
        get => (CssValue<ShadowBuilder>?) _shadow;
        set => _shadow = value;
    }
    /// <summary>
    /// Gets or sets backdrop filter.
    /// </summary>
    public CssValue<BackdropFilterBuilder>? BackdropFilter
    {
        get => (CssValue<BackdropFilterBuilder>?) _backdropFilter;
        set => _backdropFilter = value;
    }

    /// <summary>
    /// Gets or sets backdrop blur utility classes.
    /// </summary>
    public CssValue<BackdropBlurBuilder>? BackdropBlur
    {
        get => (CssValue<BackdropBlurBuilder>?) _backdropBlur;
        set => _backdropBlur = value;
    }

    /// <summary>
    /// Gets or sets backdrop brightness utility classes.
    /// </summary>
    public CssValue<BackdropBrightnessBuilder>? BackdropBrightness
    {
        get => (CssValue<BackdropBrightnessBuilder>?) _backdropBrightness;
        set => _backdropBrightness = value;
    }

    /// <summary>
    /// Gets or sets backdrop contrast utility classes.
    /// </summary>
    public CssValue<BackdropContrastBuilder>? BackdropContrast
    {
        get => (CssValue<BackdropContrastBuilder>?) _backdropContrast;
        set => _backdropContrast = value;
    }

    /// <summary>
    /// Gets or sets backdrop grayscale utility classes.
    /// </summary>
    public CssValue<BackdropGrayscaleBuilder>? BackdropGrayscale
    {
        get => (CssValue<BackdropGrayscaleBuilder>?) _backdropGrayscale;
        set => _backdropGrayscale = value;
    }

    /// <summary>
    /// Gets or sets backdrop hue rotate utility classes.
    /// </summary>
    public CssValue<BackdropHueRotateBuilder>? BackdropHueRotate
    {
        get => (CssValue<BackdropHueRotateBuilder>?) _backdropHueRotate;
        set => _backdropHueRotate = value;
    }

    /// <summary>
    /// Gets or sets backdrop invert utility classes.
    /// </summary>
    public CssValue<BackdropInvertBuilder>? BackdropInvert
    {
        get => (CssValue<BackdropInvertBuilder>?) _backdropInvert;
        set => _backdropInvert = value;
    }

    /// <summary>
    /// Gets or sets backdrop opacity utility classes.
    /// </summary>
    public CssValue<BackdropOpacityBuilder>? BackdropOpacity
    {
        get => (CssValue<BackdropOpacityBuilder>?) _backdropOpacity;
        set => _backdropOpacity = value;
    }

    /// <summary>
    /// Gets or sets backdrop saturate utility classes.
    /// </summary>
    public CssValue<BackdropSaturateBuilder>? BackdropSaturate
    {
        get => (CssValue<BackdropSaturateBuilder>?) _backdropSaturate;
        set => _backdropSaturate = value;
    }

    /// <summary>
    /// Gets or sets backdrop sepia utility classes.
    /// </summary>
    public CssValue<BackdropSepiaBuilder>? BackdropSepia
    {
        get => (CssValue<BackdropSepiaBuilder>?) _backdropSepia;
        set => _backdropSepia = value;
    }
    /// <summary>
    /// Gets or sets filter.
    /// </summary>
    public CssValue<FilterBuilder>? Filter
    {
        get => (CssValue<FilterBuilder>?) _filter;
        set => _filter = value;
    }

    /// <summary>
    /// Gets or sets blur utility classes.
    /// </summary>
    public CssValue<BlurBuilder>? Blur
    {
        get => (CssValue<BlurBuilder>?) _blur;
        set => _blur = value;
    }

    /// <summary>
    /// Gets or sets brightness utility classes.
    /// </summary>
    public CssValue<BrightnessBuilder>? Brightness
    {
        get => (CssValue<BrightnessBuilder>?) _brightness;
        set => _brightness = value;
    }

    /// <summary>
    /// Gets or sets contrast utility classes.
    /// </summary>
    public CssValue<ContrastBuilder>? Contrast
    {
        get => (CssValue<ContrastBuilder>?) _contrast;
        set => _contrast = value;
    }

    /// <summary>
    /// Gets or sets drop shadow utility classes.
    /// </summary>
    public CssValue<DropShadowBuilder>? DropShadow
    {
        get => (CssValue<DropShadowBuilder>?) _dropShadow;
        set => _dropShadow = value;
    }

    /// <summary>
    /// Gets or sets drop shadow color utility classes.
    /// </summary>
    public CssValue<DropShadowColorBuilder>? DropShadowColor
    {
        get => (CssValue<DropShadowColorBuilder>?) _dropShadowColor;
        set => _dropShadowColor = value;
    }

    /// <summary>
    /// Gets or sets grayscale utility classes.
    /// </summary>
    public CssValue<GrayscaleBuilder>? Grayscale
    {
        get => (CssValue<GrayscaleBuilder>?) _grayscale;
        set => _grayscale = value;
    }

    /// <summary>
    /// Gets or sets hue rotate utility classes.
    /// </summary>
    public CssValue<HueRotateBuilder>? HueRotate
    {
        get => (CssValue<HueRotateBuilder>?) _hueRotate;
        set => _hueRotate = value;
    }

    /// <summary>
    /// Gets or sets invert utility classes.
    /// </summary>
    public CssValue<InvertBuilder>? Invert
    {
        get => (CssValue<InvertBuilder>?) _invert;
        set => _invert = value;
    }

    /// <summary>
    /// Gets or sets saturate utility classes.
    /// </summary>
    public CssValue<SaturateBuilder>? Saturate
    {
        get => (CssValue<SaturateBuilder>?) _saturate;
        set => _saturate = value;
    }

    /// <summary>
    /// Gets or sets sepia utility classes.
    /// </summary>
    public CssValue<SepiaBuilder>? Sepia
    {
        get => (CssValue<SepiaBuilder>?) _sepia;
        set => _sepia = value;
    }
    /// <summary>
    /// Gets or sets resize.
    /// </summary>
    public CssValue<ResizeBuilder>? Resize
    {
        get => (CssValue<ResizeBuilder>?) _resize;
        set => _resize = value;
    }
    /// <summary>
    /// Gets or sets transform.
    /// </summary>
    public CssValue<TransformBuilder>? Transform
    {
        get => (CssValue<TransformBuilder>?) _transform;
        set => _transform = value;
    }
    /// <summary>
    /// Gets or sets animation.
    /// </summary>
    public CssValue<AnimationBuilder>? Animation
    {
        get => (CssValue<AnimationBuilder>?) _animation;
        set => _animation = value;
    }
    /// <summary>
    /// Gets or sets duration.
    /// </summary>
    public CssValue<DurationBuilder>? Duration
    {
        get => (CssValue<DurationBuilder>?) _duration;
        set => _duration = value;
    }
    /// <summary>
    /// Gets or sets transition.
    /// </summary>
    public CssValue<TransitionBuilder>? Transition
    {
        get => (CssValue<TransitionBuilder>?) _transition;
        set => _transition = value;
    }
}
