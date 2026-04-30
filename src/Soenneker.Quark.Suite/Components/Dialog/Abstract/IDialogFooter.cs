namespace Soenneker.Quark;

/// <summary>
/// Represents the footer section of a dialog, typically containing action buttons.
/// </summary>
public interface IDialogFooter : IElement
{
    /// <summary>
    /// Gets or sets whether the footer should render the shadcn outline close button after its children.
    /// </summary>
    bool ShowCloseButton { get; set; }
}

