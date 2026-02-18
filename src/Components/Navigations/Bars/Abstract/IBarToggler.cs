using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Represents a toggle button for collapsing/expanding a navigation bar menu.
/// </summary>
public interface IBarToggler : IElement
{
    /// <summary>
    /// Gets or sets the ID of the target element to toggle.
    /// </summary>
    string? Target { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked When the toggler is clicked.
    /// </summary>
    EventCallback<MouseEventArgs> OnToggle { get; set; }

    /// <summary>
    /// Gets or sets the display mode of the toggler.
    /// </summary>
    BarTogglerMode BarTogglerMode { get; set; }

    /// <summary>
    /// Gets or sets whether the toggler is disabled.
    /// </summary>
    bool? Disabled { get; set; }
}

