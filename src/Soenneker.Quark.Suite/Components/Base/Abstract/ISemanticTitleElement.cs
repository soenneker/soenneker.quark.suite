namespace Soenneker.Quark;

/// <summary>
/// Contract for a visually styled title that may optionally render as a semantic HTML heading.
/// </summary>
public interface ISemanticTitleElement : IElement
{
    /// <summary>
    /// Gets or sets the semantic heading level. When omitted, the component retains its neutral default element.
    /// </summary>
    HeadingLevel? HeadingLevel { get; set; }
}
