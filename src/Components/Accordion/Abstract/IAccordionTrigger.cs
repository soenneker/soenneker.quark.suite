namespace Soenneker.Quark;

/// <summary>
/// Represents the clickable trigger for an accordion item.
/// </summary>
public interface IAccordionTrigger : IElement
{
    /// <summary>
    /// Gets or sets whether the trigger chevron should be displayed.
    /// </summary>
    bool ShowChevron { get; set; }
}
