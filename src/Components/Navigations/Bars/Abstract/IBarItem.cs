namespace Soenneker.Quark;

/// <summary>
/// Represents an item within a navigation bar.
/// </summary>
public interface IBarItem : IElement
{
    /// <summary>
    /// Gets or sets whether the item is active/selected.
    /// </summary>
    bool Active { get; set; }

    /// <summary>
    /// Gets or sets whether the item is disabled.
    /// </summary>
    bool Disabled { get; set; }
}

