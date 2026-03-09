namespace Soenneker.Quark;

/// <summary>
/// Represents an accordion item within an accordion root.
/// </summary>
public interface IAccordionItem : IElement
{
    /// <summary>
    /// Gets or sets the unique item value used for state tracking.
    /// </summary>
    string? Value { get; set; }

    /// <summary>
    /// Gets or sets whether the item is disabled.
    /// </summary>
    bool Disabled { get; set; }
}
