using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark;

/// <summary>
/// Represents an item within a bar dropdown menu.
/// </summary>
public interface IBarDropdownItem : IElement
{
    /// <summary>
    /// Gets or sets the URL to navigate to When the item is clicked.
    /// </summary>
    string? To { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked When the item is clicked.
    /// </summary>
    EventCallback<MouseEventArgs> Clicked { get; set; }

    /// <summary>
    /// Gets or sets whether the item is disabled.
    /// </summary>
    bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets the indentation level for nested dropdown items.
    /// </summary>
    double Indentation { get; set; }
}