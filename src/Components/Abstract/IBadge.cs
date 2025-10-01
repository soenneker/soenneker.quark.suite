using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Represents a badge component for displaying labels or indicators.
/// </summary>
public interface IBadge : IElement
{
    /// <summary>
    /// Gets or sets the type of badge (e.g., badge, pill, dot).
    /// </summary>
    BadgeType Type { get; set; }

    /// <summary>
    /// Gets or sets the background color of the badge.
    /// </summary>
    CssValue<ColorBuilder>? Color { get; set; }

    /// <summary>
    /// Gets or sets whether the badge should have rounded corners.
    /// </summary>
    bool Rounded { get; set; }
}