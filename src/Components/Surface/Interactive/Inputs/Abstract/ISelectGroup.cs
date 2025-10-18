namespace Soenneker.Quark;

/// <summary>
/// Represents a select group component for organizing related options.
/// </summary>
public interface ISelectGroup : IElement
{
    /// <summary>
    /// Gets or sets the label text for the select group.
    /// </summary>
    string? Label { get; set; }

    /// <summary>
    /// Gets or sets whether the select group is disabled.
    /// </summary>
    bool Disabled { get; set; }
}

