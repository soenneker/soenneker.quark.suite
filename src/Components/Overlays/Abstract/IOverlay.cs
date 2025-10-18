using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents an overlay component that displays content above other page elements.
/// </summary>
public interface IOverlay : IComponent
{
    /// <summary>
    /// Gets or sets whether the overlay is visible.
    /// </summary>
    bool Visible { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the visibility state changes.
    /// </summary>
    EventCallback<bool> VisibleChanged { get; set; }

    /// <summary>
    /// Gets or sets whether a backdrop should be shown behind the overlay.
    /// </summary>
    bool ShowBackdrop { get; set; }

    /// <summary>
    /// Gets or sets whether the overlay should close when the backdrop is clicked.
    /// </summary>
    bool CloseOnBackdropClick { get; set; }

    /// <summary>
    /// Gets or sets whether the overlay should close when the Escape key is pressed.
    /// </summary>
    bool CloseOnEscape { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the overlay is shown.
    /// </summary>
    EventCallback OnShow { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the overlay is hidden.
    /// </summary>
    EventCallback OnHide { get; set; }
}

