namespace Soenneker.Quark;

/// <summary>
/// Menu item wrapper that groups a trigger and optional content.
/// </summary>
public interface INavigationMenuItem : IElement
{
    /// <summary>
    /// Gets or sets whether the item is currently open.
    /// </summary>
    bool Open { get; set; }

    /// <summary>
    /// Gets or sets whether the item is disabled.
    /// </summary>
    bool Disabled { get; set; }
}
