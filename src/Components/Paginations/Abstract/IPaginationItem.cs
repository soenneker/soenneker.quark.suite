namespace Soenneker.Quark;

/// <summary>
/// Represents an individual pagination item.
/// </summary>
public interface IPaginationItem : IElement
{
    /// <summary>
    /// Gets or sets whether this pagination item is active (current page).
    /// </summary>
    bool Active { get; set; }

    /// <summary>
    /// Gets or sets whether this pagination item is disabled.
    /// </summary>
    bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets the color scheme of the pagination item.
    /// </summary>
    CssValue<ColorBuilder> Color { get; set; }
}

