using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Base class for overlay components with layered content and positioning/backdrop.
/// Used for modals, drawers, popovers, tooltips, etc.
/// </summary>
public abstract class Overlay : Component, IOverlay
{
    /// <summary>
    /// Gets or sets the child content to be rendered within the overlay.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Gets or sets whether the overlay is visible.
    /// </summary>
    [Parameter]
    public bool Visible { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the visibility state changes.
    /// </summary>
    [Parameter]
    public EventCallback<bool> VisibleChanged { get; set; }

    /// <summary>
    /// Gets or sets whether a backdrop should be shown behind the overlay.
    /// </summary>
    [Parameter]
    public bool ShowBackdrop { get; set; } = true;

    /// <summary>
    /// Gets or sets whether the overlay should close when the backdrop is clicked.
    /// </summary>
    [Parameter]
    public bool CloseOnBackdropClick { get; set; } = true;

    /// <summary>
    /// Gets or sets whether the overlay should close when the Escape key is pressed.
    /// </summary>
    [Parameter]
    public bool CloseOnEscape { get; set; } = true;

    /// <summary>
    /// Gets or sets the callback invoked when the overlay is shown.
    /// </summary>
    [Parameter]
    public EventCallback OnShow { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the overlay is hidden.
    /// </summary>
    [Parameter]
    public EventCallback OnHide { get; set; }
}

