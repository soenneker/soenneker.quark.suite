using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Represents an offcanvas component that slides in from the edge of the viewport.
/// </summary>
public interface IOffcanvas : IElement
{
    /// <summary>
    /// Gets or sets the placement of the offcanvas (start, end, top, bottom).
    /// </summary>
    PlacementType Placement { get; set; }

    /// <summary>
    /// Gets or sets whether the page should remain scrollable when offcanvas is open.
    /// </summary>
    bool AllowScroll { get; set; }

    /// <summary>
    /// Gets or sets whether a backdrop should be shown behind the offcanvas.
    /// </summary>
    bool ShowBackdrop { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the offcanvas is shown.
    /// </summary>
    EventCallback OnShow { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the offcanvas is hidden.
    /// </summary>
    EventCallback OnHide { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the backdrop is clicked.
    /// </summary>
    EventCallback OnBackdropClick { get; set; }

    /// <summary>
    /// Gets whether the offcanvas is currently visible.
    /// </summary>
    bool IsVisible { get; }

    /// <summary>
    /// Shows the offcanvas.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task Show();

    /// <summary>
    /// Hides the offcanvas.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task Hide();

    /// <summary>
    /// Requests the offcanvas to close.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task RequestClose();
}

