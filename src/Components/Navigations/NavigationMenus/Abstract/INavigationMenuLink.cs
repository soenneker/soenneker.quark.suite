namespace Soenneker.Quark;

/// <summary>
/// Link element used inside navigation menu content and lists.
/// </summary>
public interface INavigationMenuLink : IElement
{
    /// <summary>
    /// Gets or sets the destination URL.
    /// </summary>
    string? Href { get; set; }

    /// <summary>
    /// Gets or sets whether the link is active.
    /// </summary>
    bool Active { get; set; }

    /// <summary>
    /// Gets or sets the target for the anchor element.
    /// </summary>
    string? Target { get; set; }
}
