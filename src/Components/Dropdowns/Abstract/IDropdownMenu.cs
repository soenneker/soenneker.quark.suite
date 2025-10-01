using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Represents a dropdown menu component that displays a list of items in a dropdown.
/// </summary>
public interface IDropdownMenu : IElement
{
    /// <summary>
    /// Gets or sets whether the dropdown menu is visible.
    /// </summary>
    bool Visible { get; set; }

    /// <summary>
    /// Gets or sets whether the dropdown menu should be aligned to the right.
    /// </summary>
    bool RightAligned { get; set; }

    /// <summary>
    /// Gets or sets the direction of the dropdown menu (up, down, start, or end).
    /// </summary>
    Direction Direction { get; set; }
}

