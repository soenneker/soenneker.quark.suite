namespace Soenneker.Quark;

/// <summary>
/// Represents an individual accordion item within an accordion component.
/// </summary>
public interface IAccordionItem : IElement
{
    /// <summary>
    /// Gets or sets the unique name/identifier of the accordion item.
    /// </summary>
    string? Name { get; set; }

    /// <summary>
    /// Gets or sets whether this accordion item is currently expanded.
    /// </summary>
    bool Expanded { get; set; }

    /// <summary>
    /// Gets or sets whether this accordion item is disabled.
    /// </summary>
    bool Disabled { get; set; }
}
