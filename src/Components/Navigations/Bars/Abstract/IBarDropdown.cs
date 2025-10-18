namespace Soenneker.Quark;

/// <summary>
/// Represents a dropdown menu within a navigation bar.
/// </summary>
public interface IBarDropdown : IElement
{
    /// <summary>
    /// Gets or sets whether the dropdown menu should be aligned to the right.
    /// </summary>
    bool RightAligned { get; set; }

    /// <summary>
    /// Gets the unique collapse ID for this dropdown.
    /// </summary>
    string CollapseId { get; }
}

