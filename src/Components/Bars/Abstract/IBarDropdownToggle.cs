using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark;

/// <summary>
/// Represents a toggle button for a bar dropdown menu.
/// </summary>
public interface IBarDropdownToggle : IElement
{
    /// <summary>
    /// Gets or sets the callback invoked when the toggle is clicked.
    /// </summary>
    EventCallback<MouseEventArgs> OnToggle { get; set; }

    /// <summary>
    /// Gets or sets whether the toggle is disabled.
    /// </summary>
    bool? Disabled { get; set; }

    /// <summary>
    /// Gets or sets the indentation level for nested dropdown toggles.
    /// </summary>
    double Indentation { get; set; }

    /// <summary>
    /// Gets or sets whether the dropdown is currently expanded.
    /// </summary>
    bool Expanded { get; set; }
}

