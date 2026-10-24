namespace Soenneker.Quark;

/// <summary>
/// Represents the content wrapper within a dialog.
/// </summary>
public interface IDialogContent : IElement
{
    /// <summary>
    /// Gets or sets whether the dialog content should be centered. Defaults to true.
    /// </summary>
    bool Centered { get; set; }
}

