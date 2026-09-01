using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a native HTML dialog element.
/// </summary>
public interface INativeDialog : IElement
{
    /// <summary>
    /// Gets or sets whether the dialog is open.
    /// </summary>
    bool Open { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the open state changes.
    /// </summary>
    EventCallback<bool> OpenChanged { get; set; }

    /// <summary>
    /// Gets or sets whether the dialog opens modally.
    /// </summary>
    bool Modal { get; set; }

    /// <summary>
    /// Gets or sets whether pressing Escape closes the dialog.
    /// </summary>
    bool CloseOnEscape { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the browser requests that the dialog be canceled.
    /// </summary>
    EventCallback OnCancel { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the dialog closes. The callback receives the dialog return value.
    /// </summary>
    EventCallback<string?> OnClose { get; set; }

    /// <summary>
    /// Opens the dialog as a non-modal dialog.
    /// </summary>
    Task Show();

    /// <summary>
    /// Opens the dialog as a modal dialog.
    /// </summary>
    Task ShowModal();

    /// <summary>
    /// Closes the dialog with an optional return value.
    /// </summary>
    /// <param name="returnValue">The value exposed by the native dialog's <c>returnValue</c> property.</param>
    Task Close(string? returnValue = null);
}
