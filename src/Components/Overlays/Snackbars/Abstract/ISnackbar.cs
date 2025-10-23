using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a snackbar notification component for displaying brief messages.
/// </summary>
public interface ISnackbar : IElement
{
    /// <summary>
    /// Gets or sets the unique key identifier for this snackbar.
    /// </summary>
    string Key { get; set; }

    /// <summary>
    /// Gets or sets whether the snackbar is visible.
    /// </summary>
    bool Visible { get; set; }

    /// <summary>
    /// Gets or sets the delay in milliseconds before the snackbar automatically hides.
    /// </summary>
    int AutoHideDelay { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the snackbar is closed.
    /// </summary>
    EventCallback<SnackbarClosedEventArgs> Closed { get; set; }

    /// <summary>
    /// Shows the snackbar.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task Show();

    /// <summary>
    /// Hides the snackbar.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task Hide();
}

