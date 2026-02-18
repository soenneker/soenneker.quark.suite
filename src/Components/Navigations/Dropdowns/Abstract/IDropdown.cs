using Microsoft.AspNetCore.Components;
using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Represents a dropdown component that toggles the visibility of a dropdown menu.
/// </summary>
public interface IDropdown : IElement
{
    /// <summary>
    /// Gets or sets whether the dropdown is visible.
    /// </summary>
    bool Visible { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked When the visibility state changes.
    /// </summary>
    EventCallback<bool> VisibleChanged { get; set; }

    /// <summary>
    /// Gets or sets whether the dropdown menu should be aligned to the right.
    /// </summary>
    bool RightAligned { get; set; }

    /// <summary>
    /// Gets or sets whether the dropdown is disabled.
    /// </summary>
    bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets the direction of the dropdown menu (up, down, start, or end).
    /// </summary>
    Direction Direction { get; set; }

    /// <summary>
    /// Gets or sets whether this dropdown is a submenu.
    /// </summary>
    bool IsSubmenu { get; set; }

    /// <summary>
    /// Gets or sets whether this dropdown is part of a button group.
    /// </summary>
    bool IsGroup { get; set; }

    /// <summary>
    /// Gets or sets the target ID of the dropdown menu element.
    /// </summary>
    string? DropdownMenuTargetId { get; set; }
}

