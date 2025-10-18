namespace Soenneker.Quark;

/// <summary>
/// Represents the header of an accordion item that can be clicked to expand/collapse the content.
/// </summary>
public interface IAccordionHeader : IElement
{
    /// <summary>
    /// Gets or sets the icon name to display in the accordion header.
    /// </summary>
    string? IconName { get; set; }

    /// <summary>
    /// Gets or sets whether the header is currently expanded.
    /// </summary>
    bool Expanded { get; set; }

    /// <summary>
    /// Gets or sets whether the header is disabled.
    /// </summary>
    bool Disabled { get; set; }
}
