using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents an alert dialog component for important user confirmations.
/// </summary>
public interface IAlertDialog : IElement
{
    /// <summary>
    /// Gets or sets whether the alert dialog is visible.
    /// </summary>
    bool Visible { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the visibility state changes.
    /// </summary>
    EventCallback<bool> VisibleChanged { get; set; }

    /// <summary>
    /// Gets or sets whether a backdrop should be shown behind the alert dialog.
    /// </summary>
    bool ShowBackdrop { get; set; }

    /// <summary>
    /// Gets or sets whether pressing Escape should close the alert dialog.
    /// </summary>
    bool CloseOnEscape { get; set; }

    /// <summary>
    /// Gets or sets whether clicking the backdrop should close the alert dialog.
    /// </summary>
    bool CloseOnBackdropClick { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the alert dialog is shown.
    /// </summary>
    EventCallback OnShow { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the alert dialog is hidden.
    /// </summary>
    EventCallback OnHide { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the backdrop is clicked.
    /// </summary>
    EventCallback OnBackdropClick { get; set; }

    /// <summary>
    /// Shows the alert dialog.
    /// </summary>
    Task Show();

    /// <summary>
    /// Hides the alert dialog.
    /// </summary>
    Task Hide();
}
