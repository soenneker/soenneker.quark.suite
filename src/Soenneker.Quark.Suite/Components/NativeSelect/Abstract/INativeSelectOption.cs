namespace Soenneker.Quark;

/// <summary>
/// Represents an option within <see cref="NativeSelect"/>.
/// </summary>
public interface INativeSelectOption : IElement
{
    /// <summary>
    /// Gets or sets the HTML label attribute.
    /// </summary>
    string? Label { get; set; }

    /// <summary>
    /// Gets or sets the HTML selected attribute.
    /// </summary>
    bool? Selected { get; set; }


    /// <summary>
    /// Gets or sets the option value.
    /// </summary>
    string? Value { get; set; }

    /// <summary>
    /// Gets or sets whether this option is disabled.
    /// </summary>
    bool Disabled { get; set; }
}
