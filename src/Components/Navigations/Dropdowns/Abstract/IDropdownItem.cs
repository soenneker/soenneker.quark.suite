using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark;

/// <summary>
/// Represents an item within a dropdown menu.
/// </summary>
public interface IDropdownItem : IElement
{
    /// <summary>
    /// Gets or sets whether the dropdown item is marked as active.
    /// </summary>
    bool Active { get; set; }

    /// <summary>
    /// Gets or sets whether the dropdown item is disabled.
    /// </summary>
    bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets whether the item should be rendered as a link (anchor tag) or button.
    /// </summary>
    bool IsLink { get; set; }

    /// <summary>
    /// Gets or sets the URL to navigate to When the item is clicked (only applicable When IsLink is true).
    /// </summary>
    string? Href { get; set; }

    /// <summary>
    /// Gets or sets the target attribute for the link (e.g., "_blank", "_self").
    /// </summary>
    string? Target { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked When the item is clicked.
    /// </summary>
    EventCallback<MouseEventArgs> Clicked { get; set; }
}

