namespace Soenneker.Quark;

/// <summary>
/// Minimal DOM element contract for renderable HTML-like elements.
/// </summary>
public interface IElement : IComponent, ICoreElement
{
    string Tag { get; set; }
    int? TabIndex { get; set; }
    string? Role { get; set; }
    string? AriaLabel { get; set; }
    string? AriaLabelledBy { get; set; }
    string? AriaDescribedBy { get; set; }
}
