using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Represents a modal dialog component for displaying content in an overlay.
/// </summary>
public interface IModal : IElement
{
    /// <summary>
    /// Gets or sets whether the modal is visible.
    /// </summary>
    bool Visible { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked When the visibility state changes.
    /// </summary>
    EventCallback<bool> VisibleChanged { get; set; }

    /// <summary>
    /// Gets or sets whether a backdrop should be shown behind the modal.
    /// </summary>
    bool ShowBackdrop { get; set; }

    /// <summary>
    /// Gets or sets whether the modal should be vertically centered.
    /// </summary>
    bool Centered { get; set; }

    /// <summary>
    /// Gets or sets whether the modal content should be scrollable.
    /// </summary>
    bool Scrollable { get; set; }

    /// <summary>
    /// Gets or sets the size of the modal (small, default, large, extra-large, fullscreen).
    /// </summary>
    ModalSize? ModalSize { get; set; }

    /// <summary>
    /// Gets or sets whether the modal is static (cannot be closed by clicking backdrop or pressing Escape).
    /// </summary>
    bool Static { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked When the modal is shown.
    /// </summary>
    EventCallback OnShow { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked When the modal is hidden.
    /// </summary>
    EventCallback OnHide { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked When the backdrop is clicked.
    /// </summary>
    EventCallback OnBackdropClick { get; set; }

    /// <summary>
    /// Shows the modal.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task Show();

    /// <summary>
    /// Hides the modal.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task Hide();
}

