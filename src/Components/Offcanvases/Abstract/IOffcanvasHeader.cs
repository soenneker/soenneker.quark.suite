namespace Soenneker.Quark;

/// <summary>
/// Represents the header section of an offcanvas component with optional close button.
/// </summary>
public interface IOffcanvasHeader : IElement
{
    /// <summary>
    /// Gets or sets whether the close button should be displayed.
    /// </summary>
    bool ShowCloseButton { get; set; }
}

