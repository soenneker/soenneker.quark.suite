namespace Soenneker.Quark;

/// <summary>
/// Represents a collapsible dropdown menu within a navigation bar.
/// </summary>
public interface IBarDropdownMenu : IElement
{
    /// <summary>
    /// Gets or sets whether the dropdown menu is shown.
    /// </summary>
    bool Show { get; set; }

    /// <summary>
    /// Gets or sets whether the dropdown menu should be aligned to the right.
    /// </summary>
    bool RightAligned { get; set; }
}

