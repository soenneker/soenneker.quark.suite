using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a collapsible content component that can be shown or hidden.
/// </summary>
public interface ICollapse : IElement
{
    /// <summary>
    /// Gets or sets whether the collapse is currently expanded/visible.
    /// </summary>
    bool Expanded { get; set; }

    /// <summary>
    /// Gets or sets the callback that is invoked when the expanded state changes.
    /// </summary>
    EventCallback<bool> OnExpandedChanged { get; set; }

    /// <summary>
    /// Gets or sets whether the collapse should use horizontal animation instead of vertical.
    /// </summary>
    bool Horizontal { get; set; }
}
