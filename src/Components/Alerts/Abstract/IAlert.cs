using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents an alert component for displaying contextual feedback messages.
/// </summary>
public interface IAlert : IElement
{
    /// <summary>
    /// Gets or sets the color scheme of the alert.
    /// </summary>
    CssValue<ColorBuilder>? Color { get; set; }

    /// <summary>
    /// Gets or sets whether the alert can be dismissed by the user.
    /// </summary>
    bool Dismissible { get; set; }

    /// <summary>
    /// Gets or sets whether the alert is visible.
    /// </summary>
    bool Visible { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the visibility state changes.
    /// </summary>
    EventCallback<bool> VisibleChanged { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the alert is dismissed.
    /// </summary>
    EventCallback OnDismiss { get; set; }

    /// <summary>
    /// Makes the alert visible to users.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task Show();

    /// <summary>
    /// Hides the alert from users.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task Hide();

    /// <summary>
    /// Toggles the alert visibility between shown and hidden states.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task Toggle();
}

