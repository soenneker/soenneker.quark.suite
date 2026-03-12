namespace Soenneker.Quark;

/// <summary>
/// Represents a close button for a modal dialog.
/// </summary>
public interface IModalCloseButton : IElement
{
    /// <summary>
    /// Gets or sets whether the modal should automatically close When the button is clicked.
    /// </summary>
    bool AutoClose { get; set; }
}

