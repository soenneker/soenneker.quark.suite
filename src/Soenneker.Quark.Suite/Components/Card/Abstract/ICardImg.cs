namespace Soenneker.Quark;

/// <summary>
/// Represents an image component within a card.
/// </summary>
public interface ICardImg : IElement
{
    /// <summary>
    /// Gets or sets whether the image should be positioned at the top of the card.
    /// </summary>
    bool AtTop { get; set; }

    /// <summary>
    /// Gets or sets whether the image should be positioned at the bottom of the card.
    /// </summary>
    bool AtBottom { get; set; }

    /// <summary>
    /// Gets or sets the source URL of the image.
    /// </summary>
    string? Source { get; set; }

    /// <summary>
    /// Gets or sets the alternative text for the image.
    /// </summary>
    string? Alt { get; set; }
}
