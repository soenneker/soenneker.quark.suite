namespace Soenneker.Quark;

/// <summary>
/// Represents the content container inside an alert dialog.
/// </summary>
public interface IAlertDialogContent : IElement
{
    /// <summary>
    /// Gets or sets the size variant of the content container.
    /// </summary>
    string Size { get; set; }
}
