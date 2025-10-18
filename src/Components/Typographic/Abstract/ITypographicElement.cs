namespace Soenneker.Quark;

/// <summary>
/// Interface for typographic elements that handle text styling properties.
/// Provides typography-specific properties like color, size, weight, alignment, wrapping, and breaking.
/// </summary>
public interface ITypographicElement : IElement
{
    CssValue<TextColorBuilder>? TextColor { get; set; }
    CssValue<TextSizeBuilder>? TextSize { get; set; }
    CssValue<TextAlignmentBuilder>? TextAlignment { get; set; }
    CssValue<TextDecorationBuilder>? TextDecoration { get; set; }
    CssValue<TextTransformBuilder>? TextTransform { get; set; }
    CssValue<FontWeightBuilder>? FontWeight { get; set; }
    CssValue<FontStyleBuilder>? FontStyle { get; set; }
    CssValue<LineHeightBuilder>? LineHeight { get; set; }
    CssValue<TextWrapBuilder>? TextWrap { get; set; }
    CssValue<TextBreakBuilder>? TextBreak { get; set; }
    CssValue<TextOverflowBuilder>? TextOverflow { get; set; }
    CssValue<TruncateBuilder>? Truncate { get; set; }
    CssValue<TextStyleBuilder>? TextStyle { get; set; }
    CssValue<TextBackgroundBuilder>? TextBackground { get; set; }
    CssValue<TextOpacityBuilder>? TextOpacity { get; set; }
}