namespace Soenneker.Quark;

/// <summary>
/// Represents a text component wrapper element.
/// </summary>
public interface IText : IElement
{
    /// <summary>
    /// Gets or sets the text configuration for Bootstrap text utilities.
    /// </summary>
    CssValue<TextStyleBuilder>? TextStyle { get; set; }
}