namespace Soenneker.Quark;

/// <summary>
/// Represents the collapsible body content of an accordion item.
/// </summary>
public interface IAccordionBody : IElement
{
    /// <summary>
    /// Gets or sets whether the body is currently expanded/visible.
    /// </summary>
    bool Expanded { get; set; }
}
