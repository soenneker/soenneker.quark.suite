using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a dialog component for displaying content in an overlay.
/// </summary>
public interface IDialog : IElement
{
    /// <summary>
    /// Gets or sets whether the dialog is visible.
    /// </summary>
    bool Visible { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked When the visibility state changes.
    /// </summary>
    EventCallback<bool> VisibleChanged { get; set; }

    /// <summary>
    /// Gets or sets whether a backdrop should be shown behind the dialog.
    /// </summary>
    bool ShowBackdrop { get; set; }

    /// <summary>
    /// Gets or sets whether the backdrop should blur content behind the dialog.
    /// </summary>
    bool BlurBackdrop { get; set; }

    /// <summary>
    /// Gets or sets whether the dialog should be vertically centered.
    /// </summary>
    bool Centered { get; set; }

    /// <summary>
    /// Gets or sets whether the dialog content should be scrollable.
    /// </summary>
    bool Scrollable { get; set; }

    /// <summary>
    /// Gets or sets the size of the dialog (small, default, large, extra-large, fullscreen).
    /// </summary>
    DialogSize? DialogSize { get; set; }

    /// <summary>
    /// Gets or sets whether the dialog is static (cannot be closed by clicking backdrop or pressing Escape).
    /// </summary>
    bool Static { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked When the dialog is shown.
    /// </summary>
    EventCallback OnShow { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked When the dialog is hidden.
    /// </summary>
    EventCallback OnHide { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked When the backdrop is clicked.
    /// </summary>
    EventCallback OnBackdropClick { get; set; }

    /// <summary>
    /// Shows the dialog.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task Show();

    /// <summary>
    /// Hides the dialog.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task Hide();
}

