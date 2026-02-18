using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a container for managing and displaying multiple snackbar notifications.
/// </summary>
public interface ISnackbarStack : IElement
{
    /// <summary>
    /// Gets or sets the location where snackbars should appear on the screen.
    /// If null, will use the theme's default location or fallback to BottomEnd.
    /// </summary>
    SnackbarLocation? Location { get; set; }

    /// <summary>
    /// Gets or sets the default delay in milliseconds before snackbars automatically hide.
    /// If null, will use the theme's default delay or fallback to 5000ms.
    /// </summary>
    int? DefaultDelay { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked When a snackbar is closed.
    /// </summary>
    EventCallback<SnackbarClosedEventArgs> Closed { get; set; }

    /// <summary>
    /// Pushes a message to the snackbar stack.
    /// </summary>
    /// <param name="message">The message text to display.</param>
    /// <param name="color">The color scheme of the snackbar.</param>
    /// <param name="options">Additional options for the snackbar.</param>
    /// <param name="location">Optional location override for this specific snackbar.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Push(string message, CssValue<BackgroundColorBuilder>? color = null, Action<SnackbarOptions>? options = null, SnackbarLocation? location = null);

    /// <summary>
    /// Pushes custom content to the snackbar stack.
    /// </summary>
    /// <param name="content">The custom content template to display.</param>
    /// <param name="color">The color scheme of the snackbar.</param>
    /// <param name="options">Additional options for the snackbar.</param>
    /// <param name="location">Optional location override for this specific snackbar.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Push(RenderFragment content, CssValue<BackgroundColorBuilder>? color = null, Action<SnackbarOptions>? options = null, SnackbarLocation? location = null);
}

