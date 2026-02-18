using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a sidebar component within a layout.
/// </summary>
public interface ILayoutSider : IElement
{
    /// <summary>
    /// Gets or sets whether the sidebar is visible.
    /// </summary>
    bool Visible { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked When the visibility state changes.
    /// </summary>
    EventCallback<bool> VisibleChanged { get; set; }
}

