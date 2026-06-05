namespace Soenneker.Quark;

/// <summary>
/// Minimal DOM element contract for renderable HTML-like elements.
/// </summary>
public interface IElement : IComponent
{
    /// <summary>
    /// Gets or sets tag.
    /// </summary>
    string Tag { get; set; }
    /// <summary>
    /// Gets or sets tab index.
    /// </summary>
    int? TabIndex { get; set; }
    /// <summary>
    /// Gets or sets role.
    /// </summary>
    string? Role { get; set; }
    /// <summary>
    /// Gets or sets aria label.
    /// </summary>
    string? AriaLabel { get; set; }
    /// <summary>
    /// Gets or sets aria labelled by.
    /// </summary>
    string? AriaLabelledBy { get; set; }
    /// <summary>
    /// Gets or sets aria described by.
    /// </summary>
    string? AriaDescribedBy { get; set; }
    /// <summary>
    /// Gets or sets aria current.
    /// </summary>
    string? AriaCurrent { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether aria hidden.
    /// </summary>
    bool? AriaHidden { get; set; }
}
