using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a cancel button in an alert dialog.
/// </summary>
public interface IAlertDialogCancel : IElement
{
    /// <summary>
    /// Gets or sets whether the parent alert dialog should automatically close when clicked.
    /// </summary>
    bool AutoClose { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when cancel is clicked.
    /// </summary>
    EventCallback OnCancel { get; set; }
}
