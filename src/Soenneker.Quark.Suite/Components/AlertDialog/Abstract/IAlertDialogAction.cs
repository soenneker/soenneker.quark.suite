using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a confirm/action button in an alert dialog.
/// </summary>
public interface IAlertDialogAction : IElement
{
    /// <summary>
    /// Gets or sets whether the parent alert dialog should automatically close when clicked.
    /// </summary>
    bool AutoClose { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the action is clicked.
    /// </summary>
    EventCallback OnAction { get; set; }
}
