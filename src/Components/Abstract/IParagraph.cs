namespace Soenneker.Quark;

/// <summary>
/// Represents a paragraph element.
/// </summary>
public interface IParagraph : IElement
{
    /// <summary>
    /// Gets or sets the text configuration for Bootstrap text utilities.
    /// </summary>
    CssValue<TextStyleBuilder>? TextStyle { get; set; }
}