namespace Soenneker.Quark;

/// <summary>
/// Represents a tabbed navigation component for organizing content into tabs.
/// </summary>
public interface ITabs : IElement
{
    /// <summary>
    /// Gets or sets the background color scheme of the tabs.
    /// </summary>
    CssValue<ColorBuilder> BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets whether the tabs should be styled as pills instead of tabs.
    /// </summary>
    bool Pills { get; set; }

    /// <summary>
    /// Gets or sets whether the tabs should be displayed vertically.
    /// </summary>
    bool Vertical { get; set; }

    /// <summary>
    /// Gets or sets whether the tabs should take up the full width (justified).
    /// </summary>
    bool Justified { get; set; }
}

