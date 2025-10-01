namespace Soenneker.Quark;

/// <summary>
/// Represents an individual item within a breadcrumb navigation.
/// </summary>
public interface IBreadcrumbItem : IElement
{
    /// <summary>
    /// Gets or sets the URL to navigate to when the item is clicked.
    /// </summary>
    string? To { get; set; }

    /// <summary>
    /// Gets or sets whether this item represents the current page (active state).
    /// </summary>
    bool Active { get; set; }

    /// <summary>
    /// Gets or sets the color scheme of the breadcrumb item.
    /// </summary>
    CssValue<ColorBuilder> Color { get; set; }
}

