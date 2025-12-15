namespace Soenneker.Quark;

/// <summary>
/// Represents the content wrapper within a modal dialog.
/// </summary>
public interface IModalContent : IElement
{
    /// <summary>
    /// Gets or sets whether the modal content should be centered.
    /// </summary>
    bool Centered { get; set; }
}

