namespace Soenneker.Quark.Components.Surface.Interactive.Buttons.Abstract;

/// <summary>
/// Represents a button group component for grouping related buttons together.
/// </summary>
public interface IButtonGroup : IElement
{
    /// <summary>
    /// Gets or sets the size of all buttons in the group.
    /// </summary>
    CssValue<SizeBuilder>? Size { get; set; }

    /// <summary>
    /// Gets or sets whether the button group should be displayed vertically.
    /// </summary>
    bool Vertical { get; set; }

    /// <summary>
    /// Gets or sets the role attribute for accessibility.
    /// </summary>
    string? Role { get; set; }

    /// <summary>
    /// Gets or sets the aria-label for accessibility.
    /// </summary>
    string? AriaLabel { get; set; }
}
