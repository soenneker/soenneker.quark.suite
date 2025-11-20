namespace Soenneker.Quark;

/// <summary>
/// Interface for typographic elements that handle text styling properties.
/// Provides typography-specific properties like color, size, weight, alignment, wrapping, and breaking.
/// </summary>
public interface ITypographicElement : IElement
{
    /// <summary>
    /// Gets or sets the text size.
    /// </summary>
    CssValue<TextSizeBuilder>? TextSize { get; set; }

    /// <summary>
    /// Gets or sets the text decoration (underline, line-through, etc.).
    /// </summary>
    CssValue<TextDecorationBuilder>? TextDecoration { get; set; }

    /// <summary>
    /// Gets or sets the text transform (uppercase, lowercase, capitalize).
    /// </summary>
    CssValue<TextTransformBuilder>? TextTransform { get; set; }

    /// <summary>
    /// Gets or sets the font weight (bold, normal, light, etc.).
    /// </summary>
    CssValue<FontWeightBuilder>? FontWeight { get; set; }

    /// <summary>
    /// Gets or sets the font style (italic, normal, oblique).
    /// </summary>
    CssValue<FontStyleBuilder>? FontStyle { get; set; }

    /// <summary>
    /// Gets or sets the line height.
    /// </summary>
    CssValue<LineHeightBuilder>? LineHeight { get; set; }

    /// <summary>
    /// Gets or sets the text wrapping behavior.
    /// </summary>
    CssValue<TextWrapBuilder>? TextWrap { get; set; }

    /// <summary>
    /// Gets or sets the text breaking behavior.
    /// </summary>
    CssValue<TextBreakBuilder>? TextBreak { get; set; }

    /// <summary>
    /// Gets or sets the text overflow behavior (ellipsis, clip).
    /// </summary>
    CssValue<TextOverflowBuilder>? TextOverflow { get; set; }

    /// <summary>
    /// Gets or sets whether text should be truncated with ellipsis.
    /// </summary>
    CssValue<TruncateBuilder>? Truncate { get; set; }

    /// <summary>
    /// Gets or sets the text style configuration.
    /// </summary>
    CssValue<TextStyleBuilder>? TextStyle { get; set; }

    /// <summary>
    /// Gets or sets the text background color.
    /// </summary>
    CssValue<TextBackgroundBuilder>? TextBackground { get; set; }

    /// <summary>
    /// Gets or sets the text opacity.
    /// </summary>
    CssValue<TextOpacityBuilder>? TextOpacity { get; set; }
}