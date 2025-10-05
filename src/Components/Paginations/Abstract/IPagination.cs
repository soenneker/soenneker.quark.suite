namespace Soenneker.Quark;

/// <summary>
/// Represents a pagination component for navigating through multiple pages.
/// </summary>
public interface IPagination : IElement
{
    /// <summary>
    /// Gets or sets the background color scheme of the pagination.
    /// </summary>
    CssValue<ColorBuilder>? BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the size of the pagination.
    /// </summary>
    CssValue<SizeBuilder>? Size { get; set; }

    /// <summary>
    /// Gets or sets whether the pagination should be displayed vertically.
    /// </summary>
    bool Vertical { get; set; }
}

