using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark;

/// <summary>
/// Represents a dropdown toggle button that opens/closes a dropdown menu.
/// </summary>
public interface IDropdownToggle : IElement
{
    /// <summary>
    /// Gets or sets whether the dropdown toggle is disabled.
    /// </summary>
    bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets whether this is a split dropdown toggle button.
    /// </summary>
    bool IsSplit { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the toggle is clicked.
    /// </summary>
    EventCallback<MouseEventArgs> Clicked { get; set; }
}

