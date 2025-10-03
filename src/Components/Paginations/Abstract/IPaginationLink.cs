namespace Soenneker.Quark;

/// <summary>
/// Represents a clickable link within a pagination item.
/// </summary>
public interface IPaginationLink : IElement
{
    /// <summary>
    /// Gets or sets whether this pagination link is active (current page).
    /// </summary>
    bool Active { get; set; }

    /// <summary>
    /// Gets or sets whether this pagination link is disabled.
    /// </summary>
    bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets the background color scheme of the pagination link.
    /// </summary>
    CssValue<ColorBuilder> BackgroundColor { get; set; }
}

