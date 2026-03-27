namespace Soenneker.Quark;

/// <summary>
/// Represents a close button for a dialog.
/// </summary>
public interface IDialogCloseButton : IElement
{
    /// <summary>
    /// Gets or sets whether the dialog should automatically close When the button is clicked.
    /// </summary>
    bool AutoClose { get; set; }
}

